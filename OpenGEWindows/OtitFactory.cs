﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class OtitFactory
    {
        private Otit otit;
        private botWindow botwindow;

        public OtitFactory()
        { }

        public OtitFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        /// <summary>
        /// возвращает ноывй экземпляр класса указанного сервера
        /// </summary>
        /// <returns></returns>
        public Otit createOtit()
        {
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    otit = new OtitAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    otit = new OtitEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    otit = new OtitSing(botwindow);
                    break;
                default:
                    otit = new OtitSing(botwindow);
                    break;
            }
            return otit;
        }
    }
}
