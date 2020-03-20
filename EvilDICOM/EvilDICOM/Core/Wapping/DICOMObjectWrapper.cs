#region

using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Writing;

#endregion

namespace EvilDICOM.Core.Wrapping
{
    public abstract class DICOMObjectWrapper
    {
        public DICOMObject DCM { get; set; }

        public DICOMObjectWrapper()
        {
        }

        public DICOMObjectWrapper(DICOMObject dicom)
        {
            DCM = dicom;
        }

        public DICOMData<T> GetValue<T>(Tag tag)
        {
            return DCM.TryGetDataValue<T>(tag, null);
        }

        public void SetValue<T>(Tag tag, T value)
        {
            DCM.TrySetDataValue(tag, value);
        }

        /// <summary>
        ///     Wraps a DICOM sequence with a type that inherits from DICOMObjectWrapper
        /// </summary>
        /// <typeparam name="T">the wrapping class for the DICOM objects within the sequence</typeparam>
        /// <param name="tag">the DICOM tag of the sequence which contains the objects to wrap</param>
        /// <returns>a list of wrapped DICOM objects</returns>
        public SequenceItemWrapper<T> GetWrappedSequence<T>(Tag tag) where T : DICOMObjectWrapper, new()
        {
            var seq = DCM.TryGetDataValue<DICOMObject>(tag, null);
            if (seq != null)
                return new SequenceItemWrapper<T>(seq);
            return null;
        }

        /// <summary>
        ///     Extracts the DICOMObjects out of a list of DICOMObjectWrappers and sets these as the new values in the sequence
        /// </summary>
        /// <param name="tag">the DICOM tag of the sequence where to place the newly extracted objects</param>
        /// <param name="value">the list of wrapped DICOMObjects to go in the sequence</param>
        public void SetWrappedSequence<T>(Tag tag, List<T> value) where T : DICOMObjectWrapper
        {
            DCM.TrySetDataValue<List<DICOMObject>>(tag, value.Select(dw => dw.DCM).ToList());
        }

        public void Write(string filePath, DICOMIOSettings settings = null)
        {
            DCM.Write(filePath, settings);
        }

        public void WriteAddMeta(string filePath, DICOMIOSettings settings = null)
        {
            DCM.WriteAddMeta(filePath, settings);
        }
    }
}