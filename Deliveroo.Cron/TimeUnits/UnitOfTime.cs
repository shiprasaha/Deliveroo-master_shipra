using System.Collections.Generic;
using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Deliveroo.Cron.Factory;

namespace Deliveroo.Cron.TimeUnits
{
    public abstract class UnitOfTime
    {
        private static ParserFactory parserFactory = new ParserFactory();
        public abstract int GetTimeUnitLowerBound();
        public abstract int GetTimeUnitUpperBound();

        public IEnumerable<int> ParseNotation(string notation)
        {
            ICronNotationParser cronNotationParser = parserFactory.GetApplicableParser(notation);

            if (cronNotationParser == null)
            {
                throw new InvalidNotationArgumentException(notation);
            }
            return cronNotationParser.Parse(notation, GetTimeUnitLowerBound(), GetTimeUnitUpperBound());
        }
    }
}
