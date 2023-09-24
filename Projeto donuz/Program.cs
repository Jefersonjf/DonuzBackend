using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Projeto_donuz.AppDbContex;
using Projeto_donuz.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("Data source = Projeto donuz"));
builder.Services.AddScoped<IClienteRepositories, ClienteRepositories>();
builder.Services.AddControllers();
builder.Services.AddScoped<ITransacaoRepositories, TransacaoRepositories>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(e => e.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
