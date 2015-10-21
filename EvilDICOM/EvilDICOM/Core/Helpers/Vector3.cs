using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Helpers
{
    /// <summary>
    /// This is a temporary class until .NET 4.6 is released with Vector3 support. Borrowed from my Cardan.Math library
    /// </summary>
    public class Vector3
    {
        private double[] values;

        #region CONSTRUCTORS

        public Vector3(double x = 0, double y = 0, double z = 0)
        {
            values = new[] { x, y, z };
        }

        public Vector3(double[] values)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("Must be three dimensions!");
            }
            this.values = values;
        }

        #endregion

        #region ACCESSORS

        /// <summary>
        ///     Allows the vector class elements to be accessed by index
        /// </summary>
        /// <param name="index">the index of the element to return</param>
        /// <returns>the element at the specified index</returns>
        public double this[long index]
        {
            get
            {
                if (index < values.Length)
                    return values[index];
                throw new IndexOutOfRangeException("Vector 3 array only has length of 3!");
            }
            set
            {
                if (index < values.Length)
                    values[index] = value;
                else
                {
                    throw new IndexOutOfRangeException("Vector 3 array only has length of 3!");
                }
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        ///     A static zero value vector creator. Returns a vector3 containing only zero value elements.
        /// </summary>
        public static Vector3 Zeroes
        {
            get { return new Vector3(0, 0, 0); }
        }

        /// <summary>
        ///     A static infinite value vector creator. Returns a vector3 containing positive infinite value elements.
        /// </summary>
        public static Vector3 Infinite
        {
            get { return new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity); }
        }

        /// <summary>
        ///     A static infinite value vector creator. Returns a vector3 containing NaN value elements.
        /// </summary>
        public static Vector3 NaN
        {
            get { return new Vector3(double.NaN, double.NaN, double.NaN); }
        }

        public double X
        {
            get { return this[0]; }
            set { this[0] = value; }
        }

        public double Y
        {
            get { return this[1]; }
            set { this[1] = value; }
        }

        public double Z
        {
            get { return this[2]; }
            set { this[2] = value; }
        }

        public int Length
        {
            get { return 3; }
        }

        public double[] Values
        {
            get { return values; }
            set
            {
                if (value.Length != Length)
                {
                    throw new ArgumentException("Must be three dimensions!");
                }
                values = value;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        ///     Creates a copy of this vector
        /// </summary>
        /// <returns></returns>
        public Vector3 Copy()
        {
            return new Vector3(this[0], this[1], this[2]);
        }

        /// <summary>
        ///     Finds the magnitude of this vector
        /// </summary>
        /// <returns>a double representing the magnitude of the vector</returns>
        public double Norm()
        {
            return System.Math.Sqrt(this * this);
        }

        public float DistanceTo(Vector3 v)
        {
            return
                (float)
                    System.Math.Sqrt(System.Math.Pow((v[0] - this[0]), 2) + System.Math.Pow((v[1] - this[1]), 2) +
                                     System.Math.Pow((v[2] - this[2]), 2));
        }

        public double[] ToArray()
        {
            return values;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", values[0], values[1], values[2]);
        }

        /// <summary>
        ///     Computes the cross product of this vector and another input vector
        /// </summary>
        /// <param name="v">the input vector</param>
        /// <returns>a new vector that is the cross product of the two vectors</returns>
        public Vector3 CrossMultiply(Vector3 v)
        {
            return new Vector3(this[1] * v[2] - this[2] * v[1], this[2] * v[0] - this[0] * v[2], this[0] * v[1] - this[1] * v[0]);
        }

        #endregion

        #region OPERATORS

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1[0] + v2[0], v1[1] + v2[1], v1[2] + v2[2]);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1[0] - v2[0], v1[1] - v2[1], v1[2] - v2[2]);
        }

        public static Vector3 operator *(Vector3 v1, double s)
        {
            return new Vector3(v1[0] * s, v1[1] * s, v1[2] * s);
        }

        public static Vector3 operator *(double s, Vector3 v1)
        {
            return new Vector3(v1[0] * s, v1[1] * s, v1[2] * s);
        }

        /// <summary>
        ///     Computes the scalar (dot) product of two vectors
        /// </summary>
        /// <param name="v1">the first vector</param>
        /// <param name="v2">the second vector</param>
        /// <returns>The scalar product of two vectors</returns>
        public static double operator *(Vector3 v1, Vector3 v2)
        {
            return v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];
        }

        public static Vector3 operator /(Vector3 v1, double s)
        {
            return new Vector3(v1[0] / s, v1[1] / s, v1[2] / s);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !v1.Equals(v2);
        }

        #endregion

        #region COMPARATORS

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                var v = (Vector3)obj;
                return ReferenceEquals(v, null) ? false : ((v.X == X) && (v.Y == Y) && (v.Z == Z));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        #endregion
    }
}
