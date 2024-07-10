using Microsoft.EntityFrameworkCore;
using PractiTech.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("Customers"));
builder.Services.AddDbContext<InvoiceContext>(opt => opt.UseInMemoryDatabase("Invoices"));
builder.Services.AddDbContext<ItemContext>(opt => opt.UseInMemoryDatabase("Items"));

var app = builder.Build();
app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
