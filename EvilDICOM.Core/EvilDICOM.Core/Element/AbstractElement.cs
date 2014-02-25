using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// The overarching abstract class from which all DICOM element classes derive. Contains properties that are common to elements.
    /// </summary>
    /// <typeparam name="T">the data type of the element</typeparam>
    public abstract class AbstractElement<T> : IDICOMElement
    {
        public AbstractElement()
        {
            DataContainer = new DICOMData<T>();
        }

        public AbstractElement(Tag tag, T[] dataArray)
        {
            DataContainer = new DICOMData<T>();
            this.Tag = tag;
            this.DataContainer = DICOMData<T>.CreateFromArray(dataArray);
        }

        public AbstractElement(Tag tag, T data)
        {
            DataContainer = new DICOMData<T>();
            this.Tag = tag;
            this.DataContainer.SingleValue = data;
        }

        /// <summary>
        /// To string override to visualize tag and vr of element
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Tag.ToString(), VR.ToString(), string.Join(" | ", DData_.ToArray()));
        }

        /// <summary>
        /// The tag of the element
        /// </summary>
        public Tag Tag
        {
            get;
            set;
        }

        /// <summary>
        /// The value representation of the element
        /// </summary>
        public VR VR
        {
            get;
            set;
        }

        /// <summary>
        /// The data of type T of the element
        /// </summary>
        internal DICOMData<T> DataContainer { get; set; }

        /// <summary>
        /// The data of the element
        /// </summary>
        public T Data { get { return GetDataOrDefault(); } set { SetData(value); } }
        /// <summary>
        /// The data of the element as a list (for multiple data)
        /// </summary>
        public List<T> Data_ { get { return DataContainer.MultipicityValue; } set { SetData(value.ToArray()); } }

        /// <summary>
        /// Plumbing method wrap the data in a DICOMData container
        /// </summary>
        /// <param name="value">the typed data</param>
        public void SetData(T value)
        {
            DataContainer = DICOMData<T>.CreateFromSingle(value);
        }

        /// <summary>
        /// Plumbing method wrap the data in a DICOMData container
        /// </summary>
        /// <param name="value">the array of typed data</param>
        public void SetData(T[] dataArray)
        {
            DataContainer = DICOMData<T>.CreateFromArray(dataArray);
        }

        /// <summary>
        /// Plumbing method to get data from the underlying DICOMData object
        /// </summary>
        public T GetDataOrDefault()
        {
            return DataContainer != null ? DataContainer.SingleValue : default(T);
        }

        /// <summary>
        /// The clr type of the contained data
        /// </summary>
        public Type DataType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// The non-typed data that can be accessed in a dynamic context
        /// </summary>
        public dynamic DData
        {
            get { return DataContainer.SingleValue; }
            set { DataContainer.SingleValue = (T)value; }
        }

        /// <summary>
        /// The non-typed data that can be accessed in a dynamic context. Format is List<T>
        /// </summary>
        public dynamic DData_
        {
            get { return DataContainer.MultipicityValue; }
            set { DataContainer.MultipicityValue = value; }
        }
    }
}
