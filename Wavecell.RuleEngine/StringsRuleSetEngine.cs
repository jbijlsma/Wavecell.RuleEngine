namespace Wavecell.RuleEngine;

public class StringsRuleSetEngine
{
    private readonly IEnumerable<IStringsRule> _rules;

    public StringsRuleSetEngine(IEnumerable<IStringsRule> rules)
    {
        _rules = rules.OrderByDescending(rule => rule.Priority);
    }
    
    public IStringsRule? FindRule(string f1, string f2, string f3, string f4)
    {
        foreach (var rule in _rules)
        {
            if (rule.Matches(f1, f2, f3, f4)) return rule;
        }

        return null;
    }
}