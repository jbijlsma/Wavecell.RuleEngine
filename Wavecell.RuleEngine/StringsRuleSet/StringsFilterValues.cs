using JetBrains.Annotations;

namespace Wavecell.RuleEngine.StringsRuleSet;

public class StringsFilterValues : IEquatable<StringsFilterValues>
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

    public static StringsFilterValues Create(string? filterValue)
    {
        return new StringsFilterValues(filterValue, filterValue, filterValue, filterValue);
    }

    public bool Equals(StringsFilterValues? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        if (Filter1 != null & Filter1 != other.Filter1) return false;
        if (Filter2 != null & Filter2 != other.Filter2) return false;
        if (Filter3 != null & Filter3 != other.Filter3) return false;
        if (Filter4 != null & Filter4 != other.Filter4) return false;

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((StringsFilterValues)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Filter1, Filter2, Filter3, Filter4);
    }
}