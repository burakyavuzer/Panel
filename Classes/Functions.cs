using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Yeni_Panel_2015.Classes
{
    public class Functions
    {
        public string antiSql(string kelime)
        {
            kelime = Regex.Replace(kelime, ",", "");
            kelime = Regex.Replace(kelime, "/", "");
            kelime = Regex.Replace(kelime, "\n", "");
            kelime = Regex.Replace(kelime, "/?", "");
            kelime = Regex.Replace(kelime, "/*", "");
            kelime = Regex.Replace(kelime, "'", "");
            kelime = Regex.Replace(kelime, "&", "");
            kelime = Regex.Replace(kelime, "<", "");
            kelime = Regex.Replace(kelime, ">", "");
            kelime = Regex.Replace(kelime, "=", "");
            kelime = Regex.Replace(kelime, "%", "[%]");
            kelime = Regex.Replace(kelime, "--", "");
            kelime = Regex.Replace(kelime, ";", "");
            kelime = Regex.Replace(kelime, "AND", "");
            kelime = Regex.Replace(kelime, "OR", "");
            kelime = Regex.Replace(kelime, "LIKE", "");
            kelime = Regex.Replace(kelime, "JOIN", "");
            kelime = Regex.Replace(kelime, "UNION", "");
            kelime = Regex.Replace(kelime, "UPDATE", "");
            kelime = Regex.Replace(kelime, "SELECT", "");
            kelime = Regex.Replace(kelime, "INSERT", "");
            kelime = Regex.Replace(kelime, "İNSERT", "");
            kelime = Regex.Replace(kelime, "CREATE", "");
            kelime = Regex.Replace(kelime, "DELETE", "");
            kelime = Regex.Replace(kelime, "DROP", "");
            kelime = Regex.Replace(kelime, "ALTER", "");
            kelime = Regex.Replace(kelime, "HAVING", "");
            kelime = Regex.Replace(kelime, "GROUP", "");
            kelime = Regex.Replace(kelime, "BY", "");
            kelime = Regex.Replace(kelime, "BETWEEN", "");
            kelime = Regex.Replace(kelime, "IN", "");
            kelime = Regex.Replace(kelime, "INNER", "");
            kelime = Regex.Replace(kelime, "JOİN", "");
            kelime = Regex.Replace(kelime, "SUM", "");
            kelime = Regex.Replace(kelime, "SET", "");
            kelime = Regex.Replace(kelime, "İNNER", "");
            kelime = Regex.Replace(kelime, "İN", "");
            kelime = Regex.Replace(kelime, "HAVİNG", "");
            kelime = Regex.Replace(kelime, "LİKE", "");
            kelime = Regex.Replace(kelime, "UNİON", "");
            return kelime;
        }
        public static string PageURL(string sayfaAdi)
        {
            string Temp = sayfaAdi.ToLower();

            Temp = Temp.Replace("-", "");
            Temp = Temp.Replace(" ", "-");

            Temp = Temp.Replace("ç", "c"); Temp = Temp.Replace("ğ", "g");

            Temp = Temp.Replace("ı", "i"); Temp = Temp.Replace("ö", "o");

            Temp = Temp.Replace("ş", "s"); Temp = Temp.Replace("ü", "u");

            Temp = Temp.Replace("\"", ""); Temp = Temp.Replace("/", "");

            Temp = Temp.Replace("(", ""); Temp = Temp.Replace(")", "");

            Temp = Temp.Replace("{", ""); Temp = Temp.Replace("}", "");

            Temp = Temp.Replace("%", ""); Temp = Temp.Replace("&", "");

            Temp = Temp.Replace("+", ""); Temp = Temp.Replace(",", "");

            Temp = Temp.Replace("?", ""); Temp = Temp.Replace(".", "_");

            Temp = Temp.Replace("ı", "i");

            string pageURL = Temp;

            return pageURL;
        }
    }
}