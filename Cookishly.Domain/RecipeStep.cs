namespace Cookishly.Domain
{
    public class RecipeStep
    {
        public int Id { get; set; }

        public int Ordinal { get; set; }

        public string Description { get; set; }

        public int RecipeId { get; set; }
    }
}