using Cookishly.Web.Controllers;
using Cookishly.Web.Models;
using Cookishly.Web.Tests.FunctionalTests.PageObjects;
using NUnit.Framework;

namespace Cookishly.Web.Tests.FunctionalTests
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void ValidRegistration()
        {
            BrowserHost.Instance.Application.Browser
                .Navigate()
                .GoToUrl(BrowserHost.RootUrl + @"Account\Register");

            var registerPage = BrowserHost.Instance.NavigateToInitialPage<AccountController, RegisterPage>(
                x => x.Register());

            var formData = new RegisterViewModel
            {
                Email = "testuser3@example.com",
                Password = "Recipe!8803",
                ConfirmPassword = "Recipe!8803"
            };

            var resultPage = registerPage.EnterRegisterFormData(formData).SubmitRegisterForm<HomePage>();

            Assert.AreEqual("ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.", resultPage.MainHeading);
        }
    }
}
