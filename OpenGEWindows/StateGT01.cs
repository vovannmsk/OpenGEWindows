using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateGT01 : State
    {
        private botWindow botwindow;
        private ServerInterface server;                 
        private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;


        public StateGT01()
        {

        }

        public StateGT01(botWindow botwindow)   //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();
            this.tekStateInt = 1;
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
                {
                    if (this.tekStateInt == other.getTekStateInt()) result = true;
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
        /// метод осуществляет действия для перехода из состояния GT01 в GT02
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            // ================= убирает все лишние окна с экрана =================================
            botwindow.PressEscThreeTimes();
            botwindow.Pause(500);
            //================ переход в тот город, где надо продаться (переход по Alt+W) =================================
            server.TeleportToTownAltW();            //метод без ветвлений и циклов

            //ожидание загрузки города
            int counter = 0;
            while (((!server.isTown()) && (!server.isTown_2())) && (counter < 30))                  
            { botwindow.Pause(1000); counter++; }

            botwindow.PressEscThreeTimes(); //29.04.17
            botwindow.Pause(500);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход из состояния GT01 в GT02
        /// </summary>
        public void elseRun()            
        {
            botwindow.PressEscThreeTimes();
            botwindow.Pause(500);
        }

        /// <summary>
        /// проверяет, получилось ли перейти к состоянию GT02
        /// </summary>
        /// <returns> true, если получилось перейти к состоянию GT02 </returns>
        public bool isAllCool()          // получилось ли перейти к следующему состоянию. true, если получилось
        {
            return  ( (server.isTown()) || (server.isTown_2()) );   //GT1   проверка по двум стойкам
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        { 
            return new StateGT03(botwindow);  //, gototrade);
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
