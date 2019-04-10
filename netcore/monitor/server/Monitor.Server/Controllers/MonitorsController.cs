using Microsoft.AspNetCore.Mvc;
using Monitor.Server.Entities;
using Monitor.Server.Sockets.Interfaces;
using System.Threading.Tasks;

namespace Monitor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly IMonitorsSocket _monitorsSocket;

        public MonitorsController(IMonitorsSocket monitorsSocket)
        {
            _monitorsSocket = monitorsSocket;
        }

        /// <summary>
        /// Create new web-socket connection to communicate with client
        /// </summary>
        /// <returns>Returns socket identificator</returns>
        /// <response code="200">Client made request and web-socket send streaming info</response>
        /// <response code="202">Client made request and web-socket worked correctly</response>
        /// <response code="400">Client made request not with web-socket provider</response>
        /// <response code="404">Client made request to disconnect but socket was not found</response>
        /// <response code="422">Client made request to connect but socket could not be inited correctly</response>
        [HttpGet("run")]
        [ProducesResponseType(200, Type = typeof(MonitorState))]
        public async Task<ActionResult> Run()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
                return BadRequest("Connection should be init as a web-socket");

            if (await _monitorsSocket.TryOpenSocket(HttpContext.GetOrigin(), HttpContext.WebSockets))
                return UnprocessableEntity("Connection could not be inited or has been created already");
            else
                return await Stop();
        }

        /// <summary>
        /// Create new web-socket connection to communicate with client
        /// </summary>
        /// <returns>Returns socket identificator</returns>
        /// <response code="202">Client made request and web-socket was closed</response>
        /// <response code="404">Client made request but socket was not found</response>
        [HttpPost("stop")]
        public async Task<ActionResult> Stop()
        {
            if (!await _monitorsSocket.TryCloseSocket(HttpContext.GetOrigin()))
                return NotFound("Connection was not found");

            return NoContent();
        }
    }
}
