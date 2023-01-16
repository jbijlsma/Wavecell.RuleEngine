using Wavecell.RuleEngine;
using Wavecell.RuleEngine.MixedRuleSet;
using Wavecell.RuleEngine.StringsRuleSet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<RuleSetEngine<StringsFilterValues>>(_ =>
{
    var ruleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StringsRuleSet.csv");
    var rules = new StringsRuleSetFileReader(new RuleSetFileLoader(ruleFile)).Read();
    return new RuleSetEngine<StringsFilterValues>(rules);
});

builder.Services.AddSingleton<RuleSetEngine<MixedFilterValues>>(_ =>
{
    var ruleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MixedRuleSet.csv");
    var rules = new MixedRuleSetFileReader(new RuleSetFileLoader(ruleFile)).Read();
    return new RuleSetEngine<MixedFilterValues>(rules);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/rule-sets/strings/rules", (string s1, string s2, string s3, string s4, RuleSetEngine<StringsFilterValues> ruleSetEngine) =>
{
    var rule = ruleSetEngine.FindRule(new StringsFilterValues(s1, s2, s3, s4));
    return Task.FromResult(Results.Ok(rule));
});

app.MapGet("/rule-sets/mixed/rules", (int intValue, bool boolValue, string stringValue, RuleSetEngine<MixedFilterValues> ruleSetEngine) =>
{
    var rule = ruleSetEngine.FindRule(new MixedFilterValues(intValue, boolValue, stringValue));
    return Task.FromResult(Results.Ok(rule));
});

app.Run();
