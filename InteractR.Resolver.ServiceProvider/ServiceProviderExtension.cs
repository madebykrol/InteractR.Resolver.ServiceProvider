using InteractR.Resolver.ServiceCollection;
using Microsoft.Extensions.DependencyInjection;

namespace InteractR.Resolver.ServiceProvider
{
    public static class ServiceProviderExtension
    {
        public static void AddInteractR(this IServiceCollection collection)
        {
            collection.AddSingleton<IResolver>(x => new ServiceProviderResolver(x));
            collection.AddSingleton<IInteractorHub, Hub>();
        }
    }
}
