using System;
using System.Data.Entity;
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
    public class RecipeService : IRecipeService
    {
        public async Task<Recipe> CreateRecipeAsync(SaveRecipeArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var newRecipeEntity = new RecipeEntity(args.Recipe)
                {
                    ProfileId = user.ProfileId
                };

                context.Recipes.Add(newRecipeEntity);
                await context.SaveChangesAsync();

                return newRecipeEntity.ToDomain();
            }
        }

        public async Task<Recipe> UpdateRecipeAsync(SaveRecipeArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);
                var recipeEntity = await context.Recipes.FindAsync(args.Recipe.Id);

                if (recipeEntity == null)
                {
                    throw new Exception("Failed to update recipe. Recipe not found.");
                }

                if (recipeEntity.ProfileId.Equals(user.ProfileId) == false)
                {
                    throw new Exception("Failed to update recipe. User is not authorized.");
                }

                recipeEntity.UpdateProperties(args.Recipe);

                var specificationsToDelete = recipeEntity.Ingredients.Where(x => args.Recipe.Ingredients
                    .Select(i => i.Id)
                    .All(id => id != x.Id));

                context.IngredientSpecifications.RemoveRange(specificationsToDelete);

                var specificationsToUpdate = recipeEntity.Ingredients.Where(x => args.Recipe.Ingredients
                    .Select(i => i.Id)
                    .Any(id => id == x.Id));

                foreach (var specificationEntity in specificationsToUpdate)
                {
                    var specification = args.Recipe.Ingredients.SingleOrDefault(x => x.Id == specificationEntity.Id);
                    specificationEntity.Update(specification);
                }

                var specificationsToAdd = args.Recipe.Ingredients.Where(x => x.Id == Global.DefaultId)
                    .Select(x => new IngredientSpecificationEntity(x));

                context.IngredientSpecifications.AddRange(specificationsToAdd);

                await context.SaveChangesAsync();
                return recipeEntity.ToDomain();
            }
        }

        public async Task<IPagedResult<Recipe>> GetRecipesAsync(GetRecipesArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var recipeEntities = context.Recipes.Where(x => x.ProfileId == null || x.ProfileId == user.ProfileId);

                if (args.RecipeCategory.HasValue)
                {
                    recipeEntities = recipeEntities.Where(x => x.Category == args.RecipeCategory.Value);
                }

                if (args.IngredientIds.Any())
                {
                    recipeEntities = recipeEntities.Where(r => args.IngredientIds
                        .All(id => r.Ingredients
                            .Select(i => i.IngredientId)
                            .Any(iid => iid == id)));
                }

                var pageRecipeEntities = await recipeEntities.OrderBy(x => x.Name)
                    .Skip(args.Limit * args.Offset)
                    .Take(args.Limit)
                    .ToListAsync();

                var pageRecipes = pageRecipeEntities.Select(x => x.ToDomain()).ToList();
                var resultData = new PagedResult<Recipe>(pageRecipes, await recipeEntities.CountAsync(),
                    args.Limit, args.Offset);

                return resultData;
            }
        }

        public async Task DeleteRecipeAsync(DeleteRecipeArgs args)
        {
            using (var context = new CookishlyContext())
            {
                var user = await context.FindUserByUsernameAsync(args.Username);

                var recipe = context.Recipes.Where(x => x.ProfileId == user.ProfileId)
                    .SingleOrDefault(x => x.Id == args.RecipeId);

                if (recipe == null)
                {
                    throw new Exception("Recipe could not be found or user is not authorized.");
                }

                context.Recipes.Remove(recipe);
                await context.SaveChangesAsync();
            }
        }
    }
}