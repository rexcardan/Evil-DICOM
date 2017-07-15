using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the DecimalString VR type
    /// </summary>
    public class DecimalString : AbstractElement<double>
    {
        public DecimalString()
        {
        }

        public DecimalString(Tag tag, string data)
            : base(tag, StringDataParser.ParseDecimalString(data))
        {
            VR = VR.DecimalString;
        }

        public DecimalString(Tag tag, double data)
            : base(tag, data)
        {
            VR = VR.DecimalString;
        }

        public DecimalString(Tag tag, double[] data)
            : base(tag, data)
        {
            VR = VR.DecimalString;
        }
    }
}