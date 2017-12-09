using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace OpenGEWindows
{
    public abstract class Otit : Server2
    {

        protected iPointColor pointOldMan1;
        protected iPointColor pointOldMan2;
        protected iPointColor pointTask1;
        protected iPointColor pointTask2;
        protected Dialog dialog;
        protected Server server;
        protected static int counterRoute;


        // ============  методы  ========================

        /// <summary>
        /// проверяем, находимся ли мы в диалоге со старым мужиком в Лос Толдосе
        /// </summary>
        public bool isOldMan()
        {
            return (pointOldMan1.isColor() && pointOldMan2.isColor());
        }

        /// <summary>
        /// получить задачу у старого мужика
        /// </summary>
        public void GetTask()
        {
            dialog.PressStringDialog(1);
            dialog.PressOkButton(1);

            dialog.PressStringDialog(2);
            dialog.PressOkButton(2);

        }

        /// <summary>
        /// проверяем, выполнено ли задание
        /// </summary>
        /// <returns></returns>
        public bool isTaskDone()
        {
            return (pointTask1.isColor() && pointTask2.isColor());
        }

        /// <summary>
        /// войти в Земли Мертвых через старого мужика
        /// </summary>
        public void EnterToTierraDeLosMuertus()
        {
            dialog.PressStringDialog(2);
            dialog.PressOkButton(1);

            dialog.PressStringDialog(3);
            dialog.PressOkButton(1);

            dialog.PressStringDialog(1);
            dialog.PressOkButton(1);

        }

        /// <summary>
        /// получить чистый отит (забрать в диалоге у старого мужика)
        /// </summary>
        public void TakePureOtite()
        {
            dialog.PressStringDialog(1);
            dialog.PressOkButton(1);

            dialog.PressStringDialog(1);
            dialog.PressOkButton(3);
        }



        /// <summary>
        /// подходим к старому человеку после перехода из казарм
        /// </summary>
        public void GoToOldMan()
        {
            //while (!town.isOpenMap())
            //{
                server.OpenMapForState();
                Pause(1000);
            //}

            town.PressOldManonMap();
            town.ClickMoveMap();

            botwindow.PressEscThreeTimes();
            Pause(5000);

            town.PressOldMan1();
            Pause(2000);
        }

        /// <summary>
        /// переход по карте Земли мертвых к месту начала маршрута для набивания андидов (100 шт.)
        /// </summary>
        public void GotoWork()
        {
            CounterRouteToNull();
            RouteNextPoint().PressMouseL();
            Pause(RouteNextPointTime());
            //pointWorkOnMap.PressMouseL();
        }

        /// <summary>
        /// переход по карте Земли мертвых к месту начала маршрута для набивания андидов (100 шт.)
        /// </summary>
        public void GotoNextPointRoute()
        {
            RouteNextPoint().PressMouseR();
            Pause(RouteNextPointTime());
            counterRoute++; if (counterRoute > 4) counterRoute = 1;

        }


        /// <summary>
        /// обнуляем счетчик, чтобы начать с начала маршрута
        /// </summary>
        public void  CounterRouteToNull()
        {
            counterRoute = 0;
        }


        /// <summary>
        /// получаем следующий пункт маршрута
        /// </summary>
        /// <returns>следующая точка</returns>
        public abstract iPoint RouteNextPoint();

        /// <summary>
        /// получаем время для прохождения следующего участка маршрута
        /// </summary>
        /// <returns>время в мс</returns>
        public abstract int RouteNextPointTime();




    }

}
