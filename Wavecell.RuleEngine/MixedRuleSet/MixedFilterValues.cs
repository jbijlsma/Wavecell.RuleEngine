using JetBrains.Annotations;

namespace Wavecell.RuleEngine.MixedRuleSet;

public class MixedFilterValues : IEquatable<MixedFilterValues>
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
    
    public bool Equals(MixedFilterValues? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (Filter1 != null && Filter1 != other.Filter1) return false;
        if (Filter2 != null && Filter2 != other.Filter2) return false;
        if (Filter3 != null && Filter3 != other.Filter3) return false;

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MixedFilterValues)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Filter1, Filter2, Filter3);
    }
}