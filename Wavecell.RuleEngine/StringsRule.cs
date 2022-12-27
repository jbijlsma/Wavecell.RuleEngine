using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public interface IStringsRule
{
    ushort Priority { get; }
    bool Matches(string filter1, string filter2, string filter3, string filter4);
}

public class StringsRule : IStringsRule
{
    private const string AnyRule = "<ANY>";
    
    public int RuleId { [UsedImplicitly] get; init; }
    public ushort Priority { [UsedImplicitly] get; init; }
    public int? OutputValue { [UsedImplicitly] get; init; }

    public string Filter1 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter2 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter3 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter4 { [UsedImplicitly] get; init; } = string.Empty;

    public bool Matches(string filter1, string filter2, string filter3, string filter4)
    {
        if (Filter1 != AnyRule && filter1 != Filter1) return false;
        if (Filter2 != AnyRule && filter2 != Filter2) return false;
        if (Filter3 != AnyRule && filter3 != Filter3) return false;
        if (Filter4 != AnyRule && filter4 != Filter4) return false;

        return true;
    }
}