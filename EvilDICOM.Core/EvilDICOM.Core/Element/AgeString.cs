using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the AgeString VR type
    /// </summary>
    public class AgeString : AbstractElement<string>
    {
        public AgeString() { }

        public AgeString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.AgeString;
        }

        /// <summary>
        /// The age stored in the element. Supplements the data property.
        /// </summary>
        public Age Age
        {
            get
            {
                return StringDataParser.ParseAgeString(Data);
            }
            set
            {
                Data = StringDataComposer.ComposeAgeString(value);
            }
        }
    }

    /// <summary>
    /// A small class to help manipulate the age in the AgeString class
    /// </summary>
    public class Age
    {
        public int Number { get; set; }
        public Unit Units { get; set; }

        public enum Unit { DAYS, WEEKS, MONTHS, YEARS }
    }
}
