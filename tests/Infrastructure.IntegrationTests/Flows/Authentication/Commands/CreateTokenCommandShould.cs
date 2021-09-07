using System.Text.Json;
using Application.Flows.Authentication.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Authentication.Commands
{
    public class CreateTokenCommandShould
    {
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var command = new Faker<CreateTokenCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Password, f => f.Internet.Password(50))
                .Generate();

            command.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(command));
        }
    }
}