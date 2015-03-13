namespace Cookishly.Domain
{
    public class IngredientSpecification
    {
        public int Id { get; set; }

        public string Quantity { get; set; }

        public string Units { get; set; }

        public string Preparation { get; set; }

        public Ingredient Ingredient { get; set; }

        public int RecipeId { get; set; }
    }
}