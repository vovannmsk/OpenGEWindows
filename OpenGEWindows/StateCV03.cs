using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateCV03 : State
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


        public StateCV03()
        {

        }

        public StateCV03(botWindow botwindow, botMerchant dealer)
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


            tekStateInt = 3;
        }

        public StateCV03(botWindow botwindow, botWindow botwindowDealer)
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


            tekStateInt = 3;
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
        /// метод осуществляет действия для перехода  в следующее состояние
        /// </summary>
        public void run()                
        {
            //Нажать согласие на торговлю у торговца, положить фесо, нажать "ок" и "обмен"
            //делаем окно торговца активным
            botwindowDealer.ReOpenWindow();
            botwindowDealer.Pause(500);

            //// наживаем Yes
            //// открываем карман (инвентарь)
            //// открываем закладку кармана, там где фесо
            //// перетаскиваем фесо
            //// нажимаем Ок для подтверждения передаваемой суммы фесо
            //// нажимаем ок и обмен
            dealer.ChangeVisTrader1();
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход из состояния GT03 в GT03
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
            return true;
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateCV04(botwindow, dealer);
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
