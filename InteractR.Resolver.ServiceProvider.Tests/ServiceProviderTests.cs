using System;
using System.Threading;
using System.Threading.Tasks;
using InteractR.Interactor;
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
        private IServiceCollection _serviceCollection;

        [SetUp]
        public void Setup()
        { 
            _serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            _useCaseInteractor = Substitute.For<IInteractor<MockUseCase, IMockOutputPort>>();
            _serviceCollection.AddSingleton(_useCaseInteractor);

            _serviceCollection.AddSingleton<IMiddleware<IAmMock>, MockMiddleWare>();
            _serviceCollection.AddInteractR();
            
            var serviceProvider = _serviceCollection.BuildServiceProvider();

            _interactorHub = serviceProvider.GetService<IInteractorHub>();
        }

        [Test]
        public async Task Test_ServiceProvider_Resolver()
        {
            await _interactorHub.Execute(new MockUseCase(), (IMockOutputPort)new MockOutputPort());
            await _useCaseInteractor.Received().Execute(Arg.Any<MockUseCase>(),Arg.Any<IMockOutputPort>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Test_MiddleWare_Resolver()
        {
            Assert.ThrowsAsync<MockException>(async () =>
            {
                await _interactorHub.Execute(new MiddlewareUseCase(), new MockOutputPort());
            });
        }
    }
}