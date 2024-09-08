using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
    public class StockInwardValidator : AbstractValidator<AddInvoiceRequest>
    {
        public StockInwardValidator()
        {
            RuleFor(t => t.InvoiceNo)
                .NotEmpty().WithMessage("This Field must not be empty. ");

            RuleFor(t => t.WarehouseLocationId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");

            RuleFor(t => t.StatusId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");

            RuleFor(t => t.DateOfPurchase)
              .NotEmpty().WithMessage("This Field must not be empty. ");

            RuleFor(t => t.VendorCompanyNameId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");

            RuleFor(t => t.ItemTypeId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");

            RuleFor(t => t.MoveTypeId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");
        }
    }

    public class StockInwardValidator2 : AbstractValidator<AddProductInInvoiceRequest>
    {
        public StockInwardValidator2()
        {
            RuleFor(t => t.Quantity)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");

            RuleFor(t => t.ProductSkuId)
                .NotEmpty().WithMessage("This Field must not be empty. ")
                .GreaterThan(0).WithMessage("Please Use a valid input, dont use 0 .");
        }
    }

    public class StockInwardValidator3 : AbstractValidator<List<AddProductInInvoiceRequest>>
    {
        public StockInwardValidator3()
        {
            RuleForEach(list => list)
               .SetValidator(new StockInwardValidator2());
        }
    }
}
