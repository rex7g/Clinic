using API_CLINICA.Data;
using API_CLINICA.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CLINICAContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmpleados, EmpleadosRepository>();
builder.Services.AddScoped<IDoctores, DoctoresRepository>();
builder.Services.AddScoped<ICitaMedica, CitaMedicaRepository>();
builder.Services.AddScoped<IUser, UsuarioRepository>();
builder.Services.AddScoped<IFotoRepository, FotoReposirtory>();




var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
