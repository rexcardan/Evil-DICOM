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
            Data = new DICOMData<T>();
        }

        public AbstractElement(Tag tag, T[] dataArray)
        {
            Data = new DICOMData<T>();
            this.Tag = tag;
            this.Data = DICOMData<T>.CreateFromArray(dataArray);
        }

        public AbstractElement(Tag tag, T data)
        {
            Data = new DICOMData<T>();
            this.Tag = tag;
            this.Data.SingleValue = data;
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
        public DICOMData<T> Data { get; set; }

        public void SetData(T value)
        {
            Data = DICOMData<T>.CreateFromSingle(value);
        }

        public void SetData(T[] dataArray)
        {
            Data = DICOMData<T>.CreateFromArray(dataArray);
        }

        public T GetDataOrDefault()
        {
            return Data != null ? Data.SingleValue : default(T);
        }

        public Type DataType
        {
            get { return typeof(T); }
        }

        public object UntypedData
        {
            get { return Data; }
            set { Data = (DICOMData<T>)value; }
        }


    }
}
