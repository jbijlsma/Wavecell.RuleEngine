using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Wavecell.RuleEngine.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class StringsRuleEngineBenchmarks
{
    private static StringsRuleSetEngine? _largeSimulationEngine;
    private static StringsFilterValues? _largeSimulationFilterValues;
    
    private static StringsRuleSetEngine? _fileSimulationEngine;
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
        
        _largeSimulationEngine = new StringsRuleSetEngine(allRules);
        
        _largeSimulationFilterValues = StringsFilterValues.Create("XXX");
    }
    
    private static void CreateFileSimulation()
    {
        var ruleFile = Path.Combine(Environment.CurrentDirectory, "StringsRuleSet.csv");
        var loader = new RuleSetFileLoader(ruleFile);
        var rules = new StringsRuleSetFileReader(loader).Read();
        _fileSimulationEngine = new StringsRuleSetEngine(rules);
        _fileSimulationFilterValues = StringsFilterValues.Create("BBB");
    }

    private static StringsRule CreateTestRule(ushort priority, StringsFilterValues filters)
    {
        return new StringsRule(1, priority, 10, filters);
    }

    [Benchmark]
#pragma warning disable CA1822
    // Necessary for Benchmark .NET
    public void LargeSimulation_FindRule()
#pragma warning restore CA1822
    {
        _largeSimulationEngine!.FindRule(_largeSimulationFilterValues!);
    }
    
    [Benchmark]
#pragma warning disable CA1822
    // Necessary for Benchmark .NET
    public void RuleFile_FindRule()
#pragma warning restore CA1822
    {
        _fileSimulationEngine!.FindRule(_fileSimulationFilterValues!);
    }
}