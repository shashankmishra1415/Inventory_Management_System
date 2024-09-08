using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
	public class SalesOrderItemInformationValidator : AbstractValidator<SalesOrderItemsInformationRequest>
	{
		public SalesOrderItemInformationValidator()
		{
			RuleFor(x => x.ProductSKU).NotNull().WithMessage("ProductSkuId can't be empty.")
										.GreaterThan(0).WithMessage("ProductSKU Id must be greater than 0.");
			RuleFor(x => x.ItemQuantity).NotNull().WithMessage("ItemQuantity can't be empty.")
										.GreaterThan(0).WithMessage("ItemQuantity Id must be greater than 0.");
		}
	}
}
