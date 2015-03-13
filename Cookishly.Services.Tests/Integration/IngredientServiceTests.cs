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
        public void Create_GivenValidIngredient_CreatesIngredient()
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

            var result = _service.CreateIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            var resultContent = result.Content;

            Assert.IsNotNull(resultContent);
            Assert.AreEqual(ingredient.Name, resultContent.Name);
            Assert.AreEqual(ingredient.Category, resultContent.Category);
            Assert.IsTrue(resultContent.Id > 0);
        }

        [Test]
        public void Update_GivenBuiltInIngredientId_Fails()
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

            var result = _service.UpdateIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Update_GivenOtherProfilesIngredientId_Fails()
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

            var result = _service.UpdateIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Update_GivenValidIngredientArgs_Succeeds()
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

            var result = _service.UpdateIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            var resultContent = result.Content;

            Assert.IsNotNull(resultContent);
            Assert.AreEqual(ingredient.Name, resultContent.Name);
            Assert.AreEqual(ingredient.Category, resultContent.Category);
            Assert.AreEqual(ingredient.Id, resultContent.Id);
        }

        [Test]
        public void Get_GivenAllIngredientTypes_ReturnsCorrectCounts()
        {
            var testUser = _data.TestUser2;

            var args = new GetIngredientsArgs
            {
                Username = testUser.UserName,
                IngredientType = IngredientType.All,
                Limit = 3,
                Offset = 1
            };

            var result = _service.GetIngredientsAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            var expectedRecordCount = _data.BuiltInIngredients.Count() +
                                      _data.CustomIngredients.Count(x => x.ProfileId.Equals(testUser.ProfileId));

            var resultContent = result.Content;

            Assert.IsNotNull(resultContent);
            Assert.AreEqual(expectedRecordCount, resultContent.TotalItemCount);
            Assert.AreEqual(args.Offset, resultContent.PageNumber);
            Assert.AreEqual(args.Limit, resultContent.PageSize);

            Assert.IsTrue(resultContent.PageItemCount <= args.Limit);
        }

        [Test]
        public void Delete_GivenIngredientUsedInRecipes_Fails()
        {
            var testUser = _data.TestUser1;

            var args = new DeleteIngredientArgs
            {
                IngredientId = 6,
                Username = testUser.UserName
            };

            var result = _service.DeleteIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Delete_GivenBuiltInIngredient_Fails()
        {
            var testUser = _data.TestUser1;

            var args = new DeleteIngredientArgs
            {
                IngredientId = 1,
                Username = testUser.UserName
            };

            var result = _service.DeleteIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Delete_GivenUnusedIngredient_Succeeds()
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

            var result = _service.DeleteIngredientAsync(args).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
