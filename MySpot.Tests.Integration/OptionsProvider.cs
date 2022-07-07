﻿using Microsoft.Extensions.Configuration;
using ParkingSpot.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration
{
    public sealed class OptionsProvider
    {
        private readonly IConfigurationRoot _configuration;

        public OptionsProvider()
        {
            _configuration = GetConfigurationRoot();
        }

        public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);

        private static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", true)
                .AddEnvironmentVariables()
                .Build();
    }
}
