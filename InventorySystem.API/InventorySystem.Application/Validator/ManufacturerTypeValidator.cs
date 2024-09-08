using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class ManufacturerTypeValidator : AbstractValidator<ManufacturerTypeRequest>
    {
        public ManufacturerTypeValidator()
        {
            RuleFor(t => t.ManufacturerType).NotEmpty().WithMessage("ManufacturerType should not be empty.").NotNull().WithMessage("ManufacturerType should not be null.");
        }
    }
}
