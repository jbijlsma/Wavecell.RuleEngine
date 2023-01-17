using FluentAssertions;
using Wavecell.RuleEngine.StringsRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests.StringsRulesSet;

public class StringsFilterValuesTests
{
    [Fact]
    public void HashCode()
    {
        // Given
        var stringsFilterValues = new StringsFilterValues("AAA");
        var strings2FilterValues = new StringsFilterValues(null, "AAA");
        
        // Then
        Assert.NotEqual(stringsFilterValues.GetHashCode(), strings2FilterValues.GetHashCode());
    }
    
    [Theory]
    [InlineData("AAA", "BBB", "CCC", "DDD", true)]
    [InlineData("XXX", "BBB", "CCC", "DDD", false)]
    [InlineData("AAA", "XXX", "CCC", "DDD", false)]
    [InlineData("AAA", "BBB", "XXX", "DDD", false)]
    [InlineData("AAA", "BBB", "CCC", "XXX", false)]
    public void Equals_True(string? filter1, string filter2, string filter3, string filter4, bool expected)
    {
        // Given
        var filters = new StringsFilterValues("AAA", "BBB", "CCC", "DDD");
        var filterValues = new StringsFilterValues(filter1, filter2, filter3, filter4);
        
        // When
        var actual = filters.Equals(filterValues);

        // Then
        actual.Should().Be(expected);
    }

    [Fact]
    public void Equals_Ignores_NullFilters()
    {
        // Given
        var filters = new StringsFilterValues();
        var filterValues = new StringsFilterValues("AAA", "BBB", "CCC", "DDD");
        
        // When
        var actual = filters.Equals(filterValues);

        // Then
        actual.Should().Be(true);
    }

    [Fact]
    public void Create_SetsAllFilterValues()
    {
        // Given
        const string filterValue = "AAA";
        
        // When
        var actual = StringsFilterValues.Create(filterValue);

        // Then
        actual.Filter1.Should().Be(filterValue);
        actual.Filter2.Should().Be(filterValue);
        actual.Filter3.Should().Be(filterValue);
        actual.Filter4.Should().Be(filterValue);
    }
}