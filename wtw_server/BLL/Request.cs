using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using Entity;
using Entity.DTOs;
using BLL.Validators;

namespace BLL
{
    public interface IRequest
    {
        // [Methods]
        Task<List<Requests>> GetAllRequestsAsync();
        Task<List<Requests>> GetFilteredRequestsAsync(RequestFilterDto filter);
        Task<Requests> CreateRequestAsync(RequestCreateDto requestDto);
        Task<Requests?> GetRequestByIdAsync(Guid id);
        Task<bool> DeleteRequestAsync(Guid id);
        Task<List<Requests>> SearchByJsonPropertyAsync(string propertyName, string value);
    }

    public class Request : IRequest
    {
        // [Properties]
        private readonly DAL.IRequest _dalRequest;

        // [Constructor]
        public Request(DAL.IRequest dalRequest)
        {
            _dalRequest = dalRequest;
        }

        // [Methods]
        public async Task<List<Requests>> GetAllRequestsAsync()
        {
            var result = await _dalRequest.GetAllRequestsAsync();
            return result.ToList();
        }

        public async Task<List<Requests>> GetFilteredRequestsAsync(RequestFilterDto filter)
        {
            var result = await _dalRequest.GetFilteredRequestsAsync(filter);
            return result.ToList();
        }

        public async Task<Requests> CreateRequestAsync(RequestCreateDto requestDto)
        {
            // [Validate JSON schema based on request type]
            var jsonData = JsonSerializer.Serialize(requestDto.DynamicData);
            var validationResult = JsonSchemaValidator.ValidateRequestData(
                string.IsNullOrEmpty(requestDto.RequestTypeId) ? Guid.Empty : Guid.Parse(requestDto.RequestTypeId), 
                jsonData);
            
            if (validationResult != ValidationResult.Success)
            {
                throw new ValidationException(validationResult.ErrorMessage);
            }

            return await _dalRequest.CreateRequestAsync(requestDto);
        }

        public async Task<Requests?> GetRequestByIdAsync(Guid id)
        {
            return await _dalRequest.GetRequestByIdAsync(id);
        }

        public async Task<bool> DeleteRequestAsync(Guid id)
        {
            return await _dalRequest.DeleteRequestAsync(id);
        }

        public async Task<List<Requests>> SearchByJsonPropertyAsync(string propertyName, string value)
        {
            var result = await _dalRequest.SearchByJsonPropertyAsync(propertyName, value);
            return result.ToList();
        }
    }
}
