using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDicom.Components;
using EvilDicom.VR;

namespace EvilDicom.Helper
{
    /// <summary>
    /// A class for reading DICOM files.
    /// </summary>
    class DICOMReader
    {
        /// <summary>
        /// A string array containing all possible VR types
        /// </summary>
        public static string[] vrs = {"CS", "SH", "LO", "ST", "LT", "UT", "AE", "PN", "UI", "DA", "TM", "DT", "AS", "IS", "DS", "SS", "US", "SL", "UL",
        "AT", "FL", "FD", "OB", "OW", "OF", "SQ", "UN"};
        /// <summary>
        /// A string array containg all VR with long explicit encoding
        /// </summary>
        public static string[] longVrs = { "OB", "OW", "OF", "SQ", "UT", "UN" };


        /// <summary>
        /// The method checks to see if the DICOM file is valid to begin.
        /// All valid DICOM files have the first 128 bits set to null (0x00)
        /// This is followed by 4 ASCII characters 'DICM'
        /// Returns true if both of these conditions are met.
        /// </summary>
        /// <returns></returns>
        public static bool IsValidDicom(BinaryReader r)
        {
            try
            {
                //128 null bytes
                byte[] nullBytes = new byte[128];
                r.Read(nullBytes, 0, 128);
                foreach (byte b in nullBytes)
                {
                    if (b != 0x00)
                    {
                        //Not valid
                        Console.WriteLine("Missing 128 null bit preamble. Not a valid DICOM file!");
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Could not read 128 null bit preamble. Perhaps file is too short");
                return false;
            }

            try
            {
                //4 DICM characters
                char[] dicm = new char[4];
                r.Read(dicm, 0, 4);
                if (dicm[0] != 'D' || dicm[1] != 'I' || dicm[2] != 'C' || dicm[3] != 'M')
                {
                    //Not valid
                    Console.WriteLine("Missing characters D I C M in bits 128-131. Not a valid DICOM file!");
                    return false;
                }
                return true;

            }
            catch (Exception)
            {

                Console.WriteLine("Could not read DICM letters in bits 128-131.");
                return false;
            }

        }
        /// <summary>
        /// This method reads the next four bytes of a binary reader
        /// object and parses out the DICOM tag. It then sets this tag as
        /// the tag for the DicomObject parameter.
        /// </summary>
        /// <param name="r">The BinaryReader object that is coming from the DicomFile.</param>
        /// <param name="d">The DicomObject that the tag will be set to.</param>
        public static void ReadTag(BinaryReader r, DICOMElement d)
        {
            try
            {
                byte[] tagBytes = new byte[4];
                r.Read(tagBytes, 0, 4);
                d.Tag = new Tag(tagBytes, d.IsLittleEndian);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not read tag.");
            }

        }

        /// <summary>
        /// This method reads the next four bytes of a binary reader
        /// object and parses out the DICOM tag. It then returns
        /// this tag.
        /// </summary>
        /// <param name="r">The BinaryReader object that is coming from the DicomFile.</param>
        /// <param name="isLittleEndian">The boolean that indicates how the DicomFile was written.</param>
        /// <returns>A Tag with the correct group and element ids set.</returns>
        public static Tag ReadTag(BinaryReader r, bool isLittleEndian)
        {
            try
            {
                byte[] tagBytes = new byte[4];
                r.Read(tagBytes, 0, 4);
                return new Tag(tagBytes, isLittleEndian);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not read tag.");
                return null;
            }

        }

        /// <summary>
        /// This method takes a binary reader and parses out the next DICOM object
        /// in the sequence of objects.
        /// </summary>
        /// <param name="r">the Binary reader containing the DICOM object</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        /// <returns>the parsed DICOM object</returns>
        public static DICOMElement ReadObject(BinaryReader r, bool isLittleEndian)
        {
            //Read tag first
            Tag t = ReadTag(r, isLittleEndian);
            //See if DicomObject is Explicit or Implicit encoding
            string vr;
            int dataLength;
            Constants.EncodeType encType;
            GetEncoding(r, out vr, out dataLength, out encType, isLittleEndian);
            bool isIndefinite = dataLength < -1 ? true : false;
            if (isIndefinite)
            { 
                dataLength = dataLength * -1; 
            }

            //Read Data
            byte[] data = new byte[dataLength];        
            r.Read(data, 0, data.Length);

            //See if tag is in DICOM Dictionary, if so
            //Set description for dicomObject
            //Select appropriate dicomObject to build
            if (string.IsNullOrEmpty(vr))
            {
                vr = DICOMDictionary.LookupTag(t);
            }
            else { DICOMDictionary.LookupTag(t); }
            return CreateVRObject(vr, encType, data, t, isLittleEndian, isIndefinite);
        }

        /// <summary>
        /// This method finds the next Dicom object matching the input tag and returns it. It is faster than parsing the entire Dicom
        /// structure with the ReadObject method as it skips objects not matching the criteria.
        /// </summary>
        /// <param name="r">the Binary reader containing the DICOM object</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        /// <param name="tag">the tag of the form "GGGGEEEE" that is being searched where G is group id and E is element id</param>
        /// <returns></returns>
        public static DICOMElement FindObject(BinaryReader r, bool isLittleEndian, string tag)
        {
            //Read tag first
            Tag t = ReadTag(r, isLittleEndian);
            if (t.Id == tag)
            {
                //See if DicomObject is Explicit or Implicit encoding
                string vr;
                int dataLength;
                Constants.EncodeType encType;
                GetEncoding(r, out vr, out dataLength, out encType, isLittleEndian);
                bool isIndefinite = dataLength < -1 ? true : false;
                if (isIndefinite) { dataLength = dataLength * -1; }

                //Read Data
                byte[] data = new byte[dataLength];
                r.Read(data, 0, data.Length);

                //See if tag is in DICOM Dictionary, if so
                //Set description for dicomObject
                //Select appropriate dicomObject to build
                vr = DICOMDictionary.LookupTag(t);

                return CreateVRObject(vr, encType, data, t, isLittleEndian, isIndefinite);
            }
            else
            {
                //Skip Object
                string vr;
                int dataLength;
                Constants.EncodeType encType;
                GetEncoding(r, out vr, out dataLength, out encType, isLittleEndian);
                if (vr == "SQ")
                {
                    //TODO Read inner objects

                }
                //Skip Data
                r.ReadBytes(dataLength);
                return null;
            }

        }

        /// <summary>
        /// This method reads and returns the next Sequence Item from a Binary reader object.
        /// </summary>
        /// <param name="r">A Binary reader containing the bytes of the sequence item.</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        /// <returns></returns>
        public static DICOMElement ReadSequenceItem(BinaryReader r, bool isLittleEndian)
        {
            //Read tag first
            Tag t = ReadTag(r, isLittleEndian);

            //See if DicomObject is Explicit or Implicit encoding
            string vr;
            int dataLength;
            Constants.EncodeType encType;
            GetEncoding(r, out vr, out dataLength, out encType, isLittleEndian);

            bool isIndefinite = dataLength < -1 ? true : false;
            if (isIndefinite) { dataLength = dataLength * -1; }

            //Read Data
            byte[] data = new byte[dataLength];
            r.Read(data, 0, data.Length);

            //See if tag is in DICOM Dictionary, if so
            //Set description for dicomObject
            //Select appropriate dicomObject to build
            vr = DICOMDictionary.LookupTag(t);

            return CreateVRObject(vr, encType, data, t, isLittleEndian, isIndefinite);
        }

        private static DICOMElement CreateVRObject(string vr, Constants.EncodeType encType, byte[] data, Tag t, bool isLittleEndian, bool isIndefinite)
        {
            switch (vr)
            {
                case "CS":
                    CodeString cs = new CodeString();
                    cs.ByteData = data;
                    cs.EncodeType = encType;
                    cs.IsLittleEndian = isLittleEndian;
                    cs.Tag = t;
                    return cs;

                case "SH":
                    ShortString sh = new ShortString();
                    sh.ByteData = data;
                    sh.EncodeType = encType;
                    sh.IsLittleEndian = isLittleEndian;
                    sh.Tag = t;
                    return sh;

                case "LO":
                    LongString lo = new LongString();
                    lo.ByteData = data;
                    lo.EncodeType = encType;
                    lo.IsLittleEndian = isLittleEndian;
                    lo.Tag = t;
                    return lo;

                case "ST":
                    ShortText st = new ShortText();
                    st.ByteData = data;
                    st.EncodeType = encType;
                    st.IsLittleEndian = isLittleEndian;
                    st.Tag = t;
                    return st;

                case "LT":
                    LongText lt = new LongText();
                    lt.ByteData = data;
                    lt.EncodeType = encType;
                    lt.IsLittleEndian = isLittleEndian;
                    lt.Tag = t;
                    return lt;

                case "UT":
                    UnlimitedText ut = new UnlimitedText();
                    ut.ByteData = data;
                    ut.EncodeType = encType;
                    ut.IsLittleEndian = isLittleEndian;
                    ut.Tag = t;
                    return ut;

                case "AE":
                    ApplicationEntity ae = new ApplicationEntity();
                    ae.ByteData = data;
                    ae.EncodeType = encType;
                    ae.IsLittleEndian = isLittleEndian;
                    ae.Tag = t;
                    return ae;

                case "PN":
                    PersonsName pn = new PersonsName();
                    pn.ByteData = data;
                    pn.EncodeType = encType;
                    pn.IsLittleEndian = isLittleEndian;
                    pn.Tag = t;
                    return pn;

                case "UI":
                    UniqueIdentifier ui = new UniqueIdentifier();
                    ui.ByteData = data;
                    ui.EncodeType = encType;
                    ui.IsLittleEndian = isLittleEndian;
                    ui.Tag = t;
                    return ui;

                case "DA":
                    DateVR da = new DateVR();
                    da.ByteData = data;
                    da.EncodeType = encType;
                    da.IsLittleEndian = isLittleEndian;
                    da.Tag = t;
                    return da;

                case "TM":
                    TimeVR tm = new TimeVR();
                    tm.ByteData = data;
                    tm.EncodeType = encType;
                    tm.IsLittleEndian = isLittleEndian;
                    tm.Tag = t;
                    return tm;

                case "DT":
                    DateTimeVR dt = new DateTimeVR();
                    dt.ByteData = data;
                    dt.EncodeType = encType;
                    dt.IsLittleEndian = isLittleEndian;
                    dt.Tag = t;
                    return dt;

                case "AS":
                    AgeString aSt = new AgeString();
                    aSt.ByteData = data;
                    aSt.EncodeType = encType;
                    aSt.IsLittleEndian = isLittleEndian;
                    aSt.Tag = t;
                    return aSt;

                case "IS":
                    IntegerString iSt = new IntegerString();
                    iSt.ByteData = data;
                    iSt.EncodeType = encType;
                    iSt.IsLittleEndian = isLittleEndian;
                    iSt.Tag = t;
                    return iSt;

                case "DS":
                    DecimalString ds = new DecimalString();
                    ds.ByteData = data;
                    ds.EncodeType = encType;
                    ds.IsLittleEndian = isLittleEndian;
                    ds.Tag = t;
                    return ds;

                case "SS":
                    SignedShort ss = new SignedShort();
                    ss.ByteData = data;
                    ss.EncodeType = encType;
                    ss.IsLittleEndian = isLittleEndian;
                    ss.Tag = t;
                    return ss;

                case "US":
                    UnsignedShort us = new UnsignedShort();
                    us.ByteData = data;
                    us.EncodeType = encType;
                    us.IsLittleEndian = isLittleEndian;
                    us.Tag = t;
                    return us;

                case "SL":
                    SignedLong sl = new SignedLong();
                    sl.ByteData = data;
                    sl.EncodeType = encType;
                    sl.IsLittleEndian = isLittleEndian;
                    sl.Tag = t;
                    return sl;

                case "UL":
                    UnsignedLong ul = new UnsignedLong();
                    ul.ByteData = data;
                    ul.EncodeType = encType;
                    ul.IsLittleEndian = isLittleEndian;
                    ul.Tag = t;
                    return ul;

                case "AT":
                    AttributeTag at = new AttributeTag();
                    at.ByteData = data;
                    at.EncodeType = encType;
                    at.IsLittleEndian = isLittleEndian;
                    at.Tag = t;
                    return at;

                case "FL":
                    FloatingPointSingle fl = new FloatingPointSingle();
                    fl.ByteData = data;
                    fl.EncodeType = encType;
                    fl.IsLittleEndian = isLittleEndian;
                    fl.Tag = t;
                    return fl;

                case "FD":
                    FloatingPointDouble fd = new FloatingPointDouble();
                    fd.ByteData = data;
                    fd.EncodeType = encType;
                    fd.IsLittleEndian = isLittleEndian;
                    fd.Tag = t;
                    return fd;

                case "OB":
                    if (t.Id == TagHelper.PIXEL_DATA)
                    {
                        PixelData fd1 = new PixelData(data, encType, isLittleEndian, "OB", isIndefinite);
                        fd1.Format = isIndefinite ? FrameDataFormat.ENCAPSULATED : FrameDataFormat.NATIVE;
                        fd1.EncodeType = encType;
                        fd1.IsLittleEndian = isLittleEndian;
                        fd1.Tag = t;
                        return fd1;
                    }
                    else
                    {
                        OtherByteString ob = new OtherByteString();
                        ob.ByteData = data;
                        ob.EncodeType = encType;
                        ob.IsLittleEndian = isLittleEndian;
                        ob.Tag = t;
                        return ob;
                    }
                case "OW":
                    if (t.Id == TagHelper.PIXEL_DATA)
                    {
                        PixelData fd2 = new PixelData(data, encType, isLittleEndian, "OW", isIndefinite);
                        fd2.Format = isIndefinite ? FrameDataFormat.ENCAPSULATED : FrameDataFormat.NATIVE;
                        fd2.EncodeType = encType;
                        fd2.IsLittleEndian = isLittleEndian;
                        fd2.Tag = t;
                        return fd2;
                    }
                    else
                    {
                        OtherWordString ow = new OtherWordString();
                        ow.ByteData = data;
                        ow.EncodeType = encType;
                        ow.IsLittleEndian = isLittleEndian;
                        ow.Tag = t;
                        return ow;
                    }


                case "OF":
                    OtherFloatString of = new OtherFloatString();
                    of.ByteData = data;
                    of.EncodeType = encType;
                    of.IsLittleEndian = isLittleEndian;
                    of.Tag = t;
                    return of;

                case "SQ":
                    Sequence s = new Sequence();
                    s.ByteData = data;
                    s.EncodeType = encType;
                    s.IsLittleEndian = isLittleEndian;
                    s.Tag = t;
                    s.ReadChildren();
                    return s;

                default:
                    //Case for unknown VR
                    DICOMElement dOb = new DICOMElement();
                    dOb.ByteData = data;
                    dOb.EncodeType = encType;
                    dOb.IsLittleEndian = isLittleEndian;
                    dOb.Tag = t;
                    return dOb;
            }
        }

        /// <summary>
        /// This method determines the encoding type of the VR of the next DICOM object in a Binary reader.
        /// </summary>
        /// <param name="r">The Binary reader containing the bytes of a DICOM object</param>
        /// <param name="vr">The two letter representation of the VR of the DICOM object</param>
        /// <param name="dataLength">The lenght of the data in this DICOM object</param>
        /// <param name="encType">The encoding type of the VR in this DICOM object</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        private static void GetEncoding(BinaryReader r, out string vr, out int dataLength, out Constants.EncodeType encType, bool isLittleEndian)
        {
            byte[] vrBytes = new byte[4];
            r.Read(vrBytes, 0, 4);

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            string possibleVR = enc.GetString(vrBytes).Substring(0, 2);

            //Check to see if the dicom object is explicit or implicit vr
            //Start out as implicit and change if neccessary
            encType = Constants.EncodeType.IMPLICIT;
            vr = "";
            foreach (string s in vrs)
            {
                if (s.Contains(possibleVR))
                {
                    //Encoding is Explicit VR with 2 byte length
                    encType = Constants.EncodeType.EXPLICIT_2;
                    vr = possibleVR;
                    foreach (string st in longVrs)
                    {
                        if (st.Contains(possibleVR))
                        {
                            //Encoding is Explicit VR with 4 byte length
                            encType = Constants.EncodeType.EXPLICIT_4;
                            break;
                        }

                    }
                }
            }

            //Based on encoding type pull the length attribute
            dataLength = 0;
            switch (encType)
            {
                case Constants.EncodeType.IMPLICIT:
                    //Check for indefinite length
                    if (Helper.ArrayHelper.isEqualArray(vrBytes, Constants.INDEFINITE_LENGTH))
                    {
                        if (string.IsNullOrEmpty(vr))
                        {
                            vr = "SQ";
                        }
                        dataLength = GetIndefiniteLength(r, isLittleEndian);
                    }
                    else
                    {
                        if (!isLittleEndian) { vrBytes = ArrayHelper.ReverseArray(vrBytes); }
                        dataLength = BitConverter.ToInt32(vrBytes, 0);
                    }
                    break;

                case Constants.EncodeType.EXPLICIT_2:
                    //Read last 2 bytes as length
                    byte[] vrBytesEnd = new byte[] { vrBytes[2], vrBytes[3] };
                    if (!isLittleEndian) { vrBytesEnd = ArrayHelper.ReverseArray(vrBytesEnd); }
                    dataLength = BitConverter.ToInt16(vrBytesEnd, 0);
                    break;
                case Constants.EncodeType.EXPLICIT_4:
                    //Read next four bytes as length
                    r.Read(vrBytes, 0, 4);

                    //Check for indefinite length
                    if (Helper.ArrayHelper.isEqualArray(vrBytes, Constants.INDEFINITE_LENGTH))
                    {
                        if (string.IsNullOrEmpty(vr))
                        {
                            vr = "SQ";
                        }
                        dataLength = GetIndefiniteLength(r, isLittleEndian);
                    }
                    else
                    {
                        if (!isLittleEndian) { vrBytes = ArrayHelper.ReverseArray(vrBytes); }
                        dataLength = BitConverter.ToInt32(vrBytes, 0);
                    }
                    break;
            }
        }

        private static int GetIndefiniteLength(BinaryReader r, bool isLittleEndian)
        {
            int curPos = (int)r.BaseStream.Position;
            int endPos = SequenceHelper.FindEndOfSequence(r, isLittleEndian);
            r.BaseStream.Position = curPos;
            return (endPos - curPos) * -1;
        }

    }
}



//Copyright © 2012 Rex Cardan, Ph.D


