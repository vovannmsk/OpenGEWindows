﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateGT07 : State
    {
        private botWindow botwindow;
        private ServerInterface server;
        private Town town;
        private ServerFactory serverFactory;
        //        GotoTrade gototrade;
        private int tekStateInt;

        public StateGT07()
        {

        }

        public StateGT07(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();
            //            this.gototrade = gototrade;
            this.tekStateInt = 7;
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
        /// метод осуществляет действия для перехода из состояния GT07 в GT08
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            ////тыкаем в другого торговца (который стоит рядом с нужным нам)
            town.GoToTraderMap();

            ////тыкаем "Move"
            town.ClickMoveMap();

            // закрываем все окна
            botwindow.PressEscThreeTimes();

            //время, чтобы добежать до нужного торговца (разное время для разных городов) 
            town.PauseToTrader();

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
        public bool isAllCool()          // получилось ли перейти к следующему состоянию. true, если получилось
        {
            return !town.isOpenMap();    //если нет меню Alt+Z, то значит добежали до торговца
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT08(botwindow);  //, gototrade);
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
                return new StateGT07(botwindow);  //, gototrade);    //состояние из которого осуществляется End Programm
            }
        }

        /// <summary>
        /// геттер. возвращает номер текущего состояния
        /// </summary>
        /// <returns> номер состояния </returns>
        public int getTekStateInt()
        {
            return this.tekStateInt;
        }
    }
}
