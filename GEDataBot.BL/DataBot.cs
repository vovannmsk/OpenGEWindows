using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEDataBot.BL
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
        public int needToChange { get; set; }
        public int[] triangleX { get; set; }
        public int[] triangleY { get; set; }

    }
}
