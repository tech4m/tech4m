using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tech4mUI.Startup))]
namespace tech4mUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }

    }
}
