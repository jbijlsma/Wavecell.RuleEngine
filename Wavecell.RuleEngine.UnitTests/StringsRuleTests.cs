using FluentAssertions;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class StringsRuleTests
{
    [Fact]
    public void Matches()
    {
        // Given
        var rule = new StringsRule
        {
            Filter1 = "AAA",
            Filter2 = "BBB",
            Filter3 = "CCC",
            Filter4 = "DDD"
        };

        // When
        var actual = rule.Matches(rule.Filter1, rule.Filter2, rule.Filter3, rule.Filter4);

        // Then
        actual.Should().Be(true);
    }
}