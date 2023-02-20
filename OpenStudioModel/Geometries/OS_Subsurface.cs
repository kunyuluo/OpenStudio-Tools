using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel.Geometries
{
    class OS_Subsurface
    {
        Surface srf = null;

        public OS_Subsurface(
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
