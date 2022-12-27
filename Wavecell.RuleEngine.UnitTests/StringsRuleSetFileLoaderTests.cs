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
            new(1, 80, 8, "AAA", "<ANY>", "CCC", "DDD"),
            new(2, 10, 1, "<ANY>", "<ANY>", "AAA", "<ANY>")
        };
        actual.Should().BeEquivalentTo(expected);
    }
}