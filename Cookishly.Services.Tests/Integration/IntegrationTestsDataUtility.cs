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
                }
            };

            CustomIngredients = new[]
            {
                new IngredientEntity
                {
                    Id = 2,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Cherry Tomatoes",
                    ProfileId = 1
                },

                new IngredientEntity
                {
                    Id = 3,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Kale",
                    ProfileId = 2
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
                    manager.Create(TestUser1);
                }

                if (context.Users.Any(x => x.UserName.Equals(TestUser2.UserName)) == false)
                {
                    manager.Create(TestUser2);
                }

                context.Ingredients.AddOrUpdate(BuiltInIngredients);
                context.Ingredients.AddOrUpdate(CustomIngredients);

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