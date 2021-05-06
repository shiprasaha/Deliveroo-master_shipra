namespace Deliveroo.Cron.TimeUnits
{
    public class DayOfMonthTimeUnit : UnitOfTime
    {
        private static int lowerBound = 1;
        private static int upperBound = 31;

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