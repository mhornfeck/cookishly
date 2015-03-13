using System.Threading.Tasks;
using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public interface IRecipeService
    {
        Task<IResult<Ingredient>> CreateRecipeAsync(SaveRecipeArgs args);
        Task<IResult<Ingredient>> UpdateRecipeAsync(SaveRecipeArgs args);
        Task<IPagedResult<Ingredient>> GetRecipesAsync(GetRecipesArgs args);
        Task<IResult> DeleteRecipeAsync(DeleteRecipeArgs args);
    }
}