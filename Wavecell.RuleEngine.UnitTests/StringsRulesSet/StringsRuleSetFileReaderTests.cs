using FluentAssertions;
using NSubstitute;
using Wavecell.RuleEngine.StringsRuleSet;
using Xunit;

namespace Wavecell.RuleEngine.UnitTests.StringsRulesSet;

public class StringsRuleSetFileReaderTests
{
    private readonly IRuleSetFileLoader _loader;
    private readonly StringsRuleSetFileReader _reader;
    
    public StringsRuleSetFileReaderTests()
    {
        _loader = Substitute.For<IRuleSetFileLoader>();
        _reader = new StringsRuleSetFileReader(_loader);
    }
    
    [Fact]
    public void Read()
    {
        // Given
        _loader.Lines.Returns(new List<string>
        {
            "1,80,AAA,<ANY>,CCC,DDD,8",
            "2,10,<ANY>,<ANY>,AAA,<ANY>,1"
        });
        
        // When
        var actual = _reader.Read();

        // Then
        var expected = new List<Rule<StringsFilterValues>>
        {
            new(1, 80, 8, new StringsFilterValues("AAA", null, "CCC", "DDD")),
            new(2, 10, 1, new StringsFilterValues(null, null, "AAA"))
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
        _loader.Received(1).ValidateHeader("RuleId,Priority,Filter1,Filter2,Filter3,Filter4,OutputValue");
    }
}