﻿using System.Collections.Generic;
using Cookishly.Domain;

namespace Cookishly.Services.Contract
{
    public class GetRecipesArgs : PagingArgs
    {
        public string Username { get; set; }
        public RecipeCategory? RecipeCategory { get; set; }
        public IList<string> SearchTerms { get; set; }
    }
}