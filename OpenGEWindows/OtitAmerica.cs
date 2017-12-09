using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class OtitAmerica : Otit
    {
        public OtitAmerica()
        { }

        public OtitAmerica(botWindow botwindow) 
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion


        }

        // ======================  методы  ========================

        /// <summary>
        /// получаем следующую точку маршрута
        /// </summary>
        /// <returns></returns>
        public override iPoint RouteNextPoint()
        {
            iPoint[] route = { new Point(505 - 5 + xx, 505 - 5 + yy), 
                               new Point(462 - 5 + xx, 468 - 5 + yy), 
                               new Point(492 - 5 + xx, 437 - 5 + yy), 
                               new Point(539 - 5 + xx, 486 - 5 + yy), 
                               new Point(462 - 5 + xx, 468 - 5 + yy) };

            iPoint result = route[counterRoute];
            counterRoute++; if (counterRoute > 4) counterRoute = 0;

            return result;
        }


        /// <summary>
        /// получаем время для прохождения следующего участка маршрута
        /// </summary>
        /// <returns>время в мс</returns>
        public override int RouteNextPointTime()
        {
            int[] routeTime = { 10000, 15000, 15000, 15000, 15000 };

            int result = routeTime[counterRoute];

            return result;
        }



    }
}
