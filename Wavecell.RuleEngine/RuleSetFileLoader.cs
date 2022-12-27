namespace Wavecell.RuleEngine;

public interface IRuleSetFileLoader
{ 
    void Load();
    void ValidateHeader(string expectedHeader);
    List<string> Lines { get; }
}

public class RuleSetFileLoader : IRuleSetFileLoader
{
    private readonly string _ruleFileFullPath;
    private string _headerLine = string.Empty;

    public List<string> Lines { get; } = new();
    
    public RuleSetFileLoader(string ruleFileFullPath)
    {
        _ruleFileFullPath = ruleFileFullPath;
    }

    public void Load()
    {
        using var sr = new StreamReader(_ruleFileFullPath);
            
        _headerLine = sr.ReadLine()!;
        
        while (sr.ReadLine() is { } line)
        {
            Lines.Add(line);
        }
    }

    public void ValidateHeader(string expectedHeader)
    {
        if (_headerLine != expectedHeader)
        {
            throw new ApplicationException($"Expected header {expectedHeader}, but found {_headerLine}");
        }
    }
}