using System.Collections;
using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public class SaveIngredientArgs
    {
        public Ingredient Ingredient { get; set; }
        public string Username { get; set; }
    }

    public class PagingArgs
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetIngredientsArgs : PagingArgs
    {
        public string Username { get; set; }
        public IngredientCategory? IngredientCategory { get; set; }
        public IngredientType IngredientType { get; set; }
        public IList<string> SearchTerms { get; set; }
    }
}