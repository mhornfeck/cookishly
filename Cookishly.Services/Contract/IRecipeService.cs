using System.Threading.Tasks;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Results;

namespace Cookishly.Services.Contract
{
    public interface IRecipeService
    {
        Task<Recipe> CreateRecipeAsync(SaveRecipeArgs args);
        Task<Recipe> UpdateRecipeAsync(SaveRecipeArgs args);
        Task<Recipe> GetRecipeAsync(GetRecipeArgs args);
        Task<IPagedResult<Recipe>> GetRecipesAsync(GetRecipesArgs args);
        Task DeleteRecipeAsync(DeleteRecipeArgs args);
    }
}