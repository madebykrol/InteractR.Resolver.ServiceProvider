using System;
using InteractR.Interactor;
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
            where TUseCase : IUseCase<TOutputPort> => _serviceProvider.GetService<IInteractor<TUseCase, TOutputPort>>();
    }
}
