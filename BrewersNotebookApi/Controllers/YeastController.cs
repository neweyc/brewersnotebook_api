using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beer.Core.Entities;
using Beer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrewersNotebookApi.Controllers
{
    //todo - add error handling, make async
    
    [Authorize]
    [Produces("application/json")]
    [Route("api/Yeast")]
    public class YeastController : Controller
    {
        private readonly IBeerDataService dataService;

        public YeastController(IBeerDataService dataService)
        {
            this.dataService = dataService;
        }


        [Route("getAllForUser/{userEmail}")]
        [HttpGet]
        public IEnumerable<Yeast> GetAllForUser(string userEmail)
        {
            var req = this.Request;
            var user = HttpContext.User;
            var h = req.Headers;
            var yeast = dataService.GetAllYeastForUser(userEmail).OrderBy(y => y.Name);
            return yeast;
        }

        
        [Route("save/{userEmail}")]
        [HttpPost]
        public Yeast Save([FromBody]Yeast yeast, string userEmail)
        {
            var savedYeast = dataService.SaveYeast(yeast, userEmail);
            return savedYeast;
        }
    }
}