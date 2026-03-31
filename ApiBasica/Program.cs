// INTEGRANTES DA EQUIPE
// Eloisa Santos Zlotek
// Lucas Felipe Cristo
// Mylena dos Santos
// Murieli Schrickte

using Microsoft.EntityFrameworkCore;
using ApiBasica;

var builder = WebApplication.CreateBuilder(args);

var conn = builder
            .Configuration
            .GetConnectionString("ConnPadrao");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

var app = builder.Build();

app.MapGet("/", () => "API rodando");

app.Run();