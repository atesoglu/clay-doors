using Application.Flows.Authentication.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Infrastructure.IntegrationTests.Flows.Authentication.Commands
{
    public class CreateTokenValidatorShould
    {
        private readonly CreateTokenValidator _validator;

        public CreateTokenValidatorShould()
        {
            _validator = new CreateTokenValidator();
        }

        [Fact]
        public void NotAllowEmptyEmail()
        {
            var command = new Faker<CreateTokenCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Password, f => f.Internet.Password(50))
                .Generate();

            command.Email = null;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowInvalidEmail()
        {
            var command = new Faker<CreateTokenCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Password, f => f.Internet.Password(50))
                .Generate();

            command.Email = "null";

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowEmptyPassword()
        {
            var command = new Faker<CreateTokenCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Password, f => f.Internet.Password(50))
                .Generate();

            command.Password = null;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void PasswordMustBeLessThan()
        {
            var command = new Faker<CreateTokenCommand>()
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Password, f => f.Internet.Password(51))
                .Generate();

            _validator.Validate(command).IsValid.Should().BeFalse();
        }
    }
}