using System.Collections.Generic;

namespace Cookishly.Data.Entities
{
    public class ProfileEntity : EntityBase
    {
        public IList<IngredientEntity> Ingredients { get; set; }

        public IList<RecipeEntity> Recipes { get; set; }

        public IList<ApplicationUser> Users { get; set; } 
    }
}