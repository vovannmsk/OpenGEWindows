﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class GoldenEggFactory
    {
        private GoldenEgg goldenEgg;
        private botWindow botwindow;

        public GoldenEggFactory()
        { }

        public GoldenEggFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        /// <summary>
        /// возвращает ноывй экземпляр класса указанного сервера
        /// </summary>
        /// <returns></returns>
        public GoldenEgg create()
        {
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    goldenEgg = new GoldenEggAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    goldenEgg = new GoldenEggEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    goldenEgg = new GoldenEggSing(botwindow);
                    break;
                default:
                    goldenEgg = new GoldenEggSing(botwindow);
                    break;
            }
            return goldenEgg;
        }
    }
}