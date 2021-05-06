using Deliveroo.Cron.CronNotationParsers;
using System;
using System.Collections.Generic;

namespace Deliveroo.Cron.Factory
{
    public class ParserFactory
    {
        private HashSet<ICronNotationParser> parsers;

        public ParserFactory()
        {
            parsers = new HashSet<ICronNotationParser>();
            parsers.Add(new CommaNotationParser());
            parsers.Add(new IntervalNotationParser());
            parsers.Add(new RangeNotationParser());
            parsers.Add(new SimpleCronNotationParser());
            parsers.Add(new StarNotationParser());
        }

        public ICronNotationParser GetApplicableParser(String notation)
        {
            foreach (ICronNotationParser parser in parsers)
            {
                if (parser.IsNotationMatched(notation))
                {
                    return parser;
                }
            }
            Console.WriteLine("No valid parser found for the expression : " + notation);
            return null;
        }
    }
}
