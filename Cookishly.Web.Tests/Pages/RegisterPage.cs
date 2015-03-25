using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cookishly.Web.Models;
using OpenQA.Selenium;
using TestStack.Seleno.PageObjects;


namespace Cookishly.Web.Tests.Pages
{
    public class RegisterPage : Page<RegisterViewModel>
    {
        public RegisterPage EnterRegisterFormData(RegisterViewModel data)
        {
            var passwordField = Find.Element(By.Id("Password"));
            passwordField.SendKeys(data.Password);

            var confirmPasswordField = Find.Element(By.Id("ConfirmPassword"));
            confirmPasswordField.SendKeys(data.ConfirmPassword);

            var emailField = Find.Element(By.Id("Email"));
            emailField.SendKeys(data.Email);

            return this;
        }

        public T SubmitRegisterForm<T>() where T : UiComponent, new()
        {
            return Navigate.To<T>(By.Id("SignUpButton"));
        }
    }
}
