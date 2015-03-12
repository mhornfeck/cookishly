using System.Threading.Tasks;
using Cookishly.Domain;
using Cookishly.Services.Concrete;

namespace Cookishly.Services.Contract
{
    public interface IIngredientService
    {
        Task<IResult<Ingredient>> CreateIngredientAsync(SaveIngredientArgs args);
        Task<IResult<Ingredient>> UpdateIngredientAsync(SaveIngredientArgs args);
    }
}