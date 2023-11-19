using BadSmellFinder.Domain.Entities;
using BadSmellFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddSingleton<BadSmellFinderStorage>();
builder.Services.AddSingleton<ProjectAnalysis>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/read", (BadSmellFinderStorage storage, CodeConfig? body) => {

	storage.Clear();
	storage.AddManualConfig(body);

	storage.Analyses.AddRange(new BadSmellFinderService(storage).Find());

	return storage;
}).WithOpenApi();


app.MapGet("/clean", (BadSmellFinderStorage storage) => {
	storage.Clear();
}).WithOpenApi();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.Run();