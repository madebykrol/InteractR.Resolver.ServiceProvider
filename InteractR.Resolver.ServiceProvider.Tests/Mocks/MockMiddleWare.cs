using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InteractR.Interactor;

namespace InteractR.Resolver.ServiceProvider.Tests.Mocks
{
    internal class MockMiddleWare : IMiddleware<IAmMock>
    {
        public Task<UseCaseResult> Execute<TUseCase>(TUseCase usecase, Func<TUseCase, Task<UseCaseResult>> next, CancellationToken cancellationToken) where TUseCase : IAmMock
        {
            throw new MockException();
        }
    }
}
