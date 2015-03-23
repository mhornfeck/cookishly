using System.Threading.Tasks;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Results;

namespace Cookishly.Services.Contract
{
    public interface IIngredientService
    {
        Task<Ingredient> CreateIngredientAsync(SaveIngredientArgs args);
        Task<Ingredient> UpdateIngredientAsync(SaveIngredientArgs args);
        Task<Ingredient> GetIngredientAsync(GetIngredientArgs args);
        Task<IPagedResult<Ingredient>> GetIngredientsAsync(GetIngredientsArgs args);
        Task DeleteIngredientAsync(DeleteIngredientArgs args);
    }
}