namespace Deliveroo.Cron.TimeUnits
{
    public class MinuteTimeUnit : UnitOfTime
    {
        private static int lowerBound = 0;
        private static int upperBound = 59;

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
