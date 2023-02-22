using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Geometries
{
    static class GeoTools
    {
        public static double roofThreshold { get;}
        //public static double angle { get; set; }
        public static Vector3d MakeVector3d(double[] vector)
        {
            Vector3d Vector3d = null;
            if (vector.Length != 3)
            {
                throw new ArgumentNullException("vector", "The length of array must be three");
            }
            else if(vector.Length == 0)
            {
                Vector3d = new Vector3d(1.0, 0.0, 0.0);
            }
            else
            {
                Vector3d = new Vector3d(vector[0], vector[1], vector[2]);
            }
            return Vector3d;


        }


        public static Surface MakeSurface(
            Model model,
            List<List<double>> vertices,
            Vector3d normal = null,
            string surfaceType = null,
            string ousideBoundaryCondition = "Surface",
            string sunExposure = "Sunexposed",
            string windExposure = "Windexposed",
            ConstructionBase constructionBase = null,
            Space space = null)
        {
            Surface Surface = null;

            if (vertices.Count != 0 && vertices.Count > 2)
            {
                Point3dVector pt_vec = new Point3dVector();
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].Count != 0)
                    {
                        Point3d pt = new Point3d(vertices[i][0], vertices[i][1], vertices[i][2]);
                        pt_vec.Add(pt);
                    }
                }

                Surface = new Surface(pt_vec, model);

                //Calculate the angle between normal vector and z-axis (by Dot Product):
                if (normal == null)
                {
                    normal = new Vector3d(1.0, 0.0, 0.0);
                }
                Vector3d zaxis = new Vector3d(0, 0, 1);
                //Vector3d normalVec = new Vector3d(normal[0], normal[1], normal[2]);
                double angle_rad = Math.Acos(zaxis.dot(normal) / (zaxis.length() * normal.length()));
                //angle = angle_rad * 180 / Math.PI;

                //Assign surface type based on their orientation:
                if (surfaceType == null)
                {
                    if (angle_rad < roofThreshold)
                    {
                        Surface.setSurfaceType("RoofCeiling");
                    }
                    else if (angle_rad >= roofThreshold && angle_rad < Math.PI)
                    {
                        Surface.setSurfaceType("Wall");
                    }
                    else
                    {
                        Surface.setSurfaceType("Floor");
                    }
                }

                Surface.setOutsideBoundaryCondition(ousideBoundaryCondition);
                Surface.setSunExposure(sunExposure);
                Surface.setWindExposure(windExposure);
                if (constructionBase != null) { Surface.setConstruction(constructionBase); }
                if (space != null) { Surface.setSpace(space); }


            }
            return Surface;
        }


        public static SubSurface MakeFenestration(
            Model model,
            List<List<double>> vertices,
            string surfaceType = "FixedWindow",
            ConstructionBase constructionBase = null,
            Surface surface = null)
        {
            SubSurface Surface = null;

            if (vertices.Count != 0 && vertices.Count > 2)
            {
                Point3dVector pt_vec = new Point3dVector();
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].Count != 0)
                    {
                        Point3d pt = new Point3d(vertices[i][0], vertices[i][1], vertices[i][2]);
                        pt_vec.Add(pt);
                    }
                }

                Surface = new SubSurface(pt_vec, model);

                //Alternatives include:
                //FixedWindow            OverheadDoor
                //OperableWindow         Skylight
                //Door                   TubularDaylightDome
                //GlassDoor              TubularDaylightDiffuser
                Surface.setSubSurfaceType("FixedWindow");
                if (constructionBase != null) { Surface.setConstruction(constructionBase); }
                if (surface != null) { Surface.setSurface(surface); }


            }
            return Surface;
        }
    }
}
