using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Geometries
{
    class OS_Surface
    {
        //Output Variable:
        public Surface Surface { get; }
        private static double roofThreshold = Math.PI / 4;
        public double angle { get;}

        //Constructor:
        public OS_Surface(
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
                if(normal == null)
                {
                    normal = new Vector3d(1.0, 0.0, 0.0);
                }
                Vector3d zaxis = new Vector3d(0, 0, 1);
                //Vector3d normalVec = new Vector3d(normal[0], normal[1], normal[2]);
                double angle_rad = Math.Acos(zaxis.dot(normal) / (zaxis.length() * normal.length()));
                angle = angle_rad * 180 / Math.PI;

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
                if(constructionBase != null) { Surface.setConstruction(constructionBase); }
                if(space != null) { Surface.setSpace(space); }


            }
        }
    }
}
