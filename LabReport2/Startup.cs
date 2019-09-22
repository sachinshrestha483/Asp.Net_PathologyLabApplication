using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabReport2.Startup))]
namespace LabReport2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
