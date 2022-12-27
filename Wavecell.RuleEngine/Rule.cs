using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public interface IRule<in TFilterValues> where TFilterValues : IFilterValues<TFilterValues>
{
    ushort Priority { get; }
    bool Matches(TFilterValues filterValues);
}

public class Rule<TFilterValues> : IRule<TFilterValues> where TFilterValues : IFilterValues<TFilterValues>
{
    [UsedImplicitly] public int RuleId { get; }
    public ushort Priority { get; }
    [UsedImplicitly] public int? OutputValue { [UsedImplicitly] get; }

    private readonly TFilterValues _filters;

    public Rule(int ruleId, ushort priority, int? outputValue, TFilterValues filters)
    {
        RuleId = ruleId;
        Priority = priority;
        OutputValue = outputValue;

        _filters = filters;
    }

    public bool Matches(TFilterValues filterValues)
    {
        return _filters.Matches(filterValues);
    }
}