using Microsoft.AspNetCore.Mvc;
using WebSCADA.BLL.Interfaces;

namespace WebSCADA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService logsService;

        public LogsController(ILogsService logsService)
        {
            this.logsService = logsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = logsService.Get();

            return Ok(result);
        }
    }
}