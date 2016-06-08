using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.RT
{
    /// <summary>
    /// A simple container for dose values as a function of 3D space
    /// </summary>
    public struct DoseValue
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; set; }
        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; set; }
        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        /// <value>The dose.</value>
        public double Dose { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoseValue"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="dose">The dose.</param>
        public DoseValue(double x, double y, double z, double dose)
            : this()
        {
            X = x; Y = y; Z = z; Dose = dose;
        }
    }
}
