using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beer.Core.Entities;
using Beer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BrewersNotebookApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Recipe")]
    public class RecipeController : Controller
    {
        private readonly IBeerDataService dataService;

        public RecipeController(IBeerDataService dataService)
        {
            this.dataService = dataService;
        }

        [Route("getAllForUser/{userEmail}")]
        [HttpGet]
        public async Task<IEnumerable<Recipe>> GetAllForUser(string userEmail)
        {
            var req = this.Request;
            var user = HttpContext.User;
            var h = req.Headers;
            var recipes = await dataService.GetAllRecipesForUser(userEmail);
            return recipes.OrderBy(y => y.Name);
        }

        [Route("save/{userEmail}")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]Recipe recipes, string userEmail)
        {
            var savedRecipe = await dataService.SaveRecipe(recipes, userEmail);            
            return Ok(savedRecipe);
        }


        [Route("delete/{userEmail}")]
        [HttpPost]
        public async Task<OpResult> Delete([FromBody]Recipe recipes, string userEmail)
        {
            var result = await dataService.DeleteRecipe(recipes, userEmail);
            return result;
        }
    }
}