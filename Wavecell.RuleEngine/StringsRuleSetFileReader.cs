namespace Wavecell.RuleEngine;

public class StringsRuleSetFileReader
{
    internal const string ExpectedHeader = "RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue";
    
    private readonly IRuleSetFileLoader _loader;

    public StringsRuleSetFileReader(IRuleSetFileLoader loader)
    {
        _loader = loader;
    }
    
    public IEnumerable<StringsRule> Read()
    {
        _loader.Load();
        _loader.ValidateHeader(ExpectedHeader);

        return _loader.Lines.Select(CreateRule);
    }

    private static StringsRule CreateRule(string line)
    {
        var parts = line.Split(',');
        
        var ruleId = Convert(parts[0], int.Parse);
        var priority = Convert(parts[1], ushort.Parse);
        var outputValue = Convert(parts[6], value => string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value));
        var filters = new StringsFilterValues(parts[2], parts[3], parts[4], parts[5]);
        
        return new StringsRule(ruleId, priority, outputValue, filters);
    }

    private static T Convert<T>(string stringValue, Func<string, T> converter)
    {
        return converter(stringValue);
    }
}