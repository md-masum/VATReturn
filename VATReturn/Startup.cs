using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VATReturn.Startup))]
namespace VATReturn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
