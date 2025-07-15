using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using Entity.Schemas;

namespace BLL.Validators
{
    public class JsonSchemaValidator
    {
        // [Methods]
        public static ValidationResult ValidateRequestData(Guid requestTypeId, string jsonData)
        {
            try
            {
                var requestTypeName = GetRequestTypeName(requestTypeId);
                
                switch (requestTypeName.ToLower())
                {
                    case "vacation":
                        return ValidateSchema<VacationRequestSchema>(jsonData);
                    case "loan":
                        return ValidateSchema<LoanRequestSchema>(jsonData);
                    case "permission":
                        return ValidateSchema<PermissionRequestSchema>(jsonData);
                    default:
                        return new ValidationResult($"Unknown request type: {requestTypeName}");
                }
            }
            catch (JsonException ex)
            {
                return new ValidationResult($"Invalid JSON format: {ex.Message}");
            }
        }

        private static ValidationResult ValidateSchema<T>(string jsonData) where T : class
        {
            try
            {
                var deserializedObject = JsonSerializer.Deserialize<T>(jsonData);
                if (deserializedObject == null)
                {
                    return new ValidationResult("Failed to deserialize JSON data");
                }

                var validationContext = new ValidationContext(deserializedObject);
                var validationResults = new List<ValidationResult>();
                
                bool isValid = Validator.TryValidateObject(deserializedObject, validationContext, validationResults, true);
                
                if (!isValid)
                {
                    var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                    return new ValidationResult($"Validation failed: {errors}");
                }
                
                return ValidationResult.Success!;
            }
            catch (Exception ex)
            {
                return new ValidationResult($"Validation error: {ex.Message}");
            }
        }

        private static string GetRequestTypeName(Guid requestTypeId)
        {
            var vacationTypeId = new Guid("0617B26B-6583-435F-B32E-35D76F70E39D");
            var loanTypeId = new Guid("35290059-1516-4D9E-9D84-1903D7DDF3CD");
            var permissionTypeId = new Guid("7EDE7098-092B-4816-957A-A96881CA3154");
            
            var requestTypes = new Dictionary<Guid, string>
            {
                { vacationTypeId, "Vacation" },
                { loanTypeId, "Loan" },
                { permissionTypeId, "Permission" }
            };
            
            return requestTypes.TryGetValue(requestTypeId, out var typeName) ? typeName : "Unknown";
        }
    }
}