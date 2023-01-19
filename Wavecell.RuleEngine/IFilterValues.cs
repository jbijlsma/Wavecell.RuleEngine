namespace Wavecell.RuleEngine;

public interface IFilterValues<in TFilterValues> where TFilterValues : IFilterValues<TFilterValues>
{
    string GetCacheKey();
    bool Matches(TFilterValues filterValues);
}