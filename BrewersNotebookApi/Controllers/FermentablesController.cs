using Beer.Core.Entities;
using Beer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrewersNotebookApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/fermentables")]
    public class FermentablesController : Controller
    {
        private readonly IBeerDataService dataService;

        public FermentablesController(IBeerDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("getAllForUser/{userEmail}")]
        [HttpGet]
        public async Task<IEnumerable<Fermentable>> GetAllForUser(string userEmail)
        {
            var req = this.Request;
            var user = HttpContext.User;
            var h = req.Headers;
            var ferm = await dataService.GetAllFermentablesForUser(userEmail);
            return ferm.OrderBy(y => y.Name);
        }

        [Route("save/{userEmail}")]
        [HttpPost]
        public async Task<Fermentable> Save([FromBody]Fermentable fermentable, string userEmail)
        {
            var savedFermentable = await dataService.SaveFermentable(fermentable, userEmail);
            return savedFermentable;
        }
    }
}
