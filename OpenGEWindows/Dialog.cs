﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace OpenGEWindows
{
    public abstract class Dialog : Server2
    {
        protected iPoint ButtonOkDialog;

        

        // ============  методы  ========================

        /// <summary>
        /// нажимаем на кнопку Ок в диалоге указанное количество раз
        /// </summary>
        /// <param name="number">количество нажатий</param>
        public void PressOkButton(int number)
        {
            for (int j = 1; j <= number; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
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