using OpenCvSharp;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.CV.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image
{
    public class Slicer3D
    {
        public static Mat GetZPlane(IMatrix _mat, double z, double xiScale = 1, double yiScale = 1)
        {
            //XDIR=> X, YDIR => Y
            //Scale
            var xScale = _mat.XRes > _mat.YRes ? (_mat.YRes / _mat.XRes) : 1.0;
            var yScale = _mat.YRes > _mat.XRes ? (_mat.XRes / _mat.YRes) : 1.0;

            //Check if in bounds
            if (z >= _mat.Origin.Z && z <= _mat.ZMax)
            {
                var zSteps = (int)((z - _mat.Origin.Z) / _mat.ZRes);
                var lowZ = _mat.Origin.Z + zSteps * _mat.ZRes;
                var highZ = _mat.Origin.Z + (zSteps + 1) * _mat.ZRes;
                var interpZ = (z - _mat.Origin.Z) / _mat.ZRes % 1 != 0;

                if (!interpZ)
                {
                    return _mat.GetZPlaneBySlice(zSteps, xiScale, yiScale)
                            .Resize(new Size(0,0), xScale, yScale);
                }
                else
                {
                    //Otherwise interpolate
                    var zd = interpZ ? (z - lowZ) / (highZ - lowZ) : 0;
                    var lowPlane = _mat.GetZPlaneBySlice(zSteps, xiScale, yiScale);
                    var highPlane = _mat.GetZPlaneBySlice(zSteps + 1, xiScale, yiScale);
                    Mat interpolated = lowPlane.EmptyClone();
                    Cv2.AddWeighted(lowPlane, 1 - zd, highPlane, zd, 0, interpolated);
                    interpolated = interpolated.Resize(new Size(0,0), xScale, yScale);
                    return interpolated;
                }
            }
            else
            {
                //Return empty
                return new Mat(_mat.DimensionY, _mat.DimensionX, _mat.MatType);
            }
        }

        public static Mat GetXPlane(IMatrix _mat, double xPositionMM, double xiScale = 1, double yiScale = 1)
        {
            //XDIR=> Y, YDIR => Z
            //Scale
            var xScale = _mat.ZRes > _mat.YRes ? (_mat.YRes / _mat.ZRes) : 1.0;
            var yScale = _mat.YRes > _mat.ZRes ? (_mat.ZRes / _mat.YRes) : 1.0;

            //Check if in bounds
            if (xPositionMM >= _mat.Origin.X && xPositionMM <= _mat.XMax)
            {
                var ordered = Enumerable.Range(1, _mat.DimensionX).Select(s =>
                {
                    return new { Key = s, Value = _mat.Origin.X + _mat.XRes * s };
                });
                var firstLessThan = ordered.LastOrDefault(o => o.Value <= xPositionMM);
                //If close enough, send this plane
                if (Math.Abs(firstLessThan.Value - xPositionMM) < 0.001)
                {
                    return _mat.GetXPlaneBySlice(firstLessThan.Key - 1, xiScale, yiScale)
                            .Resize(new Size(0,0), xScale, yScale);
                }
                var firstMoreThan = ordered.FirstOrDefault(o => o.Value >= xPositionMM);
                if (Math.Abs(firstMoreThan.Value - xPositionMM) < 0.001)
                {
                    return _mat.GetXPlaneBySlice(firstMoreThan.Key - 1, xiScale, yiScale)
                            .Resize(new Size(0,0), xScale, yScale);
                }

                //Otherwise interpolate
                var zd = (xPositionMM - firstLessThan.Value) / (firstMoreThan.Value - firstLessThan.Value);
                var lowPlane = _mat.GetXPlaneBySlice(firstLessThan.Key - 1, xiScale, yiScale);
                var highPlane = _mat.GetXPlaneBySlice(firstMoreThan.Key - 1, xiScale, yiScale);
                Mat interpolated = lowPlane.EmptyClone();
                Cv2.AddWeighted(lowPlane, zd, highPlane, 1 - zd, 0, interpolated);

                interpolated = interpolated.Resize(new Size(0,0), xScale, yScale);
                return interpolated;
            }
            else
            {
                //Return empty
                return new Mat(_mat.DimensionZ, _mat.DimensionY, _mat.MatType).Resize(new Size(0,0), xScale, yScale);
            }
        }

        public static Mat GetYPlane(IMatrix _mat, double yPositionMM, double xiScale = 1, double yiScale = 1)
        {
            //Scale
            var xScale = _mat.ZRes > _mat.XRes ? (_mat.XRes / _mat.ZRes) : 1.0;
            var yScale = _mat.XRes > _mat.ZRes ? (_mat.ZRes / _mat.XRes) : 1.0;

            //Check if in bounds
            if (yPositionMM >= _mat.Origin.Y && yPositionMM <= _mat.YMax)
            {
                var ordered = Enumerable.Range(1, _mat.DimensionY).Select(s =>
                {
                    return new { Key = s, Value = _mat.Origin.Y + _mat.YRes * s };
                });
                var firstLessThan = ordered.LastOrDefault(o => o.Value <= yPositionMM);
                //If close enough, send this plane
                if (Math.Abs(firstLessThan.Value - yPositionMM) < 0.001)
                {
                    return _mat.GetYPlaneBySlice(firstLessThan.Key - 1, xiScale, yiScale)
                            .Resize(new Size(0,0), xScale, yScale);
                }
                var firstMoreThan = ordered.FirstOrDefault(o => o.Value >= yPositionMM);
                if (Math.Abs(firstMoreThan.Value - yPositionMM) < 0.001)
                {
                    return _mat.GetYPlaneBySlice(firstMoreThan.Key - 1, xiScale, yiScale)
                            .Resize(new Size(0,0), xScale, yScale);
                }

                //Otherwise interpolate
                var zd = (yPositionMM - firstLessThan.Value) / (firstMoreThan.Value - firstLessThan.Value);
                var lowPlane = _mat.GetYPlaneBySlice(firstLessThan.Key - 1, xiScale, yiScale);
                var highPlane = _mat.GetYPlaneBySlice(firstMoreThan.Key - 1, xiScale, yiScale);
                Mat interpolated = lowPlane.EmptyClone();
                Cv2.AddWeighted(lowPlane, zd, highPlane, 1 - zd, 0, interpolated);
                interpolated = interpolated.Resize(new Size(0,0), xScale, yScale);
                return interpolated;
            }
            else
            {
                //Return empty
                return new Mat(_mat.DimensionZ, _mat.DimensionX, _mat.MatType).Resize(new Size(0,0), xScale, yScale);
            }
        }
    }
}
