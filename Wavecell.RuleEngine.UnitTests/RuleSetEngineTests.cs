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
        var filters = StringsFilterValues.Create("AAA");
        
        var lowPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        lowPriorityMatchingRule.Priority.Returns((ushort)10);
        lowPriorityMatchingRule.Matches(filters).Returns(true);
        
        var highPriorityMatchingRule = Substitute.For<IRule<StringsFilterValues>>();
        highPriorityMatchingRule.Priority.Returns((ushort)20);
        highPriorityMatchingRule.Matches(filters).Returns(true);

        var rules = new[] { lowPriorityMatchingRule, highPriorityMatchingRule };
        var engine = new RuleSetEngine<StringsFilterValues>(rules);

        var filterValues = filters;

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

        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(filters));
        
        var engine = new RuleSetEngine<StringsFilterValues>(nonMatchingRules);

        var filterValues = filters;

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(null);
    }

    private static IRule<StringsFilterValues> CreateNonMatchingRule(StringsFilterValues filterValues)
    {
        var rule = Substitute.For<IRule<StringsFilterValues>>();
        rule.Matches(filterValues).Returns(false);
        
        return rule;
    }
}