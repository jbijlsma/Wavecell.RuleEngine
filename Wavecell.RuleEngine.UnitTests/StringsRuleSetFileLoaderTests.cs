using FluentAssertions;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class StringsRuleSetFileLoaderTests
{
    [Fact]
    public void Load()
    {
        // Given
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "StringsRuleSetTestData.csv");
        var ruleFileReader = new StringsRuleSetFileLoader();

        // When
        var actual = ruleFileReader.Load(ruleFile);

        // Then
        var expected = new List<StringsRule>
        {
            new()
            { 
                RuleId = 1,
                Priority = 80,
                Filter1 = "AAA", 
                Filter2 = "<ANY>", 
                Filter3 = "CCC", 
                Filter4 = "DDD", 
                OutputValue = 8
            },
            new()
            { 
                RuleId = 2,
                Priority = 10,
                Filter1 = "<ANY>", 
                Filter2 = "<ANY>", 
                Filter3 = "AAA", 
                Filter4 = "<ANY>", 
                OutputValue = 1
            }
        };
        actual.Should().BeEquivalentTo(expected);
    }
}