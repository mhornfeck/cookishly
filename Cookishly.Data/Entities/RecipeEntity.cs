using System.Collections.Generic;
using System.Linq;
using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class RecipeEntity : EntityBase
    {
        private IList<IngredientSpecificationEntity> _ingredients;
        private IList<RecipeStepEntity> _steps;

        public RecipeEntity()
        {
            _ingredients = new List<IngredientSpecificationEntity>();
            _steps = new List<RecipeStepEntity>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public RecipeCategory Category { get; set; }

        public int ProfileId { get; set; }

        public ProfileEntity Profile { get; set; }

        public virtual IList<IngredientSpecificationEntity> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }

        public virtual IList<RecipeStepEntity> Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        public Recipe ToDomain()
        {
            return new Recipe
            {
                Id = Id,
                Name = Name,
                ImageUrl = ImageUrl,
                Category = Category,
                Ingredients = Ingredients.Select(x => x.ToDomain()).ToList()
            };
        }
    }
}
