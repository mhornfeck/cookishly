using System.Data.Entity;
using System.Threading.Tasks;
using Cookishly.Data.Configurations;
using Cookishly.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookishly.Data
{
    public class CookishlyContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<IngredientSpecificationEntity> IngredientSpecifications { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RecipeStepEntity> RecipeSteps { get; set; } 

        public CookishlyContext() : base("DefaultConnection", false)
        {
        }

        public static CookishlyContext Create()
        {
            return new CookishlyContext();
        }

        public async Task<ApplicationUser> FindUserByUsernameAsync(string username)
        {
            var store = new UserStore<ApplicationUser>(this);
            var manager = new UserManager<ApplicationUser>(store);
            return await manager.FindByNameAsync(username);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProfileEntityConfiguration());
            modelBuilder.Configurations.Add(new RecipeEntityConfiguration());
            modelBuilder.Configurations.Add(new RecipeStepEntityConfiguration());
            modelBuilder.Configurations.Add(new IngredientEntityConfiguration());
            modelBuilder.Configurations.Add(new IngredientSpecificationEntityConfiguration());
        }
    }
}