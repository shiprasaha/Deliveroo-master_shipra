using System;
using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronNotationParsers
{
    public class IntervalNotationParserTests
    {
        private readonly IntervalNotationParser _sut;

        public IntervalNotationParserTests()
        {
            _sut = new IntervalNotationParser();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsContainingSlash()
        {
            _sut.IsNotationMatched("5/1").ShouldBeTrue();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseForNotationsContainingMoreThanOneSlash()
        {
            _sut.IsNotationMatched("5/1/5/6").ShouldBeFalse();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsNotContainingSlash()
        {
            _sut.IsNotationMatched("abcd").ShouldBeFalse();
        }
        
        [Fact]
        public void Parse_WhenValuesInsideBounds_AndHasDistinctIntegers_ReturnsValues()
        {
            var input = "5/20";
            var values = _sut.Parse(input, 0, 59);
            
            values.ShouldBe(new[]{ 5, 25, 45 });
        }
        
        [Fact]
        public void Parse_WhenStartingValueIsStarCharacter_UseLowerBoundForStartingValue_ReturnsValues()
        {
            var input = "*/20";
            var values = _sut.Parse(input, 0, 59);
            
            values.ShouldBe(new[]{ 0, 20, 40 });
        }
        
        [Fact]
        public void Parse_WhenLowerBoundIsNotZeroBased_ReturnsValues()
        {
            var input = "3/3";
            var values = _sut.Parse(input, 1, 12);
            
            values.ShouldBe(new[]{ 3, 6, 9, 12 });
        }
        
        [Fact]
        public void Parse_WhenStartingValueIsNotAStarCharacterOrInt_ThrowException()
        {
            var input = "hello/20";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : hello/20");
        }
        
        [Fact]
        public void Parse_WhenIntervalValueIsNotAnInt_ThrowException()
        {
            var input = "5/hello";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 5/hello");
        }
        
        [Fact]
        public void Parse_WhenStartingValueIsOutsideLowerBound_ThrowException()
        {
            var input = "1/20";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 1/20");
        }
        
        [Fact]
        public void Parse_WhenStartingValueIsOutsideUpperBound_ThrowException()
        {
            var input = "15/20";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 15/20");
        }
        
        [Fact]
        public void Parse_WhenIntervalValueIsGreaterThanUpperBound_ReturnStartingValue()
        {
            var input = "7/1000";
            var values = _sut.Parse(input, 1, 12);
            
            values.ShouldBe(new[]{ 7 });
        }
    }
}