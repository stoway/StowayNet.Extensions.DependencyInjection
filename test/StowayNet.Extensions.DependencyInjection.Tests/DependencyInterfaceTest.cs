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

            var service = provider.GetService<ISingleFoo>();

            Assert.NotNull(service);
        }


        [Theory]
        [InlineData(typeof(Foo1))]
        [InlineData(typeof(Foo2))]
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

            var service1 = provider.GetService<ISingleFoo>();
            var service2 = provider.GetService<ISingleFoo>();

            Assert.NotEqual(service1, service2);
        }
    }

    public interface ISingleFoo : IStowayDependency
    {

    }

    public class SingleFoo : ISingleFoo
    {

    }

    public interface IFoo : IStowayDependency
    {

    }

    public class Foo1 : IFoo
    {

    }

    public class Foo2 : IFoo
    {

    }
}
