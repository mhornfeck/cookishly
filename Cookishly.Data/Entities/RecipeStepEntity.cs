using System.Security.Cryptography;
using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class RecipeStepEntity : EntityBase
    {
        public RecipeStepEntity()
        {
            
        }

        public RecipeStepEntity(RecipeStep recipeStep)
        {
            Ordinal = recipeStep.Ordinal;
            Description = recipeStep.Description;
            RecipeId = recipeStep.RecipeId;
        }

        public int Ordinal { get; set; }

        public string Description { get; set; }

        public int RecipeId { get; set; }

        public RecipeEntity Recipe { get; set; }

        public RecipeStep ToDomain()
        {
            return new RecipeStep
            {
                Id = Id,
                Ordinal = Ordinal,
                Description = Description,
                RecipeId = RecipeId
            };
        }
    }
}