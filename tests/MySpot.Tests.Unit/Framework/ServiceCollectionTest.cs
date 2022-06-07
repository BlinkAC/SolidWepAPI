using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Unit.Framework
{
    public class ServiceCollectionTest
    {
        [Fact]
        public void test()
        {
            var serviceCollection = new ServiceCollection();
        }
    }
}
