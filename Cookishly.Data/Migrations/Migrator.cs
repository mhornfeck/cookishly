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