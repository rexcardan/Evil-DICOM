#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// The overarching abstract class from which all DICOM element classes derive. Contains properties that are common to
    /// elements.
    /// </summary>
    /// <typeparam name="T">the data type of the element</typeparam>
    /// <seealso cref="EvilDICOM.Core.Interfaces.IDICOMElement" />
    public abstract class AbstractElement<T> : IDICOMElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractElement{T}"/> class.
        /// </summary>
        public AbstractElement()
        {
            DataContainer = new DICOMData<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractElement{T}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="dataArray">The data array.</param>
        public AbstractElement(Tag tag, T[] dataArray)
        {
            DataContainer = new DICOMData<T>();
            Tag = tag;
            DataContainer = DICOMData<T>.CreateFromArray(dataArray);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractElement{T}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public AbstractElement(Tag tag, T data)
        {
            DataContainer = new DICOMData<T>();
            Tag = tag;
            DataContainer.SingleValue = data;
        }

        /// <summary>
        /// The value representation of the element
        /// </summary>
        /// <value>The vr.</value>
        public VR VR { get; set; }

        /// <summary>
        /// The data of type T of the element
        /// </summary>
        /// <value>The data container.</value>
        internal DICOMData<T> DataContainer { get; set; }

        /// <summary>
        /// The data of the element
        /// </summary>
        /// <value>The data.</value>
        public virtual T Data
        {
            get { return GetDataOrDefault(); }
            set { SetData(value); }
        }

        /// <summary>
        /// The data of the element as a list (for multiple data)
        /// </summary>
        /// <value>The data_.</value>
        public virtual List<T> Data_
        {
            get { return DataContainer.MultipicityValue; }
            set { SetData(value.ToArray()); }
        }

        /// <summary>
        /// The tag of the element
        /// </summary>
        /// <value>The tag.</value>
        public Tag Tag { get; set; }

        /// <summary>
        /// The clr type of the contained data
        /// </summary>
        /// <value>The type of the dat.</value>
        public Type DatType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// The non-typed data that can be accessed in a dynamic context
        /// </summary>
        /// <value>The d data.</value>
        public object DData
        {
            get { return DataContainer.SingleValue; }
            set { DataContainer.SingleValue = (T) value; }
        }

        /// <summary>
        /// The dynamic data in the element stored in a list of type T
        /// </summary>
        /// <value>The d data_.</value>
        public IList DData_
        {
            get { return DataContainer.MultipicityValue; }
            set { DataContainer.MultipicityValue = (List<T>) value; }
        }

        /// <summary>
        /// To string override to visualize tag and vr of element
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (DatType == typeof(byte) && Data_.Count > 10)
                return string.Format("{0} ({1}) -> {2}...", Tag, VR,
                    DataContainer != null ? string.Join(" | ", Data_.Take(10)) : "null");
            return string.Format("{0} ({1}) -> {2}", Tag, VR,
                DataContainer != null ? string.Join(" | ", Data_) : "null");
        }

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
        /// <param name="dataArray">The data array.</param>
        public void SetData(T[] dataArray)
        {
            DataContainer = DICOMData<T>.CreateFromArray(dataArray);
        }

        /// <summary>
        /// Plumbing method to get data from the underlying DICOMData object
        /// </summary>
        /// <returns>T.</returns>
        public T GetDataOrDefault()
        {
            return DataContainer != null ? DataContainer.SingleValue : default(T);
        }
    }
}