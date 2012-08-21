using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    /// This class can read the DICOM preamble consisting of 128 null bits followed by the ASCII characters DICM.
    /// </summary>
    public static class DICOMPreambleReader
    {
        /// <summary>
        /// Reads the first 132 bits of a file to check if it contains the DICOM preamble.
        /// </summary>
        /// <param name="dr">a stream containing the bits of the file</param>
        /// <returns>a boolean indicating whether or not the DICOM preamble was in the file</returns>
        public static bool Read(DICOMBinaryReader dr)
        {
            byte[] preamble = new byte[132];
            try
            {
                dr.ReadBytes(preamble, 0, 132);
            }
            catch (Exception)
            {
                throw new Exception("Could not read 128 null bit preamble. Perhaps file is too short");
            }
            if (preamble[128] != 'D' || preamble[129] != 'I' || preamble[130] != 'C' || preamble[131] != 'M')
            {
                Console.WriteLine("Missing characters D I C M in bits 128-131. Technically not a valid DICOM file. Will try to read anyway.");
                dr.Reset();
                return false;
            }
            else
            {
                return true;
            }
        }   
    }
}
