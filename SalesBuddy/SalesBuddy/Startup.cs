using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesBuddy.Startup))]
namespace SalesBuddy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
