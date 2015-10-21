using System.Collections.Generic;

namespace EvilDICOM.Core.Helpers
{
    public class ArrayHelper
    {
        public static T[] Pop<T>(T[] toPop)
        {
            var popped = new List<T>(toPop);
            popped.RemoveAt(0);
            return popped.ToArray();
        }

        /// <summary>
        ///     A method that compares each object in two arrays to see if the arrays are equal
        /// </summary>
        /// <typeparam name="T">the class type of the array</typeparam>
        /// <param name="array1">the first array to be compared</param>
        /// <param name="array2">the second array to be compared</param>
        /// <returns></returns>
        public static bool AreEqual<T>(T[] array1, T[] array2)
        {
            //Check they are the same length
            if (array1.Length != array2.Length)
            {
                return false;
            }
            //Check all elements are the same
            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}