using JetBrains.Annotations;

namespace Wavecell.RuleEngine;

public interface IStringsRule
{
    ushort Priority { get; }
    bool Matches(params string[] filters);
}

public class StringsRule : IStringsRule
{
    private const string AnyRule = "<ANY>";
    
    [UsedImplicitly] public int RuleId { get; }
    public ushort Priority { get; }
    [UsedImplicitly] public int? OutputValue { [UsedImplicitly] get; }

    private readonly string[] _filters;

    public StringsRule(int ruleId, ushort priority, int? outputValue, params string[] filters)
    {
        RuleId = ruleId;
        Priority = priority;
        OutputValue = outputValue;

        _filters = filters;
    }

    public bool Matches(params string[] filterValues)
    {
        for (var i=0; i<filterValues.Length; i++)
        {
            var filter = _filters[i];
            
            if (filter == AnyRule) continue;
            if (filter != filterValues[i]) return false;
        }
        
        return true;
    }
}