using System.ComponentModel.DataAnnotations;
using Cookishly.Domain;

namespace Cookishly.Api.Models.Ingredients
{
    public class CreateIngredientBindingModel
    {
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IngredientCategory Category { get; set; }
    }
}