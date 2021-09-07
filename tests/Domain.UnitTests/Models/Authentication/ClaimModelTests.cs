using System.Text.Json;
using Bogus;
using Domain.Models.Authentication;
using Domain.Models.Base;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models.Authentication
{
    public class ClaimModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new ClaimModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }
        
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<ClaimModel>()
                .RuleFor(r => r.ClaimId, f => f.Random.Int())
                .RuleFor(r => r.UserId, f => f.Random.Int())
                .RuleFor(r => r.ClaimType, f => f.Random.String())
                .RuleFor(r => r.ClaimValue, f => f.Random.String())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}