using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class IngredientEntityConfiguration : EntityBaseConfiguration<IngredientEntity>
    {
        public IngredientEntityConfiguration()
        {
            HasOptional(x => x.Profile);
            HasMany(x => x.IngredientSpecifications).WithRequired(x => x.Ingredient);
        }
    }
}