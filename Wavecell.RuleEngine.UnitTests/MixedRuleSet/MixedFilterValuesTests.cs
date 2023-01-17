using FluentAssertions;
using Wavecell.RuleEngine.MixedRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests.MixedRuleSet;

public class MixedFilterValuesTests
{
    [Theory]
    [InlineData(111, true, "AAA", true)]
    [InlineData(222, true, "AAA", false)]
    [InlineData(111, false, "AAA", false)]
    [InlineData(111, true, "BBB", false)]
    public void Equals_True(int? filter1, bool? filter2, string filter3, bool expected)
    {
        // Given
        var filters = new MixedFilterValues(111, true, "AAA");
        var filterValues = new MixedFilterValues(filter1, filter2, filter3);
        
        // When
        var actual = filters.Equals(filterValues);

        // Then
        actual.Should().Be(expected);
    }

    [Fact]
    public void Equals_Ignores_NullFilters()
    {
        // Given
        var filters = new MixedFilterValues();
        var filterValues = new MixedFilterValues(111, true, "AAA");
        
        // When
        var actual = filters.Equals(filterValues);

        // Then
        actual.Should().Be(true);
    }

    [Fact]
    public void Create_SetsAllFilterValues()
    {
        // Given
        const int intFilter = 111;
        const bool boolFilter = true;
        const string stringFilter = "stringFilter";
        
        // When
        var actual = new MixedFilterValues(intFilter, boolFilter, stringFilter);

        // Then
        actual.Filter1.Should().Be(intFilter);
        actual.Filter2.Should().Be(boolFilter);
        actual.Filter3.Should().Be(stringFilter);
    }
}