using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenStudio;

namespace OpenStudioModel_Console.Schedule
{
    static class SchTools
    {
        //public static ScheduleRuleset AnnualSchedule()
        public static ScheduleTypeLimits TypeLimits(
            Model model,
            string unitType,
            double lowerLimit = -9999,
            double upperLimit = -9999,
            string numericType = null,
            string name = null,
            string displayName = null)
        {
            ScheduleTypeLimits typeLimits = new ScheduleTypeLimits(model);

            if (name != null) { typeLimits.setName(name); }
            if (displayName != null) { typeLimits.setDisplayName(displayName); }
            if (upperLimit != -9999) { typeLimits.setUpperLimitValue(upperLimit); }
            if (lowerLimit != -9999) { typeLimits.setLowerLimitValue(lowerLimit); }

            //Unit Type includes:
            //***************************************************
            //Dimensionless                  Fractional
            //Temperature                    OnOff
            //DeltaTemperature               ClothingInsulation
            //PrecipitationRate              MassFlowRate
            //Angle                          Pressure
            //ConvectionCoefficient          ControlMode
            //ActivityLevel                  Percent
            //Velocity                       Availability
            //Capacity                       Power   
            //***************************************************
            typeLimits.setUnitType(unitType);

            //Numeric Type includes:
            //***************************************************
            //Continuous
            //Discrete
            //***************************************************
            if (numericType != null) { typeLimits.setNumericType(numericType); }

            return typeLimits;
        }

        public static ScheduleDay ScheduleDay(
            Model model,
            List<double> values = null,
            double value = -9999,
            ScheduleTypeLimits typeLimits = null,
            string name = null,
            string displayName = null)
        {
            ScheduleDay schedule = null;

            if (name != null) { schedule.setName(name); }
            if (displayName != null) { schedule.setDisplayName(displayName); }

            if (value == -9999)
            {
                schedule = new ScheduleDay(model, value);
            }

            if (values == null)
            {
                if (value != -9999)
                {
                    schedule = new ScheduleDay(model, value);
                }
                else
                {
                    throw new ArgumentNullException("values", "the value for the schedule cannot be empty");
                }                
            }
            else
            {
                if (values.Count == 0)
                {
                    schedule = new ScheduleDay(model, 0.0);
                }
                else if(values.Count != 24)
                {
                    schedule.addValue(new Time(0, 8, 0), 0);
                    schedule.addValue(new Time(0, 18, 0), 1.0);
                    schedule.addValue(new Time(0, 24, 0), 0);
                }
                else
                {
                    for(int i = 0; i < values.Count; i++)
                    {
                        schedule.addValue(new Time(0, i + 1, 0), values[i]);
                    }
                    
                }
                    
            }
            if (typeLimits != null) { schedule.setScheduleTypeLimits(typeLimits); }

            return schedule;
        }


        public static ScheduleRule ScheduleRule(
            ScheduleRuleset scheduleRuleSet,
            ScheduleDay scheduleDay = null,
            int startMonth = 1,
            int startDay = 1,
            int endMonth = 12,
            int endDay = 31,
            bool allWeek = false,
            List<bool> applyDayTrigger = null,
            string name = null,
            string displayName = null)
        {
            ScheduleRule schedule = new ScheduleRule(scheduleRuleSet, scheduleDay);

            if (name != null) { schedule.setName(name); }
            if (displayName != null) { schedule.setDisplayName(displayName); }

            //Weeklt setting:
            if (allWeek) 
            { 
                schedule.setApplyAllDays(true); 
            }
            else
            {
                if (applyDayTrigger != null && applyDayTrigger.Count == 7)
                {
                    schedule.setApplyMonday(applyDayTrigger[0]);
                    schedule.setApplyTuesday(applyDayTrigger[1]);
                    schedule.setApplyWednesday(applyDayTrigger[2]);
                    schedule.setApplyThursday(applyDayTrigger[3]);
                    schedule.setApplyFriday(applyDayTrigger[4]);
                    schedule.setApplySaturday(applyDayTrigger[5]);
                    schedule.setApplySunday(applyDayTrigger[6]);
                }
                else
                {
                    schedule.setApplyWeekdays(true);
                    schedule.setApplyWeekends(false);
                }
            }

            //Monthly setting:
            schedule.setStartDate(new Date(new MonthOfYear(startMonth), Convert.ToUInt32(startDay)));
            schedule.setEndDate(new Date(new MonthOfYear(endMonth), Convert.ToUInt32(endDay)));


            return schedule;
        }


        public static ScheduleRuleset ScheduleRuleSet(
            Model model,
            //List<ScheduleRule> scheduleRules = null,
            double value = -9999,
            ScheduleTypeLimits typeLimits = null,
            string name = null,
            string displayName = null)
        {
            ScheduleRuleset scheduleRuleset = null;

            if(value != -9999) 
            {
                scheduleRuleset = new ScheduleRuleset(model, value);
            }
            else
            {
                scheduleRuleset = new ScheduleRuleset(model);
                //if(scheduleRules == null && scheduleRules.Count != 0)
                //{
                //    for (int i = 0; i < scheduleRules.Count; i++) 
                //    {
                //        scheduleRuleset.setScheduleRuleIndex(scheduleRules[i], Convert.ToUInt32(i));
                //    }
                //}
            }
            scheduleRuleset.setScheduleTypeLimits(typeLimits);

            if (name != null) { scheduleRuleset.setName(name); }
            if (displayName != null) { scheduleRuleset.setDisplayName(displayName); }

            return scheduleRuleset;
        }

    }
}
