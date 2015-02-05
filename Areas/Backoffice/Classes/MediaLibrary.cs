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
    public class MediaLibrary
    {
        static string bag = Config.bag;

        #region Ortamları Listele

        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> Ortamlar(string WHERE) 
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MEDIA " + WHERE + "", bag);
                DataTable _Ortamlar = new DataTable();
                sorgu.Fill(_Ortamlar);

                if (_Ortamlar.Rows.Count > 0)
                {
                    for (int i = 0; i < _Ortamlar.Rows.Count; i++)
                    {
                        var ortam = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        ortam.ID = _Ortamlar.Rows[i]["ID"].ToString();
                        ortam.MEDIANAME = _Ortamlar.Rows[i]["MEDIA_NAME"].ToString();
                        ortam.ORDERNUM = _Ortamlar.Rows[i]["ORDER_NUM"].ToString();
                        Yazdır.Add(ortam);  
                    }                    
                }

                _conn.Close();
            }

            return Yazdır;
        }

        #endregion

        #region Albüm Oluştur

        public static string AlbumOlustur(string AlbumAdi) 
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlCommand Kaydet = new SqlCommand("INSERT INTO _MEDIA(MEDIA_NAME) values('" + AlbumAdi + "')", _conn);
                Kaydet.ExecuteNonQuery();

                Sonuc = "<script type='text/javascript'>setTimeout(function(){window.location='/Backoffice/Index},1000);</script>";

                _conn.Close();
            }

            return Sonuc;
        }

        #endregion

        #region Ortam Yükle

        public string OrtamYukle(string fuResimlerUpload) 
        {
            
            return "";
        }

        #endregion

        #region Ortamları Kütüphanesi Listele

        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> OrtamKutuphanesi(string WHERE)
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MEDIA_LIBRARY " + WHERE + "", bag);
                DataTable _Ortamlar = new DataTable();
                sorgu.Fill(_Ortamlar);

                if (_Ortamlar.Rows.Count > 0)
                {
                    for (int i = 0; i < _Ortamlar.Rows.Count; i++)
                    {
                        var ortam = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        ortam.ID = _Ortamlar.Rows[i]["ID"].ToString();
                        ortam.MEDIANAME = _Ortamlar.Rows[i]["NAME"].ToString();
                        ortam.FILE = _Ortamlar.Rows[i]["FILE_URL"].ToString();
                        ortam.ORDERNUM = _Ortamlar.Rows[i]["ORDER_NUM"].ToString();
                        Yazdır.Add(ortam);
                    }
                }

                _conn.Close();
            }

            return Yazdır;
        }

        #endregion

        #region Ortam Güncelle

        public static string OrtamGuncelle(string ID, string NAME) 
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MEDIA_LIBRARY WHERE ID='" + ID + "'", bag);
                    DataTable _OrtamGetir = new DataTable();
                    sorgu.Fill(_OrtamGetir);

                    if (_OrtamGetir.Rows.Count > 0)
                    {
                        if (NAME == _OrtamGetir.Rows[0]["NAME"].ToString())
                        {
                            
                        }
                        else
                        {
                            SqlCommand OrtamGuncelle = new SqlCommand("UPDATE _MEDIA_LIBRARY SET NAME=@NAME WHERE ID='" + ID + "'", _conn);
                            OrtamGuncelle.Parameters.AddWithValue("@NAME", NAME);
                            OrtamGuncelle.ExecuteNonQuery();

                            Sonuc = "Başarılı!";
                        }
                    }
                    else
                    {
                        Sonuc = "Aynı karakter!";
                    }                   

                    _conn.Close();
                    
                }
                catch (Exception)
                {
                    Sonuc = "Hata Var!";
                }
            }

            return Sonuc;
        }

        #endregion

        #region Ortam Sıra Güncelle

        public static string OrtamSiraGuncelle(string ID, string NAME)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MEDIA_LIBRARY WHERE ID='" + ID + "'", bag);
                    DataTable _OrtamGetir = new DataTable();
                    sorgu.Fill(_OrtamGetir);

                    if (_OrtamGetir.Rows.Count > 0)
                    {
                        if (NAME == _OrtamGetir.Rows[0]["NAME"].ToString())
                        {

                        }
                        else
                        {
                            SqlCommand OrtamGuncelle = new SqlCommand("UPDATE _MEDIA_LIBRARY SET ORDER_NUM=@ORDER_NUM WHERE ID='" + ID + "'", _conn);
                            OrtamGuncelle.Parameters.AddWithValue("@ORDER_NUM", NAME);
                            OrtamGuncelle.ExecuteNonQuery();

                            Sonuc = "Başarılı!";
                        }
                    }
                    else
                    {
                        Sonuc = "Aynı karakter!";
                    }

                    _conn.Close();

                }
                catch (Exception)
                {
                    Sonuc = "Hata Var!";
                }
            }

            return Sonuc;
        }

        #endregion

        #region Albüm İçini Boşalt

        public static string AlbumIciniBosalt(string ID)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();

                    SqlCommand sil = new SqlCommand("DELETE FROM _MEDIA_LIBRARY WHERE MEDIA_ID='" + ID + "'", _conn);
                    sil.ExecuteNonQuery();

                    _conn.Close();

                    Sonuc = "Başarılı!";

                }
                catch (Exception)
                {
                    Sonuc = "Hata Var!";
                }
            }

            return Sonuc;
        }

        #endregion

        #region Albümü Sil

        public static string AlbumuSil(string ID)
        {
            string Sonuc = "";

            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();                    

                    SqlCommand Dosyasil = new SqlCommand("DELETE FROM _MEDIA_LIBRARY WHERE MEDIA_ID='" + ID + "'", _conn);
                    Dosyasil.ExecuteNonQuery();

                    SqlCommand sil = new SqlCommand("DELETE FROM _MEDIA WHERE ID='" + ID + "'", _conn);
                    sil.ExecuteNonQuery();

                    _conn.Close();

                    Sonuc = "Başarılı!";

                }
                catch (Exception)
                {
                    Sonuc = "Hata Var!";
                }
            }

            return Sonuc;
        }

        #endregion
    }
}