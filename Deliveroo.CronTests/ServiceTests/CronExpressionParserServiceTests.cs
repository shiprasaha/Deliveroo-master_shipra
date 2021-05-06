using System.Linq;
using Deliveroo.Cron.Service;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests
{
    public class CronExpressionParserServiceTests
    {
        private CronExpressionParserService _sut;

        public CronExpressionParserServiceTests()
        {
            _sut = new CronExpressionParserService();
        }
        
        [Fact]
        public void Parse_WhenNotationIsANumberForMinute_ReturnsParsedMinutes()
        {
            var input = "59 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Minutes.ShouldBe(new []{ 59 });
        }
        
        [Fact]
        public void Parse_WhenNotationIsStarForMinutes_ReturnsAllMinutes()
        {
            var input = "* 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Minutes.ShouldBe(Enumerable.Range(0,60));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsADashForMinutes_ReturnsRangeOfMinutes()
        {
            var input = "1-5 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Minutes.ShouldBe(Enumerable.Range(1,5));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsACommaForMinutes_ReturnsSpecifiedMinutes()
        {
            var input = "1,12,32,45,59 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Minutes.ShouldBe(new[]{ 1, 12, 32, 45, 59 });
        }
        
        [Fact]
        public void Parse_WhenNotationContainsASlashMinutes_ReturnsSpecifiedMinutes_FromStartingPointIncrementedByInterval()
        {
            var input = "10/12 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Minutes.ShouldBe(new[]{ 10, 22, 34, 46, 58 });
        }
        
        
        [Fact]
        public void Parse_WhenNotationIsANumberForHours_ReturnsParsedHour()
        {
            var input = "1 15 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Hours.ShouldBe(new []{ 15 });
        }

        [Fact]
        public void Parse_WhenNotationIsStarForHours_ReturnsAllHours()
        {
            var input = "1 * 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Hours.ShouldBe(Enumerable.Range(0,24));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsADashForHours_ReturnsRangeOfHours()
        {
            var input = "1 12-18 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Hours.ShouldBe(Enumerable.Range(12,7));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsACommaForHours_ReturnsSpecifiedHours()
        {
            var input = "1 13,15,21 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Hours.ShouldBe(new[]{ 13, 15, 21 });
        }
        
        [Fact]
        public void Parse_WhenNotationContainsASlash_ReturnsSpecifiedHours_FromStartingPointIncrementedByInterval()
        {
            var input = "1 13/4 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Hours.ShouldBe(new[]{ 13, 17, 21 });
        }
        
        
        [Fact]
        public void Parse_WhenNotationIsANumberForDayOfMonth_ReturnsParsedDayOfMonth()
        {
            var input = "1 1 23 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfMonth.ShouldBe(new []{ 23 });
        }

        [Fact]
        public void Parse_WhenNotationIsStarForDayOfMonth_ReturnsAllDaysOfMonth()
        {
            var input = "1 1 * 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfMonth.ShouldBe(Enumerable.Range(1,31));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsADashForDayOfMonth_ReturnsRangeOfDays()
        {
            var input = "1 1 15-17 1 1 somecommand";
            var result = _sut.Parse(input);

            result.DaysOfMonth.ShouldBe(Enumerable.Range(15, 3));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsACommaForDayOfMonth_ReturnsSpecifiedDays()
        {
            var input = "1 1 1,13,21,31 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfMonth.ShouldBe(new[]{ 1, 13, 21, 31 });
        }
        
        [Fact]
        public void Parse_WhenNotationContainsASlash_ReturnsSpecifiedDays_FromStartingPointIncrementedByInterval()
        {
            var input = "1 1 12/4 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfMonth.ShouldBe(new[]{ 12, 16, 20, 24, 28 });
        }
        
        
        [Fact]
        public void Parse_WhenNotationIsANumberForMonth_ReturnsParsedMonth()
        {
            var input = "1 1 1 8 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Months.ShouldBe(new []{ 8 });
        }

        [Fact]
        public void Parse_WhenNotationIsStarForMonth_ReturnsAllMonths()
        {
            var input = "1 1 1 * 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Months.ShouldBe(Enumerable.Range(1,12));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsADashForMonth_ReturnsRangeOfMonths()
        {
            var input = "1 1 1 6-10 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Months.ShouldBe(Enumerable.Range(6,5));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsACommaForMonth_ReturnsSpecifiedMonths()
        {
            var input = "1 1 1 3,7,11 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Months.ShouldBe(new[]{ 3,7,11 });
        }
        
        [Fact]
        public void Parse_WhenNotationContainsASlash_ReturnsSpecifiedMonths_FromStartingPointIncrementedByInterval()
        {
            var input = "1 1 1 6/3 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Months.ShouldBe(new[]{ 6, 9, 12 });
        }
        
        
        [Fact]
        public void Parse_WhenNotationIsANumberForDayOfWeek_ReturnsParsedDayOfWeek()
        {
            var input = "1 1 1 1 3 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfWeek.ShouldBe(new []{ 3 });
        }

        [Fact]
        public void Parse_WhenNotationIsStarForDaysOfWeek_ReturnsAllDaysOfWeek()
        {
            var input = "1 1 1 1 * somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfWeek.ShouldBe(Enumerable.Range(0, 7));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsADashForDaysOfWeek_ReturnsRangeOfDaysOfWeek()
        {
            var input = "1 1 1 1 0-3 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfWeek.ShouldBe(Enumerable.Range(0,4));
        }
        
        [Fact]
        public void Parse_WhenNotationContainsACommaForDaysOfWeek_ReturnsSpecifiedDaysOfWeek()
        {
            var input = "1 1 1 1 0,3,5 somecommand";
            var result = _sut.Parse(input);
            
            result.DaysOfWeek.ShouldBe(new[]{ 0,3,5 });
        }
        
        [Fact]
        public void Parse_WhenNotationContainsASlash_ReturnsSpecifiedDaysOfWeek_FromStartingPointIncrementedByInterval()
        {
            var input = "1 1 1 1 3/2 somecommand";
            var result = _sut.Parse(input);

            result.DaysOfWeek.ShouldBe(new[] { 3, 5 });
        }
        
        
        [Fact]
        public void Parse_MapsCommandCorrectly()
        {
            var input = "1 1 1 1 1 somecommand";
            var result = _sut.Parse(input);
            
            result.Command.ShouldBe("somecommand");
        }
    }
}