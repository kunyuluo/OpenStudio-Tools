using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using OpenStudio;
using OpenStudioModel.Geometries;

namespace OpenStudioModel
{
    public class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"D:\Projects\OpenStudioDev\Face.json");
            //string text2 = File.ReadAllText(@"D:\Projects\OpenStudioDev\2DtextJson.json");

            //Get the model:
            string p = "D:\\Projects\\OpenStudioDev\\newBoxModel.osm";
            string newp = "D:\\Projects\\OpenStudioDev\\newBoxModel_vs.osm";
            OpenStudio.Path path = OpenStudioUtilitiesCore.toPath(p);
            OpenStudio.Path newPath = OpenStudioUtilitiesCore.toPath(newp);

            //Get the model:
            Model model = Model.load(path).get();

            //if(!File.Exists(p))
            //{
            //    File.Create(p);
            //}

            //string[] lines = {
            //    "OS:Version,",
            //    "  {74e7a10a-7451-4019-81c0-e6259362e7cb}, !- Handle",
            //    "  3.0.0;                                  !- Version Identifier" };
            //File.WriteAllLines(p, lines);

            ThermalZoneVector zones = model.getThermalZones();
            List<string> zoneNames = new List<string> { };
            foreach (ThermalZone zone in zones)
            {
                //Console.WriteLine(zone.nameString());
                string name = zone.nameString();
                zoneNames.Add(name);
            }

            SpaceVector spaces = model.getSpaces();
            List<string> spaceNames = new List<string> { };
            foreach (Space space in spaces)
            {
                string name = space.nameString();
                spaceNames.Add(name);
            }

            HBObject info = JsonConvert.DeserializeObject<HBObject>(text);

            OS_Surface wall = new OS_Surface(model, info.geometry.boundary);

            var hwLoop = new PlantLoop(model);
            hwLoop.setName("Hot_Water_Loop");

            Console.WriteLine(spaceNames.Count);
            Console.WriteLine(wall.GetType().ToString());
            Console.WriteLine("Well Done!!!");
            Console.ReadKey();

            model.save(newPath, true);


        }
    }
}
