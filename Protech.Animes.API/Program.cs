using Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.API.Extensions.DependencyInjection;
using Protech.Animes.API.Extensions.Auth.JWT;
using Protech.Animes.API.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProtechAnimeDbConnection");

builder.Services.AddDbContext<ProtechAnimesDbContext>(options => options.UseNpgsql(connectionString));

RepositoryDependenciesInjectionExtension.AddRepositoryDependencies(builder.Services);
ServicesDependenciesInjectionExtension.AddServicesDependencies(builder.Services);
UseCasesDependenciesInjectionExtension.AddUseCases(builder.Services);
JwtConfigDependenciesInjectionExtension.AddJwtConfigDependencies(builder.Services, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

SwaggerDefinitionExtensions.AddSwaggerDefinition(builder.Services);
JwtAuthenticationExtensions.AddJwtAuthentication(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

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
