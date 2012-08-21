using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Extensions
{
    public static class IDICOMElementListExtensions
    {
        public static void SortByTagID(this List<IDICOMElement> elements)
        {
            elements.Sort(new TagSorter());
        }
    }
}
