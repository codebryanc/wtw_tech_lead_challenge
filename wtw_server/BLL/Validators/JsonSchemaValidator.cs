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
            var loanTypeId = new Guid("BB1DF24E-51B2-4D41-8FB8-738B64AE27F6");
            var permissionTypeId = new Guid("F2348456-A1E0-4EF1-B777-4651B392D14D");
            var vacationTypeId = new Guid("1ED88739-26A8-4F62-842C-5F8019F5B791");
            
            var requestTypes = new Dictionary<Guid, string>
            {
                { loanTypeId, "Loan" },
                { permissionTypeId, "Permission" },
                { vacationTypeId, "Vacation" },
            };
            
            return requestTypes.TryGetValue(requestTypeId, out var typeName) ? typeName : "Unknown";
        }
    }
}