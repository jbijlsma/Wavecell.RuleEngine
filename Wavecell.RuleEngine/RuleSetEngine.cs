namespace Wavecell.RuleEngine;

public class RuleSetEngine<TFilterValues>
    where TFilterValues : IEquatable<TFilterValues>
{
    private readonly Dictionary<TFilterValues, IRule<TFilterValues>> _rulesByFilterValuesHash;

    public RuleSetEngine(IEnumerable<IRule<TFilterValues>> rules)
    {
        _rulesByFilterValuesHash = rules.ToDictionary(rule => rule.Filters, rule => rule);
    }
    
    public IRule<TFilterValues>? FindRule(TFilterValues filterValues)
    {
        return _rulesByFilterValuesHash.TryGetValue(filterValues, out var rule) ? rule : null;
    }
}