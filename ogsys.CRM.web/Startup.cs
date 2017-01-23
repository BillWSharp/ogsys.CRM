using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ogsys.CRM.Web.Startup))]
namespace ogsys.CRM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
