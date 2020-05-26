using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebSCADA.BLL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModbusController : ControllerBase
    {
        private readonly IModbusService modbusService;

        public ModbusController(IModbusService modbusService)
        {
            this.modbusService = modbusService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PLCDataDomain[] listRequest)
        {
            var list = listRequest.ToList<PLCDataDomain>();
            var result = modbusService.GetData(list);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PLCDataDomain PLCDataRequest)
        {
            var result = modbusService.SetData(PLCDataRequest);

            return Ok(result);
        }
    }
}