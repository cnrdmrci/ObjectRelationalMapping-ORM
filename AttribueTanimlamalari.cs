using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTanimlamalari
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DbKolon : Attribute
    {
        public DbKolon(string kolonAd, bool primaryKey = false, bool autoIncrement = false)
        {
            KolonAd = kolonAd;
            PrimaryKey = primaryKey;
            AutoIncrement = autoIncrement;
        }

        public string KolonAd { get; }
        public bool PrimaryKey { get; }
        public bool AutoIncrement { get; }

    }


    [AttributeUsage(AttributeTargets.Class)]
    public class DbTablo : Attribute
    {
        public DbTablo(string tabloAd)
        {
            TabloAd = tabloAd;
        }


        public string TabloAd { get; }
    }


}