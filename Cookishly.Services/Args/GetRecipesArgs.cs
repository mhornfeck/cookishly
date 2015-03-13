using System.Collections.Generic;
using Cookishly.Domain;
using Cookishly.Services.Contract;

namespace Cookishly.Services.Args
{
    public class GetRecipesArgs : PagingArgs
    {
        public string Username { get; set; }
        public RecipeCategory? RecipeCategory { get; set; }
        public IList<string> SearchTerms { get; set; }
    }
}