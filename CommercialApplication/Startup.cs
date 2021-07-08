using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CommercialApplicationCommand
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configure Web Api for self-host
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            app.UseWebApi(config);
        }
    }
}