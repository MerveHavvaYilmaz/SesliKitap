using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SesliKitap.Startup))]
namespace SesliKitap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
