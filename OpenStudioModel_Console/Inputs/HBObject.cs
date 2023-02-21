using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console
{
    public class HBObject
    {
        public string name { get; set; }
        public string identifier { get; set; }
        public Geometry geometry { get; set; }
        public string faceType { get; set; }
        public BoundaryCondition boundaryCond { get; set; }
        public string Type { get; set; }
        public Properties properties { get; set; }

    }

    public class Geometry
    {
        public string Type { get; set; }
        public Plane plane { get; set; }
        //public double[,] boundary { get; set; }
        public List<List<double>> boundary { get; set; }

    }

    public class Plane
    {
        public double[] o { get; set; }
        public double[] n { get; set; }
        public double[] x { get; set; }
        public string Type { get; set; }
    }

    //public class Boundary
    //{
    //    // List<List<double>> boundary { get; set; }
    //    public double[,] boundary { get; set; }
    //}

    public class BoundaryCondition
    {
        public ViewFactor viewFactor { get; set; }
        public Boolean sunExposure { get; set; }
        public Boolean windExposure { get; set; }
        public string Type { get; set; }
    }

    public class Properties
    {
        public Radiance radiance { get; set; }
        public Energy energy { get; set; }
        public string Type { get; set; }
    }

    public class Radiance
    {
        public string Type { get; set; }
    }

    public class Energy
    {
        public string Type { get; set; }
    }

    public class ArrayTest
    {
        public string[] my_texts { get; set; }
    }

    public class Array2DTest
    {
        public double[,] my_texts { get; set; }
    }
}
