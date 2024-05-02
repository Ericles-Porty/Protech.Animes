using Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.API.Extensions.DependencyInjection;
using Protech.Animes.API.Extensions.Auth.JWT;
using Protech.Animes.API.Extensions.Swagger;

using Protech.Animes.API.Extensions.RateLimit;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProtechAnimeDbConnection");

#region Services
builder.Services.AddDbContext<ProtechAnimesDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddRepositoryDependencies();
builder.Services.AddServicesDependencies();
builder.Services.AddJwtConfigDependencies(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDefinition();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddRateLimiter();

builder.Services.AddControllers();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

var app = builder.Build();


#region Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();
#endregion

app.Run();
