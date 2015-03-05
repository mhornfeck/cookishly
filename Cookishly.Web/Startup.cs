using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cookishly.Web.Startup))]
namespace Cookishly.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
