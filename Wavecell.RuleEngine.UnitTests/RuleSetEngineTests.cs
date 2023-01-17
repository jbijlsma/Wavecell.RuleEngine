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
        
        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule());

        var matchingRule = Substitute.For<IRule<StringsFilterValues>>();
        matchingRule.Filters.Returns(StringsFilterValues.Create("AAA"));

        var rules = nonMatchingRules.Concat(new[] { matchingRule });
        
        var engine = new RuleSetEngine<StringsFilterValues>(rules);

        // When
        var actual = engine.FindRule(filters);

        // Then
        actual.Should().Be(matchingRule);
    }
    
    [Fact]
    public void FindRule_MultipleMatchingRules_Returns_HighestPriority()
    {
        // Given
        var filterValues = new StringsFilterValues("AAA");
        
        var lowPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        lowPriorityMatchingRule.Priority.Returns((ushort)10);
        lowPriorityMatchingRule.Filters.Returns(StringsFilterValues.Create("AAA"));
        
        var highPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        highPriorityMatchingRule.Priority.Returns((ushort)20);
        highPriorityMatchingRule.Filters.Returns(new StringsFilterValues("AAA"));

        var rules = new[] { lowPriorityMatchingRule, highPriorityMatchingRule };
        var engine = new RuleSetEngine<StringsFilterValues>(rules);
        
        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(highPriorityMatchingRule);
    }

    [Fact]
    public void FindRule_NoMatchingRules_Returns_Null()
    {
        // Given
        var filters = StringsFilterValues.Create("AAA"); 

        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule());
        
        var engine = new RuleSetEngine<StringsFilterValues>(nonMatchingRules);

        var filterValues = filters;

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(null);
    }

    private static IRule<StringsFilterValues> CreateNonMatchingRule()
    {
        var rule = Substitute.For<IRule<StringsFilterValues>>();
        rule.Filters.Returns(StringsFilterValues.Create(Guid.NewGuid().ToString()));
        
        return rule;
    }
}