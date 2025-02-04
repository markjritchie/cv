using CvApi.Queries;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();

services.AddScoped<ICurriculumVitaeQueries, CurriculumVitaeQueries>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
