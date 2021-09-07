using Application.Flows.Access.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Access.Commands
{
    public class AccessRequestValidatorShould
    {
        private readonly AccessRequestValidator _validator;

        public AccessRequestValidatorShould()
        {
            _validator = new AccessRequestValidator();
        }

        [Fact]
        public void NotAllowEmptyEmail()
        {
            var command = new Faker<AccessRequestCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Address, f => f.Random.String(50))
                .Generate();

            command.Email = null;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowInvalidEmail()
        {
            var command = new Faker<AccessRequestCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Address, f => f.Random.String(50))
                .Generate();

            command.Email = "null";

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowEmptyAddress()
        {
            var command = new Faker<AccessRequestCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Address, f => f.Random.String(50))
                .Generate();

            command.Address = null;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }
    }
}