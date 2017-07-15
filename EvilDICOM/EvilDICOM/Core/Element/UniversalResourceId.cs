#region

using System.Net;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    public class UniversalResourceId : AbstractElement<string>
    {
        public UniversalResourceId()
        {
            VR = VR.UniversalResourceId;
        }

        public UniversalResourceId(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.UniversalResourceId;
        }

        /// <summary>
        /// Represents the raw string data
        /// </summary>
        public override string Data
        {
            get { return DataContainer.SingleValue; }
            set
            {
                DataContainer = DataContainer ?? new DICOMData<string>();
                DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value);
            }
        }

        /// <summary>
        /// Encodes and decodes a Uri string to data in this element
        /// </summary>
        public string Uri
        {
            get { return WebUtility.UrlDecode(Data); }
            set { Data = DataRestriction.EnforceUrlEncoding(value); }
        }
    }
}