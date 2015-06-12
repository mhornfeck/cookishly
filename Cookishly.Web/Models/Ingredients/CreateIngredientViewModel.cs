using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Cookishly.Domain;

namespace Cookishly.Web.Models.Ingredients
{
	public class CreateIngredientViewModel
	{
		public string Name { get; set; }

		[Display(Name = "Image")]
		public string ImageUrl { get; set; }

		public IngredientCategory Category { get; set; }

		public IList<SelectListItem> Categories
		{
			get { return EnumHelper.GetSelectList(typeof(IngredientCategory)); }
		}
	}
}