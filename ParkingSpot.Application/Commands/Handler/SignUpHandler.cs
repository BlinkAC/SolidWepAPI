using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Exceptions;
using ParkingSpot.Application.Secutiry;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Exceptions;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.Services;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Commands.Handler
{
    internal class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IUserRepository _repository;
        private readonly IClock _clock;
        private readonly IPasswordManager _passwordManager;
        public SignUpHandler(IUserRepository repository, IClock clock, IPasswordManager passwordManager)
        {
            _repository = repository;
            _clock = clock;
            _passwordManager = passwordManager;
        }

        

        public async Task HandleAsync(SignUp command)
        {
            var userId = command.UserId;
            var email = new Email(command.Email);
            var username = new Username(command.Username);
            var password = new Password(command.Password);
            var fullName = new FullName(command.FullName);
            var role = string.IsNullOrEmpty(command.Role) ? Role.User() : new Role(command.Role);
            //validate

            //check if email and username are unique
            if (await _repository.GetByUsernameAsync(command.Username) is not null)
            {
                throw new UserNameAlreadyInUseException(command.Username);
            }

            if (await _repository.GetByEmailAsync(command.Email) is not null)
            {
                throw new EmailAlreadyInUseException(command.Email);
            }

            //secure password
            var securePassword = _passwordManager.Secure(password);

            var user = new User(userId, email, username, securePassword, fullName, role, _clock.Current());
           
            //save user on storage
            await _repository.AddAsync(user);
        }
    }
}
