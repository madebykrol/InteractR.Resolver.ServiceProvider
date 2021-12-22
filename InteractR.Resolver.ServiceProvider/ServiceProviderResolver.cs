using System;
using System.Collections.Generic;
using System.Linq;
using InteractR.Interactor;
using InteractR.Resolver.Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace InteractR.Resolver.ServiceCollection
{
    public class ServiceProviderResolver : IResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IInteractor<TUseCase, TOutputPort> ResolveInteractor<TUseCase, TOutputPort>(TUseCase useCase)
            where TUseCase : IUseCase<TOutputPort> 
                => _serviceProvider.GetService<IInteractor<TUseCase, TOutputPort>>();

        public IReadOnlyList<IMiddleware<TUseCase, TOutputPort>> ResolveMiddleware<TUseCase, TOutputPort>(TUseCase useCase) 
            where TUseCase : IUseCase<TOutputPort> =>
            _serviceProvider.GetServices<IMiddleware<TUseCase, TOutputPort>>().ToList();

        public IReadOnlyList<IMiddleware<TUseCase>> ResolveMiddleware<TUseCase>()
        {
            var types = typeof(TUseCase).GetBaseTypes().ToList();
            var middlewareType = typeof(IMiddleware<>);

            return types.SelectMany(type => _serviceProvider.GetServices(middlewareType.MakeGenericType(type)).Cast<IMiddleware<TUseCase>>())
                .Where(instance => instance != null).Distinct().ToList();
        }

        public IReadOnlyList<IMiddleware> ResolveGlobalMiddleware() 
            => _serviceProvider.GetServices<IMiddleware>().ToList();
    }
}
