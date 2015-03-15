﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;

namespace Cookishly.Api.Controllers
{
    [Authorize]
    public class IngredientsController : ApiControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IHttpActionResult> Get([FromUri] GetIngredientsArgs args)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            try
            {
                args.Username = RequestContext.Principal.Identity.Name;
                var ingredients = await _ingredientService.GetIngredientsAsync(args);
                return Ok(ingredients);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
