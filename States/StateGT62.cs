using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;


namespace States
{
    public class StateGT62 : IState
    {
        private botWindow botwindow;
        private ServerInterface serverDealer;
        private ServerFactory serverFactory;
        private int tekStateInt;
        private botWindow dealer;

        public StateGT62()
        {

        }

        public StateGT62(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.dealer = new botWindow(20);   // здесь уникальные методы, присущие только торговцу
            this.serverFactory = new ServerFactory(dealer);
            this.serverDealer = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)

            this.tekStateInt = 62;
        }


        /// <summary>
        /// метод осуществляет действия для перехода в следующее состояние
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            //============ выбор персонажей  ===========
            serverDealer.TeamSelection();
            dealer.Pause(500);

            //============ выбор канала ===========
            dealer.SelectChannel(3);            //идем на 3 канал
            dealer.Pause(500);

            //============ выход в город  ===========
            dealer.NewPlace();                //начинаем в Ребольдо

            dealer.ToMoveMouse();             //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown

            dealer.Pause(2000);
            int i = 0;
            while (i < 50)      // ожидание загрузки города, проверка по двум видам оружия
            {
                dealer.Pause(500);
                i++;
                if ((serverDealer.isTown()) || (serverDealer.isTown_2())) break;    // проверяем успешный переход в город, проверка по ружью и дробовику
            }
            dealer.Pause(7000);       //поставил по Колиной просьбе
            dealer.PressEscThreeTimes();
            dealer.Pause(1000);
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
            return !serverDealer.isBarack();    //проверяем, что уже не казармы
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT63(botwindow);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            return new StateGT62(botwindow);
        }

        #region стандартные служебные методы для паттерна Состояния

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
        /// геттер. возвращает номер текущего состояния
        /// </summary>
        /// <returns> номер состояния </returns>
        public int getTekStateInt()
        {
            return this.tekStateInt;
        }

        #endregion
    }
}
