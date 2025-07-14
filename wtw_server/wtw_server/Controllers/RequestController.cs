using Microsoft.AspNetCore.Mvc;
using Entity;
using BLL;

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
        public ActionResult<List<Requests>> GetAllRequest()
        {
            var requests = _requestService.GetAllRequests();
            return Ok(requests);
        }

        #endregion
    }
}
