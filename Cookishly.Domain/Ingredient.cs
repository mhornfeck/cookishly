namespace Cookishly.Domain
{
    public class Ingredient 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IngredientCategory Category { get; set; }
    }
}