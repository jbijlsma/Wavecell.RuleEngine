using JetBrains.Annotations;

namespace Wavecell.RuleEngine.StringsRuleSet;

public class StringsFilterValues : IFilterValues<StringsFilterValues>
{
    [UsedImplicitly] public string? Filter1 { get; }
    [UsedImplicitly] public string? Filter2 { get; }
    [UsedImplicitly] public string? Filter3 { get; }
    [UsedImplicitly] public string? Filter4 { get; }

    public StringsFilterValues(string? filter1 = null, string? filter2 = null, string? filter3 = null, string? filter4 = null)
    {
        Filter1 = filter1;
        Filter2 = filter2;
        Filter3 = filter3;
        Filter4 = filter4;
    }

    public string GetCacheKey()
    {
        return $"{nameof(Filter1)}:{Filter1 ?? "<null>"}&{nameof(Filter2)}:{Filter2 ?? "<null>"}&{nameof(Filter3)}:{Filter3 ?? "<null>"}&{nameof(Filter4)}:{Filter4 ?? "<null>"}";
    }

    public bool Matches(StringsFilterValues filterValues)
    {
        if (Filter1 != null & Filter1 != filterValues.Filter1) return false;
        if (Filter2 != null & Filter2 != filterValues.Filter2) return false;
        if (Filter3 != null & Filter3 != filterValues.Filter3) return false;
        if (Filter4 != null & Filter4 != filterValues.Filter4) return false;

        return true;
    }
    
    public static StringsFilterValues Create(string? filterValue)
    {
        return new StringsFilterValues(filterValue, filterValue, filterValue, filterValue);
    }
}