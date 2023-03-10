using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Wavecell.RuleEngine.MixedRuleSet;

namespace Wavecell.RuleEngine.Benchmarks;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class MixedRuleEngineBenchmarks
{
    private static RuleSetEngine<MixedFilterValues>? _largeSimulationEngine;
    private static MixedFilterValues? _largeSimulationFilterValues;
    
    private static RuleSetEngine<MixedFilterValues>? _fileSimulationEngine;
    private static MixedFilterValues? _fileSimulationFilterValues;

    static MixedRuleEngineBenchmarks()
    {
        CreateLargeSimulation();
        CreateFileSimulation();
    }

    private static void CreateLargeSimulation()
    {
        const int numberOfNonMatchingRules = 1000;

        var filters = new MixedFilterValues(111, true, "AAA");
        var nonMatchingRules = Enumerable.Range(0, numberOfNonMatchingRules).Select(_ => CreateTestRule(10, filters));

        var anyFilters = new MixedFilterValues();
        var matchingRule = CreateTestRule(0, anyFilters);
        
        var allRules = nonMatchingRules.Concat(new [] { matchingRule });
        
        _largeSimulationEngine = new RuleSetEngine<MixedFilterValues>(allRules, new RuleCache<MixedFilterValues>());
        
        _largeSimulationFilterValues = new MixedFilterValues(444, true, "XXX");
    }
    
    private static void CreateFileSimulation()
    {
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "MixedRuleSet.csv");
        var loader = new RuleSetFileLoader(ruleFile);
        var rules = new MixedRuleSetFileReader(loader).Read();
        
        _fileSimulationEngine = new RuleSetEngine<MixedFilterValues>(rules, new RuleCache<MixedFilterValues>());
        _fileSimulationFilterValues = new MixedFilterValues(444, true, "XXX");
    }

    private static Rule<MixedFilterValues> CreateTestRule(ushort priority, MixedFilterValues filters)
    {
        return new Rule<MixedFilterValues>(1, priority, 10, filters);
    }

    [Benchmark]
    public void LargeSimulation_FindRule()
    {
        _largeSimulationEngine!.FindRule(_largeSimulationFilterValues!);
    }
    
    [Benchmark]
    public void RulesFile_FindRule()
    {
        _fileSimulationEngine!.FindRule(_fileSimulationFilterValues!);
    }
}