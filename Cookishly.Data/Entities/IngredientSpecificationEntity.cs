using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class IngredientSpecificationEntity : EntityBase
    {
        public IngredientSpecificationEntity(IngredientSpecification ingredientSpecification)
        {
            Update(ingredientSpecification);
        }

        public IngredientSpecificationEntity()
        {
            
        }

        public string Quantity { get; set; }

        public string Units { get; set; }

        public string Preparation { get; set; }

        public int IngredientId { get; set; }

        public IngredientEntity Ingredient { get; set; }

        public int RecipeId { get; set; }

        public RecipeEntity Recipe { get; set; }

        public IngredientSpecification ToDomain()
        {
            return new IngredientSpecification
            {
                Id = Id,
                Quantity = Quantity,
                Units = Units,
                Preparation = Preparation,
                Ingredient = Ingredient.ToDomain(),
                RecipeId = RecipeId
            };
        }

        public void Update(IngredientSpecification ingredientSpecification)
        {
            Quantity = ingredientSpecification.Quantity;
            Units = ingredientSpecification.Units;
            Preparation = ingredientSpecification.Preparation;
            IngredientId = ingredientSpecification.Ingredient.Id;
            RecipeId = ingredientSpecification.RecipeId;
        }
    }
}