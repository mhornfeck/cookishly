using System;
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

            args.Username = RequestContext.Principal.Identity.Name;
            var result = await _ingredientService.GetIngredientsAsync(args);
            return result.IsSuccess == false ? GetErrorResult(result) : Ok(result.Content);
        }
    }
}
