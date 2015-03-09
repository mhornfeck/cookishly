using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class RecipeEntityConfiguration : EntityBaseConfiguration<RecipeEntity>
    {
        public RecipeEntityConfiguration()
        {
            ToTable("Recipes");
            HasMany(x => x.Ingredients).WithRequired(x => x.Recipe);
            HasMany(x => x.Steps).WithRequired(x => x.Recipe);
            HasRequired(x => x.Profile).WithMany(x => x.Recipes);
        }
    }
}