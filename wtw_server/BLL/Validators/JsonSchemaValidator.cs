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
            // Here is empty because the test is probably change dynamically 
            var requestTypes = new Dictionary<Guid, string>
            {
                { Guid.Empty, "Vacation" },
                { Guid.Empty, "Loan" },
                { Guid.Empty, "Permission" }
            };
            
            return requestTypes.TryGetValue(requestTypeId, out var typeName) ? typeName : "Unknown";
        }
    }
}