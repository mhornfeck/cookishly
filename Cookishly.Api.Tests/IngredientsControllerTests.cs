using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Cookishly.Api.Controllers;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Contract;
using Cookishly.Services.Results;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cookishly.Api.Tests
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        [Test]
        public async void Get_GivenValidArgs_ReturnsPageOfIngredients()
        {
            var args = new GetIngredientsArgs();

            IPagedResult<Ingredient> ingredientsResult = new PagedResult<Ingredient>
            {
                Items = Enumerable.Repeat(new Ingredient(), 5).ToList(),
                PageItemCount = 5,
                PageNumber = 0,
                PageSize = 5,
                TotalItemCount = 20
            };

            var ingredientsService = Substitute.For<IIngredientService>();
            ingredientsService.GetIngredientsAsync(args).Returns(Task.FromResult(ingredientsResult));

            var controller = new IngredientsController(ingredientsService);

            var result = (await controller.Get(args)) as OkNegotiatedContentResult<IPagedResult<Ingredient>>;

            Assert.IsNotNull(result, "Result should be a 200 OK result.");

            var resultContent = result.Content;
            Assert.IsNotNull(resultContent);
            Assert.AreEqual(ingredientsResult, resultContent);
        }
    }
}
