using FluentAssertions;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class StringsRuleTests
{
    [Fact]
    public void Matches_MatchFound_Returns_True()
    {
        // Given
        const string filterValue1 = "AAA";
        var rule = CreateRule(filterValue1);

        // When
        var actual = rule.Matches(filterValue1);

        // Then
        actual.Should().Be(true);
    }
    
    [Fact]
    public void Matches_NoMatchFound_Returns_False()
    {
        // Given
        const string filterValue1 = "AAA";
        var rule = CreateRule(filterValue1);

        // When
        var actual = rule.Matches("BBB");

        // Then
        actual.Should().Be(false);
    }
    
    [Fact]
    public void Matches_AnyFilter_Ignored()
    {
        // Given
        const string filterValue1 = "AAA";
        var rule = CreateRule("<ANY>", filterValue1);

        // When
        var actual = rule.Matches("BBB");

        // Then
        actual.Should().Be(true);
    }

    private static StringsRule CreateRule(params string[] filters)
    {
        return new StringsRule(1, 10, 1, filters);
    }
}