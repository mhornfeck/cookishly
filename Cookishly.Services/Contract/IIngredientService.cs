using System.Threading.Tasks;
using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public interface IIngredientService
    {
        Task<IResult<Ingredient>> CreateIngredientAsync(Ingredient args);
        Task<IResult<Ingredient>> UpdateIngredientAsync(Ingredient args);
    }
}