//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OpenGEWindows
//{
//    public class GreenButton : ButtonColor
//    {
//        private botWindow botwindow;
//        private ServerInterface server;                 //замена вместо bottown и bottrade
//        private Town town;
//        private ServerFactory serverFactory;
        

//        /// <summary>
//        /// конструктор
//        /// </summary>
//        /// <param name="botwindow"></param>
//        public GreenButton(botWindow botwindow)
//        {
//            this.botwindow = botwindow;

//            this.serverFactory = new ServerFactory(botwindow);
//            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
//            this.town = server.getTown();
//        }

//        ///// <summary>
//        ///// метод переводит бота из состояния логаут в состояние работы
//        ///// </summary>
//        //private void RecoveryOneWindow()
//        //{
//        //    int i;
//        //    //=================== выполняем коннект с вводом логина и пароля  ============================
//        //    bool bb = botwindow.Connect();

//        //    if (bb)   // если получилось войти, то
//        //    {
//        //        i = 0;
//        //        while ((!server.isBarack()) & (i < 50))         //ожидание загрузки казармы
//        //        { botwindow.Pause(500); i++; }

//        //        //============ выбор персонажей  ===========
//        //        botwindow.TeamSelection();
//        //        botwindow.Pause(500);

//        //        //============ выбор канала ===========
//        //        botwindow.SelectChannel();
//        //        botwindow.Pause(500);

//        //        //============ выход в город  ===========
//        //        botwindow.NewPlace();                //начинаем в ребольдо  

//        //        i = 0;
//        //        while ((!server.isTown()) & (i < 50))        //ожидание загрузки города
//        //        { botwindow.Pause(500); i++; }
                
//        //        //botwindow.Pause(20000);                //ожидание загрузки города

//        //        botwindow.PressEscThreeTimes();
//        //        botwindow.Pause(1000);

//        //        botwindow.Cure();
//        //        botwindow.Pause(1000);

//        //        botwindow.Teleport(1);                 // телепорт на работу   

//        //        i = 0;
//        //        while ((!server.isWork()) & (i < 30))         //ожидание загрузки места работы
//        //        { botwindow.Pause(500); i++; }

//        //        botwindow.PressMitridat();            //пьем митридат

//        //        botwindow.ClickSpace();     // К бою!!!!!!!! 
//        //        botwindow.Pause(1000);

//        //        server.ActivePet();     // активируем пета 
//        //        botwindow.Pause(1000);

//        //        botwindow.PressEscThreeTimes();  //убираем лишние окна
//        //        botwindow.Pause(1000);

//        //        botwindow.RasstanovkaOneWindow(); // расставляем треугольником    
//        //        botwindow.Pause(1000);
//        //    }
//        //}

//        /// <summary>
//        /// метод делает много проверок на наличие проблем у ботов и решает их
//        /// </summary>
//        public void run()
//        {
//            if (server.isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
//            {
//                botwindow.ReOpenWindow();
//                botwindow.Pause(1000);
//                if (server.isLogout())
//                {
//                    botwindow.StateRecovery();   
//                }
//                else
//                {
//                    if (server.isSale2())         //если зависли в магазине на любой закладке
//                    {
//                        botwindow.StateExitFromShop();            //выход из магазина
//                    }
//                    else
//                    {
//                        if (botwindow.isKillHero())                  // если убиты один или несколько персов   
//                        {
//                            botwindow.CureOneWindow2();              // сделать End Programm
//                            botwindow.Pause(2000);
//                            botwindow.StateGotoWork();               // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
//                        }
//                        else
//                        {
//                            if (server.isBarack())                  //если стоят в бараке     
//                            {
//                                botwindow.StateExitFromBarack();
//                            }
//                            else
//                            {
//                                //=========================== если переполнение ==============================
//                                if (server.isBoxOverflow())           // если карман переполнился
//                                {
//                                    botwindow.StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
//                                    botwindow.Pause(2000);
//                                    botwindow.StateGotoWork();                                           // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
//                                }
//                                else
//                                {
//                                    //================== если в городе ========================================
//                                    if (server.isTown())          //если стоят в городе               //**   было istown2()
//                                    {
//                                        botwindow.StateExitFromTown();
//                                        botwindow.PressEscThreeTimes();
//                                        botwindow.Pause(2000);
//                                        botwindow.StateGotoWork();                                    // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
//                                    }
//                                    else
//                                    {
//                                        if (server.isSale())                // если застряли в магазине на странице входа
//                                        {
//                                            botwindow.StateExitFromShop2();
//                                        }
//                                        else
//                                        {
//                                            botwindow.PressMitridat();
//                                        }

//                                    } //else isTown2()
//                                } //else isBoxOverflow()
//                            } //else isBarack()
//                        } // else isKillHero()
//                    } // else isSale2()
//                } //else  isLogout()
//            } //if  Active_or_not

//        }
//    }
//}
