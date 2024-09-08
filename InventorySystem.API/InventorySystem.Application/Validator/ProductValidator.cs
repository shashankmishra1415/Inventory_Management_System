using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(t => t.ProductSku).NotEmpty().WithMessage("ProductSku should not be empty.").NotNull().WithMessage("ProductSku should not be empty.");
            RuleFor(t => t.Name).NotEmpty().WithMessage("Name should not be empty.").NotNull().WithMessage("Name should not be empty.");
            RuleFor(t => t.CategoryId).NotEmpty().WithMessage("CategoryId should not be empty.").NotNull().WithMessage("CategoryId should not be empty.");
            RuleFor(t => t.ManufacturerId).NotEmpty().WithMessage("ManufacturerId should not be empty.").NotNull().WithMessage("ManufacturerId should not be empty.");
            //RuleFor(t => t.CategoryId).NotEmpty().WithMessage("CompanyName should not be empty.").NotNull().WithMessage("CompanyName should not be empty.");
            //RuleFor(t => t.EANCode).NotEmpty().WithMessage("CompanyName should not be empty.").NotNull().WithMessage("CompanyName should not be empty.");
        }
    }
}
