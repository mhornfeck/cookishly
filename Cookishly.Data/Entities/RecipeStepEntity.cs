namespace Cookishly.Data.Entities
{
    public class RecipeStepEntity : EntityBase
    {
        public int Ordinal { get; set; }

        public string Description { get; set; }

        public int RecipeId { get; set; }

        public RecipeEntity Recipe { get; set; }
    }
}