using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OpenGEWindows
{
    public abstract class Town
    {
        protected iPoint pointMaxHeight;
        protected iPoint pointBookmark;
        protected iPoint pointTraderOnMap;
        protected iPoint pointButtonMoveOnMap;
        protected iPoint pointHeadTrader;
        protected iPoint pointSellOnMenu;
        protected iPoint pointOkOnMenu;
        protected iPoint pointTownTeleport;
        protected iPoint pointExitFromTrader;

        protected iPointColor pointOpenMap1;
        protected iPointColor pointOpenMap2;
        protected iPointColor pointBookmark1;
        protected iPointColor pointBookmark2;
        protected iPointColor pointOpenTownTeleport1;
        protected iPointColor pointOpenTownTeleport2;

        protected int xx;
        protected int yy;
        protected int PAUSE_TIME;
        protected int TELEPORT_N;   //номер городского телепорта
        protected const int NUMBER_OF_ITERATION = 10;

        /// <summary>
        /// выход из магазина путем нажатия кнопки Exit
        /// </summary>
        public void PressExitFromShop()
        {
            pointOkOnMenu.PressMouseL();
            Pause(2500);
        }

        /// <summary>
        /// проверяет, открыт ли городской телепорт (Alt + F3)                             
        /// </summary>
        /// <returns> true, если телепорт  (Alt + F3) открыт </returns>
        public bool isOpenTownTeleport()
        {
            uint ff = pointOpenTownTeleport1.GetPixelColor();
            uint gg = pointOpenTownTeleport2.GetPixelColor();

            return ((pointOpenTownTeleport1.isColor()) & (pointOpenTownTeleport2.isColor()));
        }

        /// <summary>
        /// удаляем камеру (поднимаем максимально вверх)                           
        /// </summary>
        public void MaxHeight()
        {
            for (int j = 1; j <= NUMBER_OF_ITERATION; j++)
            {
                pointMaxHeight.PressMouseWheelUp();
            }
        }

        /// <summary>
        /// перелететь по городскому телепорту на торговую улицу                            
        /// </summary>
        public void TownTeleportW()
        {
            pointTownTeleport.PressMouse();
        }

        /// <summary>
        /// проверяет, открыта ли карта Alt+Z в городе                                        
        /// </summary>
        /// <returns> true, если карта уже открыта </returns>
        public bool isOpenMap()
        {
            uint ff = pointOpenMap1.GetPixelColor();
            uint gg = pointOpenMap2.GetPixelColor();
            return ((pointOpenMap1.isColor()) && (pointOpenMap2.isColor()));
        }

        /// <summary>
        /// проверяет, открыласть ли вторая закладка карты местности (Alt + Z)          
        /// </summary>
        /// <returns> true, если вторая закладка открыта </returns>
        public bool isSecondBookmark()
        {
            //bool ff = pointisOpenMenuPet1.isColor();
            //bool gg = pointisOpenMenuPet2.isColor();
            //uint bb = pointBookmark1.GetPixelColor();
            //uint dd = pointBookmark2.GetPixelColor();
            return ((pointBookmark1.isColor()) && (pointBookmark2.isColor()));
        }

        /// <summary>
        /// открыть вторую закладку в уже открытой карте Alt+Z       
        /// </summary>
        public void SecondBookmark()
        {
            pointBookmark.PressMouseL();
            pointBookmark.PressMouseL();
        }

        /// <summary>
        /// переход к торговцу, который стоит рядом с нужным нам торговцем. карта местности Alt+Z открыта 
        /// </summary>
        public void GoToTraderMap()
        {
            pointTraderOnMap.PressMouse();
        }

        /// <summary>
        /// нажимаем на кнопку "Move" в открытой карте Alt+Z             
        /// </summary>
        public void ClickMoveMap()
        {
            pointButtonMoveOnMap.PressMouse();
        }

        /// <summary>
        /// Делаем паузу, чтобы бот успел добежать до торговца           
        /// </summary>
        public void PauseToTrader()
        {
            Pause(PAUSE_TIME);
        }

        /// <summary>
        /// тыкаем мышкой в голову торговца, чтобы войти в магазин              
        /// </summary>
        public void Click_ToHeadTrader()
        {
            pointHeadTrader.PressMouseL();
        }

        /// <summary>
        /// Кликаем на строчку Sell и кнопку "Ok" в магазине   
        /// </summary>
        public void ClickSellAndOkInTrader()                 //кандидат на перенос в serverInterface
        {
            //ребо, коимбра, ош, багама
            //========= тыкаем в "Sell/Buy Items" ======================================
            //botwindow.PressMouseL(520, 654);
            //botwindow.Pause(200);
            //botwindow.PressMouseL(520, 654);
            pointSellOnMenu.PressMouseL();
            pointSellOnMenu.PressMouseL();
            Pause(500);

            //========= тыкаем в OK =======================
            //botwindow.PressMouseL(902, 674);
            pointOkOnMenu.PressMouseL();
            Pause(2500);
        }

        /// <summary>
        /// дополнительные нажатия для выхода из магазина (бывает в магазинах необходимо что-то нажать дополнительно, чтобы выйти)
        /// </summary>
        public abstract void ExitFromTrader();

        /// <summary>
        /// Останавливает поток на некоторый период
        /// </summary>
        /// <param name="ms"> ms - период в милисекундах </param>
        protected void Pause(int ms)
        {
            //Class_Timer.Pause(ms);
            Thread.Sleep(ms);
        }


    }
}
