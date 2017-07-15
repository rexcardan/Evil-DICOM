using System;
using System.Collections.Generic;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.Helpers
{
    public class TagSorter : IComparer<IDICOMElement>
    {
        public int Compare(IDICOMElement el1, IDICOMElement el2)
        {
            byte[] tagBytes_1 = ConvertToLittleEndian(ByteHelper.HexStringToByteArray(el1.Tag.CompleteID));
            byte[] tagBytes_2 = ConvertToLittleEndian(ByteHelper.HexStringToByteArray(el2.Tag.CompleteID));

            //Compare Groups First
            int groupInt_1 = BitConverter.ToUInt16(tagBytes_1, 0);
            int groupInt_2 = BitConverter.ToUInt16(tagBytes_2, 0);

            if (groupInt_1 > groupInt_2)
            {
                //Move down
                return 1;
            }
            if (groupInt_1 < groupInt_2)
            {
                //Move up
                return -1;
            }
            //Equal groups. Check element id
            int elemInt_1 = BitConverter.ToUInt16(tagBytes_1, 2);
            int elemInt_2 = BitConverter.ToUInt16(tagBytes_2, 2);

            if (elemInt_1 > elemInt_2)
            {
                //Move down
                return 1;
            }
            if (elemInt_1 < elemInt_2)
            {
                //Move up
                return -1;
            }
            //Equal elements
            return 0;
        }

        private byte[] ConvertToLittleEndian(byte[] bigEndian)
        {
            if (bigEndian.Length == 4)
            {
                return new[]
                {
                    bigEndian[1], bigEndian[0], bigEndian[3], bigEndian[2]
                };
            }
            throw new Exception("Tag does not have four bytes. Cannot convert to little endian.");
        }
    }
}