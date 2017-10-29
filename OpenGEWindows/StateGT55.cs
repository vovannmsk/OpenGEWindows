using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateGT55 : State
    {
        private botWindow botwindow;
        private ServerInterface server;
        //private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;

        public StateGT55()
        {

        }

        public StateGT55(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            //this.town = server.getTown();
            this.tekStateInt = 55;
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
            // ============= нажимаем на первого перса (обязательно на точку ниже открытой карты)
            botwindow.FirstHero();

            botwindow.PressMouseL(botwindow.getTriangleX()[1], botwindow.getTriangleY()[1]);

            // ============= нажимаем на третьего перса (обязательно на точку ниже открытой карты)
            botwindow.ThirdHero();
            botwindow.PressMouseL(botwindow.getTriangleX()[3], botwindow.getTriangleY()[3]);

            // ============= нажимаем на второго перса (обязательно на точку ниже открытой карты)
            botwindow.SecondHero();
            botwindow.PressMouseL(botwindow.getTriangleX()[2], botwindow.getTriangleY()[2]);

            // ============= закрыть карту через верхнее меню
            botwindow.CloseMap();
            botwindow.Pause(1500);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
            botwindow.PressEscThreeTimes();
            botwindow.Pause(500);
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию 
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()
        {
            return true;                                                                                //сделать проверку, что карта закрыта
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT56(botwindow); 
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public State StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            if (!botwindow.isHwnd()) return new StateGT58(botwindow);  //последнее состояние движка, чтобы движок сразу тормознулся
            if (server.isLogout())
            {
                return new StateGT42(botwindow);  //коннект и далее
            }
            else
            {
                return new StateGT54(botwindow);
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
