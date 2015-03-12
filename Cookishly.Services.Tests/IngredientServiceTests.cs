using Cookishly.Data.Migrations;
using Cookishly.Domain;
using Cookishly.Services.Concrete;
using Cookishly.Services.Contract;
using NUnit.Framework;

namespace Cookishly.Services.Tests
{
    [TestFixture]
    public class IngredientServiceTests
    {
        private IIngredientService _service;

        [TestFixtureSetUp]
        public void Init()
        {
            Migrator.MigrateToLatestVersion();
            _service = new IngredientService();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            var migrator = new Migrator();
            migrator.CleanData();
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
                Username = "testuser@example.com"
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
    }
}
