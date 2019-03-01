using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;

namespace States
{
    /// <summary>
    /// класс хранит движки состояний для выполнения основных последовательностей действий (переход к продаже и обратно, продажа бота, восстановление бота из логаута и проч.)
    /// </summary>
    public class DriversOfState
    {
        private botWindow botwindow;
        private Server server;
        private Otit otit;
//        private Check check;

        public DriversOfState()
        { 
            
        }

        public DriversOfState(int numberOfWindow)
        {
            this.botwindow = new botWindow(numberOfWindow);
            ServerFactory serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            OtitFactory otitFactory = new OtitFactory(botwindow);
            this.otit = otitFactory.createOtit();
        }




        #region движки для запуска перехода по состояниям



        #region Гилльдия Охотников


        /// <summary>
        /// перевод из состояния 01 (в БХ) в состояние 03 (в городе продажи). 
        /// </summary>
        public void StateGotoTradeStep1BH()
        {
            StateDriverRun(new StateGT201(botwindow), new StateGT203(botwindow));
            server.WriteToLogFileBH("new StateGT201(botwindow), new StateGT203(botwindow)");
        }

        /// <summary>
        /// перевод из состояния 03 (в городе продажи) в состояние 09 (в магазине). 
        /// </summary>
        public void StateGotoTradeStep2BH()
        {
            StateDriverRun(new StateGT03(botwindow), new StateGT09(botwindow));
            server.WriteToLogFileBH("new StateGT03(botwindow), new StateGT09(botwindow)");
        }

        /// <summary>
        /// перевод из состояния 09 (в магазине) в состояние 10 (в магазине на закладке BUY или SELL). 
        /// </summary>
        public void StateGotoTradeStep3BH()
        {
            StateDriverRun(new StateGT09(botwindow), new StateGT10(botwindow));
            server.WriteToLogFileBH("new StateGT09(botwindow), new StateGT10(botwindow)");
        }

        /// <summary>
        /// перевод из состояния 10 (в магазине на закладке BUY) в состояние 11 (закладка SELL). 
        /// </summary>
        public void StateGotoTradeStep4BH()
        {
            StateDriverRun(new StateGT10(botwindow), new StateGT11(botwindow));
            server.WriteToLogFileBH("StateDriverRun(new StateGT10(botwindow), new StateGT11(botwindow));");
        }

        /// <summary>
        /// перевод из состояния 211 (в магазине на закладке BUY) в состояние 214 (логаут). 
        /// </summary>
        public void StateGotoTradeStep5BH()
        {
//            StateDriverRun(new StateGT11(botwindow), new StateGT14(botwindow));
            StateDriverRun(new StateGT211(botwindow), new StateGT214(botwindow));
            server.WriteToLogFileBH("StateDriverRun(new StateGT211(botwindow), new StateGT214(botwindow));");
        }

        /// <summary>
        /// перевод из состояния 108 (в миссии) в состояние 129 (бой) 
        /// </summary>
        public void StateFromMissionToFightBH()
        {
            server.WriteToLogFileBH("Движок 108-129 mission-->Fight");
            StateDriverRun(new StateGT108(botwindow), new StateGT129(botwindow));   // mission-->Fight
        }

        /// <summary>
        /// перевод из состояния 129 (после победы в бою) в состояние 130 (BH) 
        /// </summary>
        public void StateFromMissionToBH()
        {
            server.WriteToLogFileBH("Движок 129-130 mission --> BH");
            StateDriverRun(new StateGT129(botwindow), new StateGT130(botwindow));   // mission (Win)-->BH   (отбежать в сторону и телепортнуться)
        }

        /// <summary>
        /// перевод из состояния 250 (после победы в бою) в состояние 251 (барак) 
        /// </summary>
        public void StateFromMissionToBarackBH()
        {
            server.WriteToLogFileBH("Движок 250-251 mission --> Barack");
            StateDriverRun(new StateGT250(botwindow), new StateGT251(botwindow));   // mission (Win)-->Barack   (вызываем системное меню и в барак)
        }


        /// <summary>
        /// перевод из состояния 100 (город) в состояние 101 (в Гильдии Охотников) 
        /// </summary>
        public void StateFromTownToBH()
        {
            server.WriteToLogFileBH("Движок 100-101 Town-->BH");
            StateDriverRun(new StateGT100(botwindow), new StateGT101(botwindow));   // Town-->BH
        }

