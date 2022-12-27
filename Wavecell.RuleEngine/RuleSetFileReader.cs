namespace Wavecell.RuleEngine;

public abstract class RuleSetFileReader<TFilterValues> where TFilterValues : IFilterValues<TFilterValues>
{
    private readonly IRuleSetFileLoader _loader;
    
    protected abstract string ExpectedHeader { get; }
    protected abstract Rule<TFilterValues> CreateRule(string line);

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
}