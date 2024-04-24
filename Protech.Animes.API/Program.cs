using Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;
using Protech.Animes.Infrastructure.Data.Repositories;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.Services;
using Protech.Animes.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProtechAnimeDbConnection");

// Add services to the container.
builder.Services.AddDbContext<ProtechAnimesDbContext>(options => options.UseNpgsql(connectionString));

// Add mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();

// Services
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();

// Use cases
builder.Services.AddScoped<CreateAnimeUseCase>();


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

app.MapControllers();

app.Run();
