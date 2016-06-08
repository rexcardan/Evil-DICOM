﻿using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EvilDICOM.RT
{
    /// <summary>
    /// Class DoseMatrix.
    /// </summary>
    public class DoseMatrix
    {
        /// <summary>
        /// The _dose object
        /// </summary>
        private DICOMSelector _doseObject;
        /// <summary>
        /// Gets or sets the dose values.
        /// </summary>
        /// <value>The dose values.</value>
        public List<double> DoseValues { get; set; }

        /// <summary>
        /// Loads the specified DCM file.
        /// </summary>
        /// <param name="dcmFile">The DCM file.</param>
        /// <returns>DoseMatrix.</returns>
        public static DoseMatrix Load(string dcmFile)
        {
            return new DoseMatrix(DICOMFileReader.Read(dcmFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoseMatrix"/> class.
        /// </summary>
        /// <param name="dcm">The DCM.</param>
        public DoseMatrix(DICOMObject dcm)
        {
            _doseObject = new DICOMSelector(dcm);
            ValueSizeInBytes = _doseObject.BitsStored.Data / 8;
            DoseValues = new List<double>();
            this.Scaling= _doseObject.DoseGridScaling.Data;
            using (var stream = _doseObject.ToDICOMObject().PixelStream)
            {
                var binReader = new BinaryReader(stream);
                if (ValueSizeInBytes == 4)
                {
                    while (binReader.BaseStream.Position < binReader.BaseStream.Length)
                    {
                        DoseValues.Add(Scaling * binReader.ReadInt32());
                    }
                }
                else
                {
                    while (binReader.BaseStream.Position < binReader.BaseStream.Length)
                    {
                        DoseValues.Add(Scaling * binReader.ReadUInt16());
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value size in bytes.
        /// </summary>
        /// <value>The value size in bytes.</value>
        public int ValueSizeInBytes { get; set; }

        /// <summary>
        /// Scrapes a dose matrix along the line from startXYZ in mm to endXYZ in mm
        /// </summary>
        /// <param name="startXYZmm">the starting position of the line</param>
        /// <param name="endXYZmm">the end position of the line</param>
        /// <param name="resolution_mm">the resolution to interoplate the line dose (default 2 mm)</param>
        /// <returns>a list of dose values at the specified resolution along the line</returns>
        public List<DoseValue> GetLineDose(Vector3 startXYZmm, Vector3 endXYZmm, double resolution_mm = 2)
        {
            List<DoseValue> values = new List<DoseValue>();
            var pointer = endXYZmm - startXYZmm;
            var length = startXYZmm.DistanceTo(endXYZmm);
            var numPts = (int)Math.Ceiling(length / resolution_mm);
            for (int i = 0; i <= numPts; i++)
            {
                var pt = startXYZmm + ((1 - ((double)i / numPts)) * pointer);
                if (IsInBounds(pt))
                {
                    values.Add(GetPointDose(pt));
                }
            }
            values.Reverse();
            return values;
        }

        /// <summary>
        /// Determines whether [is in bounds] [the specified pt].
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns><c>true</c> if [is in bounds] [the specified pt]; otherwise, <c>false</c>.</returns>
        private bool IsInBounds(Vector3 pt)
        {
            return (pt.X >= X0 && pt.X <= XMax) && (pt.Y >= Y0 && pt.Y <= YMax) && (pt.Z >= X0 && pt.Z < ZMax);
        }

        /// <summary>
        /// Gets or sets the x resource.
        /// </summary>
        /// <value>The x resource.</value>
        public double XRes { get { return _doseObject.PixelSpacing.Data_[0]; } set { _doseObject.PixelSpacing.Data_[0] = value; } }
        /// <summary>
        /// Gets or sets the y resource.
        /// </summary>
        /// <value>The y resource.</value>
        public double YRes { get { return _doseObject.PixelSpacing.Data_[1]; } set { _doseObject.PixelSpacing.Data_[1] = value; } }
        /// <summary>
        /// Gets the z resource.
        /// </summary>
        /// <value>The z resource.</value>
        public double ZRes { get { return _doseObject.GridFrameOffsetVector.Data_[1] - _doseObject.GridFrameOffsetVector.Data_[0]; } }

        /// <summary>
        /// Gets or sets the x0.
        /// </summary>
        /// <value>The x0.</value>
        public double X0 { get { return _doseObject.ImagePositionPatient.Data_[0]; } set { _doseObject.ImagePositionPatient.Data_[0] = value; } }
        /// <summary>
        /// Gets or sets the y0.
        /// </summary>
        /// <value>The y0.</value>
        public double Y0 { get { return _doseObject.ImagePositionPatient.Data_[1]; } set { _doseObject.ImagePositionPatient.Data_[1] = value; } }
        /// <summary>
        /// Gets or sets the z0.
        /// </summary>
        /// <value>The z0.</value>
        public double Z0 { get { return _doseObject.ImagePositionPatient.Data_[2]; } set { _doseObject.ImagePositionPatient.Data_[2] = value; } }

        /// <summary>
        /// Gets the x maximum.
        /// </summary>
        /// <value>The x maximum.</value>
        public double XMax { get { return X0 + XRes * (DimensionX - 1); } }
        /// <summary>
        /// Gets the y maximum.
        /// </summary>
        /// <value>The y maximum.</value>
        public double YMax { get { return Y0 + YRes * (DimensionY - 1); } }
        /// <summary>
        /// Gets the z maximum.
        /// </summary>
        /// <value>The z maximum.</value>
        public double ZMax { get { return Z0 + ZRes * (DimensionZ - 1); } }

        /// <summary>
        /// Gets or sets the dimension x.
        /// </summary>
        /// <value>The dimension x.</value>
        public int DimensionX { get { return _doseObject.Columns.Data; } set { _doseObject.Columns.Data = (ushort)value; } }
        /// <summary>
        /// Gets or sets the dimension y.
        /// </summary>
        /// <value>The dimension y.</value>
        public int DimensionY { get { return _doseObject.Rows.Data; } set { _doseObject.Rows.Data = (ushort)value; } }
        /// <summary>
        /// Gets or sets the dimension z.
        /// </summary>
        /// <value>The dimension z.</value>
        public int DimensionZ { get { return _doseObject.NumberOfFrames.Data; } set { _doseObject.NumberOfFrames.Data = value; } }

        /// <summary>
        /// Gets the point dose.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>DoseValue.</returns>
        public DoseValue GetPointDose(double x, double y, double z)
        {
            //From method at http://en.wikipedia.org/wiki/Trilinear_interpolation
            var interpX = ((x - X0) / XRes) % 1 != 0.0; // Interpolate X?
            var xSteps = (int)((x - X0) / XRes);
            var lowX = X0 + (xSteps * XRes);
            var highX = X0 + (xSteps + 1) * XRes;
            var interpY = ((y - Y0) / YRes) % 1 != 0; // Interpolate Y?
            var ySteps = (int)((y - Y0) / YRes);
            var lowY = Y0 + (ySteps * YRes);
            var highY = Y0 + (ySteps + 1) * YRes;
            var interpZ = ((z - Z0) / ZRes) % 1 != 0; // Interpolate Z?
            var zSteps = (int)((z - Z0) / ZRes);
            var lowZ = Z0 + (zSteps * ZRes);
            var highZ = Z0 + (zSteps + 1) * ZRes;

            var xd = interpX ? (x - lowX) / (highX - lowX) : 0;
            var yd = interpY ? (y - lowY) / (highY - lowY) : 0;
            var zd = interpZ ? (z - lowZ) / (highZ - lowZ) : 0;

            var c00 = interpX ? GetDiscretePointDose(xSteps, ySteps, zSteps) * (1 - xd) + GetDiscretePointDose(xSteps + 1, ySteps, zSteps) * xd : GetDiscretePointDose(xSteps, ySteps, zSteps);
            var c10 = interpY ? interpX ? GetDiscretePointDose(xSteps, ySteps + 1, zSteps) * (1 - xd) + GetDiscretePointDose(xSteps + 1, ySteps + 1, zSteps) * xd : GetDiscretePointDose(xSteps, ySteps + 1, zSteps) : 0;
            var c01 = interpZ ? interpX ? GetDiscretePointDose(xSteps, ySteps, zSteps + 1) * (1 - xd) + GetDiscretePointDose(xSteps + 1, ySteps, zSteps + 1) * xd : GetDiscretePointDose(xSteps, ySteps, zSteps + 1) : 0;
            var c11 = interpY && interpZ ? interpX ? GetDiscretePointDose(xSteps, ySteps + 1, zSteps + 1) * (1 - xd) + GetDiscretePointDose(xSteps + 1, ySteps + 1, zSteps + 1) * xd : GetDiscretePointDose(xSteps, ySteps + 1, zSteps + 1) : 0;

            var c0 = interpY ? c00 * (1 - yd) + c10 * yd : c00;
            var c1 = interpY ? c01 * (1 - yd) + c11 * yd : c01;

            var c = interpZ ? c0 * (1 - zd) + c1 * zd : c0;
            return new DoseValue(x, y, z, c);

        }
        /// <summary>
        /// Gets the point dose.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns>DoseValue.</returns>
        public DoseValue GetPointDose(Vector3 pt)
        {
            return GetPointDose(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Gets the discrete point dose.
        /// </summary>
        /// <param name="xSteps">The x steps.</param>
        /// <param name="ySteps">The y steps.</param>
        /// <param name="zSteps">The z steps.</param>
        /// <returns>System.Double.</returns>
        private double GetDiscretePointDose(int xSteps, int ySteps, int zSteps)
        {
            int index;
            LatticeXYZToIndex(xSteps, ySteps, zSteps, out index);
            var value = DoseValues[index];
            return value;
        }

        /// <summary>
        /// Gets the maximum point dose.
        /// </summary>
        /// <value>The maximum point dose.</value>
        public DoseValue MaxPointDose
        {
            get
            {
                var max = DoseValues.Max();
                var index = DoseValues.IndexOf(max);
                int x; int y; int z;
                IndexToLatticeXYZ(index, out x, out y, out z);
                return new DoseValue(x * XRes + X0, y * YRes + Y0, z * ZRes + Z0, max);
            }
        }

        /// <summary>
        /// Indexes to lattice xyz.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public void IndexToLatticeXYZ(int index, out int x, out int y, out int z)
        {
            z = index / (DimensionX * DimensionY);
            y = (index % (DimensionX * DimensionY)) / (DimensionX);
            x = (index % (DimensionX * DimensionY)) % (DimensionX);
        }

        /// <summary>
        /// Lattices the index of the xyz to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="latticeWidth">Width of the lattice.</param>
        /// <param name="latticeHeight">Height of the lattice.</param>
        /// <param name="index">The index.</param>
        public static void LatticeXYZToIndex(int x, int y, int z, int latticeWidth, int latticeHeight, out int index)
        {
            index = x + (y * latticeWidth) + (z * (latticeWidth * latticeHeight));
        }

        /// <summary>
        /// Lattices the index of the xyz to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="index">The index.</param>
        public void LatticeXYZToIndex(int x, int y, int z, out int index)
        {
            LatticeXYZToIndex(x, y, z, DimensionX, DimensionY, out index);
        }

        /// <summary>
        /// Gets the directional cosines.
        /// </summary>
        /// <value>The directional cosines.</value>
        public double[] DirectionalCosines
        {
            get { return _doseObject.ImageOrientationPatient.Data_.ToArray(); }
        }

        /// <summary>
        /// Converts the relative to abs.
        /// </summary>
        /// <param name="totalDose">The total dose.</param>
        public void ConvertRelToAbs(double totalDose)
        {
            DoseValues = DoseValues.Select(d => d * totalDose).ToList();
            var _16b = 1 / Math.Pow(2, 16);
            _doseObject.DoseGridScaling.Data = _16b;
            _doseObject.DoseUnits.Data = "GY";
            _doseObject.DoseType.Data = "PHYSICAL";

            using (var stream = new MemoryStream())
            {
                var binWriter = new BinaryWriter(stream);
                foreach (var d in DoseValues)
                {
                    int integ = (int)(d / _16b);
                    var bytes = BitConverter.GetBytes(integ);
                    binWriter.Write(integ);
                }
                var ows = new OtherWordString(TagHelper.PIXEL_DATA, stream.ToArray());
                _doseObject.ToDICOMObject().Replace(ows);
            }
        }


        /// <summary>
        /// To the dicom.
        /// </summary>
        /// <returns>DICOMObject.</returns>
        public DICOMObject ToDICOM()
        {
            if (DataRestriction.EnforceRealNonZero(Scaling, "Scaling") && DataRestriction.EnforceRealNonZero(ValueSizeInBytes, "ValueSizeInBytes"))
            {
                _doseObject.BitsStored.Data = (ushort)(ValueSizeInBytes * 8);
                _doseObject.DoseGridScaling.Data = this.Scaling;
                using (var stream = _doseObject.ToDICOMObject().PixelStream)
                {
                    var bw = new BinaryWriter(stream);

                    if (ValueSizeInBytes == 4)
                    {
                        foreach (var dv in DoseValues)
                        {
                            var val = (int)(dv/this.Scaling);
                            bw.Write(BitConverter.GetBytes(val));
                        }
                    }
                    else
                    {
                        foreach (var dv in DoseValues)
                        {
                            var val = (ushort)(dv / this.Scaling);
                            bw.Write(BitConverter.GetBytes(val));
                        }
                    }
                }
            }
          
            return _doseObject.ToDICOMObject();
        }

        /// <summary>
        /// Saves the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Save(string path)
        {
            _doseObject.ToDICOMObject().Write(path);
        }

        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        /// <value>The scaling.</value>
        public double Scaling { get; set; }
    }
}
