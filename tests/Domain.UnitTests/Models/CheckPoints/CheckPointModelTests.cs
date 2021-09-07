using System.Collections.Generic;
using System.Text.Json;
using Bogus;
using Domain.Models.Access;
using Domain.Models.Base;
using Domain.Models.CheckPoints;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models.CheckPoints
{
    public class CheckPointModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new CheckPointModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }

        [Fact]
        public void AccessGrantsShouldBeNull()
        {
            var actual = new CheckPointModel();
            actual.AccessGrants.Should().BeNull();
        }

        [Fact]
        public void AccessGrantsShouldBeUpdatable()
        {
            var actual = new CheckPointModel
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
            var objectModel = new Faker<CheckPointModel>()
                .RuleFor(r => r.CheckPointId, f => f.Random.Int())
                .RuleFor(r => r.Address, f => f.Random.String())
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}