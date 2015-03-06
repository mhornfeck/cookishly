using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class IngredientEntity : EntityBase
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IngredientCategory Category { get; set; }

        public int ProfileId { get; set; }

        public ProfileEntity Profile { get; set; }

        public IList<IngredientSpecificationEntity> IngredientSpecifications { get; set; }
    }
}