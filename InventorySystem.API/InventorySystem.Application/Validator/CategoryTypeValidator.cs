using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class CategoryTypeValidator : AbstractValidator<CategoryTypeRequest>
    {
        public CategoryTypeValidator() {
            RuleFor(t => t.CategoryType).NotEmpty().WithMessage("CategoryType should not be empty.").NotNull().WithMessage("CategoryType should not be null.");
        }
    }
}
