using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Little_Beauty_2._0.Startup))]
namespace Little_Beauty_2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
