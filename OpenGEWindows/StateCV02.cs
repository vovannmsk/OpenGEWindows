using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateCV02 : State
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


        public StateCV02()
        {

        }

        public StateCV02(botWindow botwindow, botMerchant dealer)
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

            tekStateInt = 2;
        }

        public StateCV02(botWindow botwindow, botWindow botwindowDealer)
        {
            this.botwindow = botwindow;                 //бот
            this.botwindowDealer = botwindowDealer;     //торговец
            this.dealer = new botMerchant(botwindowDealer);  //делаем торговца

            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();

            this.serverFactory = new ServerFactory(this.botwindowDealer);
            this.serverDealer = serverFactory.createServer();                // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.townDealer = serverDealer.getTown();

            tekStateInt = 2;
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
                if (this.getTekStateInt() == other.getTekStateInt()) result = true;
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
            //переводит бота в Юстиар, подходим к торговцу и предлагаем ему личную сделку

            botwindow.ReOpenWindow();          //делаем окно активным
            botwindow.Pause(1000);

            // перелет бота в город для передачи песо
            botwindow.TeleportWA(4);  //юстиар (там только один канал, поэтому все боты прилетят в одно место)

            ////идем на место передачи песо
            ////жмем правой на торговце
            ////жмем левой  на пункт "Personal Trade"
            botwindow.ChangeVis1();
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход из состояния GT02 в GT02
        /// </summary>
        public void elseRun()
        {
            

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
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateCV03(botwindow, dealer);
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public State StatePrev()         // возвращает запасное состояние, если переход не осуществился
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
