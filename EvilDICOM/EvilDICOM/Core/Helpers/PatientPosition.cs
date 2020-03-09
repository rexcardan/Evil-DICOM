using EvilDICOM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static EvilDICOM.Core.Enums.Orientation;

namespace EvilDICOM.Core.Helpers
{
    public sealed class PatientPosition
    {
        public static PatientPosition FromAbbreviation(string abbr)
        {
            switch (abbr)
            {
                case "HFP": return PatientPosition.HeadFirstProne;
                case "HFS": return PatientPosition.HeadFirstSupine;
                case "HFDR": return PatientPosition.HeadFirstDecubitusRight;
                case "HFDL": return PatientPosition.HeadFirstDecubitusLeft;
                case "FFDL": return PatientPosition.FeetFirstDecubitusLeft;
                case "FFDR": return PatientPosition.FeetFirstDecubitusRight;
                case "FFP": return PatientPosition.FeetFirstProne;
                case "FFS": return PatientPosition.FeetFirstSupine;
                case "LFS": return PatientPosition.LeftFirstProne;
                case "RFS": return PatientPosition.RightFirstProne;
                case "AFDR": return PatientPosition.AnteriorFirstDecubitusRight;
                case "AFDL": return PatientPosition.AnteriorFirstDecubitusLeft;
                case "PFDR": return PatientPosition.PosteriorFirstDecubitusRight;
                case "PFDL": return PatientPosition.PosteriorFirstDecubitusLeft;
                default: throw new ArgumentException($"{abbr} not a valid patient position!");
            }
        }

        private readonly string name;
        public static readonly PatientPosition HeadFirstProne = new PatientPosition(HFP, "HFP");
        public static readonly PatientPosition HeadFirstSupine = new PatientPosition(HFS, "HFS");
        public static readonly PatientPosition HeadFirstDecubitusRight = new PatientPosition(HFDR, "HFDR");
        public static readonly PatientPosition HeadFirstDecubitusLeft = new PatientPosition(HFDL, "HFDL");
        public static readonly PatientPosition FeetFirstDecubitusRight = new PatientPosition(FFDR, "FFDR");
        public static readonly PatientPosition FeetFirstDecubitusLeft = new PatientPosition(FFDL, "FFDL");
        public static readonly PatientPosition FeetFirstProne = new PatientPosition(FFP, "FFP");
        public static readonly PatientPosition FeetFirstSupine = new PatientPosition(FFS, "FFS");
        public static readonly PatientPosition LeftFirstProne = new PatientPosition(LFP, "LFP");
        public static readonly PatientPosition RightFirstProne = new PatientPosition(RFP, "RFP");
        public static readonly PatientPosition AnteriorFirstDecubitusRight = new PatientPosition(AFDR, "AFDR");
        public static readonly PatientPosition AnteriorFirstDecubitusLeft = new PatientPosition(AFDL, "AFDL");
        public static readonly PatientPosition PosteriorFirstDecubitusRight = new PatientPosition(PFDR, "PFDR");
        public static readonly PatientPosition PosteriorFirstDecubitusLeft = new PatientPosition(PFDL, "PFDL");

        private PatientPosition(Orientation value, String name)
        {
            this.name = name;
            this.Orientation = value;
        }

        public Orientation Orientation { get; }

        public override String ToString()
        {
            return name;
        }
    }
}
