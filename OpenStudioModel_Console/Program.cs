using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenStudio;
using OpenStudioModel_Console.Geometries;
using OpenStudioModel_Console.Schedule;
using OpenStudioModel_Console.Schedule.Template;
using OpenStudioModel_Console.Constructions;
using OpenStudioModel_Console.Zones;
using OpenStudioModel_Console.Facilitiy;

namespace OpenStudioModel_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"D:\Projects\OpenStudioDev\Face.json");

            //Get the model:
            string p = "D:\\Projects\\OpenStudioDev\\Model_350.osm";
            string newp = "D:\\Projects\\OpenStudioDev\\newBoxModel_vs.osm";
            OpenStudio.Path path = OpenStudioUtilitiesCore.toPath(p);
            OpenStudio.Path newPath = OpenStudioUtilitiesCore.toPath(newp);

            //Get the model:
            Model model = Model.load(path).get();

            //Get the inpputs:
            HBObject info = JsonConvert.DeserializeObject<HBObject>(text);

            //Building:
            FacilityTool.Building(model, "Test_Tower", 42);

            //Building Story:
            BuildingStory story_1 = FacilityTool.Story(model, "1st_Floor");

            //Schedule Set:
            Office office_sch = new Office(model);

            //Thermal zone and space:
            ThermalZone thmZn = ZoneTool.ThermalZone(model, "Kunyu's Room");

            Space spc = ZoneTool.Space(
                model,
                "Kunyu's Room",
                "Office:OpenOffice",
                thmZn,
                story_1,
                1.8,
                2.5,
                0.05,
                0.005,
                0.0002,
                office_sch.Lighting,
                office_sch.Equipment,
                office_sch.Occupancy,
                office_sch.ActivityLevel,
                office_sch.Infiltration);

            Console.WriteLine("Lighting: " + spc.lightingPowerPerFloorArea());
            Console.WriteLine(spc.buildingStory().get().nameString());
            Console.WriteLine(spc.thermalZone().get().nameString());
            

            //Create an envelope surface:
            Surface wall = GeoTools.MakeSurface(model, info.geometry.boundary, GeoTools.MakeVector3d(info.geometry.plane.n));


            
            


            //Console.WriteLine(wall.outsideBoundaryCondition());
            //Console.WriteLine(wall.GetType().ToString());
            //Console.WriteLine(wall.surfaceType());


            Console.WriteLine("Well Done!!!");
            Console.ReadKey();

            model.save(path, true);
        }
    }
}
