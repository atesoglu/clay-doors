using System.Text.Json;
using Bogus;
using Domain.Models.Access;
using Domain.Models.Authentication;
using Domain.Models.Base;
using Domain.Models.CheckPoints;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models.Access
{
    public class AccessGrantModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new AccessGrantModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }

        [Fact]
        public void UserReferencePropertyShouldDefaultToNull()
        {
            var actual = new AccessGrantModel();
            actual.User.Should().Be(null);
        }

        [Fact]
        public void CheckPointReferencePropertyShouldDefaultToNull()
        {
            var actual = new AccessGrantModel();
            actual.CheckPoint.Should().Be(null);
        }

        [Fact]
        public void UserReferencePropertyShouldBeOfType()
        {
            var actual = new AccessGrantModel
            {
                User = new UserModel()
            };
            actual.User.Should().BeOfType<UserModel>();
        }

        [Fact]
        public void CheckPointReferencePropertyShouldBeOfType()
        {
            var actual = new AccessGrantModel
            {
                CheckPoint = new CheckPointModel()
            };
            actual.CheckPoint.Should().BeOfType<CheckPointModel>();
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var model = new Faker<AccessGrantModel>()
                .RuleFor(r => r.AccessGrantId, f => f.Random.Int())
                .RuleFor(r => r.CheckPointId, f => f.Random.Int())
                .RuleFor(r => r.UserId, f => f.Random.Int())
                .Generate();

            model.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(model));
        }
    }
}