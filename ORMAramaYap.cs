using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttributeTanimlamalari;

namespace ORMAramaYap
{
    public static class ExtensionMethod
    {
        public static void VeritabanindanVeriArayan(this object dbClass)
        {
            List<string> kolonlarVeDegerleri = new List<string>();

            Type dbClassType = dbClass.GetType(); //Type cinsine cevirelim

            DbTablo DbTablo = (DbTablo)Attribute.GetCustomAttribute(dbClassType, typeof(DbTablo)); // verilen type icin atrribute fonsiyonuna ulastik

            string tabloAdi = DbTablo.TabloAd; //attribute sinifinin verilerine ulasabildik

            FieldInfo[] kolonAlanlari = dbClassType.GetFields(BindingFlags.Public | BindingFlags.Instance); //classin property'lerine ulastik
            try
            {
                foreach (FieldInfo kolonAlani in kolonAlanlari)
                {
                    //kolonAdi degeri bos olmamali yoksa hata verecektir. Her alan bosta olsa gonderilmelidir.

                    DbKolon DbKolon = (DbKolon)Attribute.GetCustomAttribute(kolonAlani, typeof(DbKolon)); //Her property'nin attribute'una gidip kolon adini aldik.
                    object kolonDegeri = kolonAlani.GetValue(dbClass);
                    if (kolonDegeri != null)
                    {
                        string kolonVeDegeri = DbKolon.KolonAd + " = '" + kolonDegeri.ToString() + "' ";
                        kolonlarVeDegerleri.Add(kolonVeDegeri);
                    }
                }
                //sql sorgusunu olusturduk.
                string select = "SELECT * FROM ";
                //tabloAdi;
                string guncellenecekVeriler = string.Join(",", kolonlarVeDegerleri.ToArray());

                string sql = select + tabloAdi + " WHERE " + guncellenecekVeriler;
                Console.WriteLine(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}