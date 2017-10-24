using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestCor.Startup))]
namespace GestCor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
