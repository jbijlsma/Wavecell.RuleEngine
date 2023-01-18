namespace Wavecell.RuleEngine;

public class RuleSetEngine<TFilterValues>
    where TFilterValues : IFilterValues<TFilterValues>
{
    private readonly IRuleCache<TFilterValues> _cache;
    private readonly IEnumerable<IRule<TFilterValues>> _rules;
    
    public RuleSetEngine(IEnumerable<IRule<TFilterValues>> rules, IRuleCache<TFilterValues> cache)
    {
        _cache = cache;
        _rules = rules.OrderByDescending(rule => rule.Priority);
    }
    
    public IRule<TFilterValues>? FindRule(TFilterValues filterValues)
    {
        var (found, matchingRule) = _cache.TryGet(filterValues);
        if (found) return matchingRule;

        foreach (var rule in _rules)
        {
            if (!rule.Matches(filterValues)) continue;
            matchingRule = rule;
            break;
        }
        
        _cache.Add(filterValues, matchingRule);
        
        return matchingRule;
    }
}