namespace Deliveroo.Cron.TimeUnits
{
    public class HourTimeUnit : UnitOfTime
    {
        private static int lowerBound = 0;
        private static int upperBound = 23;

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
