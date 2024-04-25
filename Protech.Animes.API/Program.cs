using Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Protech.Animes.API.Extensions.DependencyInjection;
using Protech.Animes.API.Extensions.Auth.JWT;
using Protech.Animes.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProtechAnimeDbConnection");

// Add services to the container.
builder.Services.AddDbContext<ProtechAnimesDbContext>(options => options.UseNpgsql(connectionString));

// Add mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

RepositoryDependenciesInjectionExtension.AddRepositoryDependencies(builder.Services);

ServicesDependenciesInjectionExtension.AddServicesDependencies(builder.Services);

UseCasesDependenciesInjectionExtension.AddUseCases(builder.Services);

JwtConfigDependenciesInjectionExtension.AddJwtConfigDependencies(builder.Services, builder.Configuration);

JwtExtensions.AddJwt(builder.Services, builder.Configuration);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Protech.Animes.API",
        Version = "v1",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization : Bearer { token }\"",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
