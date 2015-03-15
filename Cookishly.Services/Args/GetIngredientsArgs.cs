using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cookishly.Domain;
using Cookishly.Services.Contract;

namespace Cookishly.Services.Args
{
    public class GetIngredientsArgs : PagingArgs
    {
        [Required]
        public string Username { get; set; }

        public IngredientCategory? IngredientCategory { get; set; }

        public IngredientType IngredientType { get; set; }

        public IList<string> SearchTerms { get; set; }
    }
}