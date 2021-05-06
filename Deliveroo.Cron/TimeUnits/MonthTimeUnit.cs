namespace Deliveroo.Cron.TimeUnits
{
    public class MonthTimeUnit : UnitOfTime
    {
        private static int lowerBound = 1;
        private static int upperBound = 12;

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
