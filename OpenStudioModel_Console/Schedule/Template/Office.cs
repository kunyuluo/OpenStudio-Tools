using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Schedule.Template
{
    class Office
    {
        public ScheduleRuleset Occupancy { get; }
        public ScheduleRuleset Lighting { get; }
        public ScheduleRuleset Equipment { get; }
        public ScheduleRuleset Infiltration { get; }
        public ScheduleRuleset CoolingSetpoint { get; }
        public ScheduleRuleset HeatingSetpoint { get; }
        public ScheduleRuleset HVACAvailability { get; }

        public Office(Model model)
        {
            //Occupancy schedule:
            //******************************************************
            ScheduleTypeLimits occ_typeLimits = SchTools.TypeLimits(model, "Dimensionless");

            Occupancy = SchTools.ScheduleRuleSet(
                model,
                //new List<ScheduleRule> { occ_wd_schrule, occ_sat_schrule, occ_sun_schrule },
                typeLimits: occ_typeLimits,
                name: "Office_Occ_Schedule",
                displayName: "Office_Occ_Schedule");

            //Week:
            List<double> occ_wd_values = new List<double> { 0, 0, 0, 0, 0, 0, 0.1, 0.2, 0.95, 0.95, 0.95, 0.95, 0.5, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.55, 0.1, 0.05, 0.05 };
            ScheduleDay schedule_wd = SchTools.ScheduleDay(model, occ_wd_values, typeLimits : occ_typeLimits);
            ScheduleDay schedule_sat = SchTools.ScheduleDay(model, occ_wd_values, typeLimits : occ_typeLimits);
            ScheduleDay schedule_sun = SchTools.ScheduleDay(model, null, 0.0, occ_typeLimits);

            //ScheduleRule:
            ScheduleRule occ_wd_schrule = SchTools.ScheduleRule(Occupancy, schedule_wd, applyDayTrigger: new List<bool> {true, true, true, true, true, false, false});
            ScheduleRule occ_sat_schrule = SchTools.ScheduleRule(Occupancy, schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule occ_sun_schrule = SchTools.ScheduleRule(Occupancy, schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });

        }

    }
}
