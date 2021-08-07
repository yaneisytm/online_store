using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineStoreAppMVC.Startup))]
namespace OnlineStoreAppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
