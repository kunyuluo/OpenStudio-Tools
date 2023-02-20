using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel.Geometries
{
    class OS_Surface
    {
        //Output Variable:
        private Surface srf { get; }

        //Constructor:
        public OS_Surface(
            Model model,
            List<List<double>> vertices,
            string surfaceType = "Wall",
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

                srf = new Surface(pt_vec, model);

                srf.setSurfaceType(surfaceType);
                srf.setOutsideBoundaryCondition(ousideBoundaryCondition);
                srf.setSunExposure(sunExposure);
                srf.setWindExposure(windExposure);
                //srf.setConstruction(constructionBase);
                //srf.setSpace(space);


            }
        }
    }
}
