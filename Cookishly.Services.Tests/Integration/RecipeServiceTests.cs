using System.Collections.Generic;
using System.Linq;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Concrete;
using Cookishly.Services.Contract;
using NUnit.Framework;

namespace Cookishly.Services.Tests.Integration
{
    [TestFixture]
    public class RecipeServiceTests
    {
        private IRecipeService _service;
        private IntegrationTestsDataUtility _data;

        [TestFixtureSetUp]
        public void Init()
        {
            _service = new RecipeService();
            _data = new IntegrationTestsDataUtility();

            _data.SeedTestData();
        }

        [Test]
        public async void Update_GivenValidIngredientArgs_Succeeds()
        {
            var recipe = new Recipe
            {
                Id = 1,
                Category = RecipeCategory.MainDishes,
                ImageUrl = "",
                Name = "Chili",
                Ingredients = new List<IngredientSpecification>
                {
                    new IngredientSpecification
                    {
                        Id = 1,
                        IngredientId = 1,
                        RecipeId = 1,
                        Preparation = "Minced",
                        Quantity = "2",
                        Units = "cups"
                    },
                    new IngredientSpecification
                    {
                        Id = Global.DefaultId,
                        IngredientId = 5,
                        RecipeId = 1,
                        Preparation = "",
                        Quantity = "12",
                        Units = "ounces"
                    }
                }
            };

            var args = new SaveRecipeArgs
            {
                Recipe = recipe,
                Username = _data.TestUser1.UserName
            };

            var result = await _service.UpdateRecipeAsync(args);

            Assert.IsNotNull(result);
            Assert.AreEqual(recipe.Name, result.Name);
            Assert.AreEqual(recipe.Category, result.Category);
            Assert.AreEqual(recipe.Id, result.Id);

            Assert.IsTrue(result.Ingredients.Any(x => x.Id == 1));
            Assert.IsTrue(result.Ingredients.All(x => x.Id != Global.DefaultId));
        }
    }
}