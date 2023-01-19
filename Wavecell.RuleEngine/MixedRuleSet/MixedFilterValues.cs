using JetBrains.Annotations;

namespace Wavecell.RuleEngine.MixedRuleSet;

public class MixedFilterValues : IFilterValues<MixedFilterValues>
{
    [UsedImplicitly] public int? Filter1 { get; }
    [UsedImplicitly] public bool? Filter2 { get; }
    [UsedImplicitly] public string? Filter3 { get; }

    public MixedFilterValues(int? filter1 = null, bool? filter2 = null, string? filter3 = null)
    {
        Filter1 = filter1;
        Filter2 = filter2;
        Filter3 = filter3;
    }
    
    public string GetCacheKey()
    {
        return $"{nameof(Filter1)}:{(Filter1.HasValue ? Filter1.ToString() : "<null>")}" +
               $"&{nameof(Filter2)}:{(Filter2.HasValue ? Filter2.ToString() : "<null>")}" +
               $"&{nameof(Filter3)}:{Filter3 ?? "<null>"}";
    }

    public bool Matches(MixedFilterValues filterValues)
    {
        if (Filter1 != null & Filter1 != filterValues.Filter1) return false;
        if (Filter2 != null & Filter2 != filterValues.Filter2) return false;
        if (Filter3 != null & Filter3 != filterValues.Filter3) return false;

        return true;
    }
}