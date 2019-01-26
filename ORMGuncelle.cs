using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttributeTanimlamalari;

namespace ORMGuncelle
{
	public static class ExtensionMethod
    {
    	public static void VeritabaniIdIleGuncelle(this object dbClass)
        {
            List<string> kolonlarVeDegerleri = new List<string>();

            Type dbClassType = dbClass.GetType(); //Type cinsine cevirelim

            DbTablo DbTablo = (DbTablo)Attribute.GetCustomAttribute(dbClassType, typeof(DbTablo)); // verilen type icin atrribute fonsiyonuna ulastik
            
            string tabloAdi = DbTablo.TabloAd; //attribute sinifinin verilerine ulasabildik
            string id = null;

            FieldInfo[] kolonAlanlari = dbClassType.GetFields(BindingFlags.Public | BindingFlags.Instance); //classin property'lerine ulastik
            try
            {
                foreach (FieldInfo kolonAlani in kolonAlanlari)
                {
                    //kolonAdi degeri bos olmamali yoksa hata verecektir. Her alan bosta olsa gonderilmelidir.

                    DbKolon DbKolon = (DbKolon)Attribute.GetCustomAttribute(kolonAlani, typeof(DbKolon)); //Her property'nin attribute'una gidip kolon adini aldik.
                    if (DbKolon.KolonAd != "ID")//Id otomatik veritabaninda atanacak insert tipinde eklemeyelim.
                    {
                        object kolonDegeri = kolonAlani.GetValue(dbClass);
                        if (kolonDegeri != null)
                        {
                            string kolonVeDegeri = DbKolon.KolonAd + " = '" + kolonDegeri.ToString() + "' ";
                            kolonlarVeDegerleri.Add(kolonVeDegeri);
                        }
                    }
                    else
                    {
                        id = kolonAlani.GetValue(dbClass).ToString();
                        if (id == "0")
                            throw new Exception("Lütfen geçerli bir Id değeri giriniz.");
                    }
                    

                }
                //sql sorgusunu olusturduk.
                string update = "UPDATE ";
                //tabloAdi;
                string guncellenecekVeriler = string.Join(",", kolonlarVeDegerleri.ToArray());

                string sql = update + tabloAdi + " SET " + guncellenecekVeriler + "WHERE ID = '" + id + "'" ;
                Console.WriteLine(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}