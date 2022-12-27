using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public interface IStringsRule
{
    ushort Priority { get; }
    bool Matches(StringsFilterValues filterValues);
}

public class StringsRule : IStringsRule
{
    [UsedImplicitly] public int RuleId { get; }
    public ushort Priority { get; }
    [UsedImplicitly] public int? OutputValue { [UsedImplicitly] get; }

    private readonly StringsFilterValues _filters;

    public StringsRule(int ruleId, ushort priority, int? outputValue, StringsFilterValues filterValues)
    {
        RuleId = ruleId;
        Priority = priority;
        OutputValue = outputValue;

        _filters = filterValues;
    }

    public bool Matches(StringsFilterValues filterValues)
    {
        if (_filters.Filter1 != null & _filters.Filter1 != filterValues.Filter1) return false;
        if (_filters.Filter2 != null & _filters.Filter2 != filterValues.Filter2) return false;
        if (_filters.Filter3 != null & _filters.Filter3 != filterValues.Filter3) return false;
        if (_filters.Filter4 != null & _filters.Filter4 != filterValues.Filter4) return false;

        return true;
    }
}