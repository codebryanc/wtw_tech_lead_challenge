using Entity;
using Microsoft.AspNetCore.Mvc;
using BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [HttpGet("GetAllRequest")]
        public async Task<ActionResult<List<Requests>>> GetAllRequest()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }
    }
}
