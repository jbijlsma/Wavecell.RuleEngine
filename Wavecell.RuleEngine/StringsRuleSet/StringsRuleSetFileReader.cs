namespace Wavecell.RuleEngine.StringsRuleSet;

public class StringsRuleSetFileReader : RuleSetFileReader<StringsFilterValues>
{
    private const string AnyFilter = "<ANY>";
    
    protected override string ExpectedHeader => "RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue";

    public StringsRuleSetFileReader(IRuleSetFileLoader loader) : base(loader)
    {
    }
    
    protected override Rule<StringsFilterValues> CreateRule(string line)
    {
        var parts = line.Split(',');
        
        var ruleId = int.Parse(parts[0]);
        var priority = ushort.Parse(parts[1]);
        var outputValue = Convert(parts[6], int.Parse);
        
        var filters = new StringsFilterValues(GetFilter(parts[2]), GetFilter(parts[3]), GetFilter(parts[4]), GetFilter(parts[5]));
        
        return new Rule<StringsFilterValues>(ruleId, priority, outputValue, filters);
    }

    private static T? Convert<T>(string stringValue, Func<string, T> converter) where T : struct
    {
        return stringValue == AnyFilter ? null : converter(stringValue);
    }

    private static string? GetFilter(string filter)
    {
        return filter == AnyFilter ? null : filter;
    }
}