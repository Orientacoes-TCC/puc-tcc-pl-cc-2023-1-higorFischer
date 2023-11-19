using BadSmellFinder.Domain.Entities;
using BadSmellFinder.Domain.Services;
using BadSmellFinder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Util;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddSingleton<BadSmellFinderStorage>();
builder.Services.AddSingleton<ProjectAnalysis>();
builder.Services.AddSingleton<BadSmellConfigLoader>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/read", (BadSmellConfigLoader configLoader, BadSmellFinderStorage storage, [FromBody] CodeConfig? codeConfig) => {
	storage.Clear();
	var config = configLoader.ReadConfig(codeConfig.Name);
	var badSmellService = new BadSmellFinderService(storage);

	storage.AddHints(badSmellService.Hints);
	storage.Analyses.AddRange(badSmellService.Find("E:\\42\\IsaBackend", config));
	return storage;
}).WithOpenApi();

app.MapPost("/add", (BadSmellConfigLoader configLoader, BadSmellFinderStorage storage, CodeConfig? body) => {
	return configLoader.TryAddConfig(body);
}).WithOpenApi();

app.MapPost("/file", (BadSmellConfigLoader configLoader, string name) => {
	return configLoader.ReadConfig(name);
}).WithOpenApi();

app.MapGet("/all", (BadSmellConfigLoader configLoader) => {
	return configLoader.All();
}).WithOpenApi();

app.MapGet("/clean", (BadSmellFinderStorage storage) => {
	storage.Clear();
}).WithOpenApi();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.Run();