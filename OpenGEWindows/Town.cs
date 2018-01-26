﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OpenGEWindows
{
    public abstract class Town
    {
        #region общие
            protected int xx;
            protected int yy;
            protected iPoint pointMaxHeight;
            protected const int NUMBER_OF_ITERATION = 10;
            protected botWindow botwindow;
            protected Dialog dialog;
        #endregion

        #region Los Toldos
            protected iPoint pointOldMan1;
        #endregion

        #region Town Teleport
            protected int TELEPORT_N;   //номер городского телепорта
            protected iPoint pointTownTeleport;
            protected iPointColor pointOpenTownTeleport1;
            protected iPointColor pointOpenTownTeleport2;
        #endregion

        #region Shop
            protected iPoint pointHeadTrader;
            //protected iPoint pointSellOnMenu;
            //protected iPoint pointOkOnMenu;
            protected int PAUSE_TIME;

        #endregion

        #region Map
            protected iPointColor pointOpenMap1;
            protected iPointColor pointOpenMap2;
            protected iPointColor pointBookmark1;
            protected iPointColor pointBookmark2;
            protected iPoint pointOldManOnMap;
            protected iPoint pointBookmark;
            protected iPoint pointTraderOnMap;
            protected iPoint pointButtonMoveOnMap;
            protected iPoint pointExitFromTrader;
        #endregion

        // ======================= Методы ====================================

        #region общие

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
            /// удаляем камеру (поднимаем максимально вверх)                           
            /// </summary>
            public void MinHeight()
            {
                for (int j = 1; j <= NUMBER_OF_ITERATION; j++)
                {
                    pointMaxHeight.PressMouseWheelDown();
                }
            }

            /// <summary>
            /// Останавливает поток на некоторый период
            /// </summary>
            /// <param name="ms"> ms - период в милисекундах </param>
            protected void Pause(int ms)
            {
                //Class_Timer.Pause(ms);
                Thread.Sleep(ms);
            }

        #endregion

        #region Los Toldos

            /// <summary>
            /// тыкаем на старого мужика
            /// </summary>
            public void PressOldMan1()
            {
                pointOldMan1.PressMouseL();
            }

        #endregion

        #region Town Teleport (Alt + F3)

            /// <summary>
            /// проверяет, открыт ли городской телепорт (Alt + F3)                             
            /// </summary>
            /// <returns> true, если телепорт  (Alt + F3) открыт </returns>
            public bool isOpenTownTeleport()
            {
                return ((pointOpenTownTeleport1.isColor()) & (pointOpenTownTeleport2.isColor()));
            }

            /// <summary>
            /// перелететь по городскому телепорту на торговую улицу                            
            /// </summary>
            public void TownTeleportW()
            {
                pointTownTeleport.PressMouse();
            }

        #endregion

        #region Shop

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
            /// дополнительные нажатия для выхода из магазина (бывает в магазинах необходимо что-то нажать дополнительно, чтобы выйти)
            /// </summary>
            public abstract void ExitFromTrader();

            /// <summary>
            /// нажать Sell и  Ok в меню входа в магазин (зависит от города)
            /// </summary>
            public abstract void SellAndOk();

        #endregion

        #region Map

            /// <summary>
            /// тыкаем на открытой карте в строчку со старым мужиком
            /// </summary>
            public void PressOldManonMap()
            {
                pointOldManOnMap.DoubleClickL();
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
            /// проверяет, открыта ли карта Alt+Z в городе                                        
            /// </summary>
            /// <returns> true, если карта уже открыта </returns>
            public bool isOpenMap()
            {
                return ((pointOpenMap1.isColor()) && (pointOpenMap2.isColor()));
            }

            /// <summary>
            /// проверяет, открыласть ли вторая закладка карты местности (Alt + Z)          
            /// </summary>
            /// <returns> true, если вторая закладка открыта </returns>
            public bool isSecondBookmark()
            {
                return ((pointBookmark1.isColor()) && (pointBookmark2.isColor()));
            }

        #endregion

    }
}
