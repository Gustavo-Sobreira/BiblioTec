using BackBiblioteca.Data;
using Microsoft.EntityFrameworkCore;

Console.Clear();

var builder = WebApplication.CreateBuilder(args);


//Banco
var connectionString = builder.Configuration.GetConnectionString("BibliotecaConnection");
builder.Services.AddDbContext<BibliotecContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BibliotecaConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
