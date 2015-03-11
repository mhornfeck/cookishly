using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookishly.Data.Migrations
{
    public class Migrator
    {
        private readonly CookishlyContext _context;

        public Migrator() : this(new CookishlyContext())
        {
            
        }

        public Migrator(CookishlyContext context)
        {
            _context = context;
        }

        public static void MigrateToLatestVersion()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);

            migrator.Update();
        }

        public void SeedData()
        {
            var testUser = new ApplicationUser
            {
                UserName = "testuser@example.com",
                Profile = new ProfileEntity
                {
                    Id = 1
                }
            };

            if (_context.Users.Any(x => x.UserName.Equals(testUser.UserName)) == false)
            {
                var store = new UserStore<ApplicationUser>(_context);
                var manager = new UserManager<ApplicationUser>(store);
                manager.Create(testUser);
            }

            var builtInIngredients = new []
            {
                new IngredientEntity
                {
                    Id = 1,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Tomatoes"
                }
            };

            var customIngredients = new []
            {
                new IngredientEntity
                {
                    Id = 2,
                    Category = IngredientCategory.Produce,
                    ImageUrl = "",
                    Name = "Cherry Tomatoes",
                    ProfileId = 1
                }
            };

            _context.Ingredients.AddOrUpdate(builtInIngredients);
            _context.Ingredients.AddOrUpdate(customIngredients);
        }

        public void CleanData()
        {
            foreach (var ingredientEntity in _context.Ingredients)
            {
                _context.Ingredients.Remove(ingredientEntity);
            }

            _context.SaveChanges();
        }
    }
}