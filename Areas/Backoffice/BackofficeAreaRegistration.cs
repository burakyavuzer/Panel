using System.Web.Mvc;

namespace Yeni_Panel_2015.Areas.Backoffice
{
    public class BackofficeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Backoffice";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Backoffice_login", "Login/{action}/{id}", new { controller = "Login", action = "Index", id = UrlParameter.Optional });
            context.MapRoute("Backoffice_default", "Backoffice/{action}/{id}/{lang}", new { controller = "Backoffice", action = "Index", lang = UrlParameter.Optional, id = UrlParameter.Optional });
            context.MapRoute("Backoffice_album", "Album/AlbumOlustur", new { controller = "Backoffice", action = "AlbumOlustur", id = UrlParameter.Optional });
            context.MapRoute("Backoffice_menu", "Menu/MenuSiraGuncelle", new { controller = "Backoffice", action = "MenuSiraGuncelle", id = UrlParameter.Optional });
            context.MapRoute("Backoffice_menuKaydet", "Menu/MenuKaydet", new { controller = "Backoffice", action = "MenuKaydet", id = UrlParameter.Optional });
            context.MapRoute("Backoffice_menuSil", "Menu/MenuSil", new { controller = "Backoffice", action = "MenuSil", id = UrlParameter.Optional });

        }
    }
}
