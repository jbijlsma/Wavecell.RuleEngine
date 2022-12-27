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
        const string f1 = "AAA";
        const string f2 = "BBB";
        const string f3 = "CCC";
        const string f4 = "DDD";
        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(f1, f2, f3, f4));

        var matchingRule = Substitute.For<IStringsRule>();
        matchingRule.Matches(f1, f2, f3, f4).Returns(true);

        var rules = nonMatchingRules.Concat(new[] { matchingRule });
        var engine = new StringsRuleSetEngine(rules);

        // When
        var actual = engine.FindRule(f1, f2, f3, f4);

        // Then
        actual.Should().Be(matchingRule);
    }
    
    [Fact]
    public void FindRule_MultipleMatchingRules_Returns_HighestPriority()
    {
        // Given
        const string f1 = "AAA";
        const string f2 = "BBB";
        const string f3 = "CCC";
        const string f4 = "DDD";

        var lowPriorityMatchingRule = Substitute.For<IStringsRule>();
        lowPriorityMatchingRule.Priority.Returns((ushort)10);
        lowPriorityMatchingRule.Matches(f1, f2, f3, f4).Returns(true);
        
        var highPriorityMatchingRule = Substitute.For<IStringsRule>();
        highPriorityMatchingRule.Priority.Returns((ushort)20);
        highPriorityMatchingRule.Matches(f1, f2, f3, f4).Returns(true);

        var rules = new[] { lowPriorityMatchingRule, highPriorityMatchingRule };
        var engine = new StringsRuleSetEngine(rules);

        // When
        var actual = engine.FindRule(f1, f2, f3, f4);

        // Then
        actual.Should().Be(highPriorityMatchingRule);
    }

    [Fact]
    public void FindRule_NoMatchingRules_Returns_Null()
    {
        // Given
        const string f1 = "AAA";
        const string f2 = "BBB";
        const string f3 = "CCC";
        const string f4 = "DDD";

        var nonMatchingRules = Enumerable.Range(0, 5).Select(_ => CreateNonMatchingRule(f1, f2, f3, f4));
        
        var engine = new StringsRuleSetEngine(nonMatchingRules);

        // When
        var actual = engine.FindRule(f1, f2, f3, f4);

        // Then
        actual.Should().Be(null);
    }

    private static IStringsRule CreateNonMatchingRule(string f1, string f2, string f3, string f4)
    {
        var rule = Substitute.For<IStringsRule>();
        rule.Matches(f1, f2, f3, f4).Returns(false);
        
        return rule;
    }
}