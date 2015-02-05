using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yeni_Panel_2015.Classes;

namespace Yeni_Panel_2015.Areas.Backoffice.Classes
{
    public class GlobalQuery
    {
        static string bag = Config.bag;

        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> Diller(string WHERE) 
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _LANGUAGES " + WHERE + "", bag);
                DataTable _Diller = new DataTable();
                sorgu.Fill(_Diller);

                if (_Diller.Rows.Count > 0)
                {
                    for (int i = 0; i < _Diller.Rows.Count; i++)
                    {
                        var diller = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        diller.ID = _Diller.Rows[i]["ID"].ToString();
                        diller.LANGNAME = _Diller.Rows[i]["LANG_NAME"].ToString();
                        diller.LANGCODE = _Diller.Rows[i]["LANG_CODE"].ToString();
                        diller.ORDERNUM = _Diller.Rows[i]["ORDER_NUM"].ToString();
                        diller.SELECTED = _Diller.Rows[i]["SELECTED"].ToString();
                        Yazdır.Add(diller);
                    }
                }

                _conn.Close();

            }
            return Yazdır;
        }

        public static string MenuSiraGuncelle(string NAME) 
        {

            char[] brackets = new char[] { '&' };
            string[] sent = NAME.Split(brackets);
            int i = 1;


            SqlConnection _conn = new SqlConnection(bag);

            _conn.Open();

            foreach (string data in sent)
            {
                SqlCommand cmd = new SqlCommand("Update _MENUS set ORDER_NUM='" + i + "' where ID='" + data + "' ", _conn);
                cmd.ExecuteNonQuery();
                i++;
            }
            _conn.Close();
            _conn.Dispose();

            return "All saved! refresh the page to see the changes";
        }
        
        public static string MenuKaydet(string MenuAdi, string LangCode) 
        {
            string[] MENU = MenuAdi.Replace("(", "").Replace(")", "").Split(',');

            SqlConnection _conn = new SqlConnection(bag);

            _conn.Open();

            foreach (var menuAdi in MENU)
            {
                string[] deger = menuAdi.Split(':');

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MENUS WHERE CAT_ID=" + deger[1] + "", bag);
                DataTable _MenuKontrol = new DataTable();
                sorgu.Fill(_MenuKontrol);

                if (_MenuKontrol.Rows.Count > 0)
                {
                    SqlCommand guncelle = new SqlCommand("Update _MENUS set CAT_ID=" + deger[1] + ", MENU_NAME='" + deger[0] + "', URL='" + deger[2] + "' where CAT_ID=" + deger[1] + " AND LANG_CODE='"+LangCode+"' ", _conn);
                    guncelle.ExecuteNonQuery();                                      
                }
                else
                {
                    SqlCommand kaydet = new SqlCommand("INSERT INTO _MENUS(CAT_ID,MENU_NAME,URL,EXTERNAL_MENU,LANG_CODE) VALUES(" + deger[1] + ",'" + deger[0] + "','" + deger[2] + "'," + deger[3] + ",'"+LangCode+"')", _conn);
                    kaydet.ExecuteNonQuery();                    
                }
            }

            _conn.Close();
            _conn.Dispose();

            return "";
        }

        public static string MenuSil(string ID) 
        {
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                try
                {
                    _conn.Open();


                    SqlCommand sil = new SqlCommand("DELETE FROM _MENUS WHERE ID=" + ID + "", _conn);
                    sil.ExecuteNonQuery();

                    _conn.Close();

                }
                catch (Exception)
                {
                }
            }
            return "";
        }

        public static List<Yeni_Panel_2015.Classes.Properties.Degiskenler> Menuler(string WHERE)
        {
            var Yazdır = new List<Yeni_Panel_2015.Classes.Properties.Degiskenler>();
            using (SqlConnection _conn = new SqlConnection(bag))
            {
                _conn.Open();

                SqlDataAdapter sorgu = new SqlDataAdapter("SELECT * FROM _MENUS " + WHERE + "", bag);
                DataTable _Menuler = new DataTable();
                sorgu.Fill(_Menuler);

                if (_Menuler.Rows.Count > 0)
                {
                    for (int i = 0; i < _Menuler.Rows.Count; i++)
                    {
                        var menu = new Yeni_Panel_2015.Classes.Properties.Degiskenler();
                        menu.ID = _Menuler.Rows[i]["ID"].ToString();
                        menu.NAME = _Menuler.Rows[i]["MENU_NAME"].ToString();
                        menu.CATID = _Menuler.Rows[i]["CAT_ID"].ToString();
                        menu.ORDERNUM = _Menuler.Rows[i]["ORDER_NUM"].ToString();
                        menu.PAGEURL = _Menuler.Rows[i]["URL"].ToString();
                        menu.EXTERNAL = _Menuler.Rows[i]["EXTERNAL_MENU"].ToString();
                        menu.LANGCODE = _Menuler.Rows[i]["LANG_CODE"].ToString();
                        Yazdır.Add(menu);
                    }
                }

                _conn.Close();

            }
            return Yazdır;
        }
    }
}