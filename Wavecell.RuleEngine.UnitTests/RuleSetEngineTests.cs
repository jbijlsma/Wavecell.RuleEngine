using FluentAssertions;
using NSubstitute;
using Wavecell.RuleEngine.StringsRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class RuleSetTests
{
    [Fact]
    public void FindRule_SingleMatch_Returns_MatchingRule()
    {
        // Given
        var filters = StringsFilterValues.Create("AAA");
        
        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(filters));

        var matchingRule = Substitute.For<IRule<StringsFilterValues>>();
        matchingRule.Matches(filters).Returns(true);

        var rules = nonMatchingRules.Concat(new[] { matchingRule });

        var cache = Substitute.For<IRuleCache<StringsFilterValues>>();
        
        var engine = new RuleSetEngine<StringsFilterValues>(rules, cache);

        // When
        var actual = engine.FindRule(filters);

        // Then
        actual.Should().Be(matchingRule);
        cache.Received(1).Add(filters, actual);
    }
    
    [Fact]
    public void FindRule_MultipleMatchingRules_Returns_HighestPriority()
    {
        // Given
        var filters = StringsFilterValues.Create("AAA");
        
        var lowPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        lowPriorityMatchingRule.Priority.Returns((ushort)10);
        lowPriorityMatchingRule.Matches(filters).Returns(true);
        
        var highPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        highPriorityMatchingRule.Priority.Returns((ushort)20);
        highPriorityMatchingRule.Matches(filters).Returns(true);

        var rules = new[] { lowPriorityMatchingRule, highPriorityMatchingRule };
        var cache = Substitute.For<IRuleCache<StringsFilterValues>>();
        var engine = new RuleSetEngine<StringsFilterValues>(rules, cache);

        var filterValues = filters;

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(highPriorityMatchingRule);
        cache.Received(1).Add(filterValues, actual);
    }

    [Fact]
    public void FindRule_NoMatchingRules_Returns_Null()
    {
        // Given
        var filters = StringsFilterValues.Create("AAA"); 

        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(filters));
        
        var cache = Substitute.For<IRuleCache<StringsFilterValues>>();
        
        var engine = new RuleSetEngine<StringsFilterValues>(nonMatchingRules, cache);

        var filterValues = filters;

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(null);
        cache.Received(1).Add(filterValues, actual);
    }

    [Fact]
    public void FindRule_FoundInCache_Returns_CachedRule()
    {
        // Given
        var filterValues = new StringsFilterValues();
        
        var cache = Substitute.For<IRuleCache<StringsFilterValues>>();
        var cachedRule = Substitute.For<IRule<StringsFilterValues>>();
        cache.TryGet(filterValues).Returns((true, cachedRule));
        
        var engine = new RuleSetEngine<StringsFilterValues>(Array.Empty<IRule<StringsFilterValues>>(), cache);

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        Assert.Equal(cachedRule, actual);
    }

    private static IRule<StringsFilterValues> CreateNonMatchingRule(StringsFilterValues filterValues)
    {
        var rule = Substitute.For<IRule<StringsFilterValues>>();
        rule.Matches(filterValues).Returns(false);
        
        return rule;
    }
}