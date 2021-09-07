using System.Text.Json;
using Application.Flows.Access.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Access.Commands
{
    public class AccessRequestCommandShould
    {
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var command = new Faker<AccessRequestCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Address, f => f.Random.String(50))
                .Generate();

            command.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(command));
        }
    }
}