        /// <summary>
        /// перевод из состояния 15 (логаут) в состояние 16 (Barack) 
        /// </summary>
        public void StateFromLogoutToBarackBH()
        {
            server.WriteToLogFileBH("Движок 215-216 Logout-->Barack");
            StateDriverRun(new StateGT215(botwindow), new StateGT216(botwindow));     // Logout-->Barack
        }

        /// <summary>
        /// перевод из состояния 216 (Barack) --> 217 (Town)
        /// </summary>
        public void StateFromBarackToTownBH()
        {
            server.WriteToLogFileBH("Движок 216-217 Barack-->Town");
            StateDriverRun(new StateGT216(botwindow), new StateGT217(botwindow));     // Barack-->Town
        }



      

        /// <summary>
        /// перевод из состояния 101 (BH) в состояние 102 (InfinityGate)
        /// </summary>
        public void StateFromBHToGateBH()
        {
            server.WriteToLogFileBH("Движок 101-102 BH-->Gate");
            //botwindow.setStatusOfAtk(0);           //обнулили статус атаки
            StateDriverRun(new StateGT101(botwindow), new StateGT102(botwindow));   // BH-->Gate
            

        }

        /// <summary>
        /// перевод из состояния 104 (ворота состояние 2) в состояние 108 (миссия) 
        /// </summary>
        public void StateFromGate2ToMissionBH()
        {
            server.WriteToLogFileBH("Движок 104-108  Gate --> Mission");
            StateDriverRun(new StateGT104(botwindow), new StateGT108(botwindow));   // Gate --> Mission
        }


        /// <summary>
        /// перевод из состояния 102 (InfinityGate) в состояние 108 (миссия) 
        /// </summary>
        public void StateFromGateToMissionBH()
        {
            server.WriteToLogFileBH("Движок 102-108  Gate --> Mission");
            StateDriverRun(new StateGT102(botwindow), new StateGT108(botwindow));   // Gate --> Mission


        }

        /// <summary>
        /// перевод из состояния 106 (состояние ворот 4) в состояние 107 (состояние ворот 5) 
        /// </summary>
        public void StateFromGate4ToGate5BH()
        {
            server.WriteToLogFileBH("Движок 106-107  Gate4 --> Gate5");
            StateDriverRun(new StateGT106(botwindow), new StateGT107(botwindow));   // Gate4 --> Gate5
        }

        /// <summary>
        /// перевод из состояния 107 (состояние ворот 5) в состояние 108 (BH) 
        /// </summary>
        public void StateFromGate5ToBH()
        {
            server.WriteToLogFileBH("Движок 107-108  Gate5 --> BH");
            StateDriverRun(new StateGT107(botwindow), new StateGT108(botwindow));   // Gate5 --> BH
        }


        #endregion





        /// <summary>
        ///                // коралл кнопка (алхимия)
        /// </summary>
        public void StateAlchemy()
        {
            botwindow.Pause(300);
            if (server.isAlchemy())
            {
                StateDriverRun(new StateGT92(botwindow), new StateGT93(botwindow));

            }
        }

        /// <summary>
        /// идем из состояния логаут до старого человека в Лос Толдосе
        /// </summary>
        public void StateGoldenEggFarm()
        {
            StateDriverRun(new StateGT74(this.botwindow), new StateGT75(this.botwindow));  // заходим на ферму
            StateDriverRun(new StateGT91(this.botwindow), new StateGT92(this.botwindow));  // открываем карту

            StateDriverRun(new StateGT85(this.botwindow), new StateGT86(this.botwindow));  // бегаем по кругу и собираем GoldenEggFruit
        }


        /// <summary>
        /// перевод из состояния 75 в состояние 90. Цель  - добыча отита. Исполнитель - барон
        /// </summary>
        public void StateOtitRunBaron()
        {
            StateDriverRun(new StateGT15(this.botwindow), new StateGT17(this.botwindow));  // переход из состояния "Логаут" в состояние "В городе" (Los Toldos)
            StateDriverRun(new StateGT75(this.botwindow), new StateGT82(this.botwindow));  // до выполнения задания на мертвых землях
            StateDriverRun(new StateGT15(this.botwindow), new StateGT17(this.botwindow));  // переход из состояния "Логаут" в состояние "В городе" (Los Toldos)
            StateDriverRun(new StateGT82(this.botwindow), new StateGT85(this.botwindow));  // получаем отит и логаут
        }

