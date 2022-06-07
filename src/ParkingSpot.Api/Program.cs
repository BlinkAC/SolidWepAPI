using ParkingSpot.Application;
using ParkingSpot.Application.Services;
using ParkingSpot.Core;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Infrastructure;
using ParkingSpot.Infrastructure.Repositories;
using ParkingSpot.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddAplication() //ReservationSerice
    .AddCore()
    .AddInfrastructure() //Iclock e InMemory
    .AddControllers();
//Componentes agregados desde cada capa/protecto
//esto hace que las clases internas sigan siendo unicamente accesibles 
//desde los proyectos donde estan definidas

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
