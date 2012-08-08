using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDicom.Components;
using EvilDicom.VR;
using EvilDicom.Image;

namespace EvilDicom.Helper
{
    public class SequenceHelper
    {
        public static bool IsSequenceItem(BinaryReader r, bool isLittleEndian)
        {
            string id = new Tag(r.ReadBytes(4), isLittleEndian).Id;
            return id == SequenceItem.StartTag.Id;
        }

        /// <summary>
        /// This will find the index of the reader in which the ending tag delimeter for the sequence
        /// ends. It successfully can skip all inner sequence items of indefinite length.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int FindEndOfSequence(BinaryReader r, bool isLittleEndian)
        {
            byte[] bytes = new byte[4];
            int numberOfInternalIndefSeq = 0;

            while (r.BaseStream.Position < r.BaseStream.Length)
            {
                bytes = r.ReadBytes(4);
                //Check to see if it is end of sequence
                if (Helper.ArrayHelper.isEqualArray(bytes, isLittleEndian ? Helper.ByteHelper.TagToLittleEndian(Constants.SEQUENCE_END_DELIMITER) : Constants.SEQUENCE_END_DELIMITER))
                {
                    if (numberOfInternalIndefSeq == 0)
                    {
                        return (int)r.BaseStream.Position+4;
                    }
                    else
                    {
                        //This end is for an internal indefinite sequence item
                        numberOfInternalIndefSeq -= 1;
                    }
                }
                //Check for indefinite internal sequence
                else if (Helper.ArrayHelper.isEqualArray(bytes, Constants.INDEFINITE_LENGTH))
                {
                    //Move Cursor back 8 positions and check if this is a sequence item
                    r.BaseStream.Position -= 8;
                    if (!IsSequenceItem(r, isLittleEndian))
                    {
                        //Is internal indefinite sequence. Be sure to skip the ending tag on this
                        numberOfInternalIndefSeq += 1;
                        r.BaseStream.Position += 4;
                    }
                    else
                    {
                        //This was just a sequence item, move cursor forward
                        r.BaseStream.Position += 4;
                    }
                }
                else
                {
                    //No match move cursor back 3
                    r.BaseStream.Position -= 3;
                }
            }
            return -1;

        }

        /// <summary>
        /// This will find the index of the reader in which the ending tag delimeter for the sequence
        /// ends. It successfully can skip all inner sequence items of indefinite length.
        /// </summary>
        /// <param name="r">the binary reader stream containing the sequence items</param>
        /// <returns></returns>
        public static int FindEndOfSequenceItem(BinaryReader r, bool isLittleEndian)
        {
            byte[] bytes = new byte[4];
            int numberOfInternalIndefSeq = 0;

            while (r.BaseStream.Position < r.BaseStream.Length)
            {
                bytes = r.ReadBytes(4);
                //Check to see if it is end of sequence
                if (Helper.ArrayHelper.isEqualArray(bytes, isLittleEndian ? Helper.ByteHelper.TagToLittleEndian(Constants.SEQUENCE_ITEM_END_DELIMITER) : Constants.SEQUENCE_ITEM_END_DELIMITER))
                {
                    if (numberOfInternalIndefSeq == 0)
                    {
                        return (int)r.BaseStream.Position+4;
                    }
                    else
                    {
                        //This end is for an internal indefinite sequence item
                        numberOfInternalIndefSeq -= 1;
                    }
                }
                //Check for indefinite internal sequence item
                else if (Helper.ArrayHelper.isEqualArray(bytes, Constants.INDEFINITE_LENGTH))
                {
                    //Move Cursor back 8 positions and check if this is a sequence item
                    r.BaseStream.Position -= 8;
                    if (IsSequenceItem(r, isLittleEndian))
                    {
                        //Is internal indefinite sequence item. Be sure to skip the ending tag on this
                        numberOfInternalIndefSeq += 1;
                        r.BaseStream.Position += 4;
                    }
                    else
                    {
                        //This was just a sequence, move cursor forward
                        r.BaseStream.Position += 4;
                    }
                }
                else
                {
                    //No match move cursor back 3
                    r.BaseStream.Position -= 3;
                }
            }
            //Not found
            return -1;
        }

        public static List<SequenceItem> ReadChildren(byte[] data, Boolean isLittleEndian)
        {
            List<SequenceItem> items = new List<SequenceItem>();
            using (BinaryReader r = new BinaryReader(new MemoryStream(data)))
            {
                while (r.BaseStream.Position < r.BaseStream.Length)
                {
                    SequenceItem si = new SequenceItem();
                    //Read length past start tag
                    byte[] dLength = new byte[4];
                    r.Read(dLength, 0, 4);
                    r.Read(dLength, 0, 4);

                    if (ArrayHelper.isEqualArray(dLength, Constants.INDEFINITE_LENGTH))
                    {
                        //INDEFINITE LENGTH: Mark current position and find end
                        si.LengthType = Constants.LengthType.INDEFINITE;
                        long dataStart = r.BaseStream.Position;
                        long dataEnd = FindEndOfSequenceItem(r, isLittleEndian);

                        if (dataEnd == -1)
                        {
                            //Didn't find end tag
                            Console.WriteLine("Could not find sequence item end tag while decoding sequence items in sequence");
                            return null;
                        }
                        else
                        {
                            //Read dicomObject from dataStart to dataEnd
                            r.BaseStream.Position = dataStart;
                            while (r.BaseStream.Position < dataEnd)
                            {
                                si.AddObject(DICOMReader.ReadSequenceItem(r, isLittleEndian));
                            }
                        }
                    }
                    else
                    {
                        //Read dicomObject from current position to end of length
                        si.LengthType = Constants.LengthType.FINITE;
                        int objectsLength = BitConverter.ToInt32(dLength, 0);
                        if (!isLittleEndian) { objectsLength = BitConverter.ToInt32(ArrayHelper.ReverseArray(dLength), 0); }
                        int dataEnd = (int)r.BaseStream.Position + objectsLength;
                        while (r.BaseStream.Position < dataEnd)
                        {
                            si.AddObject(DICOMReader.ReadObject(r, isLittleEndian));
                        }
                    }

                    items.Add(si);
                }
                return items;
            }
        }

        public static List<Fragment> ReadFragments(byte[] data, Boolean isLittleEndian)
        {
            List<Fragment> frags = new List<Fragment>();
            using (BinaryReader r = new BinaryReader(new MemoryStream(data)))
            {
                while (r.BaseStream.Position < r.BaseStream.Length)
                {
                    Fragment f = new Fragment();
                    f.Tag.Id = TagHelper.SEQUENCE_ITEM;
                    f.EncodeType = Constants.EncodeType.IMPLICIT;
                    //Read length past start tag
                    byte[] dLength = new byte[4];
                    r.Read(dLength, 0, 4);
                    r.Read(dLength, 0, 4);

                    int objectsLength = BitConverter.ToInt32(dLength, 0);
                    if (!isLittleEndian) { objectsLength = BitConverter.ToInt32(ArrayHelper.ReverseArray(dLength), 0); }

                    //Read bytes from dataStart to dataEnd
                    f.ByteData = new byte[objectsLength];
                    r.Read(f.ByteData, 0, objectsLength);
                    if (f.ByteData.Length > 0)
                    {
                        frags.Add(f);
                    }
                }
                return frags;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


