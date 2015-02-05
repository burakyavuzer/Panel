using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using Yeni_Panel_2015.Classes;

namespace Yeni_Panel_2015.Areas.Backoffice.Classes
{
    public class Settings
    {
        static string bag = Config.bag;

        #region Site Ayarlarını Getir

        public static Yeni_Panel_2015.Classes.Properties.Degiskenler AyarlariGetir(string WHERE)
        {
            var ayarlar = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _SETTINGS " + WHERE + "", bag);
                    DataTable _AyarlariGetir = new DataTable();
                    sorgu.Fill(_AyarlariGetir);

                    if (_AyarlariGetir.Rows.Count > 0)
                    {
                        ayarlar.ID = _AyarlariGetir.Rows[0]["ID"].ToString();
                        ayarlar.LANGCODE = _AyarlariGetir.Rows[0]["LANG_CODE"].ToString();
                        ayarlar.STORE = _AyarlariGetir.Rows[0]["STORE"].ToString();
                        ayarlar.SELECTED = _AyarlariGetir.Rows[0]["SELECTED"].ToString();
                        ayarlar.SLIDER = _AyarlariGetir.Rows[0]["SLIDER"].ToString();
                        ayarlar.MetaTitle = _AyarlariGetir.Rows[0]["META_TITLE"].ToString();
                        ayarlar.MetaKeywords = _AyarlariGetir.Rows[0]["META_KEYWORDS"].ToString();
                        ayarlar.MetaDescription = _AyarlariGetir.Rows[0]["META_DESCRIPTION"].ToString();
                    }

                    _conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return ayarlar;
        }

        #endregion

        #region Site Ayarları Düzenle
        public static string AyarlariDuzenle(string Dil, string IsletmeAdi, string Slider, string MetaTitle, string MetaKeywords, string MetaDescription)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlCommand SayfayıGuncelle = new SqlCommand("UPDATE _SETTINGS SET LANG_CODE=@LANG_CODE, STORE=@STORE, SLIDER=@SLIDER, META_TITLE=@META_TITLE, META_KEYWORDS=@META_KEYWORDS, META_DESCRIPTION=@META_DESCRIPTION WHERE LANG_CODE='" + Dil + "'", _conn);
                        SayfayıGuncelle.Parameters.AddWithValue("@LANG_CODE", Dil);
                        SayfayıGuncelle.Parameters.AddWithValue("@STORE", IsletmeAdi);
                        SayfayıGuncelle.Parameters.AddWithValue("@SLIDER", Slider);
                        SayfayıGuncelle.Parameters.AddWithValue("@META_TITLE", MetaTitle);
                        SayfayıGuncelle.Parameters.AddWithValue("@META_KEYWORDS", MetaKeywords);
                        SayfayıGuncelle.Parameters.AddWithValue("@META_DESCRIPTION", MetaDescription);

                        SayfayıGuncelle.ExecuteNonQuery();

                        Sonuc = "<div class='alert alert-success'><strong>Başarılı!</strong> Site ayarları düzenlendi.</div>";
                   

                    _conn.Close();
                }
                catch (Exception)
                {
                    Sonuc = "Hata var :(";
                }
            }
            return Sonuc;
        }

        #endregion

        #region Seçili Dil

        public static string SeciliDil() 
        {
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _SETTINGS WHERE SELECTED=1", bag);
                DataTable _SiteAyarlari = new DataTable();
                sorgu.Fill(_SiteAyarlari);

                if (_SiteAyarlari.Rows.Count > 0)
                {
                    HttpContext.Current.Session["site_LANG"] = _SiteAyarlari.Rows[0]["LANG_CODE"].ToString();
                }

                return "";
            }
        }
        #endregion
    }
}