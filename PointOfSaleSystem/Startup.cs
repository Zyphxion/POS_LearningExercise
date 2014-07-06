using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointOfSaleSystem.Startup))]
namespace PointOfSaleSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
