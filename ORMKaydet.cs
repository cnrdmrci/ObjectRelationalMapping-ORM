using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttributeTanimlamalari;

namespace ORMKaydet
{
	public static class ExtensionMethod
    {
    	public static void VeritabaninaKaydet(this object dbClass)
        {
            List<string> columnNames = new List<string>();
            List<string> values = new List<string>();

            Type dbClassType = dbClass.GetType(); //Type cinsine cevirelim

            DbTablo DbTablo = (DbTablo)Attribute.GetCustomAttribute(dbClassType, typeof(DbTablo)); // verilen type icin atrribute fonsiyonuna ulastik

            string tabloAdi = DbTablo.TabloAd + " "; //attribute sinifinin verilerine ulasabildik

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
                            columnNames.Add(DbKolon.KolonAd);
                            values.Add("'" + kolonDegeri.ToString() + "'"); //Her property'nin runtime degerine ulastik
                        }
                    }
                }
                //sql sorgusunu olusturduk.
                string insert = "INSERT INTO ";
                string columnSet = string.Join(",", columnNames.ToArray());
                string valueSet = string.Join(",", values.ToArray());

                string sql = insert + tabloAdi + "(" + columnSet + ")" + " VALUES " + "(" + valueSet + ")";
                Console.WriteLine(sql);
            }
            catch(Exception ex)
            {
                Console.WriteLine(columnNames[columnNames.Count-1] + " alanı eklenmemiş!");
            }
        }

    }


}
