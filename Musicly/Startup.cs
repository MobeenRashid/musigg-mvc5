using Microsoft.Owin;
using Musicly;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Musicly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
