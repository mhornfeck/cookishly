using OpenQA.Selenium;
using TestStack.Seleno.PageObjects;

namespace Cookishly.Web.Tests.FunctionalTests.PageObjects
{
    public class HomePage : Page
    {
        public string MainHeading
        {
            get
            {
                var lead = Find.Element(By.ClassName("lead"));
                return lead.Text;
            }
        }
    }
}