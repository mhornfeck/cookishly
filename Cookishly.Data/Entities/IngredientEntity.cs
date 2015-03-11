using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class IngredientEntity : EntityBase
    {
        public IngredientEntity()
        {
            IngredientSpecifications = new List<IngredientSpecificationEntity>();
        }

        public IngredientEntity(Ingredient ingredient) : this()
        {
            Update(ingredient);
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IngredientCategory Category { get; set; }

        public int? ProfileId { get; set; }

        public ProfileEntity Profile { get; set; }

        public IList<IngredientSpecificationEntity> IngredientSpecifications { get; set; }

        public Ingredient ToDomain()
        {
            return new Ingredient
            {
                Id = Id,
                Name = Name,
                ImageUrl = ImageUrl,
                Category = Category
            };
        }

        public void Update(Ingredient ingredient)
        {
            Name = ingredient.Name;
            ImageUrl = ingredient.ImageUrl;
            Category = ingredient.Category;
        }
    }
}