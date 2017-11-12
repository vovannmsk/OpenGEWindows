﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;


namespace States
{
    public class StateGT50 : IState
    {
        private botWindow botwindow;
        private ServerInterface server;
        private ServerFactory serverFactory;
        private Town town;

        private int tekStateInt;

        public StateGT50()
        {

        }

        public StateGT50(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();
            //            this.gototrade = gototrade;
            this.tekStateInt = 50;
        }

        /// <summary>
        /// задаем метод Equals для данного объекта для получения возможности сравнения объектов State
        /// </summary>
        /// <param name="other"> объект для сравнения </param>
        /// <returns> true, если номера состояний объектов равны </returns>
        public bool Equals(IState other)
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
        public IState getTekState()
        {
            return this;
        }

        /// <summary>
        /// метод осуществляет действия для перехода в следующее состояние
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            if (!server.isKillHero())             //если никто не убит, то можно призывать пета
                server.buttonSummonPet();

            //botwindow.PressMouseL(569, 375);  //Click Pet
            //botwindow.PressMouseL(569, 375);
            //botwindow.Pause(500);
            //botwindow.PressMouseL(408, 360);  //Click кнопку "Summon"
            //botwindow.PressMouseL(408, 360);
            //botwindow.Pause(1000);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию 
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()
        {
            bool r1 = server.isSummonPet();
            bool r2 = !server.isKillHero();
            return (server.isSummonPet() && !server.isKillHero());     //пет призван и никто не убит
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT51(botwindow);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            if (!botwindow.isHwnd()) return new StateGT58(botwindow);  //последнее состояние движка, чтобы движок сразу тормознулся
            if (server.isLogout())
            {
                return new StateGT42(botwindow);  //коннект и далее
            }
            else
            {
                if (!server.isKillHero()) { return this; }          //если никто не убит, то остаемся в этом же состоянии и пытаемся призвать пета вновь
                else { return new StateGT58(botwindow); }           // если кто-то из героев убит присваиваем конечное состояние, а именно GT01, чтобы на следующем круге реанимировать бота
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