using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Deliveroo.Cron.Factory;
using System.Collections.Generic;
using System;

namespace Deliveroo.Cron.TimeUnits
{
    public class DayOfWeekTimeUnit : UnitOfTime
    {
        private static int lowerBound = 0; 
        private static int upperBound = 6;

        public override int GetTimeUnitLowerBound()
        {
            return lowerBound;
        }

        public override int GetTimeUnitUpperBound()
        {
            return upperBound;
        }
    }
}
