using System.ComponentModel.DataAnnotations;

namespace Cookishly.Api.Models
{
    public class UpdateIngredientBindingModel : CreateIngredientBindingModel
    {
        [Required]
        public int Id { get; set; }
    }
}