using FluentAssertions;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests;

public class RuleSetFileLoaderTests
{
    private readonly RuleSetFileLoader _loader;
    
    public RuleSetFileLoaderTests()
    {
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "StringsRuleSetTestData.csv");
        _loader = new RuleSetFileLoader(ruleFile);
    }
    
    [Fact]
    public void Load_SetsProperty_Lines()
    {
        // When
        _loader.Load();

        // Then
        var expected = new [] { "1,80,AAA,<ANY>,CCC,DDD,8", "2,10,<ANY>,<ANY>,AAA,<ANY>,1" };
        _loader.Lines.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void ValidateHeader_ValidHeader_Succeeds()
    {
        // Given
        _loader.Load();

        // When, then no exception
        _loader.ValidateHeader("RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue");
    }
    
    [Fact]
    public void ValidateHeader_InvalidHeader_ThrowException()
    {
        // Given
        _loader.Load();

        // When, then exception
        Assert.Throws<ApplicationException>(() => _loader.ValidateHeader("Invalid"));
    }
}