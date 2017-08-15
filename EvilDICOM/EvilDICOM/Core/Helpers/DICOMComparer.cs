using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Helpers
{
    public class DICOMComparer
    {
        public static List<string> CompareObjects(DICOMObject dcm1, DICOMObject dcm2)
        {
            List<string> results = new List<string>();
            for (int i = 0; i < dcm1.Elements.Count; i++)
            {
                var dcm1El = dcm1.Elements[i];
                var dcm2El = dcm2.Elements.FirstOrDefault(e => e.Tag == dcm1El.Tag);
                if (dcm2El == null)
                {
                    results.Add($"Missing element {dcm1El.Tag}");
                }
                else
                {
                    if (dcm2El.DData_ != null && dcm1El.DData_ != null)
                    {
                        if (dcm2El.DData_.Count == dcm1El.DData_.Count)
                        {
                            for (int k = 0; k < dcm2El.DData_.Count; k++)
                            {
                                var a = ((dynamic)dcm1El).Data_[k];
                                var b = ((dynamic)dcm2El).Data_[k];
                                if (a != b)
                                    results.Add($"Element {dcm1El.Tag} not same data. Item {k} : {a} != {b}");
                            }
                        }
                        else
                        {
                            results.Add($"Element {dcm1El.Tag} not same number of data items (different VM)");
                        }
                    }
                    else
                    {
                        if (dcm2El.DData_ != dcm1El.DData_)
                        {
                            results.Add($"Element {dcm1El.Tag} one data is null, but other is not");
                        }
                    }
                }
            }
            return results;
        }
    }
}
