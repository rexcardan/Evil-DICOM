using System.Collections.Generic;
using System.Linq;

namespace EvilDICOM.Core
{
    /// <summary>
    ///     A class to hold DICOM data. DICOM data is unique in that it can be a single value, multiple values and null. This
    ///     class tries to encapsulate those attributes while maintaining a flexible programming interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DICOMData<T>
    {
        /// <summary>
        ///     The constructor which ininitializes the underlying enumerable collection
        /// </summary>
        public DICOMData()
        {
            _data = new List<T>();
        }

        private List<T> _data { get; set; }

        /// <summary>
        ///     Gets and sets a single value for the data of the DICOM element. If the collection contains has more than one data
        ///     element, only the first is returned. If setting a value, the data is cleared and only a single entry is saved.
        /// </summary>
        public T SingleValue
        {
            get { return _data.Count > 0 ? _data.First() : default(T); }
            set
            {
                _data.Clear();
                _data.Add(value);
            }
        }

        /// <summary>
        ///     A list of the data within the element. This is designed to be accomodate the multiplicity aspect of DICOM data
        /// </summary>
        public List<T> MultipicityValue
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        ///     Creates a new DICOM Data object from a single data value
        /// </summary>
        /// <param name="dataValue">the data value from which to initialize the DICOM Data object</param>
        /// <returns></returns>
        public static DICOMData<T> CreateFromSingle(T dataValue)
        {
            var data = new DICOMData<T>();
            if (dataValue != null)
            {
                data.SingleValue = dataValue;
                return data;
            }
            return null;
        }

        /// <summary>
        ///     Creates a new DICOM Data object from an array of data
        /// </summary>
        /// <param name="dataArray">the data array from which to initialize the DICOM Data object</param>
        /// <returns></returns>
        public static DICOMData<T> CreateFromArray(T[] dataArray)
        {
            var data = new DICOMData<T>();
            if (dataArray != null)
            {
                data.MultipicityValue = dataArray.ToList();
                return data;
            }
            return null;
        }

        public override string ToString()
        {
            if (MultipicityValue.Count > 1)
            {
                return string.Join(" | ", MultipicityValue.ToArray());
            }
            return SingleValue != null ? SingleValue.ToString() : "";
        }
    }
}