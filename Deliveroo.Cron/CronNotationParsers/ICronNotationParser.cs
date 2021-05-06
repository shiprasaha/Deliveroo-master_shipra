using System.Collections.Generic;

namespace Deliveroo.Cron.CronNotationParsers
{
    public interface ICronNotationParser
    {
        bool IsNotationMatched(string notation);

        IEnumerable<int> Parse(string notation, int timeUnitLowerBound, int timeUnitUpperBound);
    }
}