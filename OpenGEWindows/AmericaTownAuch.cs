using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class AmericaTownAuch : Town
    {
        //iPoint pointMaxHeight;
        //iPoint pointBookmark ;
        //iPoint pointTraderOnMap;
        //iPoint pointButtonMoveOnMap;
        //iPoint pointHeadTrader;
        //iPoint pointSellOnMenu;
        //iPoint pointOkOnMenu;
        //iPoint pointTownTeleport;

        //iPointColor pointOpenMap1;
        //iPointColor pointOpenMap2;
        //iPointColor pointBookmark1;
        //iPointColor pointBookmark2;
        //iPointColor pointOpenTownTeleport1;
        //iPointColor pointOpenTownTeleport2;

        //int xx;
        //int yy;

        //int PAUSE_TIME = 2000;
        //int TELEPORT_N = 2;   //номер городского телепорта

        private botWindow botwindow;

        /// <summary>
        /// конструктор для класса
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public AmericaTownAuch(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();
            this.PAUSE_TIME = 2000;
            this.TELEPORT_N = 2;   //номер городского телепорта
            //точки для нажимания на них
            this.pointMaxHeight = new Point(545 - 5 + xx, 500 - 5 + yy);
            this.pointBookmark = new Point(875 + xx, 45 + yy);
            this.pointTraderOnMap = new Point(875 + xx, 170 + yy);
            this.pointButtonMoveOnMap = new Point(925 + xx, 723 + yy);
            this.pointHeadTrader = new Point(291 + xx, 225 + yy);
            //this.pointSellOnMenu = new Point(520 + xx, 654 + yy);
            //this.pointOkOnMenu = new Point(902 + xx, 674 + yy);
            this.pointTownTeleport = new Point(110 + xx, 328 + (TELEPORT_N - 1) * 30 + yy);
            //точки для проверки цвета
            this.pointOpenMap1 = new PointColor(801 - 5 + xx, 46 - 5 + yy, 16440000, 4);
            this.pointOpenMap2 = new PointColor(804 - 5 + xx, 46 - 5 + yy, 16510000, 4);
            this.pointBookmark1 = new PointColor(850 - 5 + xx, 43 - 5 + yy, 7700000, 4);
            this.pointBookmark2 = new PointColor(860 - 5 + xx, 43 - 5 + yy, 7700000, 4);
            this.pointOpenTownTeleport1 = new PointColor(105 - 5 + xx, 292 - 5 + yy, 12500000, 4);
            this.pointOpenTownTeleport2 = new PointColor(105 - 5 + xx, 296 - 5 + yy, 13030000, 4);
        }

//        /// <summary>
//        /// проверяет, открыт ли городской телепорт (Alt + F3)                             
//        /// </summary>
//        /// <returns> true, если телепорт  (Alt + F3) открыт </returns>
//        public bool isOpenTownTeleport()
//        {
////            return botwindow.isColor2(105 - 5, 292 - 5, 12500000, 105 - 5, 296 - 5, 13030000, 4);
//            return ((pointOpenTownTeleport1.isColor()) & (pointOpenTownTeleport2.isColor()));
//        }

//        /// <summary>
//        /// удаляем камеру (поднимаем максимально вверх)                           
//        /// </summary>
//        public void MaxHeight()
//        {
//            const int NUMBER_OF_ITERATION = 10;
//            for (int j = 1; j <= NUMBER_OF_ITERATION; j++)
//            {
//                //botwindow.PressMouseWheelUp(545 - 5, 500 - 5);
//                //botwindow.Pause(200);
//                pointMaxHeight.PressMouseWheelUp();
//            }
//        }

//        /// <summary>
//        /// перелететь по городскому телепорту на торговую улицу                            
//        /// </summary>
//        public void TownTeleportW()
//        {
//            //TownTeleport(TELEPORT_N);
//            pointTownTeleport.PressMouse();

//        }

//        /// <summary>
//        /// проверяет, открыта ли карта Alt+Z в городе                                        
//        /// </summary>
//        /// <returns> true, если карта уже открыта </returns>
//        public bool isOpenMap()
//        {
////            return botwindow.isColor2(801 - 5, 46 - 5, 16440000, 804 - 5, 46 - 5, 16510000, 4);
//            return ((pointOpenMap1.isColor()) && (pointOpenMap2.isColor()));
//        }

//        /// <summary>
//        /// проверяет, открыласть ли вторая закладка карты местности (Alt + Z)          
//        /// </summary>
//        /// <returns> true, если вторая закладка открыта </returns>
//        public bool isSecondBookmark()
//        {
////            return botwindow.isColor2(850 - 5, 43 - 5, 7700000, 860 - 5, 43 - 5, 7700000, 4);
//            return ((pointBookmark1.isColor())&&(pointBookmark2.isColor()));
//        }

//        /// <summary>
//        /// открыть вторую закладку в уже открытой карте Alt+Z       
//        /// </summary>
//        public void SecondBookmark()                                                             
//        {
//            //botwindow.PressMouse(875, 45);
//            //botwindow.PressMouse(875, 45);
//            pointBookmark.PressMouse();
//            pointBookmark.PressMouse();
//        }

//        /// <summary>
//        /// переход к торговцу, который стоит рядом с нужным нам торговцем. карта местности Alt+Z открыта 
//        /// </summary>
//        public void GoToTraderMap()
//        {
//            //Ош,   Лорч в Оше
//            //botwindow.Pause(500);
//            //botwindow.PressMouse(875, 170);
//            pointTraderOnMap.PressMouse();
//        }

//        /// <summary>
//        /// нажимаем на кнопку "Move" в открытой карте Alt+Z             
//        /// </summary>
//        public void ClickMoveMap()
//        {
//            //botwindow.Pause(500);
//            //botwindow.PressMouse(925, 723);
//            //botwindow.Pause(200);
//            pointButtonMoveOnMap.PressMouse();
//        }

//        /// <summary>
//        /// Делаем паузу, чтобы бот успел добежать до торговца           
//        /// </summary>
//        public void PauseToTrader()
//        {
//            botwindow.Pause(PAUSE_TIME);
//        }

//        /// <summary>
//        /// тыкаем мышкой в голову торговца, чтобы войти в магазин              
//        /// </summary>
//        public void Click_ToHeadTrader()
//        {
//            //botwindow.Pause(200);
//            ////Ош, тыкаем в торговца (Ош)
//            //botwindow.PressMouseL(291, 225);
//            //botwindow.Pause(200);
//            pointHeadTrader.PressMouseL();
//        }

//        /// <summary>
//        /// Кликаем на строчку Sell и кнопку "Ok" в магазине   
//        /// </summary>
//        public void ClickSellAndOkInTrader()
//        {
//            //ребо, коимбра, ош, багама
//            //========= тыкаем в "Sell/Buy Items" ======================================
//            //botwindow.PressMouseL(520, 654);
//            //botwindow.Pause(200);
//            //botwindow.PressMouseL(520, 654);
//            pointSellOnMenu.PressMouseL();
//            pointSellOnMenu.PressMouseL();
//            botwindow.Pause(500);

//            //========= тыкаем в OK =======================
//            //botwindow.PressMouseL(902, 674);
//            pointOkOnMenu.PressMouseL();
//            botwindow.Pause(2500);
//        }

        /// <summary>
        /// дополнительные нажатия для выхода из магазина (бывает в магазинах необходимо что-то нажать дополнительно, чтобы выйти)
        /// </summary>
        public override void ExitFromTrader()
        {
            //Ош

        }
    }
}
