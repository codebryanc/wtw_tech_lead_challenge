using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using BLL;
using Entity;
using Entity.DTOs;

namespace wtw_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        // [Properties]
        private readonly IRequest _requestService;

        // [Constructor]
        public RequestController(IRequest requestService)
        {
            _requestService = requestService;
        }

        // [Methods]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Requests>>> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("GetFiltered")]
        public async Task<ActionResult<List<Requests>>> GetFilteredRequests([FromQuery] RequestFilterDto filter)
        {
            var requests = await _requestService.GetFilteredRequestsAsync(filter);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Requests>> GetRequestById(Guid id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound();
            
            return Ok(request);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Requests>> CreateRequest([FromBody] RequestCreateDto requestDto)
        {
            try
            {
                var request = await _requestService.CreateRequestAsync(requestDto);
                return CreatedAtAction(nameof(GetRequestById), new { id = request.reqId }, request);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRequest(Guid id)
        {
            var success = await _requestService.DeleteRequestAsync(id);
            if (!success)
                return NotFound();
            
            return NoContent();
        }

        [HttpGet("SearchByProperty")]
        public async Task<ActionResult<List<Requests>>> SearchByJsonProperty([FromQuery] string propertyName, [FromQuery] string value)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(value))
                return BadRequest("PropertyName and Value are required");

            var requests = await _requestService.SearchByJsonPropertyAsync(propertyName, value);
            return Ok(requests);
        }
    }
}
