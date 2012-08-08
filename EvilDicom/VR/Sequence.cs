using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.Helper;
using System.IO;

namespace EvilDicom
{
    namespace VR
    {
        public class Sequence : DICOMElement
        {
            public Sequence() { VR = "SQ"; }
            private List<SequenceItem> items = new List<SequenceItem>();
            protected Constants.LengthType lengthType = Constants.LengthType.FINITE;
            private Tag END_TAG = new Tag("FFFE", "E0DD");

            public List<SequenceItem> Items
            {
                set { items = value; }
                get { return items; }
            }

            public void AddItem(SequenceItem si)
            {
                items.Add(si);
            }

            public Constants.LengthType LengthType
            {
                get { return lengthType; }
                set { lengthType = value; }
            }

            public override int Length
            {
                get
                {
                    int len = 0;
                    foreach (SequenceItem si in this.items)
                    {
                        len += si.Length;
                    }
                    switch (EncodeType)
                    {
                        case Constants.EncodeType.IMPLICIT:
                            if (lengthType == Constants.LengthType.FINITE) { return 8 + len; }
                            else { return 16 + len; }
                        case Constants.EncodeType.EXPLICIT_4:
                            if (lengthType == Constants.LengthType.FINITE) { return 12 + len; }
                            else { return 20 + len; }
                        default:
                            if (lengthType == Constants.LengthType.FINITE) { return 8 + len; }
                            else { return 16 + len; }
                    }
                }
            }

            private int StartItemsIndex
            {
                get
                {
                    switch (EncodeType)
                    {
                        case Constants.EncodeType.IMPLICIT:
                            if (lengthType == Constants.LengthType.FINITE) { return 8; }
                            else { return 16; }
                        case Constants.EncodeType.EXPLICIT_4:
                            if (lengthType == Constants.LengthType.FINITE) { return 12; }
                            else { return 20; }
                        default:
                            if (lengthType == Constants.LengthType.FINITE) { return 8; }
                            else { return 16; }
                    }
                }
            }

            public override void WriteBytes(BinaryWriter b, bool isLittleEndian)
            {
                Helper.DICOMWriter.WriteTag(b, this.Tag, this.IsLittleEndian);
                Helper.DICOMWriter.WriteVR(b, this.VR, this.EncodeType);

                //Write Length
                if (lengthType == Constants.LengthType.FINITE)
                {
                    Helper.DICOMWriter.WriteLength(b, this.EncodeType, this.Length - this.StartItemsIndex, this.IsLittleEndian);
                }
                else
                {
                    Helper.DICOMWriter.WriteLength(b, Constants.INDEFINITE_LENGTH);
                }

                //Write Sequence Items
                foreach (SequenceItem si in items)
                {
                    si.WriteBytes(b, isLittleEndian);
                }

                //Write End Delimeter for Indefinite Length Sequences
                if (lengthType == Constants.LengthType.INDEFINITE)
                {
                    Helper.DICOMWriter.WriteTag(b, END_TAG, isLittleEndian);
                    Helper.DICOMWriter.WriteLength(b, Constants.ZERO_LENGTH);
                }

            }


            public void ReadChildren()
            {
                foreach (SequenceItem si in SequenceHelper.ReadChildren(ByteData, IsLittleEndian))
                {
                    items.Add(si);
                }
            }

            internal override List<DICOMElement> find(string[] ids)
            {
                List<DICOMElement> found = new List<DICOMElement>();
                foreach (SequenceItem si in items)
                {
                    foreach (DICOMElement d in si.Find(ids))
                    {
                        found.Add(d);
                    }
                }
                return found;
            }

            public new byte[] ByteData
            {
                get
                {
                    return base.ByteData;
                }
                set
                {
                    base.ByteData = value;
                }

            }

            public override string[] DataAsStringArray()
            {
                string[] sData = new string[Items.Count];
                for (int i = 0; i < Items.Count; i++)
                {
                    sData[i] = string.Format("Item {0}:{1} Subitems",i,Items[i].DicomObjects.Count);
                }
                return sData;
            }
        }
    }
}

//Copyright © 2012 Rex Cardan, Ph.D


