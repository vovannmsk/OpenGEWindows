﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;


namespace States
{
    public class StateGT34 : IState
    {
        private botWindow botwindow;
        private ServerInterface server;
        //private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;

        public StateGT34()
        {

        }

        public StateGT34(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            //this.town = server.getTown();
            this.tekStateInt = 34;
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
            server.CreateOfTeam();

            //============ выбор канала ===========
            botwindow.SelectChannel();
            botwindow.Pause(500);

            //============ выход в город  ===========
            botwindow.NewPlace();                //начинаем в ребольдо  

        //    botwindow.PressMouseR(200, 570);     //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown

            botwindow.Pause(15000);
            //int i = 0;
            //while ((!server.isTown()) && (i < 50))      //GT34 ожидание загрузки города
            //{ botwindow.Pause(500); i++; }

            botwindow.PressEscThreeTimes();
            botwindow.Pause(1000);
            // в итоге мы попадаем в Стартонию

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
            return true;                                                                                //считаем, что осечек не будет на этом этапе, и мы 100% переёдем к следующему пункту
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT35(botwindow);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            return new StateGT30(botwindow);                                                                                  //может другое состояние????????
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