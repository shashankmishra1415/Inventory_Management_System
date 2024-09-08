using FluentValidation;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.Application.Validator
{
	public class SalesOrderBasicDetailsValidator : AbstractValidator<SalesOrderBasicInformationRequest>
	{
		public SalesOrderBasicDetailsValidator() 
		{ 
			RuleFor(x => x.SalesOrderNo).NotEmpty().WithMessage("SalesOrderNo should not be empty.").NotNull().WithMessage("SalesOrderNo should not be empty.");
			RuleFor(t => t.CustomerId).NotEmpty().WithMessage("CustomerId should not be empty.").NotNull().WithMessage("CustomerId should not be empty.");
			RuleFor(t => t.MovementTypeId).NotEmpty().WithMessage("MovementTypeId should not be empty.").NotNull().WithMessage("MovementTypeId should not be empty.");
			RuleFor(t => t.WarehouseId).NotEmpty().WithMessage("WarehouseId should not be empty.").NotNull().WithMessage("WarehouseId should not be empty.");
		}
	}
}
