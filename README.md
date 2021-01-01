<p align="center">
  <img height="80" src="https://s.gravatar.com/avatar/6275a0046443d6fb33421b52e503bc3e?s=140">
</p>

# StowayNet.Extensions.DependencyInjection 　　　　　　　　　　　[En](https://github.com/stoway/StowayNet.Extensions.DependencyInjection/blob/master/README.en-us.md)
 StowayNet.Extensions.DependencyInjection 是基于 .net 5 框架中的依赖注入实现的扩展方法，它可以在项目中更简捷的实现依赖注入。

## 入门
### NuGet 

你可以运行以下命令在你的项目中安装 StowayNet.Extensions.DependencyInjection。

```
PM> Install-Package StowayNet.Extensions.DependencyInjection
```

### 配置

首先配置 StowayNet.Extensions.DependencyInjection 到 Startup.cs 文件中，如下：
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

### 注入方式

#### 方式一：实现 `IStowayDependency` 空接口 

`IStowayDependency` 本身为空接口，所有实现 `IStowayDependency` 接口的类及其子类都将被注入到 `Transient` 生命周期中。

```c#

public class BookService : IStowayDependency
{
    ......
}

```
如需要注入 `Scoped`、 `Singleton` 生命周期，则需要通过在类中增加特性 `StowayDependencyAttribute` 实现。

#### 方式二：特性 `StowayDependencyAttribute`

通过在类中增加特性 `StowayDependencyAttribute`，通过指定 `StowayDependencyType` 参数，可以注入 `Transient`、`Scoped`、 `Singleton` 生命周期。允许子类通过继承基类从而继承该特性。

```c#

[StowayDependency(StowayDependencyType.Scoped)]
public class BookService {

}

......

[StowayDependency(StowayDependencyType.Singleton)]
public class AuthorService {

}

```

#### 方式三：实现 `IStowayServiceRegister` 接口的 `Register` 方法

通过实现 `IStowayServiceRegister` 接口的 `Register` 方法，可以实现自定义的注入服务。

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

同时也可以将其他第三方框架的配置实现通过此方式管理。
