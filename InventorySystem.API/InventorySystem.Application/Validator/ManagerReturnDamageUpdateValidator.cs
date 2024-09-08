using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class ManagerReturnDamageUpdateValidator: AbstractValidator<ManagerApprovalUpdateRequest>
    {
        public ManagerReturnDamageUpdateValidator() 
        {
            RuleFor(t => t.RecordTypeId).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");
            RuleFor(t => t.RecordId).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");
            RuleFor(t => t.ReturnDamageType).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");
            RuleFor(t => t.VendorId).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");
            RuleFor(t => t.Status).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");           
            RuleFor(t => t.Remarks).NotEmpty().WithMessage("Name should not be empty").NotNull().WithMessage("Name should not be empty");           
        }
    }
}
