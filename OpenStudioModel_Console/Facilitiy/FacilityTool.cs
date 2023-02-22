using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Facilitiy
{
    static class FacilityTool
    {
        public static void Building(Model model, string name = "Building 1", double northAxis = 0)
        {
            Building bldg = model.getBuilding();
            
            bldg.setName(name);
            bldg.setNorthAxis(northAxis);

        }

        public static BuildingStory Story(Model model, string name)
        {
            BuildingStory story = new BuildingStory(model);
            story.setName(name);

            return story;
        }
    }
}
