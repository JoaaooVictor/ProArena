using ProArena.Infrastructure.Injection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProArenaConnection");

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(connectionString!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
