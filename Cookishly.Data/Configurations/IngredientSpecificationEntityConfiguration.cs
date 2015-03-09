using Cookishly.Data.Entities;

namespace Cookishly.Data.Configurations
{
    public class IngredientSpecificationEntityConfiguration : EntityBaseConfiguration<IngredientSpecificationEntity>
    {
        public IngredientSpecificationEntityConfiguration()
        {
            ToTable("IngredientSpecifications");
            HasRequired(x => x.Ingredient).WithMany(x => x.IngredientSpecifications);
        }
    }
}