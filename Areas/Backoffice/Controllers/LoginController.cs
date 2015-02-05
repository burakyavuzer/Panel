using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yeni_Panel_2015.Areas.Backoffice.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Backoffice/Login/
        static string bag = Yeni_Panel_2015.Classes.Config.bag;
        Yeni_Panel_2015.Classes.Functions Fonksiyon = new Yeni_Panel_2015.Classes.Functions();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["_User"] != null)
            {
                Response.Redirect("/Backoffice/Index");
            }
        }
        public ActionResult Index()
        {
            if (Request.Cookies["panelUser"] != null) 
            {
                string myguid = Request.Cookies["panelUser"].Value;
                SqlConnection _conn = new SqlConnection(bag);

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _USERS WHERE ID='" + myguid + "' AND STATUS=1", bag);
                DataTable _Kullanicilar = new DataTable();
                sorgu.Fill(_Kullanicilar);

                if (_Kullanicilar.Rows.Count > 0)
                {
                   
                        Session["_User"] = "Aktif";

                        Session["user_Kullanici"] = _Kullanicilar.Rows[0]["UNAME"].ToString();
                        Session["user_Id"] = _Kullanicilar.Rows[0]["ID"].ToString();
                        Session["user_Name"] = _Kullanicilar.Rows[0]["NAME"].ToString();
                        Session["user_Surname"] = _Kullanicilar.Rows[0]["SURNAME"].ToString();
                        Session["user_Email"] = _Kullanicilar.Rows[0]["EMAIL"].ToString();
                        Session["user_Permission"] = _Kullanicilar.Rows[0]["TYPE"].ToString();

                        LangSession();

                        Response.Redirect("/Backoffice/Index");
                }
                else
                {
                    Response.Cookies["panelUser"].Expires = DateTime.Now.AddYears(-30);
                }
            }

            return View();
        }

        [HttpPost]
        public string UyeGirisi()
        {
            string email = Request.Form["username"];
            string sifre = Request.Form["password"];
            string beniHatirla = Request.Form["remember"];

            if (String.IsNullOrEmpty(email) && String.IsNullOrEmpty(sifre))
            {
                return "<div class='alert alert-danger'><button class='close' data-close='alert'></button><span>Lütfen e-mail ve şifrenizi giriniz.</span></div>";
            }
            else if (String.IsNullOrEmpty(email))
            {
                return "<div class='alert alert-danger'><button class='close' data-close='alert'></button><span>E-posta adresinizi girmediniz.</span></div>";
            }
            else if (String.IsNullOrEmpty(sifre))
            {
                return "<div class='alert alert-danger'><button class='close' data-close='alert'></button><span>Şifrenizi girmediniz.</span></div>";
            }
            else
            {
                using (SqlConnection _conn = new SqlConnection(bag))
                {
                    _conn.Open();

                    string EpostaAdresi = Fonksiyon.antiSql(email);

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _USERS WHERE EMAIL='" + EpostaAdresi + "' AND STATUS=1", bag);
                    DataTable _Kullanicilar = new DataTable();
                    sorgu.Fill(_Kullanicilar);

                    if (_Kullanicilar.Rows.Count > 0)
                    {
                        if (_Kullanicilar.Rows[0]["pwd"].ToString() == sifre)
                        {
                            Session["_User"] = "Aktif";

                            Session["user_Kullanici"] = _Kullanicilar.Rows[0]["UNAME"].ToString();
                            Session["user_Id"] = _Kullanicilar.Rows[0]["ID"].ToString();
                            Session["user_Name"] = _Kullanicilar.Rows[0]["NAME"].ToString();
                            Session["user_Surname"] = _Kullanicilar.Rows[0]["SURNAME"].ToString();
                            Session["user_Email"] = _Kullanicilar.Rows[0]["EMAIL"].ToString();
                            Session["user_Permission"] = _Kullanicilar.Rows[0]["TYPE"].ToString();

                           
                            if (beniHatirla == "1")
                            {
                                Response.Cookies["panelUser"].Value = _Kullanicilar.Rows[0]["ID"].ToString();
                                Response.Cookies["panelUser"].Expires = DateTime.Now.AddYears(30);
                            }

                            LangSession();

                            return "<div class='alert alert-success'><button class='close' data-close='alert'></button><span>Bilgiler doğru, yönlendiriliyorsunuz...</span></div><script type='text/javascript'>setTimeout(function(){window.location='/Backoffice/Index'},1000);</script>";
                        }
                        else
                        {
                            return "<div class='alert alert-danger'><button class='close' data-close='alert'></button><span>Hatalı email veya şifre.</span></div>";
                        }
                    }
                    else
                    {
                        return "<div class='alert alert-danger'><button class='close' data-close='alert'></button><span>Sistemde kayıtlı değilsiniz.</span></div>";
                    }
                }
            }

        }

        public string LangSession() 
        {
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _SETTINGS WHERE SELECTED=1", bag);
                DataTable _SiteAyarlari = new DataTable();
                sorgu.Fill(_SiteAyarlari);

                if (_SiteAyarlari.Rows.Count > 0)
                {
                    Session["panel_LANG"] = _SiteAyarlari.Rows[0]["LANG_CODE"].ToString();
                }

                return "";
            }
            
        }

    }
}
