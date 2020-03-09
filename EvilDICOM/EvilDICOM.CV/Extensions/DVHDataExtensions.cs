using EvilDICOM.RT;
using EvilDICOM.RT.Data.DVH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvilDICOM.CV.Helpers.MathHelper;

namespace EvilDICOM.CV.Extensions
{
    public static class DVHDataExtensions
    {
        /// <summary>
        ///     Gets the volume that recieves the input dose
        /// </summary>
        /// <param name="dvh">the dose volume histogram for this structure</param>
        /// <param name="dv">the dose value to sample the curve</param>
        /// <returns>the volume in the same units as the DVH point array</returns>
        public static double GetVolumeAtDose(this DVHPoint[] dvh, double doseVal)
        {
            var curve = dvh.Select(d => new { Dose = d.Dose.Value, Volume = d.Volume.Value, d.Volume.Unit });
            var maxDose = curve.Max(d => d.Dose);
            var minDose = curve.Min(d => d.Dose);

            //If the max dose is less than the queried dose, then there is no volume at the queried dose (out of range)
            //If the min dose is greater than the queried dose, then 100% of the volume is at the queried dose
            if (doseVal >= maxDose) return 0;
            if (doseVal < minDose) { return dvh.Max(d => d.Volume.Value); }

            var higherPoint = curve.First(p => p.Dose > doseVal);
            var lowerPoint = curve.Last(p => p.Dose <= doseVal);
            var volumeAtPoint = Interpolate(higherPoint.Dose, lowerPoint.Dose, higherPoint.Volume, lowerPoint.Volume,
                doseVal);
            return volumeAtPoint;
        }

        /// <summary>
        ///     Gets the Complement volume (volume about a certain dose point) for the structure dvh
        /// </summary>
        /// <param name="dvh">the dose volume histogram for this structure</param>
        /// <param name="dv">the dose value to sample the curve</param>
        /// <returns></returns>
        public static double GetComplementVolumeAtDose(this DVHPoint[] dvh, double doseVal)
        {
            var maxVol = dvh.Max(d => d.Volume).Value;
            var normalVolume = dvh.GetVolumeAtDose(doseVal);
            return maxVol - normalVolume;
        }

        /// <summary>
        ///     Gets the dose value at the specified volume for the curve
        /// </summary>
        /// <param name="dvh">the dvhPoint array that is queried</param>
        /// <param name="volume">the volume in the same units as the DVH curve</param>
        /// <returns></returns>
        public static Dose GetDoseAtVolume(this DVHPoint[] dvh, double volume)
        {
            var minVol = dvh.Min(d => d.Volume.Value);
            var maxVol = dvh.Max(d => d.Volume.Value);

            //Check for max point dose scenario
            if (volume <= minVol) return dvh.MaxDose();

            //Check dose to total volume scenario (min dose)
            if (volume == maxVol)
                return dvh.MinDose();

            //Overvolume scenario, undefined
            if (volume > maxVol)
                return new Dose(double.NaN, "?");

            //Convert to list so we can grab indices
            var dvhList = dvh.ToList();

            //Find the closest point to the requested volume,
            //If its really close, let's use it instead of interpolating
            var minVolumeDiff = dvhList.Min(d => Math.Abs(d.Volume.Value - volume));
            var closestPoint = dvhList.First(d => Math.Abs(d.Volume.Value - volume) == minVolumeDiff);
            if (minVolumeDiff < 0.001) return closestPoint.Dose;

            //Interpolate
            var index1 = dvhList.IndexOf(closestPoint);
            var index2 = closestPoint.Volume.Value < volume ? index1 - 1 : index1 + 1;

            if (index1 >= 0 && index2 < dvh.Count())
            {
                var point1 = dvhList[index1];
                var point2 = dvhList[index2];
                var doseAtPoint = Interpolate(point1.Volume.Value, point2.Volume.Value, point1.Dose.Value,
                    point2.Dose.Value, volume);
                return new Dose(doseAtPoint, point1.Dose.Unit);
            }
            return new Dose(double.NaN, closestPoint.Dose.Unit);
            throw new Exception(string.Format(
                "Interpolation failed. Index was : {0}, DVH Point Count : {1}, Volume was {2}, ClosestVol was {3}",
                index1, dvh.Count(), volume, minVolumeDiff));
        }