        /// <summary>
        /// идем из состояния логаут до старого человека в Лос Толдосе
        /// </summary>
        public void StateGotoOldMan ()
        {
            StateDriverRun(new StateGT15(this.botwindow), new StateGT18(this.botwindow));  // переход из состояния "Логаут" в состояние "Около Мамона" 
            StateDriverRun(new StateGT86(this.botwindow), new StateGT88(this.botwindow));  // Говорим с Мамоном и переходим в Лос Толдос 
            StateDriverRun(new StateGT75(this.botwindow), new StateGT751(this.botwindow));  // подбегаем к старому мужику
        }

        /// <summary>
        ///  Цель  - добыча отита. Исполнитель - не барон, но с отитовыми духами
        /// </summary>
        public void StateOtitRun2()
        {
            StateDriverRun(new StateGT751(this.botwindow), new StateGT81(this.botwindow));  // берем задание и выполняем его на мертвых землях
            if (!server.isKillHero())        //если никого не убили
            {
                StateDriverRun(new StateGT88(this.botwindow), new StateGT89(this.botwindow));  // отбегаем в сторону (на мертвых землях)
                StateDriverRun(new StateGT89(this.botwindow), new StateGT90(this.botwindow));  // летим к Мамону
                StateDriverRun(new StateGT86(this.botwindow), new StateGT88(this.botwindow));  // Говорим с Мамоном и переходим в Лос Толдос 
                StateDriverRun(new StateGT82(this.botwindow), new StateGT84(this.botwindow));  // получаем отит и остаёмся в городе (Лос Толдосе) около старого мужика
            }
            else         //если в процессе выполнения задания кто-то из персов был убит
            {
                otit.ChangeNumberOfRoute();  //сменить маршрут, чтобы в следующий раз не попасть в ту же ловушку
                StateDriverRun(new StateGT81(this.botwindow), new StateGT82(this.botwindow));  // отбегаем в сторону (на случай, если кто-то выжил)  и логаут
                StateDriverRun(new StateGT15(this.botwindow), new StateGT18(this.botwindow));  // переход из состояния "Логаут" в состояние "Около Мамона" 
                StateDriverRun(new StateGT86(this.botwindow), new StateGT88(this.botwindow));  // Говорим с Мамоном и переходим в Лос Толдос 
                StateDriverRun(new StateGT75(this.botwindow), new StateGT751(this.botwindow));  // подбегаем к старому мужику
            }
        }

        /// <summary>
        /// перевод из состояния 75 в состояние 90. Цель  - добыча отита. Исполнитель - не барон, но с отитовыми духами
        /// </summary>
        public void StateOtitRun()
        {
            StateDriverRun(new StateGT89(this.botwindow), new StateGT90(this.botwindow));  // летим из любого города к Мамону (первая строка в телепортах)
            StateDriverRun(new StateGT86(this.botwindow), new StateGT88(this.botwindow));  // Говорим с Мамоном и переходим в Лос Толдос 
            StateDriverRun(new StateGT75(this.botwindow), new StateGT81(this.botwindow));  // берем задание и выполняем его на мертвых землях
            if (!server.isKillHero())        //если никого не убили
            {
                StateDriverRun(new StateGT88(this.botwindow), new StateGT89(this.botwindow));  // отбегаем в сторону (на мертвых землях)
                StateDriverRun(new StateGT89(this.botwindow), new StateGT90(this.botwindow));  // летим к Мамону
                StateDriverRun(new StateGT86(this.botwindow), new StateGT88(this.botwindow));  // Говорим с Мамоном и переходим в Лос Толдос 
                StateDriverRun(new StateGT82(this.botwindow), new StateGT84(this.botwindow));  // получаем отит и остаёмся в городе (Лос Толдосе)
            }
            else         //если в процессе выполнения задания кто-то из персов был убит
            {
                otit.ChangeNumberOfRoute();  //сменить маршрут, чтобы в следующий раз не попасть в ту же ловушку
                StateDriverRun(new StateGT81(this.botwindow), new StateGT82(this.botwindow));  // отбегаем в сторону (на случай, если кто-то выжил)  и логаут
                StateDriverRun(new StateGT15(this.botwindow), new StateGT17(this.botwindow));  // переход из состояния "Логаут" в состояние "В городе" 
                StateDriverRun(new StateGT90(this.botwindow), new StateGT91(this.botwindow));  // лечение и патроны в городе
            }
        }

