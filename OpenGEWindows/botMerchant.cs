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
            static Timer myTimer = new Timer();
            private botWindow botwindow;
            private ServerInterface server;
            private ServerFactory serverFactory;

            //private Point pointFesoBegin;
            //private Point pointFesoEnd;
        
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
            this.botwindow = new botWindow(numberOfWindow);
            serverFactory = new ServerFactory(botwindow);
            server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            //int x = botwindow.getX();  //смещешие окна на экране
            //int y = botwindow.getY();
            //pointFesoBegin = new Point(881 - 5 + x, 186 - 5 + y);    //1576 - 700, 361 - 180
            //pointFesoEnd   = new Point(395 - 5 + x, 361 - 5 + y);    //1090 - 700, 536 - 180
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public botWindow getBotWindow()
        {
            return this.botwindow;
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
            botwindow.Pause(500);

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


        /// <summary>
        /// обмен песо на фесо (часть 3 со стороны торговца). продаём 10 ВК в фесо шопе, чтобы было что отдать следующему боту
        /// </summary>
        public void ToTradePartTwo()
        {
            //делаем окно торговца активным
            botwindow.ReOpenWindow();
            botwindow.Pause(500);

            //открываем магазин фесо
            OpenFesoShop();

            //нажимаем на закладку sell
            OpenBookmarkSell();

            // продаем 10 ВК в фесо шопе
            SellGrowthStone10();

            //убираем лишнее с экрана торговца
            botwindow.PressEscThreeTimes();
            botwindow.Pause(500);
        }

        /// <summary>
        /// открыть фесо шоп 
        /// </summary>
        public void OpenFesoShop()
        {
            server.TopMenu(2, 2);
            botwindow.Pause(1000);
        }

        /// <summary>
        /// продать 10 ВК в фесо шопе 
        /// </summary>
        public void SellGrowthStone10()
        {
            // 10 раз нажимаем на стрелку вверх, чтобы отсчитать 10 ВК
            for (int i = 1; i <= 10; i++)
            {
                botwindow.PressMouseL(375, 246);
                botwindow.Pause(500);
            }

            //нажимаем кнопку Sell
            botwindow.PressMouseL(725, 620);
            botwindow.Pause(500);

            //нажимаем кнопку Close
            botwindow.PressMouseL(848, 620);
            botwindow.Pause(500);
        }

        /// <summary>
        /// открыть вкладку Sell в фесо шопе
        /// </summary>
        public void OpenBookmarkSell()
        {
            botwindow.PressMouseL(226, 196);
            botwindow.Pause(1500);
        }


    }
}
