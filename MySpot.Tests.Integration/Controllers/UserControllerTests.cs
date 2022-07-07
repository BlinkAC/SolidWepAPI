using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.DTO;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.ValueObjects;
using ParkingSpot.Infrastructure.Security;
using ParkingSpot.Infrastructure.Time;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    //sin esto al correr todas las pruebas falla porque dropea la misma bd
    //con colelction corre cada una separada
    [Collection("api")]
    //levanta toda la api, las pruebas se hacen en memoria de forma "privada"
    public class UserControllerTests : ControllerTests, IDisposable
    {
        private IUserRepository _userRepository;
        private readonly TestDatabase _testDatabase;
        public UserControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
            _testDatabase =  new TestDatabase();
        }
        //Esto asegura que cada que la prueba acabe se dropea la base pudiendo hacer la misma prueba varias veces
        public void Dispose()
        {
            _testDatabase.Dispose();
        }

        [Fact]
        public async Task post_users_should_return_no_content_204_status_code()
        {
            await _testDatabase.Context.Database.MigrateAsync();

            var command = new SignUp(Guid.Empty, "user3@gmail.com", "MisiFu9944", "RafTla!#55", "user", "Brandon Gomez");
            var response = await Client.PostAsJsonAsync("users", command);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task post_sign_in_should_return_ok_200_status_code_and_jwt()
        {
            // Arrange
            var passwordManager = new PasswordManager(new PasswordHasher<User>());
            var clock = new Clock();
            const string password = "RafTla!#55";

            var user = new User(Guid.NewGuid(), "user3@gmail.com",
                "MisiFu99441", passwordManager.Secure(password), "Brandon Gomez", Role.User(), clock.Current());
            //Se hace en el repositorio en memoria
            //es el caso donde no se controla la db (y no la toca) y se quiere hacer la prueba
            await _userRepository.AddAsync(user);

            //Se hace en la db que se dropea
            //await _testDatabase.Context.Database.MigrateAsync();
            //await _testDatabase.Context.users.AddAsync(user);
            //await _testDatabase.Context.SaveChangesAsync();

            // Act
            var command = new SignIn(user.Email, password);
            var response = await Client.PostAsJsonAsync("users/sign-in", command);
            var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

            // Assert
            jwt.ShouldNotBeNull();
            jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task get_users_me_should_return_ok_200_status_code_and_user()
        {
            // Arrange
            var passwordManager = new PasswordManager(new PasswordHasher<User>());
            var clock = new Clock();
            const string password = "RafTla!#55";

            var user = new User(Guid.NewGuid(), "user3@gmail.com",
                "MisiFu99441", passwordManager.Secure(password), "Brandon Gomez", Role.User(), clock.Current());
            await _testDatabase.Context.Database.MigrateAsync();
            await _testDatabase.Context.users.AddAsync(user);
            await _testDatabase.Context.SaveChangesAsync();

            // Act
            Authorize(user.Id, user.Role);
            var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");

            // Assert
            userDto.ShouldNotBeNull();
            userDto.UserId.ShouldBe(user.Id.Value);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            _userRepository = new TestUserRepository();
            services.AddSingleton(_userRepository);
        }
    }

}
