using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookishly.Services.Contract;
using Cookishly.Web.Models.Ingredients;

namespace Cookishly.Web.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public ActionResult Index()
        {
            var viewModel = new BrowseIngredientsViewModel
            {
                Title = "Ingredients"
            };

            return View(viewModel);
        }

	    public ActionResult Create()
	    {
		    var viewModel = new CreateIngredientViewModel();

		    return View(viewModel);
	    }
    }
}