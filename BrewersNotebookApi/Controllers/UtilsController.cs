using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrewersNotebookApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Utils")]
    public class UtilsController : Controller
    {
        [HttpGet]
        [Route("GetIP")]
        public IActionResult GetIP()
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            return Ok(new { ip = ip.ToString() });
        }
    }
}