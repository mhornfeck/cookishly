﻿using Cookishly.Domain;

namespace Cookishly.Data.Entities
{
    public class IngredientSpecificationEntity : EntityBase
    {
        public string Quantity { get; set; }

        public string Units { get; set; }

        public string Preparation { get; set; }

        public int IngredientId { get; set; }

        public IngredientEntity Ingredient { get; set; }

        public int RecipeId { get; set; }

        public RecipeEntity Recipe { get; set; }
    }
}