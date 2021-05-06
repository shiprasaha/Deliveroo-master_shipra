using System.Collections.Generic;

namespace Deliveroo.Cron
{
    public class CronExpressionResult
    {
        public IEnumerable<int> Minutes { get; set; }
        public IEnumerable<int> Hours { get; set; }
        public IEnumerable<int> DaysOfMonth { get; set; }
        public IEnumerable<int> Months { get; set; }
        public IEnumerable<int> DaysOfWeek { get; set; }
        public string Command { get; set; }
    }
}