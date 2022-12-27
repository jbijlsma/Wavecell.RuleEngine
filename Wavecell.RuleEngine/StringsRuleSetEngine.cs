namespace Wavecell.RuleEngine;

public class StringsRuleSetEngine
{
    private readonly IEnumerable<IStringsRule> _rules;

    public StringsRuleSetEngine(IEnumerable<IStringsRule> rules)
    {
        _rules = rules.OrderByDescending(rule => rule.Priority);
    }
    
    public IStringsRule? FindRule(StringsFilterValues filterValues)
    {
        foreach (var rule in _rules)
        {
            if (rule.Matches(filterValues)) return rule;
        }

        return null;
    }
}