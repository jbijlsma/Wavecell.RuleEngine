namespace Wavecell.RuleEngine;

public class StringsRuleSetFileLoader
{
    private const string ExpectedHeader = "RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue";
    
    public IEnumerable<StringsRule> Load(string ruleFileFullPath)
    {
        using var sr = new StreamReader(ruleFileFullPath);
            
        var headerLine = sr.ReadLine()!;
        if (headerLine != ExpectedHeader)
        {
            throw new ApplicationException($"Expected header {ExpectedHeader}, but found {headerLine}");
        }

        var rules = new List<StringsRule>();
        while (sr.ReadLine() is { } line)
        {
            rules.Add(CreateRule(line));
        }

        return rules;
    }

    private static StringsRule CreateRule(string line)
    {
        var parts = line.Split(',');

        return new StringsRule
        {
            RuleId = Convert(parts[0], int.Parse),
            Priority = Convert(parts[1], ushort.Parse),
            OutputValue = Convert(parts[6], value => string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value)),
            Filter1 = parts[2],
            Filter2 = parts[3],
            Filter3 = parts[4],
            Filter4 = parts[5]
        };
    }

    private static T Convert<T>(string stringValue, Func<string, T> converter)
    {
        return converter(stringValue);
    }
}