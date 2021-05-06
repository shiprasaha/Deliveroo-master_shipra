using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Factory;
using Xunit;

namespace Deliveroo.CronTests.FactoryTests
{
    public class FactoryTests
    {
        private readonly ParserFactory _sut;

        public FactoryTests()
        {
            _sut = new ParserFactory();
        }

        [Fact]
        public void GetApplicableParser_WhenInputStringIsStar_ReturnsObjectOfStarNotationParser()
        {
            string input = "*";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }

        [Fact]
        public void GetApplicableParser_WhenInputStringIsInterval_ReturnsObjectOfIntervalNotationParser()
        {
            string input = "*/5";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }
        
        [Fact]
        public void GetApplicableParser_WhenInputStringIsRange_ReturnsObjectOfRangeNotationParser()
        {
            string input = "1-5";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }

        [Fact]
        public void GetApplicableParser_WhenInputStringIsSimpleInteger_ReturnsObjectOfSimpleCronNotationParser()
        {
            string input = "5";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }

        [Fact]
        public void GetApplicableParser_WhenInputStringHasComma_ReturnsObjectOfCommaNotationParser()
        {
            string input = "1,5";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }

        [Fact]
        public void GetApplicableParser_WhenInputStringIsInvalid_ReturnsObjectOfInvalidNotationParser()
        {
            string input = "1-3*";
            var result = _sut.GetApplicableParser(input);
            result.GetType().Equals(typeof(StarNotationParser));
        }

    }
}
