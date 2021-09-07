using System.Collections.Generic;
using System.Text.Json;
using Bogus;
using Domain.Models.Access;
using Domain.Models.Authentication;
using Domain.Models.Base;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models.Authentication
{
    public class UserModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new UserModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }

        [Fact]
        public void ClaimsShouldBeNull()
        {
            var actual = new UserModel();
            actual.Claims.Should().BeNull();
        }

        [Fact]
        public void AccessGrantsShouldBeNull()
        {
            var actual = new UserModel();
            actual.AccessGrants.Should().BeNull();
        }

        [Fact]
        public void ClaimsShouldBeUpdatable()
        {
            var actual = new UserModel
            {
                Claims = new List<ClaimModel>
                {
                    new ClaimModel { ClaimType = "claim-type", ClaimValue = "claim-value"}
                }
            };

            actual.Claims.Should().NotBeNull();
        }

        [Fact]
        public void AccessGrantsShouldBeUpdatable()
        {
            var actual = new UserModel
            {
                AccessGrants = new List<AccessGrantModel>
                {
                    new AccessGrantModel { AccessGrantId = 0, UserId = 1, CheckPointId = 1 }
                }
            };

            actual.AccessGrants.Should().NotBeNull();
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<UserModel>()
                .RuleFor(r => r.UserId, f => f.Random.Int())
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Salt, f => f.Random.String())
                .RuleFor(r => r.PasswordHash, f => f.Random.String())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}