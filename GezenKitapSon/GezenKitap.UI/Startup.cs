using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GezenKitap.UI.Startup))]
namespace GezenKitap.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
