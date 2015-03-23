using System;
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
                Name = "Chili with Black Beans",
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

        [Test]
        [ExpectedException(typeof(Exception))]
        public async void Update_GivenOtherProfilesRecipeId_Fails()
        {
            var recipe = new Recipe
            {
                Id = _data.Recipes.First(x => x.ProfileId != _data.TestUser1.ProfileId).Id,
                Name = "Fake Recipe",
                Category = RecipeCategory.Other
            };

            var args = new SaveRecipeArgs()
            {
                Recipe = recipe,
                Username = _data.TestUser1.UserName
            };

            await _service.UpdateRecipeAsync(args);
        }

        [Test]
        public async void Get_GivenSpecificIngredientIds_ReturnsCorrectRecipes()
        {
            var testUser = _data.TestUser2;

            var args = new GetRecipesArgs
            {
                Username = testUser.UserName,
                IngredientIds = new []{ 8 }
            };

            var result = await _service.GetRecipesAsync(args);

            var expectedRecords = _data.Recipes.Where(r => r.ProfileId == testUser.ProfileId && 
                r.Ingredients.Any(i => args.IngredientIds.Contains(i.IngredientId)));

            Assert.IsNotNull(result);

            foreach (var expectedRecord in expectedRecords)
            {
                Assert.IsTrue(result.Items.Any(x => x.Id == expectedRecord.Id));
            }
        }

        [Test]
        public async void GetRecipe_GivenValidId_ReturnsCorrectRecipe()
        {
            var testUser = _data.TestUser2;
            var recipe = _data.Recipes.First(x => x.ProfileId == testUser.ProfileId);

            var args = new GetRecipeArgs
            {
                Username = testUser.UserName,
                RecipeId = recipe.Id
            };

            var result = await _service.GetRecipeAsync(args);

            Assert.IsNotNull(result);

            Assert.AreEqual(recipe.Id, result.Id);
            Assert.AreEqual(recipe.Name, result.Name);
        }

        [Test]
        [ExpectedException(typeof (Exception))]
        public async void GetRecipe_GivenInvalidId_Throws()
        {
            var testUser = _data.TestUser2;
            var recipe = _data.Recipes.First(x => x.ProfileId != testUser.ProfileId);

            var args = new GetRecipeArgs
            {
                Username = testUser.UserName,
                RecipeId = recipe.Id
            };

            var result = await _service.GetRecipeAsync(args);
        }
    }
}