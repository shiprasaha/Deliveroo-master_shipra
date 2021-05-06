using Deliveroo.Cron.Controllers;
using Deliveroo.Cron.Exceptions;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronExpressionParserTests
{
    //This class contains test cases to test input string
    public class CronExpressionParserTests
    {
        private CronApplicationController _sut;
        public CronExpressionParserTests()
        {
            _sut = new CronApplicationController();
        }

        [Fact]
        public void Parse_WhenValidInputString_ReturnsValidParsedOutput()
        {
            string[] args = { "*/15 0 1,15 * 1-5 /usr/bin/find" };

            var result = _sut.ParseExpression(args[0]);

            Assert.Contains("minute        0 15 30 45", result);
            Assert.Contains("hour          0", result);
            Assert.Contains("day of month  1 15", result);
            Assert.Contains("month         1 2 3 4 5 6 7 8 9 10 11 12", result);
            Assert.Contains("day of week   1 2 3 4 5", result);
            Assert.Contains("command       /usr/bin/find", result);
        }

        [Fact]
        public void Parse_WhenInvalidInputString_ReturnsException()
        {
            string[] args = { "*/15 0 1,15 * 1*-6" };

            Should.Throw<ExpressionParsingException>(() => _sut.ParseExpression(args[0])
              .ShouldBe("Invalid input arguments"));
        }
    }
}
