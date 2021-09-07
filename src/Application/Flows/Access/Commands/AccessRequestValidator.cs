using FluentValidation;

namespace Application.Flows.Access.Commands
{
    /// <summary>
    /// Object validator for AccessRequestCommand
    /// </summary>
    public class AccessRequestValidator : AbstractValidator<AccessRequestCommand>
    {
        /// <summary>
        /// Creates a new instance Of AccessRequestValidator
        /// </summary>
        public AccessRequestValidator()
        {
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be valid email address.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(50).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
            RuleFor(t => t.Address)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(50).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
        }
    }
}