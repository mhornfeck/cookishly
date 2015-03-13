using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookishly.Services.Tests.Integration
{
    public class IntegrationTestsDataUtility
    {
        public ApplicationUser TestUser1 { get; private set; }
        public ApplicationUser TestUser2 { get; private set; }
        public IngredientEntity[] BuiltInIngredients { get; private set; }
        public IngredientEntity[] CustomIngredients { get; private set; }
        public RecipeEntity[] Recipes { get; private set; }
        public IngredientSpecificationEntity[] IngredientSpecifications { get; private set; }

        public IntegrationTestsDataUtility()
        {
            TestUser1 = new ApplicationUser
            {
                UserName = "testuser1@example.com",
                ProfileId = 1,
                Profile = new ProfileEntity
                {
                    Id = 1
                }
            };

            TestUser2 = new ApplicationUser
            {
                UserName = "testuser2@example.com",
                ProfileId = 2,
                Profile = new ProfileEntity
                {
                    Id = 2
                }
            };

            BuiltInIngredients = new[]
            {
                new IngredientEntity
                {
                    Id = 1,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Tomatoes"
                },

                new IngredientEntity
                {
                    Id = 2,
                    Category = IngredientCategory.DryGrains,
                    ImageUrl = "",
                    Name = "Spaghetti Noodles"
                },

                new IngredientEntity
                {
                    Id = 3,
                    Category = IngredientCategory.Dairy,
                    ImageUrl = "",
                    Name = "Eggs"
                },

                new IngredientEntity
                {
                    Id = 4,
                    Category = IngredientCategory.Dairy,
                    ImageUrl = "",
                    Name = "Milk"
                },

                new IngredientEntity
                {
                    Id = 5,
                    Category = IngredientCategory.CannedJarred,
                    ImageUrl = "",
                    Name = "Black Beans"
                }
            };

            CustomIngredients = new[]
            {
                new IngredientEntity
                {
                    Id = 6,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Cherry Tomatoes",
                    ProfileId = 1
                },

                new IngredientEntity
                {
                    Id = 7,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Poblano Peppers",
                    ProfileId = 1
                },

                new IngredientEntity
                {
                    Id = 8,
                    Category = IngredientCategory.Dairy,
                    ImageUrl = "",
                    Name = "Half & Half",
                    ProfileId = 1
                },

                new IngredientEntity
                {
                    Id = 9,
                    Category = IngredientCategory.Seasonings,
                    ImageUrl = "",
                    Name = "Basil",
                    ProfileId = 2
                }
            };

            Recipes = new[]
            {
                new RecipeEntity
                {
                    Id = 1,
                    Category = RecipeCategory.MainDishes,
                    ImageUrl = "",
                    Name = "Chili",
                    ProfileId = 1
                },

                new RecipeEntity
                {
                    Id = 2,
                    Category = RecipeCategory.MainDishes,
                    ImageUrl = "",
                    Name = "Spaghetti",
                    ProfileId = 2
                }
            };

            IngredientSpecifications = new[]
            {
                new IngredientSpecificationEntity
                {
                    Id = 1,
                    IngredientId = 1,
                    RecipeId = 1,
                    Preparation = "Chopped",
                    Quantity = "1",
                    Units = "cups"
                },
                new IngredientSpecificationEntity
                {
                    Id = 2,
                    IngredientId = 6,
                    RecipeId = 1,
                    Preparation = "Chopped",
                    Quantity = "2",
                    Units = "cups"
                },
                new IngredientSpecificationEntity
                {
                    Id = 3,
                    IngredientId = 1,
                    RecipeId = 2,
                    Preparation = "Blended",
                    Quantity = "4",
                    Units = "cups"
                },
                new IngredientSpecificationEntity
                {
                    Id = 4,
                    IngredientId = 8,
                    RecipeId = 2,
                    Preparation = "Chopped",
                    Quantity = "2",
                    Units = "tbsp"
                }
            };

        }

        public void SeedTestData()
        {
            ClearTestData();

            using (var context = new CookishlyContext())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                if (context.Users.Any(x => x.UserName.Equals(TestUser1.UserName)) == false)
                {
                    manager.Create(TestUser1, "recipe8801");
                }

                if (context.Users.Any(x => x.UserName.Equals(TestUser2.UserName)) == false)
                {
                    manager.Create(TestUser2, "recipe8802");
                }

                context.Ingredients.AddOrUpdate(BuiltInIngredients);
                context.Ingredients.AddOrUpdate(CustomIngredients);
                context.Recipes.AddOrUpdate(Recipes);
                context.IngredientSpecifications.AddOrUpdate(IngredientSpecifications);

                context.SaveChanges();
            }
        }

        public void ClearTestData()
        {
            using (var context = new CookishlyContext())
            {
                var maxCustomIngredientId = CustomIngredients.Max(c => c.Id);
                var testCustomIngredients = context.Ingredients.Where(x => x.Id > maxCustomIngredientId);
                context.Ingredients.RemoveRange(testCustomIngredients);

                context.SaveChanges();
            }
        }
    }
}