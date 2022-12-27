namespace Wavecell.RuleEngine;

public interface IFilterValues<in TFilterValues> where TFilterValues : IFilterValues<TFilterValues>
{
    bool Matches(TFilterValues filterValues);
}