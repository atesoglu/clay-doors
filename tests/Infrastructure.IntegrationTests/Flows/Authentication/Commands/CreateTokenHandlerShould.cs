using System.Threading;
using System.Threading.Tasks;
using Application.Flows.Authentication.Commands;
using Application.Models.Authentication;
using Application.Persistence;
using Application.Request;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Authentication.Commands
{
    public class CreateTokenHandlerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<CreateTokenCommand, TokenObjectModel> _handler;

        public CreateTokenHandlerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<CreateTokenCommand, TokenObjectModel>>();
            fixture.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        [Fact]
        public async Task ResponseMustContainAccessToken()
        {
            var command = new CreateTokenCommand
            {
                Email = "user1@domain.com",
                Password = "password"
            };

            var result = await _handler.HandleAsync(command, CancellationToken.None);

            result.AccessToken.Should().NotBeNullOrEmpty();
        }
    }
}