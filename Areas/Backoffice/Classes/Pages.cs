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
    public class Pages
    {
        static string bag = Config.bag;

        #region Sayfa Konumlandırma

        static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> Listelesayfa = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
        static DataTable _SayfaListele = new DataTable();
        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> SayfaListele(string dilSorgu)
        {
            _SayfaListele = new DataTable();
            Listelesayfa = null;
            Listelesayfa = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                if (dilSorgu != "")
                {
                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGES WHERE LANG_CODE ='" + dilSorgu + "' ORDER BY ORDER_NUM ASC", bag);
                    sorgu.Fill(_SayfaListele);
                }
                else
                {
                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGES ORDER BY ORDER_NUM ASC", bag);
                    sorgu.Fill(_SayfaListele);
                }

                DataRow[] drowKat0 = _SayfaListele.Select("CAT_ID='0'");
                foreach (var drow1 in drowKat0)
                {

                    if (dilSorgu == drow1["LANG_CODE"].ToString())
                    {
                        var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        sayfa.PageName = drow1["PAGE_NAME"].ToString();
                        sayfa.ID = drow1["ID"].ToString();
                        sayfa.ORDERNUM = drow1["ORDER_NUM"].ToString();
                        sayfa.STATUS = drow1["STATUS"].ToString();
                        sayfa.LANGCODE = drow1["LANG_CODE"].ToString();
                        sayfa.PAGEURL = drow1["PAGE_URL"].ToString();

                        DataRow[] drows = _SayfaListele.Select("ID=" + sayfa.ID);
                        DDLDoldurYeni(drows, "", dilSorgu);
                    }
                    else
                    {
                        if (drow1["ID"].ToString() == drow1["LANG_ID"].ToString())
                        {
                            var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                            sayfa.PageName = drow1["PAGE_NAME"].ToString();
                            sayfa.ID = drow1["ID"].ToString();
                            sayfa.ORDERNUM = drow1["ORDER_NUM"].ToString();
                            sayfa.STATUS = drow1["STATUS"].ToString();
                            sayfa.LANGCODE = drow1["LANG_CODE"].ToString();
                            sayfa.PAGEURL = drow1["PAGE_URL"].ToString();

                            DataRow[] drows = _SayfaListele.Select("ID=" + sayfa.ID);
                            DDLDoldurYeni(drows, "", dilSorgu);
                        }
                    }


                }
            }
            Listelesayfa.OrderByDescending(p => p.ORDERNUM);
            return Listelesayfa;
        }
        public static void DDLDoldurYeni(DataRow[] drc, string yol, string dilSorgu)
        {
            foreach (var drow in drc)
            {
                if (dilSorgu != null)
                {
                    var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();

                    sayfa.PageName = yol + drow["PAGE_NAME"].ToString();

                    sayfa.ID = drow["ID"].ToString();
                    sayfa.CATID = drow["CAT_ID"].ToString();
                    sayfa.ORDERNUM = drow["ORDER_NUM"].ToString();
                    sayfa.STATUS = drow["STATUS"].ToString();
                    sayfa.LANGCODE = drow["LANG_CODE"].ToString();
                    sayfa.PAGEURL = drow["PAGE_URL"].ToString();

                    Listelesayfa.Add(sayfa);
                    DataRow[] dr = _SayfaListele.Select("CAT_ID='" + sayfa.ID + "'");
                    DDLDoldurYeni(dr, yol + drow["PAGE_NAME"].ToString() + " > ", dilSorgu);
                }
                else
                {
                    if (drow["ID"].ToString() == drow["LANG_ID"].ToString())
                    {
                        var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();

                        sayfa.PageName = yol + drow["PAGE_NAME"].ToString();

                        sayfa.ID = drow["ID"].ToString();
                        sayfa.CATID = drow["CAT_ID"].ToString();
                        sayfa.ORDERNUM = drow["ORDER_NUM"].ToString();
                        sayfa.STATUS = drow["STATUS"].ToString();
                        sayfa.LANGCODE = drow["LANG_CODE"].ToString();
                        sayfa.PAGEURL = drow["PAGE_URL"].ToString();

                        Listelesayfa.Add(sayfa);
                        DataRow[] dr = _SayfaListele.Select("CAT_ID='" + sayfa.ID + "'");
                        DDLDoldurYeni(dr, yol + drow["PAGE_NAME"].ToString() + " > ", dilSorgu);
                    }
                }
            }
        }

        #endregion

        #region Sayfaları Listele
        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> Sayfalar(string WHERE)
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGES " + WHERE + "", bag);
                DataTable _Sayfalar = new DataTable();
                sorgu.Fill(_Sayfalar);

                if (_Sayfalar.Rows.Count > 0)
                {
                    for (int i = 0; i < _Sayfalar.Rows.Count; i++)
                    {
                        var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        sayfa.ID = _Sayfalar.Rows[i]["ID"].ToString();
                        sayfa.CATID = _Sayfalar.Rows[i]["CAT_ID"].ToString();
                        sayfa.LANGID = _Sayfalar.Rows[i]["LANG_ID"].ToString();
                        sayfa.LANGCODE = _Sayfalar.Rows[i]["LANG_CODE"].ToString();
                        sayfa.PageName = _Sayfalar.Rows[i]["PAGE_NAME"].ToString();
                        sayfa.PageContent = _Sayfalar.Rows[i]["PAGE_CONTENT"].ToString();
                        sayfa.PAGEURL = _Sayfalar.Rows[i]["PAGE_URL"].ToString();
                        sayfa.ORDERNUM = _Sayfalar.Rows[i]["ORDER_NUM"].ToString();
                        sayfa.STATUS = _Sayfalar.Rows[i]["STATUS"].ToString();
                        Yazdır.Add(sayfa);
                    }
                }

                _conn.Close();

            }
            return Yazdır;
        }

        #endregion

        #region Sayfa Ekle
        public static string SayfaEkle(string Dil, string Kategori, string SayfaAdı, string Sıra, string Icerik)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlCommand Kaydet = new SqlCommand("INSERT INTO _PAGES(CAT_ID,LANG_CODE,PAGE_NAME) values('" + Kategori + "','" + Dil + "','" + SayfaAdı + "') select Scope_identity()", _conn);

                    int KayıtID = Convert.ToInt32(Kaydet.ExecuteScalar());

                    SqlCommand SayfayıGuncelle = new SqlCommand("UPDATE _PAGES SET LANG_ID=@LANG_ID, PAGE_CONTENT=@PAGE_CONTENT, ORDER_NUM=@ORDER_NUM, PAGE_URL=@PAGE_URL WHERE ID='" + KayıtID + "'", _conn);
                    SayfayıGuncelle.Parameters.AddWithValue("@LANG_ID", KayıtID);
                    SayfayıGuncelle.Parameters.AddWithValue("@PAGE_CONTENT", Icerik);
                    SayfayıGuncelle.Parameters.AddWithValue("@ORDER_NUM", Sıra);
                    SayfayıGuncelle.Parameters.AddWithValue("@PAGE_URL", Functions.PageURL(SayfaAdı + " " + KayıtID));
                    SayfayıGuncelle.ExecuteNonQuery();

                    Sonuc = "<div class='alert alert-success'><strong>Başarılı!</strong> Sayfaya yönlendiriliyorsunuz.</div><script type='text/javascript'>setTimeout(function(){window.location='/Backoffice/SayfaDuzenle/" + KayıtID + "/" + Dil + "'},1000);</script>";

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

        #region Sayfa Getir

        public static Yeni_Panel_2015.Classes.Properties.Degiskenler SayfaGetir(string WHERE)
        {
            var sayfa = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGES " + WHERE + "", bag);
                    DataTable _SayfaGetir = new DataTable();
                    sorgu.Fill(_SayfaGetir);

                    if (_SayfaGetir.Rows.Count > 0)
                    {
                        sayfa.ID = _SayfaGetir.Rows[0]["ID"].ToString();
                        sayfa.CATID = _SayfaGetir.Rows[0]["CAT_ID"].ToString();
                        sayfa.LANGID = _SayfaGetir.Rows[0]["LANG_ID"].ToString();
                        sayfa.LANGCODE = _SayfaGetir.Rows[0]["LANG_CODE"].ToString();
                        sayfa.PageName = _SayfaGetir.Rows[0]["PAGE_NAME"].ToString();
                        sayfa.PageContent = _SayfaGetir.Rows[0]["PAGE_CONTENT"].ToString();
                        sayfa.PAGEURL = _SayfaGetir.Rows[0]["PAGE_URL"].ToString();
                        sayfa.ORDERNUM = _SayfaGetir.Rows[0]["ORDER_NUM"].ToString();
                        sayfa.STATUS = _SayfaGetir.Rows[0]["STATUS"].ToString();
                    }

                    _conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return sayfa;
        }

        #endregion

        #region Sayfa Düzenle
        public static string SayfaDuzenle(string Id, string DilId, string Dil, string Kategori, string SayfaAdı, string Sıra, string Icerik, string MetaTitle, string MetaKeywords, string MetaDescription)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGES WHERE LANG_ID='" + Id + "' AND LANG_CODE='" + Dil + "'", bag);
                    DataTable _SayfaGetir = new DataTable();
                    sorgu.Fill(_SayfaGetir);

                    if (_SayfaGetir.Rows.Count > 0)
                    {
                        SqlCommand SayfayıGuncelle = new SqlCommand("UPDATE _PAGES SET LANG_ID=@LANG_ID, PAGE_NAME=@PAGE_NAME, PAGE_CONTENT=@PAGE_CONTENT, ORDER_NUM=@ORDER_NUM, PAGE_URL=@PAGE_URL WHERE LANG_ID='" + Id + "' AND LANG_CODE='" + Dil + "'", _conn);
                        SayfayıGuncelle.Parameters.AddWithValue("@LANG_ID", DilId);
                        SayfayıGuncelle.Parameters.AddWithValue("@PAGE_NAME", SayfaAdı);
                        SayfayıGuncelle.Parameters.AddWithValue("@PAGE_CONTENT", Icerik);
                        SayfayıGuncelle.Parameters.AddWithValue("@ORDER_NUM", Sıra);
                        SayfayıGuncelle.Parameters.AddWithValue("@PAGE_URL", Functions.PageURL(SayfaAdı + " " + Id));
                        SayfayıGuncelle.ExecuteNonQuery();

                        Classes.MetaTags.MetaTagGuncelle(Id, Dil, MetaTitle, MetaKeywords, MetaDescription);

                        Sonuc = "<div class='alert alert-success'><strong>Başarılı!</strong> Sayfaya düzenlendi.</div>";
                    }
                    else
                    {
                        SqlCommand Kaydet = new SqlCommand("INSERT INTO _PAGES(CAT_ID,LANG_CODE,PAGE_NAME) values('" + Kategori + "','" + Dil + "','" + SayfaAdı + "') select Scope_identity()", _conn);

                        int KayıtID = Convert.ToInt32(Kaydet.ExecuteScalar());

                        SqlCommand SayfayıGuncelle = new SqlCommand("UPDATE _PAGES SET LANG_ID=@LANG_ID, PAGE_CONTENT=@PAGE_CONTENT, ORDER_NUM=@ORDER_NUM, PAGE_URL=@PAGE_URL WHERE LANG_ID='" + Id + "' AND LANG_CODE='" + Dil + "'", _conn);
                        SayfayıGuncelle.Parameters.AddWithValue("@LANG_ID", Id);
                        SayfayıGuncelle.Parameters.AddWithValue("@PAGE_CONTENT", Icerik);
                        SayfayıGuncelle.Parameters.AddWithValue("@ORDER_NUM", Sıra);
                        SayfayıGuncelle.Parameters.AddWithValue("@PAGE_URL", Functions.PageURL(SayfaAdı + " " + KayıtID));
                        SayfayıGuncelle.ExecuteNonQuery();

                        Classes.MetaTags.MetaTagGuncelle(Id, Dil, MetaTitle, MetaKeywords, MetaDescription);

                        Sonuc = "<div class='alert alert-success'><strong>Başarılı!</strong> Sayfaya yönlendiriliyorsunuz.<script type='text/javascript'>setTimeout(function(){window.location='/Backoffice/SayfaDuzenle/" + KayıtID + "/" + Dil + "'},1000);</script></div>";
                    }

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

        #region Sayfaya Ortam Ekle

        public static string SayfayaOrtamEkle(string ID, string PAGE_ID)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    string[] parcala = ID.Replace(" ", "").Split(',');

                    foreach (var item in parcala)
                    {
                        SqlCommand Kaydet = new SqlCommand("INSERT INTO _PAGE_LIBRARY(MEDIA_ID,PAGE_ID) values('" + item + "','" + PAGE_ID + "')", _conn);
                        Kaydet.ExecuteNonQuery();
                    }

                    Sonuc = "Başarıyla eklendi.";

                    _conn.Close();
                    _conn.Dispose();
                }
                catch (Exception)
                {
                    Sonuc = "Hata var :(";
                }
            }

            return Sonuc;
        }

        #endregion

        #region Sayfa Ortam Getir

        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> SayfaOrtamGetir(string WHERE)
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _PAGE_LIBRARY " + WHERE + "", bag);
                DataTable _OrtamGetir = new DataTable();
                sorgu.Fill(_OrtamGetir);

                if (_OrtamGetir.Rows.Count > 0)
                {
                    for (int i = 0; i < _OrtamGetir.Rows.Count; i++)
                    {
                        var ortam = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        ortam.ID = _OrtamGetir.Rows[i]["ID"].ToString();
                        ortam.PAGEID = _OrtamGetir.Rows[i]["PAGE_ID"].ToString();
                        ortam.MEDIAID = _OrtamGetir.Rows[i]["MEDIA_ID"].ToString();
                        ortam.SELECTED = _OrtamGetir.Rows[i]["SELECT_DOC"].ToString();
                        Yazdır.Add(ortam);
                    }
                }

                _conn.Close();
                _conn.Dispose();

            }
            return Yazdır;
        }

        #endregion

        #region Sayfa Ortam Varsayılan Değiştir / Ve Dosya Sil

        public static string VarsayilanDegistir(string ID, string ISLEM, string PAGE_ID)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    if (ISLEM == "Sil")
                    {
                        SqlCommand sil = new SqlCommand("DELETE FROM _PAGE_LIBRARY WHERE ID='" + ID + "' AND PAGE_ID='"+PAGE_ID+"'", _conn);
                        sil.ExecuteNonQuery();

                        Sonuc = "Silindi!";
                    }
                    else
                    {
                        SqlCommand HepsiDefault = new SqlCommand("UPDATE _PAGE_LIBRARY SET SELECT_DOC=@SELECT_DOC WHERE PAGE_ID='" + PAGE_ID + "'", _conn);
                        HepsiDefault.Parameters.AddWithValue("@SELECT_DOC", 0);
                        HepsiDefault.ExecuteNonQuery();

                        SqlCommand Guncelle = new SqlCommand("UPDATE _PAGE_LIBRARY SET SELECT_DOC=@SELECT_DOC WHERE ID='" + ID + "' AND PAGE_ID='" + PAGE_ID + "'", _conn);
                        Guncelle.Parameters.AddWithValue("@SELECT_DOC", ISLEM);
                        Guncelle.ExecuteNonQuery();

                        Sonuc = "Değiştirildi!";
                    }



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

    }
}