using Dapper;
using System.Reflection;

namespace TravelAndExpense.Infrastructure.Common
{
    public class DynamicParameterHelper
    {
        public static DynamicParameters BuildParameters<T>(T model)
        {
            // Get the properties of 'Type' class object.
            PropertyInfo[] properties = typeof(T).GetProperties();

            DynamicParameters parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var paramName = "_" + char.ToLower(property.Name.ToString()[0]) + property.Name.ToString().Substring(1);
                var paramValue = model.GetType().GetProperty(property.Name).GetValue(model, null);

                parameters.Add(paramName, paramValue);
            }
            return parameters;
        }
    }
}
