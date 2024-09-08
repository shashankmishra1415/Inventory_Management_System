using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(t => t.UserName).NotNull().WithMessage("Username should not be blank.")
                                .NotEmpty().WithMessage("Username should not be empty");
            RuleFor(t => t.Password).NotNull().WithMessage("Username should not be blank.")
                                .NotEmpty().WithMessage("Username should not be empty");
        }
    }
}
