using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class IngredientSpecificationEntityConfiguration : EntityBaseConfiguration<IngredientSpecificationEntity>
    {
        public IngredientSpecificationEntityConfiguration()
        {
            HasRequired(x => x.Ingredient).WithMany(x => x.IngredientSpecifications);
        }
    }
}