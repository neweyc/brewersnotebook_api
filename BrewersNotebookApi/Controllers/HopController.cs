using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beer.Core.Entities;
using Beer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrewersNotebookApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Hop")]
    public class HopController : Controller
    {
        private readonly IBeerDataService dataService;

        public HopController(IBeerDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("getAllForUser/{userEmail}")]
        [HttpGet]
        public async Task<IEnumerable<Hop>> GetAllForUser(string userEmail)
        {
            var req = this.Request;
            var user = HttpContext.User;
            var h = req.Headers;
            var hop = await dataService.GetAllHopsForUser(userEmail);
            return hop.OrderBy(y => y.Name);
        }

        [Route("save/{userEmail}")]
        [HttpPost]
        public async Task<Hop> Save([FromBody]Hop hop, string userEmail)
        {
            var savedHop = await dataService.SaveHop(hop, userEmail);
            return savedHop;
        }
    }
}