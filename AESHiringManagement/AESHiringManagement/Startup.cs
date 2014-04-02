using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AESHiringManagement.Startup))]
namespace AESHiringManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
