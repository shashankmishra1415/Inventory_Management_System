using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class UserValidator:AbstractValidator<SaveUserRequest>
    {
        public UserValidator() 
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");
            RuleFor(t => t.WareHouseId).NotEmpty().WithMessage("WareHouseId should not be empty").NotNull().WithMessage("WareHouseId should not be empty");
            RuleFor(t => t.Mobile).NotEmpty().WithMessage("Mobile should not be empty").NotNull().WithMessage("Mobile should not be empty").Must(t => t.ToString().Length < 11).WithMessage("enter no not more than 10");
            RuleFor(t => t.Status).NotEmpty().WithMessage("Status should not be empty").NotNull().WithMessage("Status should not be empty");
            RuleFor(t => t.Email).NotEmpty().WithMessage("Email should not be empty").NotNull().WithMessage("Status should not be empty").EmailAddress().WithMessage("A valid email is required");
        }
    }
}
