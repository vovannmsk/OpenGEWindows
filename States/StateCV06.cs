using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;

namespace States
{
    public class StateCV06 : IState
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


        public StateCV06()
        {

        }

        public StateCV06(botWindow botwindow, botMerchant dealer)
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


            tekStateInt = 6;
        }

        public StateCV06(botWindow botwindow, botWindow botwindowDealer)
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


            tekStateInt = 6;
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
        /// метод осуществляет действия для перехода  в следующее состояние
        /// </summary>
        public void run()
        {
            //продаём 10 ВК в фесо шопе, чтобы было что отдать следующему боту

            //делаем окно торговца активным
            botwindowDealer.ReOpenWindow();
            botwindowDealer.Pause(500);

            //открываем магазин фесо
            botwindowDealer.OpenFesoShop();

            //нажимаем на закладку sell
            botwindowDealer.OpenBookmarkSell();

            // продаем 10 ВК в фесо шопе
            botwindowDealer.SellGrowthStone10();

            //убираем лишнее с экрана торговца
            botwindowDealer.PressEscThreeTimes();
            botwindowDealer.Pause(500);
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход из состояния GT06 в GT06
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
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            return new StateCV07(botwindow, dealer);
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
