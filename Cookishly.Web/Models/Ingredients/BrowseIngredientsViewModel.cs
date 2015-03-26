using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Cookishly.Domain;

namespace Cookishly.Web.Models.Ingredients
{
    public class BrowseIngredientsViewModel
    {
        public string Title { get; set; }

        public IList<SelectListItem> Categories
        {
            get { return EnumHelper.GetSelectList(typeof (IngredientCategory)); }
        }
    }
}