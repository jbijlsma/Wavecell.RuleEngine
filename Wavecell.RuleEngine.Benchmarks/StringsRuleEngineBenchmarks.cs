using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Wavecell.RuleEngine.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class StringsRuleEngineBenchmarks
{
    private static StringsRuleSetEngine? _largeSimulationEngine;
    private static string[] _largeSimulationFilterValues = Array.Empty<string>();
    
    private static StringsRuleSetEngine? _fileSimulationEngine;
    private static string[] _fileSimulationFilterValues = Array.Empty<string>();

    static StringsRuleEngineBenchmarks()
    {
        CreateLargeSimulation();
        CreateFileSimulation();
    }

    private static void CreateLargeSimulation()
    {
        const int numberOfFiltersPerRule = 100;
        const int numberOfNonMatchingRules = 1000;

        var filters = Enumerable.Range(0, numberOfFiltersPerRule).Select(_ => "AAA").ToArray();
        var nonMatchingRules = Enumerable.Range(0, numberOfNonMatchingRules).Select(_ => CreateTestRule(100, filters));

        var anyFilters = Enumerable.Range(0, numberOfFiltersPerRule).Select(_ => "<ANY>").ToArray();
        var matchingRule = CreateTestRule(0, anyFilters);
        
        var allRules = nonMatchingRules.Concat(new [] { matchingRule });
        
        _largeSimulationEngine = new StringsRuleSetEngine(allRules);
        
        _largeSimulationFilterValues = Enumerable.Range(0, numberOfFiltersPerRule).Select(_ => "XXX").ToArray();
    }
    
    private static void CreateFileSimulation()
    {
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "StringsRuleSet.csv");
        var loader = new RuleSetFileLoader(ruleFile);
        var rules = new StringsRuleSetFileReader(loader).Read();
        _fileSimulationEngine = new StringsRuleSetEngine(rules);
        _fileSimulationFilterValues = Enumerable.Range(0, 4).Select(_ => "BBB").ToArray();
    }
    
    private static StringsRule CreateTestRule(ushort priority, params string[] filters)
    {
        return new StringsRule(1, priority, 10, filters);
    }

    [Benchmark]
#pragma warning disable CA1822
    // Necessary for Benchmark .NET
    public void LargeSimulation_FindRule()
#pragma warning restore CA1822
    {
        _largeSimulationEngine!.FindRule(_largeSimulationFilterValues);
    }
    
    [Benchmark]
#pragma warning disable CA1822
    // Necessary for Benchmark .NET
    public void RuleFile_FindRule()
#pragma warning restore CA1822
    {
        _fileSimulationEngine!.FindRule(_fileSimulationFilterValues);
    }
}