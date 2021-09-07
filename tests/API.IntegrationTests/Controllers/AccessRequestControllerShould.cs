using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using API.Controllers.Base;
using Application.Flows.Access.Commands;
using Application.Models.Access;
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
    public class AccessRequestControllerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<AccessRequestCommand, AccessGrantObjectModel> _handler;

        public AccessRequestControllerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<AccessRequestCommand, AccessGrantObjectModel>>();
            fixture.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        /*[Fact]
        public async Task ResponseDataMustBeOfType()
        {
            var command = new AccessRequestCommand
            {
                Email = "user1@domain.com",
                Address = "Tunnel Door"
            };

            var controller = new AccessRequestController(_handler, new NullLogger<ApiControllerBase>());
            controller.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Email, command.Email) }));

            var response = await controller.AccessRequest(command, CancellationToken.None);
            response.Value.Data.Should().BeOfType<AccessGrantObjectModel>();
        }*/
    }
}