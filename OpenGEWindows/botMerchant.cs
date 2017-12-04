using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OpenGEWindows
{
    /// <summary>
    ///  это класс для торговца  
    /// </summary>
    public class botMerchant : botWindow
    {
        //static Timer myTimer = new Timer();
        private ServerInterface server;

        
        /// <summary>
        /// конструктор
        /// </summary>
        public botMerchant ()
        {
            MessageBox.Show("НУЖНЫ ПАРАМЕТРЫ (номер окна) botMerchant");
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public botMerchant(int numberOfWindow ) : base (numberOfWindow)
        {
            server = base.getserver();
        }

        /// <summary>
        /// переход торговца к месту передачи песо (внутри города)
        /// </summary>
        public void GoToChangePlace()
        {
            iPoint pointDealer = new Point(405 - 5 + getDataBot().x , 210 - 5 + getDataBot().y);
            pointDealer.DoubleClickL();
        }

          /// <summary>
        /// обмен песо на фесо (часть 1 со стороны торговца) 
        /// </summary>
        public void ChangeVisTrader1()
        {
            // наживаем Yes, подтверждая торговлю
            iPoint pointYesTrade = new Point(1161 - 700 + getDataBot().x, 595 - 180 + getDataBot().y);
            pointYesTrade.DoubleClickL();

            // открываем сундук (карман)
            server.TopMenu(8, 1);

            // открываем закладку кармана, там где фесо
            iPoint pointBookmark4 = new Point(903 - 5 + getDataBot().x, 151 - 5 + getDataBot().y);
            pointBookmark4.DoubleClickL();

            // перетаскиваем фесо на стол торговли
            iPoint pointFesoBegin = new Point(740 - 5 + getDataBot().x, 186 - 5 + getDataBot().y);
            iPoint pointFesoEnd = new Point(388 - 5 + getDataBot().x, 343 - 5 + getDataBot().y);
            pointFesoBegin.Drag(pointFesoEnd);                                                
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы фесо
            iPoint pointOkFeso = new Point(611 - 5 + getDataBot().x, 397 - 5 + getDataBot().y);
            pointOkFeso.DoubleClickL();    

            // нажимаем ок
            iPoint pointOk = new Point(441 - 5 + getDataBot().x, 502 - 5 + getDataBot().y);
            pointOk.DoubleClickL();

            // нажимаем обмен
            iPoint pointTrade = new Point(522 - 5 + getDataBot().x, 502 - 5 + getDataBot().y);
            pointTrade.DoubleClickL();
        }

    }
}
