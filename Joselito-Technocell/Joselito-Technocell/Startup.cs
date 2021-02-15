using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Joselito_Technocell.Startup))]
namespace Joselito_Technocell
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
