using Beer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beer.Core.Services
{
    public interface IBeerDataService
    {
        IEnumerable<Yeast> GetAllYeastForUser(string userEmail);
        Yeast SaveYeast(Yeast yeast, string userEmail);
        Task<IEnumerable<Fermentable>> GetAllFermentablesForUser(string userEmail);
        Task<Fermentable> SaveFermentable(Fermentable fermentable, string userEmail);
        Task<IEnumerable<Hop>> GetAllHopsForUser(string userEmail);
        Task<Hop> SaveHop(Hop hop, string userEmail);
        Task<IEnumerable<Recipe>> GetAllRecipesForUser(string userEmail);
        Task<Recipe> SaveRecipe(Recipe recipe, string userEmail);
        Task<IEnumerable<Batch>> GetAllBatchesForUser(string userEmail);
        Task<Batch> SaveBatch(Batch batch, string userEmail);
        Task<OpResult> DeleteBatch(Batch rbatchecipe, string userEmail);
        Task<OpResult> DeleteRecipe(Recipe recipe, string userEmail);
    }
}
