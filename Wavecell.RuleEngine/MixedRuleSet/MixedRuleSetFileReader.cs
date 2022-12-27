namespace Wavecell.RuleEngine.MixedRuleSet;

public class MixedRuleSetFileReader : RuleSetFileReader<MixedFilterValues>
{
    private const string AnyFilter = "<ANY>";
    
    protected override string ExpectedHeader => "RuleId,Priority,Filter1,Filter2,Filter3,OutputValue";

    public MixedRuleSetFileReader(IRuleSetFileLoader loader) : base(loader)
    {
    }
    
    protected override Rule<MixedFilterValues> CreateRule(string line)
    {
        var parts = line.Split(',');
        
        var ruleId = int.Parse(parts[0]);
        var priority = ushort.Parse(parts[1]);
        var outputValue = string.IsNullOrWhiteSpace(parts[5]) ? (int?)null : int.Parse(parts[5]);

        var intFilter = GetFilterValue(parts[2], int.Parse);
        var boolFilter = GetFilterValue(parts[3], bool.Parse);
        var stringFilter = parts[4] == AnyFilter ? null : parts[4];
        
        var filters = new MixedFilterValues(intFilter, boolFilter, stringFilter);
        
        return new Rule<MixedFilterValues>(ruleId, priority, outputValue, filters);
    }

    private static T? GetFilterValue<T>(string stringValue, Func<string, T> converter) where T : struct
    {
        return stringValue == AnyFilter ? null : converter(stringValue);
    }
}