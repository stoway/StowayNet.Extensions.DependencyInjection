<p align="center">
  <img height="80" src="https://s.gravatar.com/avatar/6275a0046443d6fb33421b52e503bc3e?s=140">
</p>

# StowayNet.DependencyInjection 　　　　　　　　　　　[中文](https://github.com/stoway/StowayNet.DependencyInjection/blob/master/README.md)
StowayNet.DependencyInjection is Dependency Injection extension methods for .net core, it can easily implement dependency injection.

## Get Started
### NuGet 

You can run the following command to install the `StowayNet.DependencyInjection` in your project.

```
PM> Install-Package StowayNet.DependencyInjection
```

### Configuration

First,You need to config `StowayNet.DependencyInjection` in your `Startup.cs`:
```c#
......
using StowayNet;
......

public void ConfigureServices(IServiceCollection services)
{
    ......

    services.AddStowayNet();

    ......
}

```

### Sample

#### Sample 1：implement `IStowayDependency` empty interface 

`IStowayDependency` is an empty interface, all class and its subclasses that implement `IStowayDependency` interface will injected into `Transient` lifecyle。

```c#

public class BookService : IStowayDependency
{
    ......
}

```
If you want to inject `Scoped` and `Singleton` lifecycle, you need to add the attribute `StowayDependencyAttribute` to class.

#### Sample 2：`StowayDependencyAttribute`

Adding the attribute `StowayDependencyAttribute` to class, specifying the `StowayDependencyType` parameter, the lifecycles of `Transient`、`Scoped`、 `Singleton` can be injected, but it cannot be injected for subclasses that inherit it.

```c#

[StowayDependency(StowayDependencyType.Scoped)]
public class BookService {

}

......

[StowayDependency(StowayDependencyType.Singleton)]
public class AuthorService {

}

```

#### Sample 3：implement `Register` method of `IStowayServiceRegister` interface.

By implementing `Register` method of ` IStowayServiceRegister` interface, you can implement a custom injection service.

```c#

internal class PressServiceRegister : IStowayServiceRegister
{
  public void Register(IServiceCollection services, List<Type> types, IConfiguration configuration)
  {
      var serviceType = typeof(IBookService);
      var stTypes = types.Where(t => !t.IsAbstract && !t.IsInterface).ToList();

      services.RegisterTypes(stTypes, ServiceLifetime.Scoped, true, true);
  }
}

```

At the same time, the configuration of other third-party frameworks can also be managed in this way.