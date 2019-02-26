using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WikiHash.Startup))]
namespace WikiHash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            WikiHash.Configuration.Load();
        }
    }
}
