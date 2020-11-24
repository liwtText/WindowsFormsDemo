using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication6
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            #region 跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            //var allowedmethods = ConfigurationManager.AppSettings["core.allowedmethods"];
            //var allowedOrigin = ConfigurationManager.AppSettings["core.allowedOrigin"];
            //var allowedHeaders = ConfigurationManager.AppSettings["core.allowedHeaders"];
            //var geduCors = new EnableCorsAttribute(allowedmethods, allowedOrigin, allowedHeaders)
            //{
            //    SupportsCredentials= true
            //};
            //config.EnableCors(geduCors);
            #endregion

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
