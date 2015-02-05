using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Yeni_Panel_2015.Classes;

namespace Yeni_Panel_2015.Areas.Backoffice.Classes
{
    public class MetaTags
    {
        static string bag = Config.bag;

        #region Meta Tagları Getir

        public static Yeni_Panel_2015.Classes.Properties.Degiskenler MetaTagGetir(string WHERE) 
        {
            var taglar = new Yeni_Panel_2015.Classes.Properties.Degiskenler();

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _META_TAGS " + WHERE + "", bag);
                DataTable _Taglar = new DataTable();
                sorgu.Fill(_Taglar);

                if (_Taglar.Rows.Count > 0)
                {
                    taglar.MetaTitle = _Taglar.Rows[0]["META_TITLE"].ToString();
                    taglar.MetaKeywords = _Taglar.Rows[0]["META_KEYWORDS"].ToString();
                    taglar.MetaDescription = _Taglar.Rows[0]["META_DESCRIPTION"].ToString();
                    taglar.PAGEID = _Taglar.Rows[0]["PAGE_ID"].ToString();
                }

                _conn.Close();
                _conn.Dispose();
            }

            return taglar;
        }

        #endregion

        #region Meta Tag Güncelle & Ekle

        public static string MetaTagGuncelle(string ID, string LANGCODE, string METATITLE, string METAKEYWORDS, string METADESCRIPTION)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _META_TAGS WHERE PAGE_ID='" + ID + "' AND LANG_CODE='"+LANGCODE+"'", bag);
                    DataTable _MetaKontrol = new DataTable();
                    sorgu.Fill(_MetaKontrol);

                    if (_MetaKontrol.Rows.Count > 0)
                    {
                           SqlCommand MetaGuncelle = new SqlCommand("UPDATE _META_TAGS SET META_TITLE=@META_TITLE, META_KEYWORDS=@META_KEYWORDS, META_DESCRIPTION=@META_DESCRIPTION, LANG_CODE=@LANG_CODE WHERE PAGE_ID='" + ID + "' AND LANG_CODE='"+LANGCODE+"'", _conn);
                           MetaGuncelle.Parameters.AddWithValue("@META_TITLE", METATITLE);
                           MetaGuncelle.Parameters.AddWithValue("@META_KEYWORDS", METAKEYWORDS);
                           MetaGuncelle.Parameters.AddWithValue("@META_DESCRIPTION", METADESCRIPTION);
                           MetaGuncelle.Parameters.AddWithValue("@LANG_CODE", LANGCODE);
                           MetaGuncelle.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand MetaKaydet = new SqlCommand("INSERT INTO _META_TAGS(META_TITLE,META_KEYWORDS,META_DESCRIPTION,PAGE_ID,LANG_CODE) VALUES(@META_TITLE,@META_KEYWORDS,@META_DESCRIPTION," + ID + ",@LANG_CODE)", _conn);
                        MetaKaydet.Parameters.AddWithValue("@META_TITLE", METATITLE);
                        MetaKaydet.Parameters.AddWithValue("@META_KEYWORDS", METAKEYWORDS);
                        MetaKaydet.Parameters.AddWithValue("@META_DESCRIPTION", METADESCRIPTION);
                        MetaKaydet.Parameters.AddWithValue("@LANG_CODE", LANGCODE);
                        MetaKaydet.ExecuteNonQuery();
                    }

                    _conn.Close();
                    _conn.Dispose();

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Sonuc;
        }

        #endregion
    }
}