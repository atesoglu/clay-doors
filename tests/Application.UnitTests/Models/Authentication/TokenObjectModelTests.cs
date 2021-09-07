using System.Text.Json;
using Application.Models.Authentication;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models.Authentication
{
    public class TokenObjectModelTests
    {
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<TokenObjectModel>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}