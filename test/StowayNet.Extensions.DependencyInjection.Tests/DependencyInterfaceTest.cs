using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StowayNet.Extensions.DependencyInjection.Tests
{
    public class DependencyInterfaceTest
    {
        [Fact]
        public void Validate_SingleClass()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var service1 = provider.GetService<ISingletonFoo>();

            var service2 = provider.GetService<ISingletonFoo>();

            Assert.Equal(service1, service2);
        }


        [Theory]
        [InlineData(typeof(Foo))]
        public void Validate_ClassesImplementInterface(Type type)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var all = provider.GetServices<IFoo>();
            var types = all.Select(p => p.GetType());
            Assert.Contains(type, types);
        }

        [Fact]
        public void Validate_ClassLifecycle()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var service1 = provider.GetService<IFoo>();
            var service2 = provider.GetService<IFoo>();

            Assert.NotEqual(service1, service2);
        }
    }

    public interface ISingletonFoo
    {

    }

    [StowayDependency(StowayDependencyType.Singleton)]
    public class SingleFoo : ISingletonFoo
    {

    }

    public interface IFoo : IStowayDependency
    {

    }

    public class Foo : IFoo
    {

    }
}
