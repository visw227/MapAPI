using System;
using System.ComponentModel;
using SQLite;
using Xamarin.Forms;
namespace MapApiApp.Model
{
    public class MapCords
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public double Lat
        {
            get;
            set;
        }
        public double Long
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

    }
}
