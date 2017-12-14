﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class EuropaTownCoimbra : Town
    {

        private botWindow botwindow;

        /// <summary>
        /// конструктор для класса
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public EuropaTownCoimbra(botWindow botwindow)
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
            this.pointExitFromTrader = new Point(906 - 5 + xx, 679 - 5 + yy);
            //точки для проверки цвета
            this.pointOpenMap1 = new PointColor(801 - 5 + xx, 46 - 5 + yy, 16440000, 4);
            this.pointOpenMap2 = new PointColor(804 - 5 + xx, 46 - 5 + yy, 16510000, 4);
            this.pointBookmark1 = new PointColor(850 - 5 + xx, 43 - 5 + yy, 7700000, 4);
            this.pointBookmark2 = new PointColor(860 - 5 + xx, 43 - 5 + yy, 7700000, 4);
            this.pointOpenTownTeleport1 = new PointColor(105 - 5 + xx, 292 - 5 + yy, 12500000, 4);
            this.pointOpenTownTeleport2 = new PointColor(105 - 5 + xx, 296 - 5 + yy, 13030000, 4);

        }



        /// <summary>
        /// дополнительные нажатия для выхода из магазина (бывает в магазинах необходимо что-то нажать дополнительно, чтобы выйти)
        /// </summary>
        public override void ExitFromTrader()
        {
            Pause(1500);
            pointExitFromTrader.PressMouseL();

        }





    }
}
