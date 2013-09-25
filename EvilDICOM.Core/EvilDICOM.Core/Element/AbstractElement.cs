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
            return string.Format("VR = {0}, Tag = {1},{2}", VR.ToString(), Tag.Group, Tag.Element);
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

        public T Data { get { return GetDataOrDefault(); } set { SetData(value); } }
        public List<T> Data_ { get { return DataContainer.MultipicityValue; } set { SetData(value.ToArray()); } }

        public void SetData(T value)
        {
            DataContainer = DICOMData<T>.CreateFromSingle(value);
        }

        public void SetData(T[] dataArray)
        {
            DataContainer = DICOMData<T>.CreateFromArray(dataArray);
        }

        public T GetDataOrDefault()
        {
            return DataContainer != null ? DataContainer.SingleValue : default(T);
        }

        public Type DataType
        {
            get { return typeof(T); }
        }

        public object UntypedData
        {
            get { return DataContainer; }
            set { DataContainer = (DICOMData<T>)value; }
        }


    }
}
