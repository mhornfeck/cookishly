using Castle.Core.Logging;
using TestStack.Seleno.Configuration;

namespace Cookishly.Web.Tests
{
    public static class BrowserHost
    {
        public static readonly SelenoHost Instance = new SelenoHost();
        public static readonly string RootUrl;

        static BrowserHost()
        {
            Instance.Run("Cookishly.Web", 4223, c => c
                .UsingLoggerFactory(new ConsoleFactory())
                .WithRouteConfig(RouteConfig.RegisterRoutes));

            RootUrl = Instance.Application.Browser.Url;
        }

    }
}
