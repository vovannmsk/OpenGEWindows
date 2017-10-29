using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEDataBot.BL
{
    public class DataBot
    {
        private String Login { get; }
        private String Password { get; }
        private int x { get; }
        private int y { get; }
        private String param { get; }           //америка или европа или синг
        private UIntPtr hwnd { get; set; }
        private int Kanal { get; }
        private int nomerTeleport { get; }
        private int needToChange { get; set; }
        private int[] triangleX { get; }
        private int[] triangleY { get; }

    }
}
