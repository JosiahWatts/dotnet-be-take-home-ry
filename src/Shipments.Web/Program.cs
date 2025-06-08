using FastEndpoints;
using Shipments.Domain.Interfaces;
using Shipments.Domain.Services;
using Shipments.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

var app = builder.Build();

app.UseFastEndpoints();

app.UseHttpsRedirection();

app.Run();
