using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class MMSing : MM
    {
        public MMSing ()
        {}

        public MMSing(botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            pointIsMMSell1 = new PointColor(549 - 5 + xx, 606 - 5 + yy, 4370000, 4);
            pointIsMMSell2 = new PointColor(549 - 5 + xx, 607 - 5 + yy, 4370000, 4);
            pointIsMMBuy1 = new PointColor(572 - 5 + xx, 604 - 5 + yy, 7850000, 4);
            pointIsMMBuy2 = new PointColor(572 - 5 + xx, 605 - 5 + yy, 7850000, 4);

            // ============  методы  ========================


        }
    }
}
