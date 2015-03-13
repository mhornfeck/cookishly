using System.Threading.Tasks;
using System.Web.Http;
using Cookishly.Services.Contract;

namespace Cookishly.Api.Controllers
{
    public class IngredientsController : ApiController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IHttpActionResult> Get(GetIngredientsArgs args)
        {
            var result = await _ingredientService.GetIngredientsAsync(args);

            if (result.IsSuccess == false)
            {
                return InternalServerError();
            }
            
            return Ok(result.Data);
        }
    }
}
