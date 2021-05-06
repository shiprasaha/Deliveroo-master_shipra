using System;
using System.Collections.Generic;
using System.Text;

namespace Deliveroo.Cron
{
    //This class formats to the CronExpression result to the desired response.
    public class CronExpressionResultFormatter
    {
        public string Format(CronExpressionResult result)
        {
            var stringBuilder = new StringBuilder();

            FormatResultTimeUnit(stringBuilder, "minute", result.Minutes);
            FormatResultTimeUnit(stringBuilder, "hour", result.Hours);
            FormatResultTimeUnit(stringBuilder, "day of month", result.DaysOfMonth);
            FormatResultTimeUnit(stringBuilder, "month", result.Months);
            FormatResultTimeUnit(stringBuilder, "day of week", result.DaysOfWeek);
            stringBuilder.AppendFormat("{0}{1}", "command".PadRight(14), result.Command);

            return stringBuilder.ToString();
        }

        private void FormatResultTimeUnit(StringBuilder stringBuilder, string timeUnit, IEnumerable<int> timeUnitValues) => stringBuilder.AppendFormat("{0}{1}{2}", timeUnit.PadRight(14), string.Join<int>(" ", timeUnitValues), Environment.NewLine);
    }
}