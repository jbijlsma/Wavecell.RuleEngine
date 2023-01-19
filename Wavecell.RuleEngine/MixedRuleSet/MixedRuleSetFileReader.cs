namespace Wavecell.RuleEngine.MixedRuleSet;

public class MixedRuleSetFileReader : RuleSetFileReader<MixedFilterValues>
{
    private const string AnyFilter = "<ANY>";
    
    protected override string ExpectedHeader => "RuleId,Priority,Filter1,Filter2,Filter3,OutputValue";
    protected override int OutputValueIndex => 5;

    public MixedRuleSetFileReader(IRuleSetFileLoader loader) : base(loader)
    {
    }

    protected override MixedFilterValues CreateFilterValues(string[] parts)
    {
        var intFilter = GetFilterValue(parts[2], int.Parse);
        var boolFilter = GetFilterValue(parts[3], bool.Parse);
        var stringFilter = parts[4] == AnyFilter ? null : parts[4];

        return new MixedFilterValues(intFilter, boolFilter, stringFilter);
    }

    private static T? GetFilterValue<T>(string stringValue, Func<string, T> converter) where T : struct
    {
        return stringValue == AnyFilter ? null : converter(stringValue);
    }
}