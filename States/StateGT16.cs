using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;


namespace States
{
    public class StateGT16 : IState
    {
        private botWindow botwindow;
        private Server server;
        private ServerFactory serverFactory;
        private int tekStateInt;

        public StateGT16()
        {

        }

        public StateGT16(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.tekStateInt = 16;
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
            //============ выбор персонажей  ===========
            //botwindow.TeamSelection();
            server.TeamSelection();
            botwindow.Pause(500);

            //============ выбор канала ===========
            botwindow.SelectChannel();
            botwindow.Pause(500);

            //============ выход в город  ===========
            botwindow.NewPlace();                //начинаем в ребольдо  

            botwindow.ToMoveMouse();             //убираем мышку в сторону, чтобы она не загораживала нужную точку для isTown

            botwindow.Pause(2000);
            int i = 0;
            while (i < 50)      // ожидание загрузки города, проверка по двум стойкам
            { 
                botwindow.Pause(500); 
                i++;
                if (server.isTown())  break;    // проверяем успешный переход в город, проверка по ружью и дробовику
            }
            botwindow.Pause(7000);       //поставил по Колиной просьбе

            botwindow.PressEscThreeTimes();
            botwindow.Pause(1000);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
            ///???
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию 
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()
        {
//            return ((server.isTown()) || (server.isTown_2()));   //GT16   проверка по двум стойкам
            return !server.isBarack();
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT17(botwindow);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
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
