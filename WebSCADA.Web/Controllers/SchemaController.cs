using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSCADA.BLL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
        private readonly ISchemaService schemaService;

        public SchemaController(ISchemaService schemaService)
        {
            this.schemaService = schemaService;
        }

        [HttpPut]
        public IActionResult Add([FromBody]SchemaDomain schema)
        {
            if (schema != null)
            {
                schemaService.AddNewSchema(schema);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Save(string name, [FromBody]SchemaDomain schema)
        {
            if (schema != null)
            {
                schemaService.SaveSchema(name, schema);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var schema = schemaService.GetSchema(name);

            return Ok(schema);
        }

        [HttpDelete("{name}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                schemaService.DeleteSchema(name);

                return Ok();
            }

            return BadRequest();
        }
    }
}