using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Services.Args
{
    public class GetRecipesArgs : PagingArgs
    {
        public string Username { get; set; }
        public RecipeCategory? RecipeCategory { get; set; }
        public IList<string> SearchTerms { get; set; }
        public IList<int> IngredientIds { get; set; } 
    }
}