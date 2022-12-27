using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class StringsRuleSetTests
{
    [Fact]
    public void FindRule_SingleMatch_Returns_MatchingRule()
    {
        // Given
        var filters = Enumerable.Range(0, 4).Select(_ => "AAA").ToArray(); 
        
        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(filters));

        var matchingRule = Substitute.For<IStringsRule>();
        matchingRule.Matches(filters).Returns(true);

        var rules = nonMatchingRules.Concat(new[] { matchingRule });
        
        var engine = new StringsRuleSetEngine(rules);

        // When
        var actual = engine.FindRule(filters);

        // Then
        actual.Should().Be(matchingRule);
    }
    
    [Fact]
    public void FindRule_MultipleMatchingRules_Returns_HighestPriority()
    {
        // Given
        var filters = Enumerable.Range(0, 4).Select(_ => "AAA").ToArray(); 
        
        var lowPriorityMatchingRule = Substitute.For<IStringsRule>();
        lowPriorityMatchingRule.Priority.Returns((ushort)10);
        lowPriorityMatchingRule.Matches(filters).Returns(true);
        
        var highPriorityMatchingRule = Substitute.For<IStringsRule>();
        highPriorityMatchingRule.Priority.Returns((ushort)20);
        highPriorityMatchingRule.Matches(filters).Returns(true);

        var rules = new[] { lowPriorityMatchingRule, highPriorityMatchingRule };
        var engine = new StringsRuleSetEngine(rules);

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
        var filters = Enumerable.Range(0, 4).Select(_ => "AAA").ToArray(); 

        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(filters));
        
        var engine = new StringsRuleSetEngine(nonMatchingRules);

        var filterValues = filters;

        // When
        var actual = engine.FindRule(filterValues);

        // Then
        actual.Should().Be(null);
    }

    private static IStringsRule CreateNonMatchingRule(string[] filterValues)
    {
        var rule = Substitute.For<IStringsRule>();
        rule.Matches(filterValues).Returns(false);
        
        return rule;
    }
}