using System;
using System.Collections.Generic;
using System.Linq;

namespace EvilDICOM.Core.IO.Data
{
    /// <summary>
    ///     Helps read DICOM data with VM > 1
    /// </summary>
    public class MultiplicityReader
    {
        /// <summary>
        ///     Reads binary data that has been concated with no delimiter. Returns an array of each instance of data in the
        ///     concated bytes.
        /// </summary>
        /// <typeparam name="T">the type of data in the bytes</typeparam>
        /// <param name="data">the concated data (also accepts data with no concation)</param>
        /// <param name="singleLength">the length in bytes of each data item</param>
        /// <param name="readSingleFunc">the function to use to parse each data element in the concated bytes</param>
        /// <returns>an array of each data instance</returns>
        public static T[] ReadMultipleBinary<T>(byte[] data, int singleLength, Func<byte[], T> readSingleFunc)
        {
            var values = new List<T>();
            for (int i = 0; i < data.Length; i += singleLength)
            {
                byte[] singleBytes = data.Skip(i).Take(singleLength).ToArray();
                values.Add(readSingleFunc(singleBytes));
            }
            return values.ToArray();
        }
    }
}