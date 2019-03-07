﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace OpenGEWindows
{
    public abstract class BHDialog : Server2
    {
        protected iPoint ButtonOkDialog;
        protected iPointColor pointDialog1;
        protected iPointColor pointDialog2;

        protected iPointColor pointsBottonGateBH1;
        protected iPointColor pointsBottonGateBH2;

        protected iPointColor pointsGateBH1;
        protected iPointColor pointsGateBH2;
        protected iPointColor pointsGateBH3;

        protected iPointColor pointsGateBH4_1;
        protected iPointColor pointsGateBH4_2;
        protected iPoint pointInputBox;
        protected iPoint pointInputBoxBottonOk;

        protected iPointColor pointsGateBH5;
        protected iPointColor pointsGateBH6;
        //        protected iPointColor pointsGateBH6;


        // ============  методы  ========================

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников)
        /// </summary>
        /// <returns></returns>
        public bool isGateBH()
        {
            //ворота могут находиться в разных состояниях. нужно проверить их все

            return isGateBH1() || isGateBH3();   //при переходе из BH к воротам может получиться только состояние №1 или №3.
                                                 //состояние №1 бывает при первых пяти бесплатных заходах в миссию. Состояние №3 бывает во всех остальных случаях
        }


        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем открыт ли диалог с кнопкой Ок
        /// </summary>
        /// <returns></returns>
        public bool isBottonGateBH()
        {
            return (pointsBottonGateBH1.isColor() && pointsBottonGateBH2.isColor());
        }


        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "Now. you can try N times for free" (N = 1..5)                             когда есть бесплатные проходы (действия - )
        /// </summary>
        /// <returns></returns>
        public bool isGateBH1()
        {
            return (isBottonGateBH() && pointsGateBH1.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "Now. you can try N times for free" (N = 1..5)                              исправть
        /// </summary>
        /// <returns></returns>
        public bool isGateBH2()
        {
            return (isBottonGateBH() && pointsGateBH2.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "You cannot enter for free today"                                           когда уже нет бесплатного прохода (действия - выбрать нижнюю строку и нажать Ок)
        /// </summary>
        /// <returns></returns>
        public bool isGateBH3()
        {
            return (isBottonGateBH() && pointsGateBH3.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "Please input Initialize"
        /// </summary>
        /// <returns></returns>
        public bool isGateBH4()
        {
            return (pointsGateBH4_1.isColor() && pointsGateBH4_2.isColor());   //только по кнопке Ок
        }

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "The difficulty level has been reset normaly"
        /// </summary>
        /// <returns></returns>
        public bool isGateBH5()
        {
            return (isBottonGateBH() && pointsGateBH5.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        /// проверяем то состояние ворот, где написано "Reset difficulty by using Shiny Crystal 200 piece(s)"
        /// </summary>
        /// <returns></returns>
        public bool isGateBH6()
        {
            return (isBottonGateBH() && pointsGateBH6.isColor());
        }

        ///// <summary>
        ///// проверяем, находимся ли в воротах в Infinity (Гильдии Охотников) 
        ///// проверяем то состояние ворот, где написано "You cannot enter for free"
        ///// </summary>
        ///// <returns></returns>
        //public bool isGateBH6()
        //{
        //    return (isBottonGateBH() && pointsGateBH6.isColor());
        //}

        /// <summary>
        /// пишем слово "Initialize" в поле ввода и нажимаем кнопку Ок
        /// </summary>
        public void WriteInitialize()
        {

            pointInputBox.DoubleClickL();    // Нажимаем на поле ввода данных
            Pause(1500);

            SendKeys.SendWait("Initialize");
            Pause(1000);

            pointInputBoxBottonOk.PressMouseL();    // Нажимаем на кнопку Ок
            Pause(1500);

        }



        ///// <summary>
        ///// проверяем, находимся ли мы в диалоге
        ///// </summary>
        //public bool isDialog()
        //{
        //    return (pointDialog1.isColor() && pointDialog2.isColor());
        //}

        /// <summary>
        /// нажимаем на кнопку Ок в диалоге указанное количество раз
        /// </summary>
        /// <param name="number">количество нажатий</param>
        public void PressOkButton(int number)
        {
            for (int j = 1; j <= number; j++)
            {
//                ButtonOkDialog.DoubleClickL();    // Нажимаем на Ok в диалоге
                ButtonOkDialog.PressMouseL();       // Нажимаем на Ok в диалоге
                Pause(1500);
            }
        }

        /// <summary>
        /// нажать указанную строку в диалоге. Отсчет с низу вверх
        /// </summary>
        /// <param name="number"></param>
        public abstract void PressStringDialog(int number);

    }
}