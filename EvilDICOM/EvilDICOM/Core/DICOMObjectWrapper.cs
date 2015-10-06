using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core
{
    public abstract class DICOMObjectWrapper
    {
        protected DICOMObject _dcm;

        public DICOMObjectWrapper()
        {
        }

        public DICOMObjectWrapper(DICOMObject dicom)
        {
            _dcm = dicom;
        }

        public DICOMData<T> GetValue<T>(Tag tag)
        {
            return _dcm.TryGetDataValue<T>(tag, null);
        }

        public void SetValue<T>(Tag tag, T value)
        {
            _dcm.TrySetDataValue(tag, value);
        }

        /// <summary>
        ///     Wraps a DICOM sequence with a type that inherits from DICOMObjectWrapper
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
        ///     Extracts the DICOMObjects out of a list of DICOMObjectWrappers and sets these as the new values in the sequence
        /// </summary>
        /// <param name="tag">the DICOM tag of the sequence where to place the newly extracted objects</param>
        /// <param name="value">the list of wrapped DICOMObjects to go in the sequence</param>
        public void SetWrappedSequence<T>(Tag tag, List<T> value) where T : DICOMObjectWrapper
        {
            _dcm.TrySetDataValue<List<DICOMObject>>(tag, value.Select(dw => dw._dcm).ToList());
        }
    }
}