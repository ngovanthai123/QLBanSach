using System.Web.Mvc;

namespace ShopOnline.Areas.AdminTK
{
    public class AdminTKAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminTK";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 name: "AdminTK_default",
                url: "AdminTK/{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "ShopOnline.Areas.AdminTK.Controllers" }
             );
        }
    }
}