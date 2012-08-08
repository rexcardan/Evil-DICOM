using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.VR
{
    public class AgeString : AbstractStringVR
    {
        public AgeString() { base.VR = "AS"; }

        public override string Data
        {
            get { return base.Data; }
            set { base.Data = value; }
        }

        public void setAge(Age age)
        {
            string ageString = String.Format("{0:00#}", age.Number);
            switch (age.Units)
            {
                case Age.Unit.DAYS: ageString += "D"; break;
                case Age.Unit.WEEKS: ageString += "W"; break;
                case Age.Unit.MONTHS: ageString += "M"; break;
                case Age.Unit.YEARS: ageString += "Y"; break;
            }
            base.Data = ageString;
        }

        public Age getAge()
        {
            Age age = new Age();
            age.Number = int.Parse(base.Data.Substring(1, 3));
            switch (base.Data.Substring(4, 1))
            {
                case "D": age.Units = Age.Unit.DAYS; break;
                case "W": age.Units = Age.Unit.WEEKS; break;
                case "M": age.Units = Age.Unit.MONTHS; break;
                case "Y": age.Units = Age.Unit.YEARS; break;
            }

            return age;
        }

        public class Age
        {
            int number;

            public int Number
            {
                get { return number; }
                set { number = value; }
            }
            Unit units;

            public Unit Units
            {
                get { return units; }
                set { units = value; }
            }

            public enum Unit { DAYS, WEEKS, MONTHS, YEARS }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


