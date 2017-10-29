using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class EuropaTownUstiar : Town
    {
        //iPoint pointMaxHeight;
        //iPoint pointBookmark;
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

        //const int PAUSE_TIME = 2000;
        //const int TELEPORT_N = 2;

        private botWindow botwindow;

        /// <summary>
        /// конструктор для класса
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public EuropaTownUstiar(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();
            this.PAUSE_TIME = 2000;
            this.TELEPORT_N = 4;   //номер городского телепорта
            //точки для нажимания на них
            this.pointMaxHeight = new Point(545 - 5 + xx, 500 - 5 + yy);                                           //проверено
            this.pointBookmark = new Point(686 - 5 + xx, 74 - 5 + yy);                                              //проверено
            this.pointTraderOnMap = new Point(663 - 5 + xx, 245 - 5 + yy);                                              //проверено
            this.pointButtonMoveOnMap = new Point(728 + xx, 563 + yy);                                              //проверено
            this.pointHeadTrader = new Point(671 - 5 + xx, 440 - 5 + yy);                                             //проверено
            this.pointSellOnMenu = new Point(520 + xx, 654 + yy);
            this.pointOkOnMenu = new Point(902 + xx, 674 + yy);
            this.pointTownTeleport = new Point(110 + xx, 328 + (TELEPORT_N - 1) * 30 + yy);                 //проверено
            //точки для проверки цвета
            this.pointOpenMap1 = new PointColor(740 - 5 + xx, 562 - 5 + yy, 7660000, 4);                    //проверено
            this.pointOpenMap2 = new PointColor(740 - 5 + xx, 563 - 5 + yy, 7660000, 4);                    //проверено
            this.pointBookmark1 = new PointColor(706 - 5 + xx, 68 - 5 + yy, 7900000, 5);                    //проверено
            this.pointBookmark2 = new PointColor(707 - 5 + xx, 68 - 5 + yy, 7900000, 5);                    //проверено
            //this.pointOpenTownTeleport1 = new PointColor(105 - 5 + xx, 292 - 5 + yy, 12500000, 4);
            //this.pointOpenTownTeleport2 = new PointColor(105 - 5 + xx, 296 - 5 + yy, 13030000, 4);
            this.pointOpenTownTeleport1 = new PointColor(98 - 5 + xx, 296 - 5 + yy, 12500000, 5);             //проверено
            this.pointOpenTownTeleport2 = new PointColor(98 - 5 + xx, 297 - 5 + yy, 12500000, 5);              //проверено

        }

        ///// <summary>
        ///// переход по городскому телепорту, пункт номер "Number_teleport"
        ///// </summary>
        ///// <param name="Number_teleport"> номер пункта телепорта, по которому надо перелететь </param>
        //public void TownTeleport(int Number_teleport)
        //{
        //    //PressMouse(110, 328 + (Number_teleport - 1) * 30);
        //    pointTownTeleport.PressMouse();
        //}

        ///// <summary>
        ///// проверяет, открыт ли городской телепорт (Alt + F3)                             
        ///// </summary>
        ///// <returns> true, если телепорт  (Alt + F3) открыт </returns>
        //public bool isOpenTownTeleport()
        //{
        //    return botwindow.isColor2(1773 - 875, 707 - 5, 7920000, 1773 - 875, 706 - 5, 7920000, 4);
        //}

        ///// <summary>
        ///// удаляем камеру (поднимаем максимально вверх)                           
        ///// </summary>
        //public void MaxHeight()
        //{
        //    const int NUMBER_OF_ITERATION = 10;
        //    for (int j = 1; j <= NUMBER_OF_ITERATION; j++)
        //    {
        //        //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(548 - 5 + botwindow.getX(), 479 - 5 + botwindow.getY(), 9);
        //        botwindow.PressMouseWheelUp(548 - 5, 479 - 5);
        //        botwindow.Pause(200);
        //    }
        //}

        ///// <summary>
        ///// перелететь по городскому телепорту на торговую улицу                            
        ///// </summary>
        //public void TownTeleportW()
        //{
        //    //багама
        //    //botwindow.TownTeleport(3);
        //    pointTownTeleport.PressMouse();

        //}

        ///// <summary>
        ///// проверяет, открыта ли карта Alt+Z в городе                                        
        ///// </summary>
        ///// <returns> true, если карта уже открыта </returns>
        //public bool isOpenMap()
        //{
        //    return botwindow.isColor2(1773 - 875, 707 - 5, 7920000, 1773 - 875, 706 - 5, 7920000, 4);
        //}

        ///// <summary>
        ///// проверяет, открыласть ли вторая закладка карты местности (Alt + Z)          
        ///// </summary>
        ///// <returns> true, если вторая закладка открыта </returns>
        //public bool isSecondBookmark()
        //{
        //    return botwindow.isColor2(1773 - 875, 707 - 5, 7920000, 1773 - 875, 706 - 5, 7920000, 4);
        //}

        ///// <summary>
        ///// открыть вторую закладку в уже открытой карте Alt+Z       
        ///// </summary>
        //public void SecondBookmark()
        //{
        //    //ребо //коимбра //Ош
        //    botwindow.PressMouseL(1780 - 875, 13 - 5);
        //    botwindow.PressMouseL(1780 - 875, 13 - 5);
        //}

        ///// <summary>
        ///// переход к торговцу, который стоит рядом с нужным нам торговцем. карта местности Alt+Z открыта 
        ///// </summary>
        //public void GoToTraderMap()
        //{
        //    //ребо, тыкаем в строчку с торговцем (Metals Merchant в Ребольдо)
        //    botwindow.Pause(500);
        //    botwindow.PressMouse(1650 - 800, 358 - 80);
        //}

        ///// <summary>
        ///// нажимаем на кнопку "Move" в открытой карте Alt+Z             
        ///// </summary>
        //public void ClickMoveMap()
        //{
        //    //ребо,//коимбра  //Ош                                                                                                                  
        //    //тыкаем в кнопку "Move"
        //    botwindow.Pause(500);
        //    botwindow.PressMouse(1795 - 875, 710 - 5);
        //}

        ///// <summary>
        ///// Делаем паузу, чтобы бот успел добежать до торговца           
        ///// </summary>
        //public void PauseToTrader()
        //{
        //    botwindow.Pause(2000);
        //}

        ///// <summary>
        ///// тыкаем мышкой в голову торговца, чтобы войти в магазин              
        ///// </summary>
        //public void Click_ToHeadTrader()
        //{
        //    botwindow.PressMouseL(1230 - 875, 485);
        //}

        ///// <summary>
        ///// Кликаем на строчку Sell и кнопку "Ok" в магазине   
        ///// </summary>
        //public void ClickSellAndOkInTrader()
        //{
        //    //ребо, коимбра, ош, багама
        //    //========= тыкаем в "Sell/Buy Items" ======================================
        //    botwindow.PressMouseL(520, 652);
        //    botwindow.PressMouseL(520, 652);
        //    botwindow.Pause(500);
        //    //========= тыкаем в OK =======================
        //    botwindow.PressMouseL(902, 674);
        //    botwindow.Pause(2500);
        //}

        /// <summary>
        /// дополнительные нажатия для выхода из магазина (бывает в магазинах необходимо что-то нажать дополнительно, чтобы выйти)
        /// </summary>
        public override void ExitFromTrader()
        {


        }
    }
}
