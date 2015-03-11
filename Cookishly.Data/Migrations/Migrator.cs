using System.Data.Entity.Migrations;
using Cookishly.Data.Entities;
using Cookishly.Domain;

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
            var ingredient = new IngredientEntity
            {
                Id = 1,
                Category = IngredientCategory.Produce,
                ImageUrl = "",
                Name = "Tomatoes"
            };

            _context.Ingredients.AddOrUpdate(ingredient);
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