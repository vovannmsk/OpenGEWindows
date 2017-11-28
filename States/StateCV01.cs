﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;

namespace States
{
    public class StateCV01 : IState
    {
        private botWindow botwindow;
        private ServerInterface server;
        private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;

        private botWindow botwindowDealer;
        private botMerchant dealer;
        private Town townDealer;
        private ServerInterface serverDealer;

        

        public StateCV01()
        {

        }

        public StateCV01(botWindow botwindow, botMerchant dealer) 
        {
            this.botwindow = botwindow;
            this.dealer = dealer;

            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();

            this.botwindowDealer = this.dealer.getBotWindow();
            this.serverFactory = new ServerFactory(this.botwindowDealer);
            this.serverDealer = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.townDealer = serverDealer.getTown();


            tekStateInt = 1;
        }

        public StateCV01(botWindow botwindow, botWindow botwindowDealer)
        {
            this.botwindow = botwindow;                 //бот
            this.botwindowDealer = botwindowDealer;     //торговец
            this.dealer = new botMerchant(botwindowDealer);  //делаем торговца
            //this.dealer = dealer;

            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();

            //this.botwindowDealer = this.dealer.getBotWindow();
            this.serverFactory = new ServerFactory(this.botwindowDealer);
            this.serverDealer = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.townDealer = serverDealer.getTown();


            tekStateInt = 1;
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
                if (this.getTekStateInt() == other.getTekStateInt()) result = true;
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
        /// метод осуществляет действия для перехода из состояния GT01 в GT02
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            botwindowDealer.ReOpenWindow();
            botwindowDealer.Pause(1000);                                 

            //=================== выполняем коннект с вводом логина и пароля  ============================
            bool result = botwindowDealer.Connect();

            if (result)   // если получилось войти, то
            {
                while (!serverDealer.isBarack())         //ожидание загрузки казармы
                { botwindowDealer.Pause(500); }

                //============ выбор персонажей  ===========
                server.TeamSelection();
                botwindowDealer.Pause(500);

                //============ выбор канала ===========
                botwindowDealer.SelectChannel(4);            //идем на 4 канал
                botwindowDealer.Pause(500);

                //============ выход в город  ===========
                botwindowDealer.NewPlace();                //начинаем в Ребольдо

                botwindow.ToMoveMouse();             //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown

                dealer.GoToChangePlace();            //  торговец следует на место передачи песо
            }
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход из состояния GT01 в GT02
        /// </summary>
        public void elseRun()
        {
            // сделать выгрузку окна

        }

        /// <summary>
        /// проверяет, получилось ли перейти к состоянию GT02
        /// </summary>
        /// <returns> true, если получилось перейти к состоянию GT02 </returns>
        public bool isAllCool()          // получилось ли перейти к следующему состоянию. true, если получилось
        {
            return true;
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateCV02(botwindow, dealer);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            return this;
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
