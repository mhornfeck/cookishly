using System.Threading.Tasks;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Cookishly.Services.Contract;

namespace Cookishly.Services.Concrete
{
    public class IngredientService : IIngredientService
    {
        public IngredientService()
        {

        }

        public async Task<IResult<Ingredient>> CreateIngredientAsync(Ingredient args)
        {
            using (var context = new CookishlyContext())
            {
                var newIngredientEntity = new IngredientEntity(args);
                context.Ingredients.Add(newIngredientEntity);
                await context.SaveChangesAsync();

                return ServiceResult<Ingredient>.Success(newIngredientEntity.ToDomain());
            }
        }

        public async Task<IResult<Ingredient>> UpdateIngredientAsync(Ingredient args)
        {
            using (var context = new CookishlyContext())
            {
                var ingredientEntity = await context.Ingredients.FindAsync(args.Id);

                if (ingredientEntity != null)
                {
                    ingredientEntity.Update(args);
                    await context.SaveChangesAsync();
                    return ServiceResult<Ingredient>.Success(ingredientEntity.ToDomain());
                }

                return ServiceResult<Ingredient>.Fail("Failed to update ingredient. Ingredient not found.");
            }
        }
    }
}
