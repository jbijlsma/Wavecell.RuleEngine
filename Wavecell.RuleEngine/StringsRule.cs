using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public class StringsRule
{
    public int RuleId { [UsedImplicitly] get; init; }
    public ushort Priority { [UsedImplicitly] get; init; }
    public int? OutputValue { [UsedImplicitly] get; init; }

    public string Filter1 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter2 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter3 { [UsedImplicitly] get; init; } = string.Empty;
    public string Filter4 { [UsedImplicitly] get; init; } = string.Empty;
}