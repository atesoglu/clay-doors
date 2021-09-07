using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using API.Controllers.Base;
using Application.Flows.Authentication.Commands;
using Application.Models.Authentication;
using Application.Persistence;
using Application.Request;
using FluentAssertions;
using Infrastructure.IntegrationTests;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace API.IntegrationTests.Controllers
{
    public class CreateTokenControllerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<CreateTokenCommand, TokenObjectModel> _handler;

        public CreateTokenControllerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<CreateTokenCommand, TokenObjectModel>>();
            fixture.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        [Fact]
        public async Task ResponseMustContainAccessToken()
        {
            var controller = new CreateTokenController(_handler, new NullLogger<ApiControllerBase>());

            var command = new CreateTokenCommand
            {
                Email = "user1@domain.com",
                Password = "password"
            };

            var response = await controller.CreateToken(command, CancellationToken.None);
            response.Value.AccessToken.Should().NotBeNullOrEmpty();
        }
    }
}