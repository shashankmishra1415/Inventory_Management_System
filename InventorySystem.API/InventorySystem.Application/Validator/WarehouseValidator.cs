using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class WarehouseValidator : AbstractValidator<WarehouseRequest>
    {
        public WarehouseValidator()
        {
            RuleFor(t => t.LocationName).NotEmpty().WithMessage("LocationName should not be blank.").NotNull().WithMessage("LocationName should not be empty.");
            RuleFor(t => t.WarehouseTypeId).NotEmpty().WithMessage("TypeId should not be blank.").NotNull().WithMessage("TypeId should not be empty.");
            RuleFor(t => t.MaxCapacity).NotEmpty().WithMessage("MaxCapacity should not be blank.").NotNull().WithMessage("MaxCapacity should not be empty.");
            RuleFor(t => t.IsActive).NotEmpty().WithMessage("IsActive should not be blank.").NotNull().WithMessage("IsActive should not be empty.");
        }
    }
}
