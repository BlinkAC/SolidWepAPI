using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;


//Estos test son para ver la integracio(end to end) de los componentes de la API
//la idea de Microsoft.AspNetCore.Mvc.Testing; es poder correr la app y hacer pruebas en memoria
namespace MySpot.Tests.Integration
{
    internal sealed class MySpotTestApp : WebApplicationFactory<Program> //actua como entry point
    {
        public HttpClient Client { get; }

        public MySpotTestApp(Action<IServiceCollection> services = null)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if (services is not null)
                {
                    //para hacer pruebas con componentes/servicios que no se pueden controlar
                    //apis externas, servicios de pago, etc
                    //sobreescribe el ioc container
                    
                    builder.ConfigureServices(services);
                }
                //Cuando se hace con componentes propios (bd, etc)
                //no hay mucho problema para hacer las pruebas
                builder.UseEnvironment("test");
            }).CreateClient();
        }

    }
}
