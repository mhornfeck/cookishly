using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Cookishly.Api.Controllers;
using Cookishly.Api.Models.Ingredients;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;
using Cookishly.Services.Results;
using NSubstitute;
using NUnit.Framework;

namespace Cookishly.Api.Tests
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        [Test]
        public async void Get_GivenValidModel_ReturnsPageOfIngredients()
        {
            var requestData = new GetIngredientsBindingModel();

            IPagedResult<Ingredient> ingredientsResult = new PagedResult<Ingredient>
            {
                Items = Enumerable.Repeat(new Ingredient(), 5).ToList(),
                PageItemCount = 5,
                PageNumber = 0,
                PageSize = 5,
                TotalItemCount = 20
            };

            var ingredientsService = Substitute.For<IIngredientService>();
            ingredientsService.GetIngredientsAsync(Arg.Any<GetIngredientsArgs>()).Returns(Task.FromResult(ingredientsResult));

            var controller = new IngredientsController(ingredientsService);

            var result = (await controller.Get(requestData)) as OkNegotiatedContentResult<IPagedResult<Ingredient>>;

            Assert.IsNotNull(result, "Result should be a 200 OK result.");

            var resultContent = result.Content;
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(ingredientsResult, resultContent);
        }

        [Test]
        public async void Post_GivenModelWithErrors_ReturnsBadRequest()
        {
            var requestData = new CreateIngredientBindingModel();

            var ingredientsService = Substitute.For<IIngredientService>();
            var controller = new IngredientsController(ingredientsService);

            // simulate model state errors
            controller.ModelState.AddModelError("Error", "An error occured.");

            var result = await controller.Post(requestData);

            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public async void Post_GivenValidModel_ReturnsOk()
        {
            var requestData = new CreateIngredientBindingModel();

            var ingredientResult = new Ingredient();

            var ingredientsService = Substitute.For<IIngredientService>();
            ingredientsService.CreateIngredientAsync(Arg.Any<SaveIngredientArgs>()).Returns(Task.FromResult(ingredientResult));

            var controller = new IngredientsController(ingredientsService);

            var result = (await controller.Post(requestData)) as OkNegotiatedContentResult<Ingredient>;

            Assert.IsNotNull(result, "Result should be a 200 OK result.");

            var resultContent = result.Content;
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(ingredientResult, resultContent);
        }
    }
}
