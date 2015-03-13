using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class RecipeEntity : EntityBase
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public RecipeCategory Category { get; set; }

        public int ProfileId { get; set; }

        public ProfileEntity Profile { get; set; }

        public IList<IngredientSpecificationEntity> Ingredients { get; set; }

        public IList<RecipeStepEntity> Steps { get; set; }

        public Recipe ToDomain()
        {
            return new Recipe();
        }
    }
}
