using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParticeCustomer.Startup))]
namespace ParticeCustomer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
