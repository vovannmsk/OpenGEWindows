using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;

namespace States
{
    public class StateGT03 : IState
    {
        private botWindow botwindow;
        private Server server;
        private Town town;
        private ServerFactory serverFactory;
        private int tekStateInt;

        public StateGT03()
        {

        }

        public StateGT03(botWindow botwindow)  //, GotoTrade gototrade)
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();
//            this.gototrade = gototrade;
            this.tekStateInt = 3;
        }

        /// <summary>
        /// задаем метод Equals для данного объекта для получения возможности сравнения объектов State
        /// </summary>
        /// <param name="other"> объект для сравнения </param>
        /// <returns> true, если номера состояний объектов равны </returns>
        public bool Equals(IState other)
        {
            //bool result = false;
            //if (!(other == null))            //если other не null, то проверяем на равенство
            //    if (this.getTekStateInt() == other.getTekStateInt()) result = true;
            //return result;
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
        /// метод осуществляет действия для перехода из в следующее состояние
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            server.WriteToLogFileBH("03");
            // ========================== убирает все лишние окна с экрана =================================
            botwindow.PressEscThreeTimes();
            botwindow.Pause(1000);

            if (server.isToken()) server.TokenClose(); //сделано для Европы. Закрываем окно с подарочными токенами
            botwindow.Pause(1000);

            // ================= выбираем главным среднего персонажа (для унификации) =================================
            botwindow.SecondHero();
            botwindow.Pause(1500);

            // ============= Удаляем камеру на максимальную высоту =================================================================
            town.MaxHeight();
            botwindow.Pause(1000);


            // ================= открывает городской телепорт (ALT + F3) =================================
            server.OpenTownTeleportForState();                                                                                  
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
        /// проверяет, получилось ли перейти к состоянию GT02
        /// </summary>
        /// <returns> true, если получилось перейти к состоянию GT02 </returns>
        public bool isAllCool()          // получилось ли перейти к следующему состоянию. true, если получилось
        {
            return town.isOpenTownTeleport();
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateGT04(botwindow); //, gototrade);
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
