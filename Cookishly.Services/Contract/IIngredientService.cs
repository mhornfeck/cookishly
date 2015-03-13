using System.Threading.Tasks;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Results;

namespace Cookishly.Services.Contract
{
    public interface IIngredientService
    {
        Task<IResult<Ingredient>> CreateIngredientAsync(SaveIngredientArgs args);
        Task<IResult<Ingredient>> UpdateIngredientAsync(SaveIngredientArgs args);
        Task<IResult<IPagedResult<Ingredient>>> GetIngredientsAsync(GetIngredientsArgs args);
        Task<IResult> DeleteIngredientAsync(DeleteIngredientArgs args);
    }
}