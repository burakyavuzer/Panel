using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeni_Panel_2015.Classes;

namespace Yeni_Panel_2015.Areas.Backoffice.Controllers
{
    public class BackofficeController : Controller
    {        
        //
        // GET: /Backoffice/Backoffice/
        static string bag = Config.bag;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["_User"] == null)
            {
                Response.Redirect("/Login/Index");
            }
        }

        public ActionResult CikisYap()
        {
            Session.RemoveAll();
            Response.Cookies["panelUser"].Expires = DateTime.Now.AddYears(-30); // Beni hatırlayı sonlandır.
            Response.Redirect("/Login");

            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sayfalar()
        {
            return View();
        }
        public ActionResult SayfaEkle()
        {          
            return View();
        }
        public ActionResult SayfaDuzenle()
        {
            return View();
        }
        public ActionResult MenuYonetimi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string db_SayfaEkle() 
        {
            string Dil = Request.Form["Diller"];
            string Kategori = Request.Form["Kategori"];
            string SayfaAdı = Request.Form["SayfaAdi"];
            string Sıra = Request.Form["SiraNumarasi"];
            string Icerik = Request.Form["sayfaIcerigi"];

            var pages = Yeni_Panel_2015.Areas.Backoffice.Classes.Pages.SayfaEkle(Dil, Kategori, SayfaAdı, Sıra, Icerik);

            return pages;
        }

        [HttpPost]
        [ValidateInput(false)]
        public string db_SayfaDuzenle()
        {
            string Id = Request.Form["SayfaID"];
            string DilId = Request.Form["LangID"];
            string Dil = Request.Form["Diller"];
            string Kategori = Request.Form["Kategori"];
            string SayfaAdı = Request.Form["SayfaAdi"];
            string Sıra = Request.Form["SiraNumarasi"];
            string Icerik = Request.Form["sayfaIcerigi"];
            string MetaTitle = Request.Form["MetaTitle"];
            string MetaKeywords = Request.Form["MetaKeywords"];
            string MetaDescription = Request.Form["MetaDescription"];

            var pages = Yeni_Panel_2015.Areas.Backoffice.Classes.Pages.SayfaDuzenle(Id, DilId, Dil, Kategori, SayfaAdı, Sıra, Icerik, MetaTitle, MetaKeywords, MetaDescription);

            return pages;
        }
       
        public ActionResult AlbumOlustur(string AlbumAdi)
        {
            var media = Yeni_Panel_2015.Areas.Backoffice.Classes.MediaLibrary.AlbumOlustur(AlbumAdi);

            return View(media);
        }

        public ActionResult Ortam()
        {
            return View();
        }

        [HttpPost]
        public string db_OrtamKutuphanesiYukle(List<HttpPostedFileBase> fuResimlerUpload, string Media_ID)
        {
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();
                foreach (var f in fuResimlerUpload)
                {
                    if (fuResimlerUpload[0] != null)
                    {
                        string resimAdi = Guid.NewGuid().ToString() + Path.GetExtension(f.FileName.ToString());
                        string yolu = "~/Uploads/upload/" + resimAdi;
                        f.SaveAs(Server.MapPath(yolu));

                        SqlCommand sorgu3 = new SqlCommand("INSERT INTO _MEDIA_LIBRARY(MEDIA_ID,NAME,FILE_URL) values('" + Media_ID + "','" + resimAdi + "','" + resimAdi + "')", _conn);
                        sorgu3.ExecuteNonQuery();
                    }
                }
                _conn.Close();
            }

            return "";
        }

        [HttpPost]
        public string KutuphaneGuncelle(string ID, string NAME)
        {
            var ortam = Yeni_Panel_2015.Areas.Backoffice.Classes.MediaLibrary.OrtamGuncelle(ID,NAME);

            return ortam;
            
        }

        [HttpPost]
        public string KutuphaneSiraGuncelle(string ID, string NAME)
        {
            var ortam = Yeni_Panel_2015.Areas.Backoffice.Classes.MediaLibrary.OrtamSiraGuncelle(ID, NAME);

            return ortam;

        }

        [HttpPost]
        public string AlbumIciniBosalt(string ID)
        {
            var bosalt = Yeni_Panel_2015.Areas.Backoffice.Classes.MediaLibrary.AlbumIciniBosalt(ID);

            return bosalt;

        }

        [HttpPost]
        public string AlbumuSil(string ID)
        {
            var sil = Yeni_Panel_2015.Areas.Backoffice.Classes.MediaLibrary.AlbumuSil(ID);

            return sil;

        }

        [HttpPost]
        public string MenuSiraGuncelle() 
        {
            string response = Request.Form.ToString().Replace("%5b%5d", "[]").Replace("order[]=", "");
            var menu = Yeni_Panel_2015.Areas.Backoffice.Classes.GlobalQuery.MenuSiraGuncelle(response);
            return menu;
        }

        [HttpPost]
        public string MenuKaydet(string MenuAdi, string LangCode)
        {
            var menuKaydet = Yeni_Panel_2015.Areas.Backoffice.Classes.GlobalQuery.MenuKaydet(MenuAdi,LangCode);
            return menuKaydet;
        }

        [HttpPost]
        public string MenuSil(string MenuID)
        {
            var menuSil = Yeni_Panel_2015.Areas.Backoffice.Classes.GlobalQuery.MenuSil(MenuID);
            return menuSil;
        }

        [HttpPost]
        public string SayfaOrtamEkle(string ID, string PAGE_ID)
        {
            var ekle = Yeni_Panel_2015.Areas.Backoffice.Classes.Pages.SayfayaOrtamEkle(ID,PAGE_ID);

            return ekle;

        }

        [HttpPost]
        public string OrtamVarsayilan(string ID, string ISLEM, string PAGE_ID)
        {
            var varsayilan = Yeni_Panel_2015.Areas.Backoffice.Classes.Pages.VarsayilanDegistir(ID, ISLEM, PAGE_ID);

            return varsayilan;
        }

        public ActionResult SiteAyarlari() 
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string db_SiteAyarlariDuzenle()
        {
            string Dil = Request.Form["Diller"];
            string IsletmeAdi = Request.Form["IsletmeAdi"];
            string Slider = Request.Form["Slider"];
            string MetaTitle = Request.Form["MetaTitle"];
            string MetaKeywords = Request.Form["MetaKeywords"];
            string MetaDescription = Request.Form["MetaDescription"];

            var pages = Yeni_Panel_2015.Areas.Backoffice.Classes.Settings.AyarlariDuzenle(Dil, IsletmeAdi, Slider, MetaTitle, MetaKeywords, MetaDescription);

            return pages;
        }
    }
}
