# Introduction

Implementation of a Rules Engine as part of recruiting process at 8x8.

The Wavecell.RuleEngine project contains 2 rulesets:

StringsRuleSet: the ruleset in the assignment with 4 string filters  
MixedRuleSet: a ruleset with an int, bool and string filter to show what needs to be done to create a new strongly typed ruleset  

# Benchmark testing

The Wavecell.RuleEngine.Benchmarks project uses BenchmarkDotNet (https://benchmarkdotnet.org/) to show that the requirement of finding matching rules at a frequency > 100/second is feasible. 

The StringsRuleEngineBenchmarks class defines the benchmarks for the ruleset in the assignment. It contains 2 benchmarks:

RuleFile_FindRule(): uses the supplied ruleset file (renamed to StringsRuleSet.csv)  
LargeSimulation_FindRule(): creates an custom ruleset with 1000 never matching rules and 1 matching (catch all) rule. This tests the worst case where for each record in the dataset 1001 rules would have to be checked

The .NET 6 results on my system (Apple M1 Pro 32GB):

|                   Method |      Mean |     Error |    StdDev | Rank |
|------------------------- |----------:|----------:|----------:|-----:|
|        RuleFile_FindRule |  1.663 us | 0.0028 us | 0.0022 us |    1 |
| LargeSimulation_FindRule | 80.406 us | 0.2763 us | 0.2157 us |    2 |

And after upgrading to .NET 7:

|                   Method |      Mean |     Error |    StdDev | Rank |
|------------------------- |----------:|----------:|----------:|-----:|
|        RuleFile_FindRule |  1.050 us | 0.0017 us | 0.0016 us |    1 |
| LargeSimulation_FindRule | 63.229 us | 0.2443 us | 0.2285 us |    2 |

To run the benchmarks:

```shell
cd ./Wavecell.RuleEngine.Benchmarks
dotnet build -c Release
dotnet ./bin/Release/net6.0/Wavecell.RuleEngine.Benchmarks.dll
```

# K6 Load Testing

Install k6: https://k6.io/docs/get-started/installation/

Start Dnw.RuleEngine.Api:

```shell
cd ./Wavecell.RuleEngine.Api
export ASPNETCORE_URLS=https://localhost:5001
dotnet run -c Release --no-launch-profile
```

```shell
cd ./Wavecell.RuleEngine.LoadTests
k6 run rule_engine_loadtest.js
```