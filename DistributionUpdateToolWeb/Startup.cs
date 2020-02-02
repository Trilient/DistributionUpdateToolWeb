using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistributionUpdateToolWeb.Startup))]
namespace DistributionUpdateToolWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
