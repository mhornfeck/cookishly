using Cookishly.Data.Migrations;
using NUnit.Framework;

namespace Cookishly.Services.Tests.Integration
{
    [SetUpFixture]
    public class ServiceTestsSetupFixture
    {
        [SetUp]
        public void Init()
        {
            Migrator.MigrateToLatestVersion();
        }
    }
}