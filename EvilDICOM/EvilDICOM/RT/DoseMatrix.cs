#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Selection;

#endregion

namespace EvilDICOM.RT
{
    public class DoseMatrix
    {
        private readonly DICOMSelector _doseObject;

        public DoseMatrix(DICOMObject dcm)
        {
            _doseObject = new DICOMSelector(dcm);
            ValueSizeInBytes = _doseObject.Bits​Stored.Data / 8;
            DoseValues = new List<double>();
            Scaling = _doseObject.Dose​Grid​Scaling.Data;
            using (var stream = _doseObject.ToDICOMObject().PixelStream)
            {
                var binReader = new BinaryReader(stream);
                if (ValueSizeInBytes == 4)
                    while (binReader.BaseStream.Position < binReader.BaseStream.Length)
                        DoseValues.Add(Scaling * binReader.ReadInt32());
                else
                    while (binReader.BaseStream.Position < binReader.BaseStream.Length)
                        DoseValues.Add(Scaling * binReader.ReadUInt16());
            }
        }

        public List<double> DoseValues { get; set; }

        public int ValueSizeInBytes { get; set; }

        public double XRes
        {
            get { return _doseObject.Pixel​Spacing.Data_[0]; }
            set { _doseObject.Pixel​Spacing.Data_[0] = value; }
        }

        public double YRes
        {
            get { return _doseObject.Pixel​Spacing.Data_[1]; }
            set { _doseObject.Pixel​Spacing.Data_[1] = value; }
        }

        public double ZRes
        {
            get
            {
                return _doseObject.Grid​Frame​Offset​Vector.Data_[1] - _doseObject.Grid​Frame​Offset​Vector.Data_[0];
            }
        }

        public double X0
        {
            get { return _doseObject.Image​Position​Patient.Data_[0]; }
            set { _doseObject.Image​Position​Patient.Data_[0] = value; }
        }

        public double Y0
        {
            get { return _doseObject.Image​Position​Patient.Data_[1]; }
            set { _doseObject.Image​Position​Patient.Data_[1] = value; }
        }

        public double Z0
        {
            get { return _doseObject.Image​Position​Patient.Data_[2]; }
            set { _doseObject.Image​Position​Patient.Data_[2] = value; }
        }

        public double XMax
        {
            get { return X0 + XRes * (DimensionX - 1); }
        }

        public double YMax
        {
            get { return Y0 + YRes * (DimensionY - 1); }
        }

        public double ZMax
        {
            get { return Z0 + ZRes * (DimensionZ - 1); }
        }

        public int DimensionX
        {
            get { return _doseObject.Columns.Data; }
            set { _doseObject.Columns.Data = (ushort)value; }
        }

        public int DimensionY
        {
            get { return _doseObject.Rows.Data; }
            set { _doseObject.Rows.Data = (ushort)value; }
        }

        public int DimensionZ
        {
            get { return _doseObject.Number​Of​Frames.Data; }
            set { _doseObject.Number​Of​Frames.Data = value; }
        }

        public DoseValue MaxPointDose
        {
            get
            {
                var max = DoseValues.Max();
                var index = DoseValues.IndexOf(max);
                var (x, y, z) = IndexHelper.IndexToLatticeXYZ(index, DimensionX, DimensionY);
                return new DoseValue(x * XRes + X0, y * YRes + Y0, z * ZRes + Z0, max);
            }
        }

        public double[] DirectionalCosines
        {
            get { return _doseObject.Image​Orientation​Patient.Data_.ToArray(); }
        }

        public double Scaling { get; set; }

        public static DoseMatrix Load(string dcmFile)
        {
            return new DoseMatrix(DICOMFileReader.Read(dcmFile));
        }

        /// <summary>
        /// Scrapes a dose matrix along the line from startXYZ in mm to endXYZ in mm
        /// </summary>
        /// <param name="startXYZmm">the starting position of the line</param>
        /// <param name="endXYZmm">the end position of the line</param>
        /// <param name="resolution_mm">the resolution to interoplate the line dose (default 2 mm)</param>
        /// <returns>a list of dose values at the specified resolution along the line</returns>
        public List<DoseValue> GetLineDose(Vector3 startXYZmm, Vector3 endXYZmm, double resolution_mm = 2)
        {
            var values = new List<DoseValue>();
            var pointer = endXYZmm - startXYZmm;
            var length = startXYZmm.DistanceTo(endXYZmm);
            var numPts = (int)Math.Ceiling(length / resolution_mm);
            for (var i = 0; i <= numPts; i++)
            {
                var pt = startXYZmm + (1 - (double)i / numPts) * pointer;
                if (IsInBounds(pt))
                    values.Add(GetPointDose(pt));
            }
            values.Reverse();
            return values;
        }

        private bool IsInBounds(Vector3 pt)
        {
            return pt.X >= X0 && pt.X <= XMax && pt.Y >= Y0 && pt.Y <= YMax && pt.Z >= X0 && pt.Z < ZMax;
        }

