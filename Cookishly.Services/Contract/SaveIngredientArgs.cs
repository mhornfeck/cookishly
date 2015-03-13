using System.Collections;
using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public class SaveIngredientArgs
    {
        public Ingredient Ingredient { get; set; }
        public string Username { get; set; }
    }
}