        /// <summary>
        ///     Gets the Complement dose for the specified volume (the cold spot). Calculated by taking the total volume and
        ///     subtracting the input volume.
        /// </summary>
        /// <param name="dvh">the dvhPoint array that is queried</param>
        /// <param name="volume">the volume in the same units as the DVH curve</param>
        /// <returns>the cold spot dose at the specified volume</returns>
        public static Dose GetDoseComplement(this DVHPoint[] dvh, double volume)
        {
            var maxVol = dvh.Max(d => d.Volume.Value);
            var volOfInterest = maxVol - volume;
            return GetDoseAtVolume(dvh, volOfInterest);
        }



        /// <summary>
        ///     Returns the max dose from the dvh curve
        /// </summary>
        /// <param name="dvh">the dvh curve</param>
        /// <returns>the max dose in the same units as the curve</returns>
        public static Dose MaxDose(this DVHPoint[] dvh)
        {
            if (dvh.Any())
            {
                var unit = dvh.First().Dose.Unit;
                var maxVal = dvh.Max(d => d.Dose.Value);
                return new Dose(maxVal, unit);
            }
            return new Dose(double.NaN, "?");
        }

        /// <summary>
        ///     Returns the min dose from the dvh curve
        /// </summary>
        /// <param name="dvh">the dvh curve</param>
        /// <returns>the minimum dose in the same units as the curve</returns>
        public static Dose MinDose(this DVHPoint[] dvh)
        {
            if (dvh.Any())
            {
                var unit = dvh.First().Dose.Unit;
                var minVal = dvh.Min(d => d.Dose.Value);
                return new Dose(minVal, unit);
            }
            return new Dose(double.NaN, "?");
        }

        /// <summary>
        ///     Returns the mean dose from the dvh curve
        /// </summary>
        /// <param name="dvh">the dvh curve</param>
        /// <returns>the mean dose in the same units as the curve</returns>
        public static Dose MeanDose(this DVHPoint[] dvh)
        {
            if (dvh.Any())
            {
                var unit = dvh.First().Dose.Unit;
                dvh = dvh.Differential();
                var sum = dvh.Sum(d => d.Volume.Value * d.Dose.Value);
                var totalVolume = dvh.Sum(d => d.Volume.Value);
                var meanVal = sum / totalVolume;
                return new Dose(meanVal, unit);
            }
            return new Dose(double.NaN, "?");
        }

        /// <summary>
        /// Converts cumulative to differntial
        /// </summary>
        /// <param name="dvh">the dvh curve</param>
        /// <returns>the differential dvh</returns>
        public static DVHPoint[] Differential(this DVHPoint[] dvh)
        {
            var maxDose = dvh.MaxDose();
            var volumeUnit = dvh.First().Volume.Unit;
            var minDose = dvh.MinDose();

            List<DVHPoint> differential = new List<DVHPoint>();

            for (int i = 0; i < dvh.Length - 1; i++)
            {
                var pt1 = dvh[i];
                var pt2 = dvh[i + 1];
                var vol = pt1.Volume.Value - pt2.Volume.Value;
                var dose = pt2.Dose.Value - pt1.Dose.Value;
                var ddvh = vol / dose;
                differential.Add(new DVHPoint(pt1.Dose, new Volume(ddvh, volumeUnit)));
            }
            //Normalize
            var max = differential.Max(d => d.Volume.Value);
            for (int i = 0; i < differential.Count; i++)
            {
                var dv = differential[i];
                differential[i] = new DVHPoint(dv.Dose, new Volume(dv.Volume.Value / max, dv.Volume.Unit));
            }

            return differential.ToArray();
        }
    }
}

