using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Wavecell.RuleEngine.StringsRuleSet;

namespace Wavecell.RuleEngine.Benchmarks;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class StringsRuleEngineBenchmarks
{
    private static RuleSetEngine<StringsFilterValues>? _largeSimulationEngine;
    private static StringsFilterValues? _largeSimulationFilterValues;
    
    private static RuleSetEngine<StringsFilterValues>? _fileSimulationEngine;
    private static StringsFilterValues? _fileSimulationFilterValues;

    static StringsRuleEngineBenchmarks()
    {
        CreateLargeSimulation();
        CreateFileSimulation();
    }

    private static void CreateLargeSimulation()
    {
        const int numberOfNonMatchingRules = 1000;

        var filters = StringsFilterValues.Create("AAA");
        var nonMatchingRules = Enumerable.Range(0, numberOfNonMatchingRules).Select(_ => CreateTestRule(10, filters));

        var anyFilters = new StringsFilterValues();
        var matchingRule = CreateTestRule(0, anyFilters);
        
        var allRules = nonMatchingRules.Concat(new [] { matchingRule });
        
        _largeSimulationEngine = new RuleSetEngine<StringsFilterValues>(allRules, new RuleCache<StringsFilterValues>());
        _largeSimulationFilterValues = StringsFilterValues.Create("XXX");
    }
    
    private static void CreateFileSimulation()
    {
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "StringsRuleSet.csv");
        var loader = new RuleSetFileLoader(ruleFile);
        var rules = new StringsRuleSetFileReader(loader).Read();
        
        _fileSimulationEngine = new RuleSetEngine<StringsFilterValues>(rules, new RuleCache<StringsFilterValues>());
        _fileSimulationFilterValues = StringsFilterValues.Create("BBB");
    }

    private static Rule<StringsFilterValues> CreateTestRule(ushort priority, StringsFilterValues filters)
    {
        return new Rule<StringsFilterValues>(1, priority, 10, filters);
    }

    [Benchmark]
    public void LargeSimulation_FindRule()
    {
        _largeSimulationEngine!.FindRule(_largeSimulationFilterValues!);
    }
    
    [Benchmark]
    public void RuleFile_FindRule()
    {
        _fileSimulationEngine!.FindRule(_fileSimulationFilterValues!);
    }
}