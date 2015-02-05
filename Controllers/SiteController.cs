using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yeni_Panel_2015.Controllers
{
    public class SiteController : Controller
    {
        //
        // GET: /Site/
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SiteDilDegistir(string LANGCODE) 
        {
            Session.Remove("site_LANG");

            Session["site_LANG"] = LANGCODE;

            return LANGCODE;
        }

    }
}
