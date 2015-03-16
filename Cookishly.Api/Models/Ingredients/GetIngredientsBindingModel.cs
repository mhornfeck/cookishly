using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Api.Models.Ingredients
{
    public class GetIngredientsBindingModel : PagingBindingModel
    {
        public IngredientCategory? IngredientCategory { get; set; }

        public IngredientType IngredientType { get; set; }

        public IList<string> SearchTerms { get; set; }
    }
}