using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttribueTanimlamalari;
using ORMKaydet;
using ORMSil;
using ORMGuncelle;
using ORMAramaYap;

namespace Main
{
    class Program
    {
            veritabaninaKaydet(); // Tabloya veri kaydettim. id = 5, Ad = "caner" , Soyad = "Demirci"
            veriTabaniIdIleGuncelle(); // id ile adımın baş harfini büyük olarak güncelledim. Ad = "Caner"
            veritabanindanVeriArayan(); // Herhangi bir alana değer atayarak arama yaptım.
            veritabanindanSil(); // id değeri ile silme işlemi yaptım.
            
    }
    
    private void veritabaninaKaydet()
    {
           var tabloAdi = new TabloAdi();
           tabloAdi.Ad = "caner";
           tabloAdi.Soyad = "Demirci";
           tabloAdi.VeritabaninaKaydet();
    }
    
    private void veritabaniIdIleGuncelle()
    {
           var tabloAdi = new TableAdi();
           tabloAdi.Id = 5;
           tabloAdi.Ad = "Caner";
           tabloAdi.VeritabaniIdIleGuncelle();
    }
    
    private void veritabanindanVeriArayan()
    {
           var tabloAdi = new TableAdi();
           tabloAdi.Ad = "Caner";
           tabloAdi.VeritabanindanVeriArayan();
    }
    
    private void veritabanindanSil()
    {
           var tabloAdi = new TableAdi();
           tabloAdi.Id = 5;
           tabloAdi.VeritabanindanSil();
    }

    [DbTablo("TABLO_ADI")]
    public class TabloAdi
    {
        [DbKolon("ID")]
        public int Id;

        [DbKolon("AD")]
        public string Ad;

        [DbKolon("SOYAD")]
        public string Soyad;
    }
}
