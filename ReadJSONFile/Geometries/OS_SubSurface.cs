using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;
using ReadJSONFile.Geometries;

namespace ReadJSONFile.Geometries
{
    public class OS_SubSurface
    {
        Surface srf = null;

        public OS_SubSurface(
            Model model,
            List<List<double>> vertices,
            string surfaceType = "FixedWindow",
            ConstructionBase constructionBase = null,
            Space space = null)
        {
            srf = null;
        }
    }
}
