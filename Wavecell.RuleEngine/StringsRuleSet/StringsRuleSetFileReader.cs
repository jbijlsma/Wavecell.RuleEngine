namespace Wavecell.RuleEngine.StringsRuleSet;

public class StringsRuleSetFileReader : RuleSetFileReader<StringsFilterValues>
{
    private const string AnyFilter = "<ANY>";
    
    protected override string ExpectedHeader => "RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue";
    protected override int OutputValueIndex => 6;

    public StringsRuleSetFileReader(IRuleSetFileLoader loader) : base(loader)
    {
    }

    protected override StringsFilterValues CreateFilterValues(string[] parts)
    {
        return new StringsFilterValues(GetFilter(parts[2]), GetFilter(parts[3]), GetFilter(parts[4]), GetFilter(parts[5]));
    }

    private static string? GetFilter(string filter)
    {
        return filter == AnyFilter ? null : filter;
    }
}