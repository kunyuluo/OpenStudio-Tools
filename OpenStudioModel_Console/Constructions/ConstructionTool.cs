using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Constructions
{
    static class ConstructionTool
    {
        public static ConstructionBase Opaque(
            Model model,
            string name,
            double thermalResistance,
            string roughness)
        {
            MasslessOpaqueMaterial mat = new MasslessOpaqueMaterial(model, roughness, thermalResistance);
            mat.setName(name);

            MaterialVector mat_vec = new MaterialVector();
            mat_vec.Add(mat.to_Material().get());

            Construction cons = new Construction(model);
            cons.setName(name);
            cons.setLayers(mat_vec);

            return cons.to_ConstructionBase().get();
        }


        public static ConstructionBase SimpleGlazing(
            Model model,
            string name,
            double Ufactor,
            double SHGC,
            double visibleTransmittance)
        {
            SimpleGlazing mat = new SimpleGlazing(model, Ufactor, SHGC);
            mat.setName(name);
            mat.setVisibleTransmittance(visibleTransmittance);

            MaterialVector mat_vec = new MaterialVector();
            mat_vec.Add(mat.to_Material().get());

            Construction cons = new Construction(model);
            cons.setName(name);
            cons.setLayers(mat_vec);

            return cons.to_ConstructionBase().get();
        }
    }
}
