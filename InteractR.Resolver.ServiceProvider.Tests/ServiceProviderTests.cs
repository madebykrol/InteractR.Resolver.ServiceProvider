using System.Threading;
using System.Threading.Tasks;
using InteractR.Interactor;
using InteractR.Resolver.ServiceCollection;
using InteractR.Resolver.ServiceProvider.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace InteractR.Resolver.ServiceProvider.Tests
{
    [TestFixture]
    public class ServiceProviderTests
    {
        private IInteractorHub _interactorHub;
        private IInteractor<MockUseCase, IMockOutputPort> _useCaseInteractor;

        [SetUp]
        public void Setup()
        { 
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            _useCaseInteractor = Substitute.For<IInteractor<MockUseCase, IMockOutputPort>>();
            serviceCollection.AddSingleton(_useCaseInteractor);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _interactorHub = new Hub(new ServiceProviderResolver(serviceProvider));
        }

        [Test]
        public async Task Test_ServiceProvider_Resolver()
        {
            await _interactorHub.Execute(new MockUseCase(), (IMockOutputPort)new MockOutputPort());
            await _useCaseInteractor.Received().Execute(Arg.Any<MockUseCase>(),Arg.Any<IMockOutputPort>(), Arg.Any<CancellationToken>());
        }
    }
}