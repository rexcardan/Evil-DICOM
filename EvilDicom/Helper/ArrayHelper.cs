using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Helper
{
    /// <summary>
    /// A class used for working with arrays and lists.
    /// </summary>
        public class ArrayHelper
        {
            /// <summary>
            /// A method which returns a copy of the passed in array in reverse order.
            /// </summary>
            /// <typeparam name="T">the class of the object type within the array</typeparam>
            /// <param name="array">the array to be reversed</param>
            /// <returns></returns>
            public static T[] ReverseArray<T>(T[] array)
            {
                T[] newArray = null;
                int count = array == null ? 0 : array.Length;
                if (count > 0)
                {
                    newArray = new T[count];
                    for (int i = 0, j = count - 1; i < count; i++, j--)
                    {
                        newArray[i] = array[j];
                    }
                }
                return newArray;
            }

            /// <summary>
            /// A method which returns a copy of the passed in array.
            /// </summary>
            /// <typeparam name="T">the class of the object type within the array</typeparam>
            /// <param name="array">the array to be copied</param>
            /// <returns></returns>
            public static T[] CopyArray<T>(T[] array)
            {
                T[] newArray = null;
                int count = array == null ? 0 : array.Length;
                if (count > 0)
                {
                    newArray = new T[count];
                    for (int i = 0; i < count; i++)
                    {
                        newArray[i] = array[i];
                    }
                }

                return newArray;
            }

            /// <summary>
            /// A method which returns a copy of the passed in list.
            /// </summary>
            /// <typeparam name="T">the class type of the list</typeparam>
            /// <param name="list">the list to be copied
            /// </param>
            /// <returns></returns>
            public static List<T> CopyList<T>(List<T> list)
            {
                return new List<T>(list);
            }

            /// <summary>
            /// A method that compares each object in two arrays to see if the arrays are equal
            /// </summary>
            /// <typeparam name="T">the class type of the array</typeparam>
            /// <param name="array1">the first array to be compared</param>
            /// <param name="array2">the second array to be compared</param>
            /// <returns></returns>
            public static bool isEqualArray<T>(T[] array1, T[] array2)
            {
                //Check they are the same length
                if (array1.Length != array2.Length)
                {
                    return false;
                }
                //Check all elements are the same
                else
                {
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
}


//Copyright © 2012 Rex Cardan, Ph.D


