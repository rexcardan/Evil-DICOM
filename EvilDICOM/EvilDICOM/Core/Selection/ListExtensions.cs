#region

using System.Collections.Generic;

#endregion

namespace EvilDICOM.Core.Selection
{
    public static class ListExtensions
    {
        public static T FindFirst<T>(this List<DICOMSelector> items, string tagId)
        {
            foreach (var d in items)
            {
                var found = d.ToDICOMObject().FindFirst(tagId);
                if (found != null)
                    return (T) found;
            }
            return default(T);
        }

        public static List<T> FindAll<T>(this List<DICOMSelector> items, string tagId)
        {
            var allFound = new List<T>();
            foreach (var d in items)
            {
                var found = d.ToDICOMObject().FindAll(tagId);
                if (found.Count > 0)
                    foreach (var f in found)
                        allFound.Add((T) f);
            }
            return allFound;
        }
    }
}