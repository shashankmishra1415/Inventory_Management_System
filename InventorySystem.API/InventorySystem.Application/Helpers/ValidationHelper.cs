using InventorySystem.SharedLayer.Models.Response;
using FluentValidation.Results;

namespace InventorySystem.Application.Helpers
{
    public class ValidationHelper
    {
        public static async Task<List<ErrorModel>> ValidationFaliure(List<ValidationFailure> failures)
        {
            List<ErrorModel> errors = new List<ErrorModel>();
            failures.ForEach(e =>
            {
                errors.Add(new ErrorModel { ErrorField = e.PropertyName, ErrorMessage = e.ErrorMessage });
            });
            return errors;
        }
    }
}
