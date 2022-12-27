// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Wavecell.RuleEngine.Benchmarks;

BenchmarkRunner.Run<StringsRuleEngineBenchmarks>();

BenchmarkRunner.Run<MixedRuleEngineBenchmarks>();
