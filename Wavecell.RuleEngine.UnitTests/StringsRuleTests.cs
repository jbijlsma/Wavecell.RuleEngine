using FluentAssertions;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class StringsRuleTests
{
    [Fact]
    public void Matches_MatchFound_Returns_True()
    {
        // Given
        var filterValues = StringsFilterValues.Create("AAA");
        var rule = CreateRule(filterValues);

        // When
        var actual = rule.Matches(filterValues);

        // Then
        actual.Should().Be(true);
    }
    
    [Fact]
    public void Matches_NoMatchFound_Returns_False()
    {
        // Given
        var filterValues = StringsFilterValues.Create("AAA");
        var rule = CreateRule(filterValues);

        // When
        var actual = rule.Matches(StringsFilterValues.Create("BBB"));

        // Then
        actual.Should().Be(false);
    }
    
    [Fact]
    public void Matches_AnyFilter_Ignored()
    {
        // Given
        var filters = new StringsFilterValues(null, "BBB", "BBB", "BBB");
        var rule = CreateRule(filters);

        // When
        var actual = rule.Matches(StringsFilterValues.Create("BBB"));

        // Then
        actual.Should().Be(true);
    }

    private static StringsRule CreateRule(StringsFilterValues filterValues)
    {
        return new StringsRule(1, 10, 1, filterValues);
    }
}