using Deliveroo.Cron.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deliveroo.Cron.CronNotationParsers
{
    public class RangeNotationParser : ICronNotationParser
    {
        public bool IsNotationMatched(string notation)
        {
            return notation.Count(n => n == '-') == 1;
        }

        public IEnumerable<int> Parse(string notation, int lowerBound, int upperBound)
        {
            var rangeParts = notation.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (!CheckIfValid(rangeParts, lowerBound, upperBound))
            {
                throw new InvalidNotationArgumentException(notation);
            }

            var startRange = int.Parse(rangeParts[0]);
            var endRange = int.Parse(rangeParts[1]);

            List<int> values = new List<int>();
            for (int i = startRange; i <= endRange; i++)
            {
                values.Add(i);
            }
            return values;
        }

        private bool CheckIfValid(string[] rangeParts, int timeUnitLowerBound, int timeUnitUpperBound)
        {
            if (rangeParts.Length > 2)
            {
                Console.WriteLine("Expected only two arguments with '-'");
                return false;
            }

            var isStartRangeValid = int.TryParse(rangeParts[0], out var startRange);
            var isEndRangeValid = int.TryParse(rangeParts[1], out var endRange);

            if (!isStartRangeValid || !isEndRangeValid)
            {
                Console.WriteLine("Invalid Number. Expected integer from range " + timeUnitLowerBound + " to " + timeUnitUpperBound);
                return false;
            }

            if (startRange > endRange || startRange < timeUnitLowerBound || endRange > timeUnitUpperBound)
            {
                Console.WriteLine("Invalid Range.Out of range!");
                return false;
            }
            return true;
        }
    }
}