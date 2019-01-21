using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace Main
{
    class Program
    {
            TabloAdi tabloAdi = new TableName();
            tabloAdi.Ad = "Caner";
            tabloAdi.Soyad = "Demirci";

            tabloAdi.VeriTabaninaKaydet();
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
