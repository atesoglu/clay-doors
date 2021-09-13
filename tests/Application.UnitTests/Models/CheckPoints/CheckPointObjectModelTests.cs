using System.Text.Json;
using Application.Models.Base;
using Application.Models.CheckPoints;
using Bogus;
using Domain.Models.CheckPoints;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models.CheckPoints
{
    public class CheckPointObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new CheckPointObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new CheckPointObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<CheckPointModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<CheckPointModel>()
                .RuleFor(r => r.Address, f => f.Random.String(2, 15))
                .Generate();

            var objectModel = new CheckPointObjectModel(domainModel);
            var assigned = new CheckPointObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.CheckPointId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<CheckPointObjectModel>()
                .RuleFor(r => r.Address, f => f.Random.String(2, 15))
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}