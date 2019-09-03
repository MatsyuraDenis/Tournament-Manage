using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tournament.Startup))]
namespace Tournament
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
