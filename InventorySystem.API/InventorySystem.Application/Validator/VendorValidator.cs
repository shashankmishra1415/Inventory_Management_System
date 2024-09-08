using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class VendorValidator : AbstractValidator<VendorRequest>
    {
        public VendorValidator()
        {
            RuleFor(t => t.CompanyName).NotEmpty().WithMessage("CompanyName should not be empty.").NotNull().WithMessage("CompanyName should not be empty.");
            RuleFor(t => t.CompanyTypeId).NotEmpty().WithMessage("CompanyTypeId should not be empty.").NotNull().WithMessage("CompanyTypeId should not be empty.");
            //RuleFor(t => t.VendorTypeId).NotEmpty().WithMessage("VendorTypeId should not be empty.").NotNull().WithMessage("VendorTypeId should not be empty.");
            //RuleFor(t => t.Location).NotEmpty().WithMessage("Location should not be empty.").NotNull().WithMessage("Location should not be empty.");
            //RuleFor(t => t.Address).NotEmpty().WithMessage("Address should not be empty.").NotNull().WithMessage("Address should not be empty.");
            //RuleFor(t => t.ContactName).NotEmpty().WithMessage("LocationNameId should not be empty.").NotNull().WithMessage("LocationNameId should not be empty.");
            RuleFor(t => t.IsActive).NotEmpty().WithMessage("IsActive should not be empty.").NotNull().WithMessage("IsActive should not be empty.");
        }
    }
}
