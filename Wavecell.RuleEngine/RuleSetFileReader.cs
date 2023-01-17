namespace Wavecell.RuleEngine;

public abstract class RuleSetFileReader<TFilterValues> where TFilterValues : IEquatable<TFilterValues>
{
    private readonly IRuleSetFileLoader _loader;
    
    protected abstract string ExpectedHeader { get; }
    protected abstract int OutputValueIndex { get; }

    protected abstract TFilterValues CreateFilterValues(string[] lineParts);
    
    protected RuleSetFileReader(IRuleSetFileLoader loader)
    {
        _loader = loader;
    }
    
    public IEnumerable<Rule<TFilterValues>> Read()
    {
        _loader.Load();
        _loader.ValidateHeader(ExpectedHeader);

        return _loader.Lines.Select(CreateRule);
    }
    
    private Rule<TFilterValues> CreateRule(string line)
    {
        var parts = line.Split(',');
        
        var ruleId = int.Parse(parts[0]);
        var priority = ushort.Parse(parts[1]);
        
        var outputValue = string.IsNullOrWhiteSpace(parts[5]) ? (int?)null : int.Parse(parts[OutputValueIndex]);

        var filters = CreateFilterValues(parts);

        return new Rule<TFilterValues>(ruleId, priority, outputValue, filters);
    }
}