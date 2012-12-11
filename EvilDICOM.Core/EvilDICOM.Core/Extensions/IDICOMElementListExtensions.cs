using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Extensions
{
    /// <summary>
    /// Adds useful methods to a List of IDICOMElements
    /// </summary>
    public static class IDICOMElementListExtensions
    {
        /// <summary>
        /// Sorts the list of elements such that the lowest Tag CompleteID is first (for DICOM compliance)
        /// </summary>
        /// <param name="elements"></param>
        public static void SortByTagID(this List<IDICOMElement> elements)
        {
            elements.Sort(new TagSorter());
        }
    }
}
