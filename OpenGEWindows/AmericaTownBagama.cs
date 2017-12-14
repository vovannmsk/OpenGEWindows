using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class AmericaTownBagama: Town
    {
        private botWindow botwindow;
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

        //const int PAUSE_TIME = 6000;
        //const int TELEPORT_N = 3;

        /// <summary>
        /// конструктор для класса
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public AmericaTownBagama(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();
            this.PAUSE_TIME = 6000;
            this.TELEPORT_N = 3;   //номер городского телепорта
            //точки для нажимания на них
            this.pointMaxHeight = new Point(538 - 5 + xx, 464 - 5 + yy);
            this.pointBookmark = new Point(775 + xx, 141 + yy);
            this.pointTraderOnMap = new Point(735 - 5 + xx, 288 - 5 + yy);                       //730, 268);
            this.pointButtonMoveOnMap = new Point(822 + xx, 631 + yy);   //822, 631);
            this.pointHeadTrader = new Point(611 + xx, 411 + yy);      //611, 411);
            //this.pointSellOnMenu = new Point(520 + xx, 654 + yy);    //520, 654);
            //this.pointOkOnMenu = new Point(902 + xx, 674 + yy);         //902, 674);
            this.pointTownTeleport = new Point(110 + xx, 328 + (TELEPORT_N - 1) * 30 + yy);
            
            //точки для проверки цвета
            this.pointOpenMap1 = new PointColor(812 - 5 + xx, 632 - 5 + yy, 7920000, 4);                       //проверено
            this.pointOpenMap2 = new PointColor(812 - 5 + xx, 633 - 5 + yy, 7920000, 4);                       //проверено
            this.pointBookmark1 = new PointColor(830 - 5 + xx, 140 - 5 + yy, 7900000, 5);                      //проверено
            this.pointBookmark2 = new PointColor(831 - 5 + xx, 140 - 5 + yy, 7900000, 5);                       //проверено
            this.pointOpenTownTeleport1 = new PointColor(105 - 5 + xx, 292 - 5 + yy, 12000000, 6);             //проверено
            this.pointOpenTownTeleport2 = new PointColor(105 - 5 + xx, 296 - 5 + yy, 13000000, 6);              //проверено


        }

        ///// <summary>
        ///// проверяет, открыт ли городской телепорт (Alt + F3)                             
        ///// </summary>
        ///// <returns> true, если телепорт  (Alt + F3) открыт </returns>
        //public bool isOpenTownTeleport()
        //{
        //    //return isColor2(812 - 5, 632 - 5, 7920000, 812 - 5, 633 - 5, 7920000, 4);                                         // проверить
        //    return true;
        //}

        ///// <summary>
        ///// удаляем камеру (поднимаем максимально вверх)                           
        ///// </summary>
        //public void MaxHeight()
        //{
        //    const int NUMBER_OF_ITERATION = 10;
        //    for (int j = 1; j <= NUMBER_OF_ITERATION; j++)
        //    {
        //        //багама
        //        //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(538 - 5 + botwindow.getX(), 464 - 5 + botwindow.getY(), 9);
        //        botwindow.PressMouseWheelUp(538 - 5, 464 - 5);
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
        //    //return isColor2(812 - 5, 632 - 5, 7920000, 812 - 5, 633 - 5, 7920000, 4);
        //    return ((pointOpenMap1.isColor()) && (pointOpenMap2.isColor())); 
        //}

        ///// <summary>
        ///// проверяет, открыласть ли вторая закладка карты местности (Alt + Z)          
        ///// </summary>
        ///// <returns> true, если вторая закладка открыта </returns>
        //public bool isSecondBookmark()
        //{
        //    //return isColor2(812 - 5, 632 - 5, 7920000, 812 - 5, 633 - 5, 7920000, 4);                                        // проверить
        //    return true;
        //}

        ///// <summary>
        ///// открыть вторую закладку в уже открытой карте Alt+Z       
        ///// </summary>
        //public void SecondBookmark()                                                             // сделать проверку, открылась ли вторая закладка
        //{
        //    //Юстиар и //багама //кастилия
        //    botwindow.PressMouse(775, 141);
        //    botwindow.PressMouse(775, 141);
        //}

        ///// <summary>
        ///// переход к торговцу, который стоит рядом с нужным нам торговцем. карта местности Alt+Z открыта 
        ///// </summary>
        //public void GoToTraderMap()
        //{
        //    //багама, тыкаем в строчку с торговцем (сокет менеджер в Багаме)                                         
        //    botwindow.Pause(500);
        //    botwindow.PressMouse(730, 268);
        //}

        ///// <summary>
        ///// нажимаем на кнопку "Move" в открытой карте Alt+Z             
        ///// </summary>
        //public void ClickMoveMap()
        //{
        //    //Юстиар //багама,  кастилия  
        //    botwindow.Pause(500);
        //    botwindow.PressMouse(822, 631);
        //    botwindow.Pause(200);
        //}

        ///// <summary>
        ///// Делаем паузу, чтобы бот успел добежать до торговца           
        ///// </summary>
        //public void PauseToTrader()
        //{
        //    //багама
        //    botwindow.Pause(6000);
        //}

        ///// <summary>
        ///// тыкаем мышкой в голову торговца, чтобы войти в магазин              
        ///// </summary>
        //public void Click_ToHeadTrader()
        //{
        //    botwindow.Pause(200);
        //    //багама
        //    botwindow.PressMouseL(611, 411);
        //    botwindow.Pause(200);
        //}

        ///// <summary>
        ///// Кликаем на строчку Sell и кнопку "Ok" в магазине   
        ///// </summary>
        //public void ClickSellAndOkInTrader()
        //{
        //    //ребо, коимбра, ош, Юстиар, багама
        //    //========= тыкаем в "Sell/Buy Items" ======================================
        //    botwindow.PressMouseL(520, 654);
        //    botwindow.Pause(200);
        //    botwindow.PressMouseL(520, 654);
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
