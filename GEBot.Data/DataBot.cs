using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBot.Data
{
    public class DataBot
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public String param { get; set; }    
        public UIntPtr hwnd { get; set; }
        public int Kanal { get; set; }
        public int nomerTeleport { get; set; }
        public String nameOfFamily { get; set; }
        public int[] triangleX { get; set; }
        public int[] triangleY { get; set; }
        public int Bullet { get; set; }                              //используемы тип патронов


        //public int NumberOfAccaunts { get; set; }

    }
}
