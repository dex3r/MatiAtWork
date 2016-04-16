using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Game2048WebView.Startup))]
namespace Game2048WebView
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
