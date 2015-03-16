using System.Collections.Generic;

namespace Cookishly.Domain
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public RecipeCategory Category { get; set; }

        public IList<IngredientSpecification> Ingredients { get; set; }

        public IList<RecipeStep> Steps { get; set; }  
    }
}
