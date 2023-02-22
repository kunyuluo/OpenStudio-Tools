using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Zones
{
    static class ZoneTool
    {
        public static Space Space(
            Model model,
            string name,
            string program = "Office:OpenOffice",
            ThermalZone thermalZone = null,
            BuildingStory story = null,
            double lightingPower = 0,
            double equipmentPower = 0,
            double peopleDensity = 0,
            double outdoorAirperFloorArea = 0,
            double outdoorAirperPerson = 0,
            ScheduleRuleset ltgSchedule = null,
            ScheduleRuleset equipSchedule = null,
            ScheduleRuleset occSchedule = null,
            ScheduleRuleset pplActSchedule = null,
            ScheduleRuleset infilSchedule = null)
        {
            Space space = new Space(model);

            space.setName(name);

            if(thermalZone != null) { space.setThermalZone(thermalZone); }

            if (story != null) { space.setBuildingStory(story); }

            //Define internal load objects:
            //Lighting:
            LightsDefinition light_def = new LightsDefinition(model);
            light_def.setWattsperSpaceFloorArea(lightingPower);
            Lights light = new Lights(light_def);
            if (ltgSchedule != null) { light.setSchedule(ltgSchedule.to_Schedule().get()); }
            light.setSpace(space);

            //Equipment:
            ElectricEquipmentDefinition equip_def = new ElectricEquipmentDefinition(model);
            equip_def.setWattsperSpaceFloorArea(equipmentPower);
            ElectricEquipment equip = new ElectricEquipment(equip_def);
            if (ltgSchedule != null) { equip.setSchedule(equipSchedule.to_Schedule().get()); }
            equip.setSpace(space);

            //People:
            PeopleDefinition people_def = new PeopleDefinition(model);
            people_def.setPeopleperSpaceFloorArea(peopleDensity);
            People people = new People(people_def);
            if (occSchedule != null) { people.setNumberofPeopleSchedule(occSchedule.to_Schedule().get()); }
            if (pplActSchedule != null) { people.setActivityLevelSchedule(pplActSchedule.to_Schedule().get()); }
            people.setSpace(space);

            //Space Type:
            SpaceType type = new SpaceType(model);
            type.setName(program + ":" + name);

            //Outdoor Air:
            DesignSpecificationOutdoorAir OA = new DesignSpecificationOutdoorAir(model);
            OA.setName(name + "_DSOA");
            OA.setOutdoorAirMethod("Sum");
            OA.setOutdoorAirFlowperFloorArea(outdoorAirperFloorArea);
            OA.setOutdoorAirFlowperPerson(outdoorAirperPerson);
            if (occSchedule != null) { OA.setOutdoorAirFlowRateFractionSchedule(occSchedule.to_Schedule().get()); }

            type.setDesignSpecificationOutdoorAir(OA);

            //Infiltration Flow rate:
            SpaceInfiltrationDesignFlowRate infil = new SpaceInfiltrationDesignFlowRate(model);
            infil.setSpaceType(type);
            infil.setFlowperExteriorSurfaceArea(0.000226568446);
            infil.setSchedule(infilSchedule.to_Schedule().get());

            //Apply space type:
            space.setSpaceType(type);

            return space;
        }

        public static ThermalZone ThermalZone(Model model, string name)
        {
            ThermalZone zone = new ThermalZone(model);
            zone.setName(name);

            return zone;
        }
    }
}
