using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlowerPower.Startup))]
namespace FlowerPower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
