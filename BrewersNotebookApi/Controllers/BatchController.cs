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
    [Route("api/Batch")]
    public class BatchController : Controller
    {
        private readonly IBeerDataService dataService;

        public BatchController(IBeerDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("getAllForUser/{userEmail}")]
        [HttpGet]
        public async Task<IEnumerable<Batch>> GetAllForUser(string userEmail)
        {
            var req = this.Request;
            var user = HttpContext.User;
            var h = req.Headers;
            var batches = await dataService.GetAllBatchesForUser(userEmail);
            return batches.OrderBy(y => y.BrewDate);
        }

        [Route("save/{userEmail}")]
        [HttpPost]
        public async Task<Batch> Save([FromBody]Batch batch, string userEmail)
        {
            var savedBatch = await dataService.SaveBatch(batch, userEmail);
            return savedBatch;
        }

        [Route("delete/{userEmail}")]
        [HttpPost]
        public async Task<OpResult> Delete([FromBody]Batch batch, string userEmail)
        {
            var result = await dataService.DeleteBatch(batch, userEmail);
            return result;
        }
    }
}