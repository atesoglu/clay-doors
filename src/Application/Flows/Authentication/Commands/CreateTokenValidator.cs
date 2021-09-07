using FluentValidation;

namespace Application.Flows.Authentication.Commands
{
    /// <summary>
    /// Object validator for CreateTokenCommand
    /// </summary>
    public class CreateTokenValidator : AbstractValidator<CreateTokenCommand>
    {
        /// <summary>
        /// Creates a new instance Of CreateTokenValidator
        /// </summary>
        public CreateTokenValidator()
        {
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be valid email address.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(50).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
            RuleFor(t => t.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(50).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
        }
    }
}