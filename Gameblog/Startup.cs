using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gameblog.Startup))]
namespace Gameblog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
