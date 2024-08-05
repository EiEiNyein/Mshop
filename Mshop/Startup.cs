using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mshop.Startup))]
namespace Mshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
