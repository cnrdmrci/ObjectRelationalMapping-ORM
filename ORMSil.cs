using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttributeTanimlamalari;

namespace ORMSil
{
	public static class ExtensionMethod
    {
    	public static void VeritabanindanSil(this object dbClass)
        {
            Type dbClassType = dbClass.GetType();
            DbTablo DbTablo = (DbTablo)Attribute.GetCustomAttribute(dbClassType, typeof(DbTablo));
            string tabloAdi = DbTablo.TabloAd;
            string id=null;
            FieldInfo[] kolonAlanlari = dbClassType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            try
            {
                foreach (FieldInfo kolonAlani in kolonAlanlari)
                {
                    //kolonAdi degeri bos olmamali yoksa hata verecektir. Her alan bosta olsa gonderilmelidir.

                    DbKolon DbKolon = (DbKolon)Attribute.GetCustomAttribute(kolonAlani, typeof(DbKolon)); //Her property'nin attribute'una gidip kolon adini aldik.
                    if (DbKolon.KolonAd == "ID")//Id otomatik veritabaninda atanacak insert tipinde eklemeyelim.
                    {
                        id = kolonAlani.GetValue(dbClass).ToString();
                        if (id == "0")
                            throw new Exception();
                        break;
                    }
                }
                //sql sorgusunu olusturduk.
                string delete = "DELETE FROM ";

                string sql = delete + tabloAdi + " WHERE ID=" + id;
                Console.WriteLine(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ID alani boş bırakılamaz! ");
            }

        }
    } 

}
