using System.Collections.Generic;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.Selection
{
    public static class ListExtensions
    {
        public static T FindFirst<T>(this List<DICOMSelector> items, string tagId)
        {
            foreach (DICOMSelector d in items)
            {
                IDICOMElement found = d.ToDICOMObject().FindFirst(tagId);
                if (found != null)
                {
                    return (T) found;
                }
            }
            return default(T);
        }

        public static List<T> FindAll<T>(this List<DICOMSelector> items, string tagId)
        {
            var allFound = new List<T>();
            foreach (DICOMSelector d in items)
            {
                List<IDICOMElement> found = d.ToDICOMObject().FindAll(tagId);
                if (found.Count > 0)
                {
                    foreach (IDICOMElement f in found)
                    {
                        allFound.Add((T) f);
                    }
                }
            }
            return allFound;
        }
    }
}