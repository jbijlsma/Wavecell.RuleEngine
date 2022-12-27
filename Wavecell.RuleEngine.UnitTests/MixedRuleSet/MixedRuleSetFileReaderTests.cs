using FluentAssertions;
using NSubstitute;
using Wavecell.RuleEngine.MixedRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests.MixedRuleSet;

public class MixedRuleSetFileReaderTests
{
    private readonly IRuleSetFileLoader _loader;
    private readonly MixedRuleSetFileReader _reader;
    
    public MixedRuleSetFileReaderTests()
    {
        _loader = Substitute.For<IRuleSetFileLoader>();
        _reader = new MixedRuleSetFileReader(_loader);
    }
    
    [Fact]
    public void Read()
    {
        // Given
        _loader.Lines.Returns(new List<string>
        {
            "1,80,111,true,AAA,8",
            "2,20,<ANY>,<ANY>,<ANY>,2"
        });
        
        // When
        var actual = _reader.Read();

        // Then
        var expected = new List<Rule<MixedFilterValues>>
        {
            new(1, 80, 8, new MixedFilterValues(111, true, "AAA")),
            new(2, 20, 2, new MixedFilterValues())
        };
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Load_ValidatesHeader()
    {
        // Given
        _loader.Lines.Returns(new List<string>());
        
        // When
        _reader.Read();

        // Then
        _loader.Received(1).ValidateHeader("RuleId,Priority,Filter1,Filter2,Filter3,OutputValue");
    }
}