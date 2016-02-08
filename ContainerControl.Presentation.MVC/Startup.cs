using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContainerControl.Presentation.MVC.Startup))]
namespace ContainerControl.Presentation.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
