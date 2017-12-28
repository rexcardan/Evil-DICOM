using System;
using System.Collections.Generic;
using System.Linq;

namespace EvilDICOM.Core.Helpers
{
    public class DICOMComparer
    {
        public static List<string> CompareObjects(DICOMObject dcm1, DICOMObject dcm2, string priorSequencePath = "")
        {
            List<string> results = new List<string>();
            for (int i = 0; i < dcm1.Elements.Count; i++)
            {
                var dcm1El = dcm1.Elements[i];
                var dcm2El = dcm2.Elements.FirstOrDefault(e => e.Tag == dcm1El.Tag);
                if (dcm2El == null)
                {
                    results.Add($"Missing element {priorSequencePath}{dcm1El.Tag}");
                }
                else
                {
                    if (dcm2El.DData_ != null && dcm1El.DData_ != null)
                    {
                        int count = dcm2El.DData_.Count;
                        if (dcm2El.DData_.Count != dcm1El.DData_.Count)
                        {
                            count = Math.Min(dcm2El.DData_.Count, dcm1El.DData_.Count);
                            results.Add($"{priorSequencePath}{dcm1El.Tag} not same number of data items (different VM)");
                            results.Add($"Reading smallest agreed = {count}");
                        }
                        for (int k = 0; k < count; k++)
                        {
                            var a = ((dynamic)dcm1El).Data_[k];
                            var b = ((dynamic)dcm2El).Data_[k];

                            if (a is DICOMObject)
                            {
                                results.AddRange(CompareObjects(a, b, $"{priorSequencePath}{dcm1El.Tag}>>"));
                            }
                            else if (a is string && b is string)
                            {
                                //Trim strings - don't let padding throw an error
                                a = ((string)a).Trim();
                                b = ((string)b).Trim();

                                if (a != b)
                                    results.Add($"{priorSequencePath}{dcm1El.Tag} not same data. Item {k} : {a} != {b}");
                            }
                            else if (a is DateTime && b is DateTime)
                            {
                                if (dcm2El is EvilDICOM.Core.Element.Date)
                                {
                                    //Compare dates
                                    if (((DateTime)a).Date != ((DateTime)b).Date)
                                        results.Add($"{priorSequencePath}{dcm1El.Tag} not same data. Item {k} : {((DateTime)a).Date} != {((DateTime)b).Date}");
                                }
                                else if (dcm2El is EvilDICOM.Core.Element.Time)
                                {
                                    //Compare times
                                    //Compare dates
                                    if (((DateTime)a).TimeOfDay != ((DateTime)b).TimeOfDay)
                                        results.Add($"{priorSequencePath}{dcm1El.Tag} not same data. Item {k} : {((DateTime)a).TimeOfDay} != {((DateTime)b).TimeOfDay}");
                                }
                            }
                            else if ((a == null && b != null) || (b == null && a != null))
                            {
                                results.Add($"{priorSequencePath}{dcm1El.Tag} not same data. Item {k} : {a ?? "Null"} != {b ?? "Null"}");
                            }
                            else
                            {
                                if (a.GetType() == b.GetType())
                                {
                                    if (a != b)
                                        results.Add($"{priorSequencePath}{dcm1El.Tag} not same data. Item {k} : {a} != {b}");
                                }
                                else
                                {
                                    results.Add($"Unable to compare : {priorSequencePath}{dcm1El.Tag} => contains different types");
                                }
                            }
                        }

                    }
                    else
                    {
                        if (dcm2El.DData_ != dcm1El.DData_)
                        {
                            results.Add($"Element {priorSequencePath}{dcm1El.Tag} one data is null, but other is not");
                        }
                    }
                }
            }
            return results;
        }
    }
}
