using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;
using Microsoft.SqlServer.Server;

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
            get { return base.DataContainer.SingleValue; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<string>();
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value);
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
