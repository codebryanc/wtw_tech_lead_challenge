using Microsoft.AspNetCore.Mvc;
using Entity;
using BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace wtw_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequest _requestService;

        public RequestController(IRequest requestService)
        {
            _requestService = requestService;
        }

        #region [ GET ]

        [HttpGet("GetAllRequest")]
        public async Task<ActionResult<List<Requests>>> GetAllRequest()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        #endregion
    }
}
