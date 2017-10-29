﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateGT111 : State
    {
        private botWindow botwindow;
        private ServerInterface server;
        private Town town;
        private ServerFactory serverFactory;
        //        GotoTrade gototrade;
        private int tekStateInt;

        public StateGT111()
        {

        }

        public StateGT111(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();
            //            this.gototrade = gototrade;
            this.tekStateInt = 11;
        }

        /// <summary>
        /// задаем метод Equals для данного объекта для получения возможности сравнения объектов State
        /// </summary>
        /// <param name="other"> объект для сравнения </param>
        /// <returns> true, если номера состояний объектов равны </returns>
        public bool Equals(State other)
        {
            bool result = false;
            if (!(other == null))            //если other не null, то проверяем на равенство
                if (other.getTekStateInt() == 1)         //27.04.17
                {
                    if (this.getTekStateInt() == other.getTekStateInt()) result = true;
                }
                else   //27.04.17
                {
                    if (this.getTekStateInt() >= other.getTekStateInt()) result = true;  //27.04.17
                }
            return result;
        }

        /// <summary>
        /// геттер, возвращает текущее состояние
        /// </summary>
        /// <returns></returns>
        public State getTekState()
        {
            return this;
        }

        /// <summary>
        /// метод осуществляет действия для перехода в следующее состояние
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            server.Botton_Sell();             // Нажимаем на кнопку Sell
            botwindow.Pause(1500);
            server.Button_Close();            // Нажимаем на кнопку Close
            //botwindow.Pause(1500);            пауза есть в самом town.ExitFromTrader
            town.ExitFromTrader();               // дополнительные нажатия при выходе из магазина
            botwindow.PressMouseR(200, 570);     //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown
            botwindow.Pause(2000);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
            //botwindow.PressEscThreeTimes();
            //botwindow.Pause(500);
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию 
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()
        {
            return ((server.isTown()) || (server.isTown_2()));   //GT111   проверка по двум стойкам
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT12(botwindow);  //, gototrade);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public State StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            if (!botwindow.isHwnd()) return new StateGT28(botwindow);  //последнее состояние движка, чтобы движок сразу тормознулся
            if (server.isLogout())
            {
                return new StateGT15(botwindow);  //коннект и далее
            }
            else
            {
                return this;
            }
        }

        public int getTekStateInt()
        {
            return this.tekStateInt;
        }
    }
}
