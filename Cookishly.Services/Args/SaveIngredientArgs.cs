using Cookishly.Domain;

namespace Cookishly.Services.Args
{
    public class SaveIngredientArgs
    {
        public Ingredient Ingredient { get; set; }
        public string Username { get; set; }
    }
}