        /// <summary>
        /// перевод из состояния 20 (пет не выпущен) в состояние 01 (на работе). Цель  - выпустить пета и расставить треугольником
        /// </summary>
        public void StateActivePet()
        {
            StateDriverRun(new StateGT20(botwindow), new StateGT01(botwindow));
//            StateDriverRun(new StateGT20(botwindow), new StateGT28(botwindow));
        }

        /// <summary>
        /// перевод из состояния 60 в состояние 80. Цель  - передача песо торговцу
        /// </summary>
        public void StateTransferVisChapter1()
        {
            botWindow dealer = new botWindow(20);  //торговец

            StateDriverRun(new StateGT60(dealer), new StateGT65(dealer));   // торговец из логаута в город  (канал 3) и далее на место передачи песо
        }

        /// <summary>
        /// перевод из состояния 60 в состояние 80. Цель  - передача песо торговцу
        /// </summary>
        public void StateTransferVis()
        {
            botWindow dealer = new botWindow(20);  //торговец

            StateDriverRun(new StateGT60(this.botwindow), new StateGT63(this.botwindow));   // бот из логаута в город  (канал 3)

            StateDriverRun(new StateGT68(this.botwindow), new StateGT69(this.botwindow));   // бот бежит на место торговли и предлагает торговлю торговцу
            StateDriverRun(new StateGT69(dealer), new StateGT70(dealer));                   // торговец перекладывает фесо и ок-обмен

            StateDriverRun(new StateGT70(this.botwindow), new StateGT72(this.botwindow));   // бот перекладывает песо, закрывает сделку, покупает еду и логаут
            StateDriverRun(new StateGT72(dealer), new StateGT73(dealer));                   // торговец закрывает все лишние окна с экрана
        }

        /// <summary>
        /// перевод из состояния 60 в состояние 80. Цель  - передача песо торговцу.  старый вариант
        /// </summary>
        public void StateTransferVis2()
        {
            Server server;
            ServerFactory serverFactory;
            serverFactory = new ServerFactory(this.botwindow);
            server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)

            if (server.isLogout())
            {
                StateDriverRun(new StateGT60(this.botwindow), new StateGT74(this.botwindow));
            }
        }

        /// <summary>
        /// перевод из состояния 60 () в состояние 67 (). Цель  - заточка оружия и брони на +6 у Иды в Ребольдо
        /// </summary>
        public void StateSharpening()
        {
            for (int numberOfEquipment = 1; numberOfEquipment <= 6; numberOfEquipment++)
            {
                StateDriverRun(new StateGTI60(botwindow, numberOfEquipment), new StateGTI67(botwindow, numberOfEquipment));
            }
        }

        /// <summary>
        /// перевод из состояния 70 () в состояние 80 (). Цель  - заточка оружия и брони на +6 у Иды в Ребольдо
        /// </summary>
        public void StateNintendo()
        {
            for (int numberOfEquipment = 1; numberOfEquipment <= 6; numberOfEquipment++)
            {
                StateDriverRun(new StateGTI70(botwindow, numberOfEquipment), new StateGTI80(botwindow, numberOfEquipment));
            }
        }

        /// <summary>
        /// перевод из состояния 151 (на работе) в состояние 170 (нет окна). Цель  - продажа после переполнения инвентаря в Катовии (снежка)
        /// </summary>
        public void StateGotoTradeKatovia()
        {
            botwindow.ReOpenWindow();
            StateDriverRun(new StateGT151(botwindow), new StateGT162(botwindow));
        } 

        /// <summary>
        /// перевод из состояния 01 (на работе) в состояние 14 (нет окна). Цель  - продажа после переполнения инвентаря
        /// </summary>
        public void StateGotoTrade()
        {
            botwindow.ReOpenWindow();
            StateDriverRun(new StateGT01(botwindow), new StateGT14(botwindow));
//            StateDriverRun(new StateGT01(botwindow), new StateGT12(botwindow));
        }

        /// <summary>
        /// перевод из состояния 14 (нет окна бота) в состояние 01 (на работе)
        /// </summary>
        public void StateGotoWork()
        {
            StateDriverRun(new StateGT14(botwindow), new StateGT01(botwindow)); 
//            StateDriverRun(new StateGT14(botwindow), new StateGT28(botwindow));
        }

        /// <summary>
        /// перевод из состояния 15 (логаут) в состояние 01 (на работе)
        /// </summary>
        public void StateRecovery()
        {
            StateDriverRun(new StateGT15(botwindow), new StateGT01(botwindow));
//            StateDriverRun(new StateGT15(botwindow), new StateGT28(botwindow));
        }

