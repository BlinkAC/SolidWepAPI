using ParkingSpot.Application;
using ParkingSpot.Core;
using ParkingSpot.Infrastructure;
using ParkingSpot.Infrastructure.Exceptions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddAplication() //ReservationSerice
    .AddCore()
    .AddInfrastructure(builder.Configuration) //Iclock e InMemory
    .AddControllers();

 builder.Host.UseSerilog((context, loggerConfig) => {
     loggerConfig
     .WriteTo.Console()/*.WriteTo.File("logs.txt")*/;
});
//para filtrar mas los logs se puede instalar getSeq

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

app.UseInfrastructure();

//https://github.com/devmentors/MySpot/blob/402465185b4bd50c6a46fb1256eb2d275ce4f902/src/MySpot.Api/UsersApi.cs#L12
//app.UseUsersApi();

app.Run();
