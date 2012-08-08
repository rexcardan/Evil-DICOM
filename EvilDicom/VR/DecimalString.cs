using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class DecimalString : AbstractStringVR
        {
            public DecimalString() { VR = "DS"; }

            public new double[] Data
            {
                get
                {
                    string[] sNumbers = base.Data.Replace(" ", "").Split(new char[] { '\\' });
                    double[] numbers = new double[sNumbers.Length];
                    for (int i = 0; i < sNumbers.Length; i++)
                    {
                        double.TryParse(sNumbers[i], out numbers[i]);
                    }
                    return numbers;

                }
                set
                {
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


