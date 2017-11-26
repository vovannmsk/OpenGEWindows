﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;


namespace States
{
    public class StateGTI73 : IState
    {
        private botWindow botwindow;
        private ServerInterface server;
        //private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;
        private int numberOfEquipvent;

        public StateGTI73()
        {

        }

        public StateGTI73(botWindow botwindow, int numberOfEquipvent)   //, GotoTrade gototrade)
        {
            this.numberOfEquipvent = numberOfEquipvent;
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            //this.town = server.getTown();
            this.tekStateInt = 73;
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
            if (!server.isGoodChipWeapon())
            {
                server.PressButtonEnchance();
                botwindow.Pause(2000);
            }
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
            return server.isGoodChipWeapon();         //если получился хороший чип, то идём далее 
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGTI80(botwindow, this.numberOfEquipvent);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            return new StateGTI73(botwindow, this.numberOfEquipvent);
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