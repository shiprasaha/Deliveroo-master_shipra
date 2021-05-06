using Deliveroo.Cron.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deliveroo.Cron.CronNotationParsers
{
    public class IntervalNotationParser : ICronNotationParser
    {
        public bool IsNotationMatched(string notation)
        {
            return notation.Count(n => n == '/') == 1;
        }

        public IEnumerable<int> Parse(string notation, int lowerBound, int upperBound)
        {
            var intervalParts = notation.Split('/', StringSplitOptions.RemoveEmptyEntries);
            int startingPoint;

            if (string.Equals("*", intervalParts[0]))
                startingPoint = lowerBound;
            else 
               if (!int.TryParse(intervalParts[0], out startingPoint))
                 throw new InvalidNotationArgumentException(notation);

            if (!CheckIfValid(intervalParts, lowerBound, upperBound, startingPoint))
                throw new InvalidNotationArgumentException(notation);

            var interval = int.Parse(intervalParts[1]);

            /*We need to apply offsets and skip the same for time units (such as days of month),
            where it does not begin with zero. */
            var offsetIndex = lowerBound == 0 ? 0 : 1;

            List<int> values = new List<int>();
            for (int i = startingPoint; i <= upperBound; i = i + interval)
            {
                values.Add(i);
            }
            return values;
        }


        private bool CheckIfValid(String[] intervalParts, int lowerBound, int upperBound, int startingPoint)
        {
            if (intervalParts.Length > 2)
            {
                Console.WriteLine("Only expected 2 arguments with '/' sign");
                return false;
            }

            if (!int.TryParse(intervalParts[1], out var interval))
            {
                Console.WriteLine("Invalid Number. Expected integer from range " + lowerBound + " to " + upperBound);
                return false;
            }

            if (startingPoint < lowerBound || startingPoint > upperBound)
            {
                Console.WriteLine("Invalid Range.Out of range!");
                return false;
            }

            return true;
        }
    }
}