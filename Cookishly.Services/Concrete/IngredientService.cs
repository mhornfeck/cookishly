using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;
using Cookishly.Services.Results;

namespace Cookishly.Services.Concrete
{
    public class IngredientService : IIngredientService
    {
        public async Task<Ingredient> CreateIngredientAsync(SaveIngredientArgs args)
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

                return newIngredientEntity.ToDomain();
            }
        }

        public async Task<Ingredient> UpdateIngredientAsync(SaveIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);
                var ingredientEntity = await context.Ingredients.FindAsync(args.Ingredient.Id);

                if (ingredientEntity == null)
                {
                    throw new Exception("Failed to update ingredient. Ingredient not found.");
                }

                if (ingredientEntity.ProfileId.Equals(user.ProfileId) == false)
                {
                    throw new Exception("Failed to update ingredient. User is not authorized.");
                }

                ingredientEntity.Update(args.Ingredient);
                await context.SaveChangesAsync();
                return ingredientEntity.ToDomain();
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

                var pageIngredientEntities = await ingredientEntities.OrderBy(x => x.Name)
                    .Skip(args.Limit * args.Offset)
                    .Take(args.Limit)
                    .ToListAsync();

                var pageIngredients = pageIngredientEntities.Select(x => x.ToDomain()).ToList();
                var resultData = new PagedResult<Ingredient>(pageIngredients, await ingredientEntities.CountAsync(), 
                    args.Limit, args.Offset);

                return resultData;
            }
        }

        public async Task DeleteIngredientAsync(DeleteIngredientArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var ingredient = context.Ingredients.Where(x => x.ProfileId == user.ProfileId)
                    .SingleOrDefault(x => x.Id == args.IngredientId);

                if (ingredient == null)
                {
                    throw new Exception("Ingredient could not be found or user is not authorized.");
                }

                if (ingredient.IngredientSpecifications.Any())
                {
                    throw new Exception("Ingredient could not be deleted because it is used in at least one recipe.");
                }

                context.Ingredients.Remove(ingredient);
                await context.SaveChangesAsync();
            }
        }
    }
}
