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

builder.Services.AddCors(options =>
{
    options.AddPolicy("Liberado",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Liberado");
app.UseAuthorization();
app.MapControllers();

app.Run();