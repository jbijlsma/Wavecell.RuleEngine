using NSubstitute;
using Wavecell.RuleEngine.StringsRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class RuleCacheTests
{
    [Fact]
    public void Add_And_Get()
    {
        // Given
        var cache = new RuleCache<StringsFilterValues>();

        var filterValues = new StringsFilterValues();
        var rule = Substitute.For<IRule<StringsFilterValues>>();
        
        // When
        cache.Add(filterValues, rule);

        // When
        var (found, actual) = cache.TryGet(filterValues);
        
        // Then
        Assert.True(found);
        Assert.Equal(rule, actual);
    }
}