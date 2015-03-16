using System;
using System.Threading.Tasks;
using System.Web.Http;
using Cookishly.Api.Models.Ingredients;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;

namespace Cookishly.Api.Controllers
{
    [Authorize]
    public class IngredientsController : ApiController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IHttpActionResult> Post([FromBody] CreateIngredientBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var args = new SaveIngredientArgs
                {
                    Ingredient = new Ingredient
                    {
                        Name = bindingModel.Name,
                        Category = bindingModel.Category,
                        ImageUrl = bindingModel.ImageUrl
                    },
                    Username = RequestContext.Principal.Identity.Name
                };

                var ingredient = await _ingredientService.CreateIngredientAsync(args);
                return Ok(ingredient);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        public async Task<IHttpActionResult> Put([FromBody] UpdateIngredientBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var args = new SaveIngredientArgs
                {
                    Ingredient = new Ingredient
                    {
                        Id = bindingModel.Id,
                        Name = bindingModel.Name,
                        Category = bindingModel.Category,
                        ImageUrl = bindingModel.ImageUrl
                    },
                    Username = RequestContext.Principal.Identity.Name
                };

                var ingredient = await _ingredientService.UpdateIngredientAsync(args);
                return Ok(ingredient);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        public async Task<IHttpActionResult> Get([FromUri] GetIngredientsBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            try
            {
                var args = new GetIngredientsArgs
                {
                    Username = RequestContext.Principal.Identity.Name,
                    IngredientCategory = bindingModel.IngredientCategory,
                    IngredientType = bindingModel.IngredientType,
                    Limit = bindingModel.Limit,
                    Offset = bindingModel.Offset,
                    SearchTerms = bindingModel.SearchTerms
                };

                var ingredients = await _ingredientService.GetIngredientsAsync(args);
                return Ok(ingredients);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("api/ingredients/{id:int}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var args = new DeleteIngredientArgs
                {
                    IngredientId = id,
                    Username = RequestContext.Principal.Identity.Name
                };

                await _ingredientService.DeleteIngredientAsync(args);
                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
