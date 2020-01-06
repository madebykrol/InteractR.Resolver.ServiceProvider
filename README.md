# InteractR.Resolver.ServiceProvider
For documentation see [InteractR](https://github.com/madebykrol/InteractR)

Install from nuget.
```PowerShell
PM > Install-Package InteractR.Resolver.ServiceProvider -Version 2.1.1
```

# Usage
Either you can register the resolver and interactorhub yourself

```Csharp
collection.AddSingleton<IResolver>(x => new ServiceProviderResolver(x));
collection.AddSingleton<IInteractorHub, Hub>();
```
Or you can use the extension method AddInteractR()
```
serviceCollection.AddInteractR();
```