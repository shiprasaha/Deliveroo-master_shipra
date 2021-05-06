using System;
using System.Linq;
using Deliveroo.Cron.CronNotationParsers;
using Deliveroo.Cron.Exceptions;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronNotationParsers
{
    //This class contains test cases for comma notation parsing
    public class CommaNotationParserTests
    {
        private readonly CommaNotationParser _sut;

        public CommaNotationParserTests()
        {
            _sut = new CommaNotationParser();
        }

        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsContainingComma()
        {
            _sut.IsNotationMatched("1,2,3").ShouldBeTrue();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForNotationsNotContainingComma()
        {
            _sut.IsNotationMatched("abcd").ShouldBeFalse();
        }
        
        [Fact]
        public void Parse_WhenValuesInsideBounds_AndHasDistinctIntegers_ReturnsValues()
        {
            var input = "1,12,32,45,59";
            var values = _sut.Parse(input, 0, 59);
            
            values.ShouldBe(new[]{ 1, 12, 32, 45, 59 });
        }
        
        [Fact]
        public void Parse_WhenValuesContainsStar_ReturnsAllValuesInsideAndInclusiveOfBounds()
        {
            var input = "*,18,32,45,59";
            var values = _sut.Parse(input, 15, 66);
            
            values.ShouldBe(Enumerable.Range(15, 52));
        }
        
        [Fact]
        public void Parse_WhenValuesOutsideLowerBound_ThrowsException()
        {
            var input = "1,2,6,7";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 1,2,6,7");
        }
        
        [Fact]
        public void Parse_WhenValuesOutsideUpperBound_ThrowsException()
        {
            var input = "6,7,12,13";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 6,7,12,13");
        }
        
        [Fact]
        public void Parse_WhenValuesOutsideBothLowerAndUpperBound_ThrowsException()
        {
            var input = "1,6,7,12,13";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : 1,6,7,12,13");
        }
        
        [Fact]
        public void Parse_WhenNotationContainsNonNumericOrStarCharacters_ThrowsException()
        {
            var input = "hello,6,7,12,13";
            Should.Throw<InvalidNotationArgumentException>(() =>  _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : hello,6,7,12,13");
        }

        [Fact]
        public void Parse_WhenOnlyNotationIsPassed_ThrowsException()
        {
            var input = ",";
            Should.Throw<InvalidNotationArgumentException>(() => _sut.Parse(input, 5, 10))
                .Message.ShouldBe("Invalid argument provided : ,");
        }

        [Fact]
        public void Parse_WhenInputContainsMoreThanOneNotation()
        {
            _sut.IsNotationMatched("*,").ShouldBeTrue();
        }
    }
}