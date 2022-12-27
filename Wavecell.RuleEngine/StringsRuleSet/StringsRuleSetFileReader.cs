namespace Wavecell.RuleEngine.StringsRuleSet;

public class StringsRuleSetFileReader : RuleSetFileReader<StringsFilterValues>
{
    protected override string ExpectedHeader => "RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue";

    public StringsRuleSetFileReader(IRuleSetFileLoader loader) : base(loader)
    {
    }
    
    protected override Rule<StringsFilterValues> CreateRule(string line)
    {
        var parts = line.Split(',');
        
        var ruleId = Convert(parts[0], int.Parse);
        var priority = Convert(parts[1], ushort.Parse);
        var outputValue = Convert(parts[6], value => string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value));
        var filters = new StringsFilterValues(parts[2], parts[3], parts[4], parts[5]);
        
        return new Rule<StringsFilterValues>(ruleId, priority, outputValue, filters);
    }

    private static T Convert<T>(string stringValue, Func<string, T> converter)
    {
        return converter(stringValue);
    }
}