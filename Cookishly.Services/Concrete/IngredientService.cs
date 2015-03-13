using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Cookishly.Services.Contract;

namespace Cookishly.Services.Concrete
{
    public class IngredientService : IIngredientService
    {
        public async Task<IResult<Ingredient>> CreateIngredientAsync(SaveIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var newIngredientEntity = new IngredientEntity(args.Ingredient)
                {
                    ProfileId = user.ProfileId
                };

                context.Ingredients.Add(newIngredientEntity);
                await context.SaveChangesAsync();

                return ServiceResult<Ingredient>.Success(newIngredientEntity.ToDomain());
            }
        }

        public async Task<IResult<Ingredient>> UpdateIngredientAsync(SaveIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);
                var ingredientEntity = await context.Ingredients.FindAsync(args.Ingredient.Id);

                if (ingredientEntity != null)
                {
                    if (ingredientEntity.ProfileId.Equals(user.ProfileId) == false)
                    {
                        return ServiceResult<Ingredient>.Fail("Failed to update ingredient. User is not authorized.");
                    }

                    ingredientEntity.Update(args.Ingredient);
                    await context.SaveChangesAsync();
                    return ServiceResult<Ingredient>.Success(ingredientEntity.ToDomain());
                }

                return ServiceResult<Ingredient>.Fail("Failed to update ingredient. Ingredient not found.");
            }
        }

        public async Task<IPagedResult<Ingredient>> GetIngredientsAsync(GetIngredientsArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var ingredientEntities = context.Ingredients.Where(x => x.ProfileId == null || x.ProfileId == user.ProfileId);

                switch (args.IngredientType)
                {
                    case IngredientType.BuiltIn:
                        ingredientEntities = ingredientEntities.Where(x => x.ProfileId == null);
                        break;
                    case IngredientType.Custom:
                        ingredientEntities = ingredientEntities.Where(x => x.ProfileId != null);
                        break;
                }

                if (args.IngredientCategory.HasValue)
                {
                    ingredientEntities = ingredientEntities.Where(x => x.Category == args.IngredientCategory.Value);
                }

                var pageIngredients = await ingredientEntities.OrderBy(x => x.Name)
                    .Skip(args.Limit * args.Offset)
                    .Take(args.Offset).ToListAsync();

                var ingredients = pageIngredients.Select(x => x.ToDomain()).ToList();

                return PagedResult<Ingredient>.Success(ingredientEntities.Count(), args.Limit, args.Offset, ingredients);
            }
        }

        public async Task<IResult> DeleteIngredientAsync(DeleteIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var ingredient = context.Ingredients.Where(x => x.ProfileId == user.ProfileId)
                    .SingleOrDefault(x => x.Id == args.IngredientId);

                if (ingredient == null)
                {
                    return ServiceResult.Fail("Ingredient could not be found or user is not authorized.");
                }

                if (ingredient.IngredientSpecifications.Any())
                {
                    return ServiceResult.Fail("Ingredient could not be deleted because it is used in at least one recipe.");
                }

                context.Ingredients.Remove(ingredient);
                await context.SaveChangesAsync();

                return ServiceResult.Success("Ingredient was deleted.");
            }
        }
    }
}
