using System.Linq;
using Deliveroo.Cron.CronNotationParsers;
using Shouldly;
using Xunit;

namespace Deliveroo.CronTests.CronNotationParsers
{
    //This class contains test cases for star notation parsing
    public class StarNotationParserTests
    {
        private readonly StarNotationParser _sut;

        public StarNotationParserTests()
        {
            _sut = new StarNotationParser();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsTrueForSingleStarCharacter()
        {
            _sut.IsNotationMatched("*").ShouldBeTrue();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseMultipleStarCharacters()
        {
            _sut.IsNotationMatched("***").ShouldBeFalse();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseEmptyString()
        {
            _sut.IsNotationMatched(string.Empty).ShouldBeFalse();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseNullString()
        {
            _sut.IsNotationMatched(null).ShouldBeFalse();
        }
        
        [Fact]
        public void IsNotationMatched_ReturnsFalseStringWithOnlyWhitespace()
        {
            _sut.IsNotationMatched("    ").ShouldBeFalse();
        }

        [Fact]
        public void Parse_ReturnsAllValues_BetweenAndIncludingLowerAndUpperBound()
        {
            var input = "*";
            var values = _sut.Parse(input, 0, 59);
            
            values.ShouldBe(Enumerable.Range(0, 60));
        }

        [Fact]
        public void IsNotationMatchedForOtherNotationWithStarNotation_ReturnsFalse()
        {
            _sut.IsNotationMatched("*-/").ShouldBeFalse();
        }
    }
}