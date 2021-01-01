using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StowayNet.Extensions.DependencyInjection.Tests
{
    public class DependencyAttributeTest
    {
        [Fact]
        public void Validate_Transient_Inherited()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var service1 = provider.GetService<ChildBarTransient>();

            Assert.NotNull(service1);
        }

        [Fact]
        public void Validate_Transient_NotEqual()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var service1 = provider.GetService<BarTransient>();
            var service2 = provider.GetService<BarTransient>();

            Assert.NotEqual(service1, service2);
        }

        [Fact]
        public void Validate_Scoped_Equal()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var scope = provider.CreateScope();
            var service1 = scope.ServiceProvider.GetService<BarScoped>();
            var service2 = scope.ServiceProvider.GetService<BarScoped>();
            Assert.Equal(service1, service2);
        }

        [Fact]
        public void Validate_Scoped_NotEqual()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var scope1 = provider.CreateScope();
            var service1 = scope1.ServiceProvider.GetService<BarScoped>();
            var scope2 = provider.CreateScope();
            var service2 = scope2.ServiceProvider.GetService<BarScoped>();
            Assert.NotEqual(service1, service2);
        }
        [Fact]
        public void Validate_Singleton_Equal()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddStowayNet();
            var provider = services.BuildServiceProvider();

            var scope1 = provider.CreateScope();
            var service1 = scope1.ServiceProvider.GetService<BarSingleton>();
            var scope2 = provider.CreateScope();
            var service2 = scope2.ServiceProvider.GetService<BarSingleton>();
            Assert.Equal(service1, service2);

        }
    }

    [StowayDependency(StowayDependencyType.Transient)]
    public class BarTransient
    {
    }

    public class ChildBarTransient : BarTransient
    {

    }

    [StowayDependency(StowayDependencyType.Scoped)]
    public class BarScoped
    {

    }

    [StowayDependency(StowayDependencyType.Singleton)]
    public class BarSingleton
    {

    }
}
