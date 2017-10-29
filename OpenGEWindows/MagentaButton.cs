//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OpenGEWindows
//{
//    public class MagentaButton : ButtonColor
//    {
//        private botMerchant dealer;
//        private botWindow botwindow;
//        private ServerInterface server;
//        private ServerFactory serverFactory;
//        private Town town;

//        /// <summary>
//        /// конструктор
//        /// </summary>
//        public MagentaButton()
//        {
//        }

//        /// <summary>
//        /// конструктор
//        /// </summary>
//        /// <param name="dealer"> торговец </param>
//        /// <param name="botwindow"> бот </param>
//        public MagentaButton(botMerchant dealer, botWindow botwindow)
//        {
//            this.dealer = dealer;
//            this.botwindow = botwindow;

//            this.serverFactory = new ServerFactory(botwindow);
//            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
//            this.town = server.getTown();
//        }

//        /// <summary>
//        /// переводит бота в Юстиар, подходим к торговцу и предлагаем ему личную сделку
//        /// </summary>
//        public void Magenta_button1()
//        {
//            botwindow.ReOpenWindow();          //делаем окно активным
//            botwindow.Pause(1000);

//            // перелет бота в город для передачи песо
//            botwindow.TeleportWA(4);  //юстиар (там только один канал, поэтому все боты прилетят в одно место)

//            ////идем на место передачи песо
//            ////жмем правой на торговце
//            ////жмем левой  на пункт "Personal Trade"
//            botwindow.ChangeVis1();
//        }

//        /// <summary>
//        /// кладем песо со стороны бота и закрываем сделку
//        /// </summary>
//        public void Magenta_button2()
//        {
//            botwindow.ReOpenWindow();          //делаем окно бота активным
//            botwindow.Pause(1000);

//            //// открываем сундук
//            //// открываем закладку кармана, там где фесо
//            //// перетаскиваем песо
//            //// нажимаем Ок для подтверждения передаваемой суммы песо
//            //// нажимаем ок и обмен
//            botwindow.ChangeVis2();
//        }

//        /// <summary>
//        /// покупаем еду для пета и закрываем окно бота
//        /// </summary>
//        public void Magenta_button3()
//        {

//            botwindow.ReOpenWindow();          //делаем окно активным
//            botwindow.Pause(1000);

//            // открываем фесо шоп
//            botwindow.OpenFesoShop();


//            // покупаем 400 еды в фесо шопе
//            Buy400PetFood();

//            // обнуляем переменную и записываем ее в файл
//            botwindow.setNeedToChange(0);     //делаем параметр NeedToChange равным нулю, т.е. передавать не надо
//            botwindow.NeedToChangeToFile();

//            server.GoToEnd();              //выгружаем окно с ботом (поправка на сервер)
//        }

//        /// <summary>
//        /// купить 400 еды в фесо шопе
//        /// </summary>
//        public void Buy400PetFood()
//        {
//            // тыкаем два раза в стрелочку вверх
//            botwindow.PressMouseL(375, 327);
//            botwindow.Pause(500);
//            botwindow.PressMouseL(380 - 5, 327);
//            botwindow.Pause(500);

//            // жмем кнопку купить

//            botwindow.PressMouseL(725, 620);
//            botwindow.Pause(500);

//            //нажимаем кнопку Close
//            botwindow.PressMouseL(848, 620);
//            botwindow.Pause(1500);
//        }

//        /// <summary>
//        /// основной метод класса, реализующий передачу песо от ботов к торговцу
//        /// </summary>
//        public void run()
//        {
//            if (server.isActive())                    // смотрим, нужно ли использовать это окно
//            {
//                if (botwindow.getNeedToChange() == 1)         // проверяем, нужно ли передавать это окно (изначально подчитываем это из файла)
//                {
//                    //бот подходит к месту передачи песо
//                    Magenta_button1();

//                    //положить вещи со стороны торговца
//                    dealer.ToTradePartOne();

//                    // положить вещи со стороны бота и завершить сделку
//                    Magenta_button2();

//                    // купить еду за фесо и закрыть окно с ботом
//                    Magenta_button3();

//                    //убрать всё лишнее с экрана торговца (нужная процедура) и продать ВК для следующего бота
//                    dealer.ToTradePartTwo();
//                }
//            }
//        }
//    }
//}