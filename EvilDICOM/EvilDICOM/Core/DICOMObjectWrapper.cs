using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core
{
    /// <summary>
    /// Class DICOMObjectWrapper.
    /// </summary>
    public abstract class DICOMObjectWrapper
    {
        /// <summary>
        /// The _DCM
        /// </summary>
        protected DICOMObject _dcm;

        /// <summary>
        /// Initializes a new instance of the <see cref="DICOMObjectWrapper"/> class.
        /// </summary>
        public DICOMObjectWrapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DICOMObjectWrapper"/> class.
        /// </summary>
        /// <param name="dicom">The dicom.</param>
        public DICOMObjectWrapper(DICOMObject dicom)
        {
            _dcm = dicom;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag">The tag.</param>
        /// <returns>DICOMData&lt;T&gt;.</returns>
        public DICOMData<T> GetValue<T>(Tag tag)
        {
            return _dcm.TryGetDataValue<T>(tag, null);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public void SetValue<T>(Tag tag, T value)
        {
            _dcm.TrySetDataValue(tag, value);
        }

        /// <summary>
        /// Wraps a DICOM sequence with a type that inherits from DICOMObjectWrapper
        /// </summary>
        /// <typeparam name="T">the wrapping class for the DICOM objects within the sequence</typeparam>
        /// <param name="tag">the DICOM tag of the sequence which contains the objects to wrap</param>
        /// <returns>a list of wrapped DICOM objects</returns>
        public List<T> GetWrappedSequence<T>(Tag tag) where T : DICOMObjectWrapper, new()
        {
            DICOMData<DICOMObject> seq = _dcm.TryGetDataValue<DICOMObject>(tag, null);
            if (seq != null)
            {
                return seq.MultipicityValue.Select(si =>
                {
                    var t = new T();
                    t._dcm = si;
                    return t;
                }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Extracts the DICOMObjects out of a list of DICOMObjectWrappers and sets these as the new values in the sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag">the DICOM tag of the sequence where to place the newly extracted objects</param>
        /// <param name="value">the list of wrapped DICOMObjects to go in the sequence</param>
        public void SetWrappedSequence<T>(Tag tag, List<T> value) where T : DICOMObjectWrapper
        {
            _dcm.TrySetDataValue<List<DICOMObject>>(tag, value.Select(dw => dw._dcm).ToList());
        }
    }
}