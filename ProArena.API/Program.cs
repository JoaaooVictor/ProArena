using ProArena.API.Extensions;
using ProArena.Application.Injection;
using ProArena.Application.Mappings;
using ProArena.Application.Utils;
using ProArena.Infrastructure.Injection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProArena.Tests")]

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProArenaConnection");

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(connectionString!);

builder.Services.AddServices(connectionString!);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("FrontPolicy", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProArenaV1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("FrontPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