        public DoseValue GetPointDose(double x, double y, double z)
        {
            //From method at http://en.wikipedia.org/wiki/Trilinear_interpolation
            var interpX = (x - X0) / XRes % 1 != 0.0; // Interpolate X?
            var xSteps = (int)((x - X0) / XRes);
            var lowX = X0 + xSteps * XRes;
            var highX = X0 + (xSteps + 1) * XRes;
            var interpY = (y - Y0) / YRes % 1 != 0; // Interpolate Y?
            var ySteps = (int)((y - Y0) / YRes);
            var lowY = Y0 + ySteps * YRes;
            var highY = Y0 + (ySteps + 1) * YRes;
            var interpZ = (z - Z0) / ZRes % 1 != 0; // Interpolate Z?
            var zSteps = (int)((z - Z0) / ZRes);
            var lowZ = Z0 + zSteps * ZRes;
            var highZ = Z0 + (zSteps + 1) * ZRes;

            var xd = interpX ? (x - lowX) / (highX - lowX) : 0;
            var yd = interpY ? (y - lowY) / (highY - lowY) : 0;
            var zd = interpZ ? (z - lowZ) / (highZ - lowZ) : 0;

            var c00 = interpX
                ? GetDiscretePointDose(xSteps, ySteps, zSteps) * (1 - xd) +
                  GetDiscretePointDose(xSteps + 1, ySteps, zSteps) * xd
                : GetDiscretePointDose(xSteps, ySteps, zSteps);
            var c10 = interpY
                ? interpX
                    ? GetDiscretePointDose(xSteps, ySteps + 1, zSteps) * (1 - xd) +
                      GetDiscretePointDose(xSteps + 1, ySteps + 1, zSteps) * xd
                    : GetDiscretePointDose(xSteps, ySteps + 1, zSteps)
                : 0;
            var c01 = interpZ
                ? interpX
                    ? GetDiscretePointDose(xSteps, ySteps, zSteps + 1) * (1 - xd) +
                      GetDiscretePointDose(xSteps + 1, ySteps, zSteps + 1) * xd
                    : GetDiscretePointDose(xSteps, ySteps, zSteps + 1)
                : 0;
            var c11 = interpY && interpZ
                ? interpX
                    ? GetDiscretePointDose(xSteps, ySteps + 1, zSteps + 1) * (1 - xd) +
                      GetDiscretePointDose(xSteps + 1, ySteps + 1, zSteps + 1) * xd
                    : GetDiscretePointDose(xSteps, ySteps + 1, zSteps + 1)
                : 0;

            var c0 = interpY ? c00 * (1 - yd) + c10 * yd : c00;
            var c1 = interpY ? c01 * (1 - yd) + c11 * yd : c01;

            var c = interpZ ? c0 * (1 - zd) + c1 * zd : c0;
            return new DoseValue(x, y, z, c);
        }

        public DoseValue GetPointDose(Vector3 pt)
        {
            return GetPointDose(pt.X, pt.Y, pt.Z);
        }

        private double GetDiscretePointDose(int xSteps, int ySteps, int zSteps)
        {
            int index = IndexHelper.LatticeXYZToIndex(xSteps, ySteps, zSteps, DimensionX, DimensionY);
            var value = DoseValues[index];
            return value;
        }

        public void ConvertRelToAbs(double totalDose)
        {
            DoseValues = DoseValues.Select(d => d * totalDose).ToList();
            var _16b = 1 / Math.Pow(2, 16);
            _doseObject.Dose​Grid​Scaling.Data = _16b;
            _doseObject.Dose​Units.Data = "GY";
            _doseObject.Dose​Type.Data = "PHYSICAL";

            using (var stream = new MemoryStream())
            {
                var binWriter = new BinaryWriter(stream);
                foreach (var d in DoseValues)
                {
                    var integ = (int)(d / _16b);
                    var bytes = BitConverter.GetBytes(integ);
                    binWriter.Write(integ);
                }
                var ows = new OtherWordString(TagHelper.Pixel​Data, stream.ToArray());
                _doseObject.ToDICOMObject().Replace(ows);
            }
        }


        public DICOMObject ToDICOM()
        {
            if (DataRestriction.EnforceRealNonZero(Scaling, "Scaling") &&
                DataRestriction.EnforceRealNonZero(ValueSizeInBytes, "ValueSizeInBytes"))
            {
                _doseObject.Bits​Stored.Data = (ushort)(ValueSizeInBytes * 8);
                _doseObject.Dose​Grid​Scaling.Data = Scaling;
                using (var stream = _doseObject.ToDICOMObject().PixelStream)
                {
                    var bw = new BinaryWriter(stream);

                    if (ValueSizeInBytes == 4)
                        foreach (var dv in DoseValues)
                        {
                            var val = (int)(dv / Scaling);
                            bw.Write(BitConverter.GetBytes(val));
                        }
                    else
                        foreach (var dv in DoseValues)
                        {
                            var val = (ushort)(dv / Scaling);
                            bw.Write(BitConverter.GetBytes(val));
                        }
                }
            }

            return _doseObject.ToDICOMObject();
        }

        public void Save(string path)
        {
            _doseObject.ToDICOMObject().Write(path);
        }
    }
}