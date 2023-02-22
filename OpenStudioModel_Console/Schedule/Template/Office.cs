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
        public ScheduleRuleset DCV { get; }
        public ScheduleRuleset ActivityLevel { get; }

        public Office(Model model)
        {
            #region Occupancy
            //Occupancy schedule:
            //******************************************************
            ScheduleTypeLimits occ_typeLimits = SchTools.TypeLimits(model, "Dimensionless", 0, 1, "Continuous");

            Occupancy = SchTools.ScheduleRuleSet(
                model,
                typeLimits: occ_typeLimits,
                name: "Office_Occ_Schedule",
                displayName: "Office_Occ_Schedule");

            //Week:
            List<double> occ_wd_values = new List<double> { 0, 0, 0, 0, 0, 0, 0.1, 0.2, 0.95, 0.95, 0.95, 0.95, 0.5, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.55, 0.1, 0.05, 0.05 };
            List<double> occ_sat_values = new List<double> { 0, 0, 0, 0, 0, 0, 0.1, 0.1, 0.3, 0.3, 0.3, 0.3, 0.1, 0.1, 0.1, 0.1, 0.1, 0.05, 0.05, 0, 0, 0, 0, 0 };
            List<double> occ_sun_values = new List<double> { 0, 0, 0, 0, 0, 0, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0, 0, 0, 0, 0, 0 };
            ScheduleDay schedule_wd = SchTools.ScheduleDay(model, occ_wd_values, typeLimits : occ_typeLimits);
            ScheduleDay schedule_sat = SchTools.ScheduleDay(model, occ_sat_values, typeLimits : occ_typeLimits);
            ScheduleDay schedule_sun = SchTools.ScheduleDay(model, occ_sun_values, typeLimits: occ_typeLimits);

            //ScheduleRule:
            ScheduleRule occ_wd_schrule = SchTools.ScheduleRule(Occupancy, schedule_wd, applyDayTrigger: new List<bool> {true, true, true, true, true, false, false});
            ScheduleRule occ_sat_schrule = SchTools.ScheduleRule(Occupancy, schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule occ_sun_schrule = SchTools.ScheduleRule(Occupancy, schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Lighting
            //Lighting schedule:
            //******************************************************
            Lighting = SchTools.ScheduleRuleSet(
                model,
                typeLimits: occ_typeLimits,
                name: "Office_Ltg_Schedule",
                displayName: "Office_Ltg_Schedule");

            //Week:
            List<double> ltg_wd_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.35, 0.3, 0.3, 0.2, 0.2, 0.1, 0.05 };
            List<double> ltg_sat_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.3, 0.3, 0.3, 0.15, 0.15, 0.15, 0.15, 0.15, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            List<double> ltg_sun_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            ScheduleDay ltg_schedule_wd = SchTools.ScheduleDay(model, ltg_wd_values, typeLimits: occ_typeLimits);
            ScheduleDay ltg_schedule_sat = SchTools.ScheduleDay(model, ltg_sat_values, typeLimits: occ_typeLimits);
            ScheduleDay ltg_schedule_sun = SchTools.ScheduleDay(model, ltg_sun_values, typeLimits: occ_typeLimits);

            //ScheduleRule:
            ScheduleRule ltg_wd_schrule = SchTools.ScheduleRule(Lighting, ltg_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule ltg_sat_schrule = SchTools.ScheduleRule(Lighting, ltg_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule ltg_sun_schrule = SchTools.ScheduleRule(Lighting, ltg_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Equipment
            //Equipment schedule:
            //******************************************************
            Equipment = SchTools.ScheduleRuleSet(
                model,
                typeLimits: occ_typeLimits,
                name: "Office_Equip_Schedule",
                displayName: "Office_Equip_Schedule");

            //Week:
            List<double> equip_wd_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.5, 0.3, 0.3, 0.2, 0.2, 0.1, 0.05 };
            List<double> equip_sat_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.3, 0.3, 0.3, 0.15, 0.15, 0.15, 0.15, 0.15, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            List<double> equip_sun_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            ScheduleDay equip_schedule_wd = SchTools.ScheduleDay(model, equip_wd_values, typeLimits: occ_typeLimits);
            ScheduleDay equip_schedule_sat = SchTools.ScheduleDay(model, equip_sat_values, typeLimits: occ_typeLimits);
            ScheduleDay equip_schedule_sun = SchTools.ScheduleDay(model, equip_sun_values, typeLimits: occ_typeLimits);

            //ScheduleRule:
            ScheduleRule equip_wd_schrule = SchTools.ScheduleRule(Equipment, equip_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule equip_sat_schrule = SchTools.ScheduleRule(Equipment, equip_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule equip_sun_schrule = SchTools.ScheduleRule(Equipment, equip_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Infiltration
            //Infiltration schedule:
            //******************************************************
            Infiltration = SchTools.ScheduleRuleSet(
                model,
                typeLimits: occ_typeLimits,
                name: "Office_Infil_Schedule",
                displayName: "Office_Infil_Schedule");

            //Week:
            List<double> infil_wd_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.5, 0.3, 0.3, 0.2, 0.2, 0.1, 0.05 };
            List<double> infil_sat_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.1, 0.1, 0.3, 0.3, 0.3, 0.3, 0.15, 0.15, 0.15, 0.15, 0.15, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            List<double> infil_sun_values = new List<double> { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
            ScheduleDay infil_schedule_wd = SchTools.ScheduleDay(model, infil_wd_values, typeLimits: occ_typeLimits);
            ScheduleDay infil_schedule_sat = SchTools.ScheduleDay(model, infil_sat_values, typeLimits: occ_typeLimits);
            ScheduleDay infil_schedule_sun = SchTools.ScheduleDay(model, infil_sun_values, typeLimits: occ_typeLimits);

            //ScheduleRule:
            ScheduleRule infil_wd_schrule = SchTools.ScheduleRule(Infiltration, infil_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule infil_sat_schrule = SchTools.ScheduleRule(Infiltration, infil_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule infil_sun_schrule = SchTools.ScheduleRule(Infiltration, infil_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Cooling Setpoint
            //Cooling Setpoint schedule:
            //******************************************************
            ScheduleTypeLimits sp_typeLimits = SchTools.TypeLimits(model, "Temperature", numericType: "Continuous");

            CoolingSetpoint = SchTools.ScheduleRuleSet(
                model,
                typeLimits: sp_typeLimits,
                name: "Office_Clg_Schedule",
                displayName: "Office_Clg_Schedule");

            //Week:
            List<double> clg_wd_values = new List<double> { 29.4, 29.4, 29.4, 29.4, 29.4, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9 };
            List<double> clg_sat_values = new List<double> { 29.4, 29.4, 29.4, 29.4, 29.4, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 23.9, 29.4, 29.4, 29.4, 29.4 };
            List<double> clg_sun_values = new List<double> { 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4, 29.4 };
            ScheduleDay clg_schedule_wd = SchTools.ScheduleDay(model, clg_wd_values, typeLimits: sp_typeLimits);
            ScheduleDay clg_schedule_sat = SchTools.ScheduleDay(model, clg_sat_values, typeLimits: sp_typeLimits);
            ScheduleDay clg_schedule_sun = SchTools.ScheduleDay(model, clg_sun_values, typeLimits: sp_typeLimits);

            //ScheduleRule:
            ScheduleRule clg_wd_schrule = SchTools.ScheduleRule(CoolingSetpoint, clg_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule clg_sat_schrule = SchTools.ScheduleRule(CoolingSetpoint, clg_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule clg_sun_schrule = SchTools.ScheduleRule(CoolingSetpoint, clg_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Heating Setpoint
            //Heating Setpoint schedule:
            //******************************************************
            HeatingSetpoint = SchTools.ScheduleRuleSet(
                model,
                typeLimits: sp_typeLimits,
                name: "Office_Htg_Schedule",
                displayName: "Office_Htg_Schedule");

            //Week:
            List<double> htg_wd_values = new List<double> { 15.6, 15.6, 15.6, 15.6, 15.6, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1 };
            List<double> htg_sat_values = new List<double> { 15.6, 15.6, 15.6, 15.6, 15.6, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 21.1, 15.6, 15.6, 15.6, 15.6, 15.6 };
            List<double> htg_sun_values = new List<double> { 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6, 15.6 };
            ScheduleDay htg_schedule_wd = SchTools.ScheduleDay(model, htg_wd_values, typeLimits: sp_typeLimits);
            ScheduleDay htg_schedule_sat = SchTools.ScheduleDay(model, htg_sat_values, typeLimits: sp_typeLimits);
            ScheduleDay htg_schedule_sun = SchTools.ScheduleDay(model, htg_sun_values, typeLimits: sp_typeLimits);

            //ScheduleRule:
            ScheduleRule htg_wd_schrule = SchTools.ScheduleRule(HeatingSetpoint, htg_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule htg_sat_schrule = SchTools.ScheduleRule(HeatingSetpoint, htg_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule htg_sun_schrule = SchTools.ScheduleRule(HeatingSetpoint, htg_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region HVAC Availability Setpoint
            //HVAC schedule:
            //******************************************************
            ScheduleTypeLimits avail_typeLimits = SchTools.TypeLimits(model, "Availability",numericType:"Discrete");

            HVACAvailability = SchTools.ScheduleRuleSet(
                model,
                typeLimits: avail_typeLimits,
                name: "Office_HVACAvail_Schedule",
                displayName: "Office_HVACAvail_Schedule");

            //Week:
            List<double> hvac_wd_values = new List<double> { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<double> hvac_sat_values = new List<double> { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            List<double> hvac_sun_values = new List<double> { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 };
            ScheduleDay hvac_schedule_wd = SchTools.ScheduleDay(model, hvac_wd_values, typeLimits: avail_typeLimits);
            ScheduleDay hvac_schedule_sat = SchTools.ScheduleDay(model, hvac_sat_values, typeLimits: avail_typeLimits);
            ScheduleDay hvac_schedule_sun = SchTools.ScheduleDay(model, hvac_sun_values, typeLimits: avail_typeLimits);

            //ScheduleRule:
            ScheduleRule hvac_wd_schrule = SchTools.ScheduleRule(HVACAvailability, hvac_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule hvac_sat_schrule = SchTools.ScheduleRule(HVACAvailability, hvac_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule hvac_sun_schrule = SchTools.ScheduleRule(HVACAvailability, hvac_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region DCV chedule
            //DCV schedule:
            //******************************************************
            DCV = SchTools.ScheduleRuleSet(
                model,
                typeLimits: occ_typeLimits,
                name: "Office_DCV_Schedule",
                displayName: "Office_DCV_Schedule");

            //Week:
            ScheduleDay dcv_schedule_wd = SchTools.ScheduleDay(model, occ_wd_values, typeLimits: occ_typeLimits);
            ScheduleDay dcv_schedule_sat = SchTools.ScheduleDay(model, occ_sat_values, typeLimits: occ_typeLimits);
            ScheduleDay dcv_schedule_sun = SchTools.ScheduleDay(model, occ_sun_values, typeLimits: occ_typeLimits);

            //ScheduleRule:
            ScheduleRule dcv_wd_schrule = SchTools.ScheduleRule(HVACAvailability, dcv_schedule_wd, applyDayTrigger: new List<bool> { true, true, true, true, true, false, false });
            ScheduleRule dcv_sat_schrule = SchTools.ScheduleRule(HVACAvailability, dcv_schedule_sat, applyDayTrigger: new List<bool> { false, false, false, false, false, true, false });
            ScheduleRule dcv_sun_schrule = SchTools.ScheduleRule(HVACAvailability, dcv_schedule_sun, applyDayTrigger: new List<bool> { false, false, false, false, false, false, true });
            #endregion

            #region Activity Level
            //HVAC schedule:
            //******************************************************
            ScheduleTypeLimits act_typeLimits = SchTools.TypeLimits(model, "ActivityLevel", 0.0, numericType: "Continuous");

            ActivityLevel = SchTools.ScheduleRuleSet(
                model,
                120,
                typeLimits: act_typeLimits,
                name: "Office_Activity_Schedule",
                displayName: "Office_Activity_Schedule");
            #endregion

        }

    }
}
