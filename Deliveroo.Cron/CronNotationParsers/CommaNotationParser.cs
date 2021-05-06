using Deliveroo.Cron.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deliveroo.Cron.CronNotationParsers
{
    public class CommaNotationParser : ICronNotationParser
    {
        public bool IsNotationMatched(string notation)
        {
            return notation.Contains(",");
        }

        public IEnumerable<int> Parse(string notation, int lowerBound, int upperBound)
        {
            var notationParts = notation.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var nonNumericParts = notationParts.Where(np => !int.TryParse(np, out _)).ToList();
            var numericParts = notationParts.Where(np => int.TryParse(np, out _)).ToList();

            if (!CheckIfValid(numericParts, nonNumericParts, lowerBound, upperBound))
            {
                throw new InvalidNotationArgumentException(notation);
            }

            if (nonNumericParts.Any())
            {
                var starNotationParser = new StarNotationParser();
                return starNotationParser.Parse("*", lowerBound, upperBound);
            }

            return removeAndGetDistinct(numericParts.Select(int.Parse));
        }

        private bool CheckIfValid(List<string> numericParts, List<string> nonNumericParts, int lowerBound, int upperBound)
        {
            if (numericParts.Count() == 0 && nonNumericParts.Count() == 0)
            {
                Console.WriteLine("Either a number or a * needs to be present with , operator");
                return false;
            }

            if (nonNumericParts.Any(np => !string.Equals("*", np)))
            {
                Console.WriteLine("Only expected non numeric string is *");
                return false;
            }

            if (numericParts.Any(np => int.Parse(np) < lowerBound || int.Parse(np) > upperBound))
            {
                Console.WriteLine("Invalid Range.Out of range!");
                return false;
            }

            return true;
        }

        private IEnumerable<int> removeAndGetDistinct(IEnumerable<int> inputList)
        {
            var result = inputList
                   .Select(s => s)
                   .Distinct()
                   .ToList();

            result.Sort();

            return result;
        }
    }
}