namespace Wavecell.RuleEngine;

public interface IRuleCache<TFilterValues>
    where TFilterValues : IFilterValues<TFilterValues>
{
    void Add(TFilterValues key, IRule<TFilterValues>? rule);
    (bool, IRule<TFilterValues>?) TryGet(TFilterValues key);
}

public class RuleCache<TFilterValues> : IRuleCache<TFilterValues>
    where TFilterValues : IFilterValues<TFilterValues>
{
    private readonly Dictionary<string, IRule<TFilterValues>?> _cache = new();

    public void Add(TFilterValues key, IRule<TFilterValues>? rule)
    {
        _cache.Add(key.GetCacheKey(), rule);
    }

    public (bool, IRule<TFilterValues>?) TryGet(TFilterValues key)
    {
        return _cache.TryGetValue(key.GetCacheKey(), out var rule) ? (true, rule) : (false, null);
    }
}