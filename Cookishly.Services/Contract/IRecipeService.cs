using System.Threading.Tasks;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Results;

namespace Cookishly.Services.Contract
{
    public interface IRecipeService
    {
        Task<IResult<Recipe>> CreateRecipeAsync(SaveRecipeArgs args);
        Task<IResult<Recipe>> UpdateRecipeAsync(SaveRecipeArgs args);
        Task<IResult<IPagedResult<Recipe>>> GetRecipesAsync(GetRecipesArgs args);
        Task<IResult> DeleteRecipeAsync(DeleteRecipeArgs args);
    }
}