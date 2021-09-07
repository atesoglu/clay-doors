using System.Text.Json;
using Application.Models.Authentication;
using Application.Models.Base;
using Bogus;
using Domain.Models.Authentication;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models.Authentication
{
    public class UserObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new UserObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new UserObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<UserModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<UserModel>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Salt, f => f.Random.String(2, 15))
                .RuleFor(r => r.PasswordHash, f => f.Random.String(12))
                .Generate();
            var objectModel = new UserObjectModel { Email = domainModel.Email };

            var assigned = new UserObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.UserId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<UserObjectModel>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}