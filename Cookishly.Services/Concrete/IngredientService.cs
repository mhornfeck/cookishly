using System.Threading.Tasks;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Cookishly.Services.Contract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookishly.Services.Concrete
{
    public class IngredientService : IIngredientService
    {
        public IngredientService()
        {

        }

        public async Task<IResult<Ingredient>> CreateIngredientAsync(SaveIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = await manager.FindByNameAsync(args.Username);

                var newIngredientEntity = new IngredientEntity(args.Ingredient)
                {
                    ProfileId = user.ProfileId
                };

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
