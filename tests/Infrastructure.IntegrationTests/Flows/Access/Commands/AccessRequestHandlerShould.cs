using System.Threading;
using System.Threading.Tasks;
using Application.Flows.Access.Commands;
using Application.Models.Access;
using Application.Persistence;
using Application.Request;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Access.Commands
{
    public class AccessRequestHandlerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<AccessRequestCommand, AccessGrantObjectModel> _handler;

        public AccessRequestHandlerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<AccessRequestCommand, AccessGrantObjectModel>>();
            fixture.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        [Fact]
        public async Task ResponseMustContainAccessToken()
        {
            var command = new AccessRequestCommand
            {
                Email = "user1@domain.com",
                Address = "Tunnel Door"
            };

            var result = await _handler.HandleAsync(command, CancellationToken.None);

            result.AccessGrantId.Should().BeGreaterThan(0);
        }
    }
}