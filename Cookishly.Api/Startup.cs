﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Cookishly.Api.Startup))]

namespace Cookishly.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
