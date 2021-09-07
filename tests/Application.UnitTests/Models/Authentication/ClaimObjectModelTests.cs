using System.Text.Json;
using Application.Models.Authentication;
using Application.Models.Base;
using Bogus;
using Domain.Models.Authentication;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models.Authentication
{
    public class ClaimObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new ClaimObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new ClaimObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<ClaimModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<ClaimModel>()
                .Generate();

            var objectModel = new ClaimObjectModel(domainModel);
            var assigned = new ClaimObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.ClaimId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<ClaimObjectModel>()
                .RuleFor(r => r.ClaimId, f => f.Random.Int())
                .RuleFor(r => r.UserId, f => f.Random.Int())
                .RuleFor(r => r.ClaimType, f => f.Random.String())
                .RuleFor(r => r.ClaimValue, f => f.Random.String())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}