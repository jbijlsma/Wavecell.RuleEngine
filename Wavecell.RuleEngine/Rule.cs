using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public interface IRule<out TFilterValues> where TFilterValues : IEquatable<TFilterValues>
{
    ushort Priority { get; }
    TFilterValues Filters { get; }
}

public class Rule<TFilterValues> : IRule<TFilterValues> where TFilterValues : IEquatable<TFilterValues>
{
    [UsedImplicitly] public int RuleId { get; }
    public ushort Priority { get; }
    [UsedImplicitly] public int? OutputValue { [UsedImplicitly] get; }
    [UsedImplicitly] public TFilterValues Filters { get; }

    public Rule(int ruleId, ushort priority, int? outputValue, TFilterValues filters)
    {
        RuleId = ruleId;
        Priority = priority;
        OutputValue = outputValue;

        Filters = filters;
    }
}