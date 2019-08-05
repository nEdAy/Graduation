using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wool.Startup))]
namespace wool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
