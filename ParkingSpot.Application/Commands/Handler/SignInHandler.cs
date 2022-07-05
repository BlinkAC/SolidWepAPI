﻿using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Exceptions;
using ParkingSpot.Application.Security;
using ParkingSpot.Application.Secutiry;
using ParkingSpot.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Commands.Handler
{
    internal sealed class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenStorage _tokenStorage;

        public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager,
            ITokenStorage tokenStorage)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
            _passwordManager = passwordManager;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(SignIn command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            if (!_passwordManager.Validate(command.Password, user.Password))
            {
                throw new InvalidCredentialsException();
            }

            var jwt = _authenticator.CreateToken(user.Id, user.Role);
            _tokenStorage.Set(jwt);
        }
    }
}
