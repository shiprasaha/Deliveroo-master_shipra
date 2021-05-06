using System;
using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronNotationParsers
{
    //This class contains test cases for simple notation parsing
    public class SimpleCronNotationParserTests
    {
        private readonly SimpleCronNotationParser _sut;

        public SimpleCronNotationParserTests()
        {
            _sut = new SimpleCronNotationParser();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNumericValues()
        {
            _sut.IsNotationMatched("12").ShouldBeTrue();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseForNonNumericValues()
        {
            _sut.IsNotationMatched("ab12c").ShouldBeFalse();
        }
        
        [Fact]
        public void Parse_WhenValuesInsideBounds_ReturnsValues()
        {
            var input = "30";
            var values = _sut.Parse(input, 0, 59);
            
            values.ShouldBe(new[]{ 30 });
        }
        
        [Fact]
        public void Parse_WhenValuesIsNotNumeric_ThrowsException()
        {
            var input = "hello";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : hello");
        }
        
        [Fact]
        public void Parse_WhenValueOutsideLowerBound_ThrowsException()
        {
            var input = "1";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 1");
        }
        
        [Fact]
        public void Parse_WhenValueOutsideUpperBound_ThrowsException()
        {
            var input = "99";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 99");
        }
    }
}