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
        public class botMerchant 
        {
            static Timer myTimer = new Timer();
            private botWindow botwindow;
            private ServerInterface server;
            private ServerFactory serverFactory;
        
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
        public botMerchant(botWindow botwindow ) 
        {
            this.botwindow = botwindow;
            serverFactory = new ServerFactory(botwindow);
            server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
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
            while (!server.isTown())         //ожидание загрузки города
            { botwindow.Pause(500); }

            botwindow.PressEscThreeTimes();
            botwindow.Pause(1000);

            botwindow.OpenMap2();             //открываем карту города
            botwindow.Pause(1000);

            //PressMouseL(322, 132);
            botwindow.PressMouseL(322, 344);

            botwindow.PressEscThreeTimes();      // закрываем карту
            //            Pause(15000);
        }

        ///// <summary>
        ///// Открытие окна торговца (vovannmsk) и переход на место торговли 
        ///// </summary>
        //public void OpenTrader()
        //{
        //    botwindow.StateDriverRun(new StateGT14(botwindow), new StateGT17(botwindow));       //  (нет окна - казарма - город)
        //    GoToChangePlace();           //  торговец следует на место передачи песо
            
        //    //botwindow.ReOpenWindow();
        //    //botwindow.Pause(1000);                                 

        //    ////=================== выполняем коннект с вводом логина и пароля  ============================
        //    //bool result = botwindow.Connect();

        //    //if (result)   // если получилось войти, то
        //    //{
        //    //    while (!server.isBarack())         //ожидание загрузки казармы
        //    //    { botwindow.Pause(500); }

        //    //    //============ выбор персонажей  ===========
        //    //    server.TeamSelection();
        //    //    botwindow.Pause(500);

        //    //    //============ выбор канала ===========
        //    //    botwindow.SelectChannel();
        //    //    botwindow.Pause(500);

        //    //    //============ выход в город  ===========
        //    //    botwindow.NewPlace();                //начинаем в Юстиаре  

        //    //    botwindow.PressMouseR(200, 570);     //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown

        //    //    GoToChangePlace();           //  торговец следует на место передачи песо
        //    //}
        //}

        /// <summary>
        /// обмен песо на фесо (часть 1 со стороны торговца) 
        /// </summary>
        public void ChangeVisTrader1()
        {
            // наживаем Yes, подтверждая торговлю
            botwindow.PressMouseL(1161 - 700, 595 - 180);

            // открываем сундук (карман)
            server.TopMenu(8, 1);

            // открываем закладку кармана, там где фесо
            botwindow.PressMouseL(1666 - 700, 329 - 180);

            // перетаскиваем фесо на стол торговли
            botwindow.MouseMoveAndDrop(1576 - 700, 361 - 180, 1090 - 700, 536 - 180);                         // фесо берется из третьей ячейки на этой закладке  
            botwindow.Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы фесо
            botwindow.PressMouseL(1305 - 700, 573 - 180);


            // нажимаем ок и обмен
            botwindow.PressMouseL(1135 - 700, 677 - 180);
            botwindow.PressMouseL(1216 - 700, 677 - 180);
        }

        /// <summary>
        /// обмен песо на фесо (часть 2 со стороны торговца). Нажать согласие на торговлю у торговца, положить фесо, нажать "ок" и "обмен"
        /// </summary>
        public void ToTradePartOne()
        {
            //делаем окно торговца активным
            botwindow.ReOpenWindow();
            botwindow.Pause(500);

            //// наживаем Yes
            //// открываем карман (инвентарь)
            //// открываем закладку кармана, там где фесо
            //// перетаскиваем фесо
            //// нажимаем Ок для подтверждения передаваемой суммы фесо
            //// нажимаем ок и обмен
            ChangeVisTrader1();
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
