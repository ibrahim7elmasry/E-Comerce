using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectMvc.Startup))]
namespace ProjectMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
