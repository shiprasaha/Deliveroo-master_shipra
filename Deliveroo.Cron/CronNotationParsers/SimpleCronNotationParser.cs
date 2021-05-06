using Deliveroo.Cron.Exceptions;
using System;
using System.Collections.Generic;

namespace Deliveroo.Cron.CronNotationParsers
{
    public class SimpleCronNotationParser : ICronNotationParser
    {
        public bool IsNotationMatched(string notation)
        {
            return int.TryParse(notation, out _);
        }

        public IEnumerable<int> Parse(string notation, int lowerBound, int upperBound)
        {
            if (!CheckIfValid(notation, lowerBound, upperBound))
            {
                throw new InvalidNotationArgumentException(notation);
            }

            var value = int.Parse(notation);
            return new[] { value };
        }

        private bool CheckIfValid(string notation, int timeUnitLowerBound, int timeUnitUpperBound)
        {
            if (!int.TryParse(notation, out var value))
            {
                Console.WriteLine("Invalid Number. Expected integer from range " + timeUnitLowerBound + " to "
                                  + timeUnitUpperBound);
                return false;
            }

            if (value < timeUnitLowerBound || value > timeUnitUpperBound)
            {
                Console.WriteLine("Invalid Range.Out of range!");
                return false;
            }

            return true;

        }
    }
}