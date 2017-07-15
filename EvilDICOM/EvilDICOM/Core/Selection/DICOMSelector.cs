#region

using EvilDICOM.Core.Element;

#endregion

namespace EvilDICOM.Core.Selection
{
    public partial class DICOMSelector
    {
        private readonly DICOMObject _dicom;

        public DICOMSelector(DICOMObject d)
        {
            _dicom = d;
        }

        public DICOMObject ToDICOMObject()
        {
            return _dicom;
        }

        /// <summary>
        /// Forge a new element or replace one if it already exists
        /// </summary>
        /// <typeparam name="T">the data type of this element</typeparam>
        /// <param name="el">the element to create or replace</param>
        /// <returns>the instance of the inserted element</returns>
        public AbstractElement<T> Forge<T>(AbstractElement<T> el)
        {
            _dicom.ReplaceOrAdd(el);
            return el;
        }

        /// <summary>
        /// Forge a new element or replace one if it already exists
        /// </summary>
        /// <typeparam name="T">the data type of this element</typeparam>
        /// <param name="el">the element to create or replace</param>
        /// <param name="data">the data for the element (replaces existing data)</param>
        /// <returns>the instance of the inserted element</returns>
        public AbstractElement<T> Forge<T>(AbstractElement<T> el, T data)
        {
            el.Data = data;
            _dicom.ReplaceOrAdd(el);
            return el;
        }
    }
}