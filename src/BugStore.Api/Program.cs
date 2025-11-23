using BugStore.Data;
using Microsoft.EntityFrameworkCore;
using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// MediatR
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<Program>();
});

var app = builder.Build();

// Configuração do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares básicos
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();

// app.MapGet("/v1/orders/{id}", () => "Hello World!");
// app.MapPost("/v1/orders", () => "Hello World!");
