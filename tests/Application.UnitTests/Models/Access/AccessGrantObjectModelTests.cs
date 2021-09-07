using System.Text.Json;
using Application.Models.Access;
using Application.Models.Base;
using Bogus;
using Domain.Models.Access;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models.Access
{
    public class AccessGrantObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new AccessGrantObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new AccessGrantObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<AccessGrantModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<AccessGrantModel>()
                /*.RuleFor(r => r.FlashType, f => f.PickRandom<FlashTypes>())
                .RuleFor(r => r.StrikedAt, f => f.Date.RecentOffset())
                .RuleFor(r => r.Latitude, f => f.Random.Double(-90, 90))
                .RuleFor(r => r.Longitude, f => f.Random.Double(-180, 180))
                .RuleFor(r => r.PeakAmps, f => f.Random.Int(-180, 180))
                .RuleFor(r => r.Reserved, f => f.Lorem.Word())
                .RuleFor(r => r.IcHeight, f => f.Random.Int(1, 180))
                .RuleFor(r => r.ReceivedAt, f => f.Date.RecentOffset())
                .RuleFor(r => r.NumberOfSensors, f => f.Random.Int(1, 180))
                .RuleFor(r => r.Multiplicity, f => f.Random.Int(1, 180))*/
                .Generate();

            var objectModel = new AccessGrantObjectModel(domainModel);
            var assigned = new AccessGrantObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.AccessGrantId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<AccessGrantObjectModel>()
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}