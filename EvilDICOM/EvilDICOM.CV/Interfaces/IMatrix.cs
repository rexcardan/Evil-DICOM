using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Interfaces
{
    public interface IMatrix
    {
        Vector3 Origin { get;  }
        double XRes { get;  }
        double YRes { get;  }
        double ZRes { get;  }
        int DimensionX { get;  }
        int DimensionY { get;  }
        int DimensionZ { get; }
        double XMax { get;  }
        double YMax { get;  }
        double ZMax { get;  }

        Mat GetZPlaneBySlice(int z, double xScale = 1, double yScale = 1);
        Mat GetXPlaneBySlice(int x, double xScale = 1, double yScale = 1);
        Mat GetYPlaneBySlice(int y, double xScale = 1, double yScale = 1);

        MatType MatType { get;}
    }
}
