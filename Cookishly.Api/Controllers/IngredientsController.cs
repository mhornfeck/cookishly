using System;
using System.Threading.Tasks;
using System.Web.Http;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;

namespace Cookishly.Api.Controllers
{
    public class IngredientsController : ApiControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IHttpActionResult> Get(GetIngredientsArgs args)
        {
            var result = await _ingredientService.GetIngredientsAsync(args);
            return result.IsSuccess == false ? GetErrorResult(result) : Ok(result.Content);
        }
    }
}
