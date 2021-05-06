using System.Collections.Generic;
using System.Linq;

namespace Deliveroo.Cron.CronNotationParsers
{
    public class StarNotationParser : ICronNotationParser
    {
        public bool IsNotationMatched(string notation)
        {
            return !string.IsNullOrWhiteSpace(notation) && string.Equals("*", notation);
        }

        public IEnumerable<int> Parse(string notation, int lowerBound, int upperBound)
        {
            List<int> values = new List<int>();
            for (int i = lowerBound; i <= upperBound; i++)
            {
                values.Add(i);
            }
            return values;
        }
    }
}