using System.Linq;
using Cookishly.Domain;
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

            var resultPayload = result.Payload;

            Assert.IsNotNull(resultPayload);
            Assert.AreEqual(ingredient.Name, resultPayload.Name);
            Assert.AreEqual(ingredient.Category, resultPayload.Category);
            Assert.IsTrue(resultPayload.Id > 0);
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

            var resultPayload = result.Payload;

            Assert.IsNotNull(resultPayload);
            Assert.AreEqual(ingredient.Name, resultPayload.Name);
            Assert.AreEqual(ingredient.Category, resultPayload.Category);
            Assert.AreEqual(ingredient.Id, resultPayload.Id);
        }

        [Test]
        public void Get_GivenAllIngredientTypes_ReturnsCorrectCounts()
        {
            var testUser = _data.TestUser1;

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

            Assert.AreEqual(expectedRecordCount, result.TotalRecords);
            Assert.AreEqual(args.Offset, result.PageNumber);
            Assert.AreEqual(args.Limit, result.PageSize);

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Count <= args.Limit);
        }
    }
}
