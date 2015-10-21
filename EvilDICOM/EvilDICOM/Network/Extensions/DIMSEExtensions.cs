using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Network.Extensions
{
    public static class DIMSEExtensions
    {
        public static void LogData(this AbstractDIMSEBase dimse, Association asc)
        {
            if (dimse is AbstractDIMSE)
            {
                var abd = dimse as AbstractDIMSE;
                if (abd.HasData)
                {
                    foreach (var el in abd.Data.Elements)
                    {
                        asc.Logger.Log(el);
                    }
                    asc.Logger.Log("");//Space
                }
            }
        }

        public static void WithSeries(this IEnumerable<CFindResponse> cFinds, DICOMSCU scu, Entity daemon)
        {
            var iods = cFinds.Where(r => r.Status == (ushort)Status.PENDING)
            .Where(r => r.HasData)
            .Where(r => r.Data.Elements.Any(e => e.Tag == TagHelper.STUDY_INSTANCE_UID))
            .Where(r => !string.IsNullOrEmpty(r.Data.Elements.First(e => e.Tag == TagHelper.STUDY_INSTANCE_UID).DData as string))
            .Select(r => r.GetIOD())
            .ToList();
            
            iods.ForEach(i=>i.QueryLevel = QueryLevel.SERIES);

            foreach (var iod in iods)
            {
                var req = new CFindRequest(iod, Root.STUDY);
                var seriesUids = scu.GetResponse(req, daemon)
                     .Where(r => r.Status == (ushort)Status.PENDING)
                     .Where(r => r.HasData)
                     .Where(r => r.Data.Elements.Any(e => e.Tag == TagHelper.SERIES_INSTANCE_UID))
                     .Select(r => new
                     {
                        // Study = study,
                         Series = r.Data.GetSelector().SeriesInstanceUID.Data,
                         Modality = r.Data.GetSelector().Modality.Data
                     })
                     .ToList();
                System.Console.Write("");
            }
        }
    }
}
