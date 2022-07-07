using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Security;
using ParkingSpot.Infrastructure.Auth;
using ParkingSpot.Infrastructure.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    
    [Collection("api")]
    public abstract class ControllerTests : IClassFixture<OptionsProvider>
    {
        private readonly IAuthenticator _authenticator;
        protected HttpClient Client { get; }

        public ControllerTests(OptionsProvider optionsProvider)
        {
            var authOptions = optionsProvider.Get<AuthOptions>("auth");
            _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), new Clock());
            var app = new MySpotTestApp(ConfigureServices);
            Client = app.Client;

        }

        protected JwtDto Authorize(Guid userId, string role)
        {
            var jwt = _authenticator.CreateToken(userId, role);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            return jwt;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }

    }
}
