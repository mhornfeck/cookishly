using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class IngredientEntityConfiguration : EntityBaseConfiguration<IngredientEntity>
    {
        public IngredientEntityConfiguration()
        {
            ToTable("Ingredients");
            HasOptional(x => x.Profile).WithMany(x => x.Ingredients);
            HasMany(x => x.IngredientSpecifications).WithRequired(x => x.Ingredient);
        }
    }
}