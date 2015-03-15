using System;
using System.Linq;
using Cookishly.Data;
using Cookishly.Data.Entities;
using Cookishly.Domain;
using Cookishly.Services.Args;
using Cookishly.Services.Concrete;
using Cookishly.Services.Contract;
using NUnit.Framework;

namespace Cookishly.Services.Tests.Integration
{
    [TestFixture]
    public class IngredientServiceTests
    {
        private IIngredientService _service;
        private IntegrationTestsDataUtility _data;

        [TestFixtureSetUp]
        public void Init()
        {
            _service = new IngredientService();
            _data = new IntegrationTestsDataUtility();

            _data.SeedTestData();
        }

        [Test]
        public async void Create_GivenValidIngredient_CreatesIngredient()
        {
            var ingredient = new Ingredient
            {
                Name = "Cheese",
                Category = IngredientCategory.Dairy
            };

            var args = new SaveIngredientArgs
            {
                Ingredient = ingredient,
                Username = _data.TestUser1.UserName
            };

            var result = await _service.CreateIngredientAsync(args);

            Assert.IsNotNull(result);
            Assert.AreEqual(ingredient.Name, result.Name);
            Assert.AreEqual(ingredient.Category, result.Category);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public async void Update_GivenBuiltInIngredientId_Fails()
        {
            var ingredient = new Ingredient
            {
                Id = _data.BuiltInIngredients.First().Id,
                Name = "Cheese",
                Category = IngredientCategory.Dairy
            };

            var args = new SaveIngredientArgs
            {
                Ingredient = ingredient,
                Username = _data.TestUser1.UserName
            };

            await _service.UpdateIngredientAsync(args);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public async void Update_GivenOtherProfilesIngredientId_Fails()
        {
            var ingredient = new Ingredient
            {
                Id = _data.CustomIngredients.First(x => x.ProfileId != _data.TestUser1.ProfileId).Id,
                Name = "Cheese",
                Category = IngredientCategory.Dairy
            };

            var args = new SaveIngredientArgs
            {
                Ingredient = ingredient,
                Username = _data.TestUser1.UserName
            };

            await _service.UpdateIngredientAsync(args);
        }

        [Test]
        public async void Update_GivenValidIngredientArgs_Succeeds()
        {
            var ingredient = new Ingredient
            {
                Id = _data.CustomIngredients.First(x => x.ProfileId == _data.TestUser1.ProfileId).Id,
                Name = "Goat Cheese",
                Category = IngredientCategory.Dairy
            };

            var args = new SaveIngredientArgs
            {
                Ingredient = ingredient,
                Username = _data.TestUser1.UserName
            };

            var result = await _service.UpdateIngredientAsync(args);

            Assert.IsNotNull(result);
            Assert.AreEqual(ingredient.Name, result.Name);
            Assert.AreEqual(ingredient.Category, result.Category);
            Assert.AreEqual(ingredient.Id, result.Id);
        }

        [Test]
        public async void Get_GivenAllIngredientTypes_ReturnsCorrectCounts()
        {
            var testUser = _data.TestUser2;

            var args = new GetIngredientsArgs
            {
                Username = testUser.UserName,
                IngredientType = IngredientType.All,
                Limit = 3,
                Offset = 1
            };

            var result = await _service.GetIngredientsAsync(args);

            var expectedRecordCount = _data.BuiltInIngredients.Count() +
                                      _data.CustomIngredients.Count(x => x.ProfileId.Equals(testUser.ProfileId));

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRecordCount, result.TotalItemCount);
            Assert.AreEqual(args.Offset, result.PageNumber);
            Assert.AreEqual(args.Limit, result.PageSize);

            Assert.IsTrue(result.PageItemCount <= args.Limit);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public async void Delete_GivenIngredientUsedInRecipes_Fails()
        {
            var testUser = _data.TestUser1;

            var args = new DeleteIngredientArgs
            {
                IngredientId = 6,
                Username = testUser.UserName
            };

            await _service.DeleteIngredientAsync(args);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public async void Delete_GivenBuiltInIngredient_Fails()
        {
            var testUser = _data.TestUser1;

            var args = new DeleteIngredientArgs
            {
                IngredientId = 1,
                Username = testUser.UserName
            };

            await _service.DeleteIngredientAsync(args);
        }

        [Test]
        public async void Delete_GivenUnusedIngredient_Succeeds()
        {
            var testUser = _data.TestUser1;

            var newIngredient = new IngredientEntity
            {
                Name = "Test Ingredient",
                ProfileId = testUser.ProfileId
            };

            using (var context = new CookishlyContext())
            {
                context.Ingredients.Add(newIngredient);
                context.SaveChanges();
            }

            var args = new DeleteIngredientArgs
            {
                IngredientId = newIngredient.Id,
                Username = testUser.UserName
            };

            await _service.DeleteIngredientAsync(args);
        }
    }
}
