using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class IntegerString : AbstractStringVR
        {
            public IntegerString() { VR = "IS"; }

            public new int[] Data
            {
                get {
                    string[] sNumbers = base.Data.Replace(" ", "").Split(new char[] { '\\' });
                    int[] numbers = new int[sNumbers.Length];
                    for (int i = 0; i < sNumbers.Length; i++)
                    {
                        int.TryParse(sNumbers[i], out numbers[i]);
                    }
                    return numbers;
                }
                set {
                    string s = "";
                    for (int i = 0; i < value.Length; i++)
                    {
                        s += value[i].ToString();
                        if (i != value.Length - 1) { s += "\\"; }
                        base.Data = s;
                    }
                }
            }

            public override string[] DataAsStringArray()
            {
                string[] sData = new string[Data.Length];
                for (int i = 0; i < Data.Length; i++)
                {
                    sData[i] = Data[i].ToString();
                }
                return sData;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


