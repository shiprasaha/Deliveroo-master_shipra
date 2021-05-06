using System;
using System.Linq;
using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronNotationParsers
{
    //This class contains test cases for range notation parsing
    public class RangeNotationParserTests
    {
        private readonly RangeNotationParser _sut;

        public RangeNotationParserTests()
        {
            _sut = new RangeNotationParser();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsContainingDash()
        {
            _sut.IsNotationMatched("1-5").ShouldBeTrue();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseForNotationsContainingMoreThanOneSlash()
        {
            _sut.IsNotationMatched("1-5-6").ShouldBeFalse();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsNotContainingSlash()
        {
            _sut.IsNotationMatched("abcd").ShouldBeFalse();
        }
        
        [Fact]
        public void Parse_RangeInsideBounds_ReturnsValues()
        {
            var input = "5-6";
            var values = _sut.Parse(input, 0, 6);
            
            values.ShouldBe(Enumerable.Range(5,2));
        }
        
        [Fact]
        public void Parse_WhenLowerRangeValueIsNotNumeric_ThrowsException()
        {
            var input = "a-4";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 1, 12))
                .Message.ShouldBe("Invalid argument provided : a-4");
        }

        [Fact]
        public void Parse_WhenUpperRangeValueIsNotNumeric_ThrowsException()
        {
            var input = "1-g";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 1, 12))
                .Message.ShouldBe("Invalid argument provided : 1-g");
        }
        
        [Fact]
        public void Parse_WhenLowerRangeGreaterThanUpperRange_ThrowsException()
        {
            var input = "5-2";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 1, 12))
                .Message.ShouldBe("Invalid argument provided : 5-2");
        }
        
        [Fact]
        public void Parse_WhenLowerRangeOutsideAllowedLowerRange_ThrowsException()
        {
            var input = "5-25";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 10, 50))
                .Message.ShouldBe("Invalid argument provided : 5-25");
        }
        
        [Fact]
        public void Parse_WhenUpperRangeOutsideAllowedUpperRange_ThrowsException()
        {
            var input = "10-25";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 10, 15))
                .Message.ShouldBe("Invalid argument provided : 10-25");
        }
        
        [Fact]
        public void Parse_WhenLowerRangeOutsideAllowedUpperRange_ThrowsException()
        {
            var input = "10-25";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 1, 10))
                .Message.ShouldBe("Invalid argument provided : 10-25");
        }
        
        [Fact]
        public void Parse_WhenUpperRangeOutsideAllowedLowerRange_ThrowsException()
        {
            var input = "1-8";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 10, 15))
                .Message.ShouldBe("Invalid argument provided : 1-8");
        }

        [Fact]
        public void Parse_WhenRangeExceedsIntegerValue_ThrowsException()
        {
            var input = "2146473534-8";
            Should.Throw<InvalidNotationArgumentException>(() => _sut.Parse(input, 10, 15))
                .Message.ShouldBe("Invalid argument provided : 2146473534-8");
        }

        [Fact]
        public void Parse_WhenDoubleNotation_ThrowsException()
        {
            var input = "10--25";
            Should.Throw<InvalidNotationArgumentException>(() => _sut.Parse(input, 1, 10))
                .Message.ShouldBe("Invalid argument provided : 10--25");
        }
    }
}