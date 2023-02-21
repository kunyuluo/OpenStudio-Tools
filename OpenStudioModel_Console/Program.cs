using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenStudio;
using OpenStudioModel_Console.Geometries;

namespace OpenStudioModel_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"D:\Projects\OpenStudioDev\Face.json");

            //Get the model:
            string p = "D:\\Projects\\OpenStudioDev\\newBoxModel3.5.osm";
            string newp = "D:\\Projects\\OpenStudioDev\\newBoxModel_vs.osm";
            OpenStudio.Path path = OpenStudioUtilitiesCore.toPath(p);
            OpenStudio.Path newPath = OpenStudioUtilitiesCore.toPath(newp);

            //Get the model:
            Model model = Model.load(path).get();

            //Get the inpputs:
            HBObject info = JsonConvert.DeserializeObject<HBObject>(text);

            ThermalZoneVector zones = model.getThermalZones();
            List<string> zoneNames = new List<string> { };
            foreach (ThermalZone zone in zones)
            {
                Console.WriteLine(zone.nameString());
                string name = zone.nameString();
                zoneNames.Add(name);
            }

            //Create an envelope surface:
            Surface wall = GeoTools.MakeSurface(model, info.geometry.boundary, GeoTools.MakeVector3d(info.geometry.plane.n));

            Console.WriteLine(wall.outsideBoundaryCondition());

            Console.WriteLine(wall.GetType().ToString());
            Console.WriteLine(wall.surfaceType());


            Console.WriteLine("Well Done!!!");
            Console.ReadKey();

            //model.save(newPath, true);
        }
    }
}