        /// <summary>
        /// перевод из состояния 14 (нет окна) в состояние 15 (логаут)              
        /// </summary>
        public void StateReOpen()
        {
            StateDriverRun(new StateGT14(botwindow), new StateGT15(botwindow));
            //StateDriverRun(new StateGT14(botwindow), new StateGT14(botwindow));
        }

        /// <summary>
        /// перевод из состояния 09 (в магазине) в состояние 12 (всё продано, в городе)                 // аква кнопка
        /// </summary>
        public void StateSelling()
        {

            botwindow.Pause(300);
            if (botwindow.getNomerTeleport() >= 100)
            {
                KatoviaMarketFactory marketFactory = new KatoviaMarketFactory(botwindow);
                KatoviaMarket kMarket = marketFactory.createMarket();
                if (kMarket.isSale())                                                            //проверяем, находимся ли в магазине
                    StateDriverRun(new StateGT157(botwindow), new StateGT162(botwindow));

            }
            else
            {
                MarketFactory marketFactory = new MarketFactory(botwindow);
                Market market = marketFactory.createMarket();

                if (market.isSale())                                 //проверяем, находимся ли в магазине
                    StateDriverRun(new StateGT09(botwindow), new StateGT12(botwindow));
            }
           
        }

        /// <summary>
        /// создание новой семьи, выход в ребольдо, получение и надевание брони 35 лвл, выполнение квеста Доминго, разговор с Линдоном, получение Кокошки и еды 100 шт.
        /// перевод из состояния 30 (логаут) в состояние 41 (пет Кокошка получен)          // розовая кнопка
        /// </summary>
        public void StateNewAcc()
        {
            StateDriverRun(new StateGT30(botwindow), new StateGT41(botwindow));
        }

        /// <summary>
        /// вновь созданный аккаунт бежит через лавовое плато в кратер, становится на выделенное ему место, сохраняет телепорт и начинает ботить
        /// </summary>
        public void StateToCrater()
        {
            StateDriverRun(new StateGT42(botwindow), new StateGT58(botwindow));
        }

        /// <summary>
        /// перевод из состояния 10 (в магазине) в состояние 14 (нет окна)
        /// </summary>
        public void StateExitFromShop()
        {
            StateDriverRun(new StateGT10(botwindow), new StateGT14(botwindow));
        }

        /// <summary>
        /// перевод из состояния 09 (в магазине, на странице входа) в состояние 14 (нет окна)
        /// </summary>
        public void StateExitFromShop2()
        {
            StateDriverRun(new StateGT09(botwindow), new StateGT14(botwindow));
            //StateDriverRun(new StateGT09(botwindow), new StateGT12(botwindow));
        }

        /// <summary>
        /// перевод из состояния 12 (в городе) в состояние 14 (нет окна) 
        /// </summary>
        public void StateExitFromTown()
        {
            StateDriverRun(new StateGT12(botwindow), new StateGT14(botwindow));
            //StateDriverRun(new StateGT12(botwindow), new StateGT12(botwindow));
        }

        /// <summary>
        /// если бот стоит на работе, то он направляется на продажу, а потом обратно
        /// </summary>
        public void StateGotoTradeAndWork()
        {
            if (server.isWork())   //если бот на работе
            {
                //StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа - продажа - нет окна)
                //botwindow.Pause(2000);
                //StateGotoWork();                                           // по паттерну "Состояние".  14-01       (нет окна - логаут - казарма - город - работа)

                //StateDriverRun(new StateGT01(botwindow), new StateGT12(botwindow));
                //server.Logout();
                
                StateDriverRun(new StateGT01(botwindow), new StateGT14(botwindow));
                
            }
        }


        

        /// <summary>
        /// запускает движок состояний StateDriver от пункта stateBegin до stateEnd
        /// </summary>
        /// <param name="stateBegin"> начальное состояние </param>
        /// <param name="stateEnd"> конечное состояние </param>
        public void StateDriverRun(IState stateBegin, IState stateEnd)
        {
            StateDriver stateDriver = new StateDriver(botwindow, stateBegin, stateEnd);
            while (!stateDriver.Equals(stateEnd))
            {
                stateDriver.run();
                stateDriver.setState();
            }
            //do
            //{
            //    stateDriver.run();
            //    stateDriver.setState();
            //} while (!stateDriver.Equals(stateEnd));
        }




        #endregion





    }
}
