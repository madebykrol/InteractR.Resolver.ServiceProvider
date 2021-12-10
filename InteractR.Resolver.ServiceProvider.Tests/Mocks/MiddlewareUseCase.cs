using InteractR.Interactor;

namespace InteractR.Resolver.ServiceProvider.Tests.Mocks
{
    internal class MiddlewareUseCase : IUseCase<IMockOutputPort>, IAmMock
    {
        public string Mock { get; set; }
    }
}
