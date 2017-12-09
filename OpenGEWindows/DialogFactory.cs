﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class DialogFactory
    {
        private Dialog dialog;
        private botWindow botwindow;

        public DialogFactory()
        { }

        public DialogFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        /// <summary>
        /// возвращает ноывй экземпляр класса указанного сервера
        /// </summary>
        /// <returns></returns>
        public Dialog createDialog()
        {
            dialog = new DialogSing(botwindow);
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    dialog = new DialogAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    dialog = new DialogEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    dialog = new DialogSing(botwindow);
                    break;
                default:
                    dialog = new DialogSing(botwindow);
                    break;
            }
            return dialog;
        }
    }
}