namespace Wavecell.RuleEngine;

public class RuleSetEngine<TFilterValues>
    where TFilterValues : IFilterValues<TFilterValues>
{
    private readonly IEnumerable<IRule<TFilterValues>> _rules;

    public RuleSetEngine(IEnumerable<IRule<TFilterValues>> rules)
    {
        _rules = rules.OrderByDescending(rule => rule.Priority);
    }
    
    public IRule<TFilterValues>? FindRule(TFilterValues filterValues)
    {
        foreach (var rule in _rules)
        {
            if (rule.Matches(filterValues)) return rule;
        }

        return null;
    }
}