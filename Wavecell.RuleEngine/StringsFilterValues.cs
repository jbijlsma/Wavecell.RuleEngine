namespace Wavecell.RuleEngine;

public class StringsFilterValues
{
    public string? Filter1 { get; }
    public string? Filter2 { get; }
    public string? Filter3 { get; }
    public string? Filter4 { get; }

    public StringsFilterValues(string? filter1 = null, string? filter2 = null, string? filter3 = null, string? filter4 = null)
    {
        Filter1 = filter1;
        Filter2 = filter2;
        Filter3 = filter3;
        Filter4 = filter4;
    }

    public static StringsFilterValues Create(string? filterValue)
    {
        return new StringsFilterValues(filterValue, filterValue, filterValue, filterValue);
    }

    public Dictionary<string, object?> ToDictionary()
    {
        return new Dictionary<string, object?>
        {
            { nameof(Filter1), Filter1 },
            { nameof(Filter2), Filter2 },
            { nameof(Filter3), Filter3 },
            { nameof(Filter4), Filter4 },
        };
    }
}