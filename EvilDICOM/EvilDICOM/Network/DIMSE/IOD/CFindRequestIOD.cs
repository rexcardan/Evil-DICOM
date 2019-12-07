using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using System.Linq;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindRequestIOD : AbstractDIMSEIOD
    {
        public CFindRequestIOD(QueryLevel ql = QueryLevel.STUDY)
        {
            QueryLevel = ql;
        }

        public CFindRequestIOD(DICOMObject dcm, QueryLevel ql = QueryLevel.STUDY) :base(dcm)
        {
            QueryLevel = ql;
        }

        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.Query​Retrieve​Level == null)
                    _sel.Query​Retrieve​Level.Data = QueryLevel.STUDY.ToString();
                return (QueryLevel)S.Enum.Parse(typeof(QueryLevel), _sel.Query​Retrieve​Level.Data);
            }
            set { _sel.Forge(DF.Query​Retrieve​Level(value.ToString())); }
        }

        public void CombineQuery(CFindRequestIOD cfRequestIod)
        {
            var ql = Elements.FirstOrDefault(e => e.Tag == TagHelper.QueryRetrieveLevel);
            Elements.Remove(ql);
            cfRequestIod.Elements
                .ForEach(this.Elements.Add);
        }

        public override string ToString()
        {
            return string.Join("|", Elements.Select(e => e.DData).ToArray());
        }
    }
}
