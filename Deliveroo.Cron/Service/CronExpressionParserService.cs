using System;
using System.Data;
using Deliveroo.Cron.TimeUnits;

namespace Deliveroo.Cron.Service
{
    public class CronExpressionParserService
    {
        private MinuteTimeUnit minutePart;
        private HourTimeUnit hourPart;
        private DayOfMonthTimeUnit dayOfMonthPart;
        private MonthTimeUnit monthPart;
        private DayOfWeekTimeUnit dayOfWeekPart;

        public CronExpressionParserService()
        {
            minutePart = new MinuteTimeUnit();
            hourPart = new HourTimeUnit();
            dayOfMonthPart = new DayOfMonthTimeUnit();
            monthPart = new MonthTimeUnit();
            dayOfWeekPart = new DayOfWeekTimeUnit();
        }

        /// <summary>
        /// Parses Cron Expressions of the following format:
        /// (Minute) (hour) (day of month) (month) (day of week) (command)
        /// * means all possible time unit
        /// - range of time unit
        /// , comma separated individual time units
        /// / increments where the left is the starting point in the time unit and the right is the interval till the max of the time unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CronExpressionResult Parse(string input)
        {
            var inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!CheckIfValid(inputParts))
            {
                throw new InvalidExpressionException("Invalid input arguments");
            }

            var result = new CronExpressionResult();
            result.Minutes = minutePart.ParseNotation(inputParts[0]);
            result.Hours = hourPart.ParseNotation(inputParts[1]);
            result.DaysOfMonth = dayOfMonthPart.ParseNotation(inputParts[2]);
            result.Months = monthPart.ParseNotation(inputParts[3]);
            result.DaysOfWeek = dayOfWeekPart.ParseNotation(inputParts[4]);
            result.Command = inputParts[5];

            return result;
        }

        private bool CheckIfValid(string[] inputParts)
        {
            if (inputParts.Length < 6)
            {
                return false;
            }
            return true;
        }

    }
}
