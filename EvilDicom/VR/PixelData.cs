using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.Image;
using System.IO;
using EvilDicom.Helper;

namespace EvilDicom.VR
{
    public enum FrameDataFormat { NATIVE, ENCAPSULATED };
    public class PixelData : DICOMElement
    {
        private Constants.EncodeType encType;
        private string p;

        public PixelData(byte[] data, Constants.EncodeType encType, bool isLittleEndian, string vr, bool isEncapsulated)
        {
            this.encType = encType;
            this.IsLittleEndian = isLittleEndian;
            this.VR = vr;
            if (isEncapsulated)
            {
                //Encapsulated Pixel Data
                this.Format = FrameDataFormat.ENCAPSULATED;
                this.Fragments = FindFragments(data);
                this.ByteData = data;
            }
            else
            {
                //Native Pixel Data
                this.Format = FrameDataFormat.NATIVE;
                this.ByteData = data;
            }
        }

        private Fragment[] FindFragments(byte[] data)
        {
            List<Fragment> frags = SequenceHelper.ReadFragments(data, IsLittleEndian);
            return frags.ToArray();
        }
        /// <summary>
        /// A property which declares this data to either be native or encapsulated
        /// </summary>
        public FrameDataFormat Format { get; set; }
        public Fragment[] Fragments { get; set; }


        //public PixelCell[] GetPixelCells(ImageProperties properties)
        //{
        //    //If no image properties exist, return null
        //    //If native, return all pixels
        //        if (properties.BitsAllocated == null) { return null; }
        //        else
        //        {
        //            if (Format == FrameDataFormat.NATIVE)
        //            {
        //                List<PixelCell> cells = new List<PixelCell>();
        //                //Go through ByteData and pull cells according to bits allocated
        //                using (BinaryReader r = new BinaryReader(new MemoryStream(ByteData)))
        //                {
        //                    int bytesAllocated = Math.Ceiling(Properties.BitsAllocated / 8);
        //                    byte[] bytesRead = new byte[bytesAllocated];                            
        //                    while (r.Read(bytesRead, 0, bytesAllocated))
        //                    {
        //                        PixelCell pc = new PixelCell();
        //                        pc.Bytes = bytesRead;
        //                        cells.Add(pc);
        //                    }
        //                }
        //                return cells.ToArray();
        //            }
        //            else if (Format == FrameDataFormat.ENCAPSULATED)
        //            {
        //            }
        //        }
        //    }
        //}
        public override void WriteBytes(BinaryWriter b, bool isLittleEndian)
        {
            DICOMWriter.WriteTag(b, this.Tag, isLittleEndian);
            //Write VR
            DICOMWriter.WriteVR(b,this.VR, this.EncodeType);
            //Write Length
            if (this.Format == FrameDataFormat.ENCAPSULATED)
            {
                DICOMWriter.WriteLength(b, Constants.INDEFINITE_LENGTH);
                foreach (Fragment f in this.Fragments)
                {
                    f.WriteBytes(b, isLittleEndian);
                }

                DICOMWriter.WriteTag(b, new Tag(TagHelper.SEQUENCE_DELIMITATION_ITEM), isLittleEndian);
                DICOMWriter.WriteLength(b, Constants.ZERO_LENGTH);
            }
            else
            {
                DICOMWriter.WriteLength(b, Constants.EncodeType.EXPLICIT_4,this.ByteData.Length, isLittleEndian);
                DICOMWriter.WriteData(b, this, isLittleEndian);
            }
        }
    }


}


//Copyright © 2012 Rex Cardan, Ph.D


