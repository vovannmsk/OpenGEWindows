﻿using System;
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

        public DriversOfState()
        { 
            
        }

        public DriversOfState(int numberOfWindow)
        {
            this.botwindow = new botWindow(numberOfWindow);
            ServerFactory serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            OtitFactory otitFactory = new OtitFactory(botwindow);
            this.otit = otitFactory.createOtit();

        }



        #region движки для запуска перехода по состояниям

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
        public void StateTransferVis()
        {
            Server server;                 
            ServerFactory serverFactory;
            serverFactory = new ServerFactory(this.botwindow);
            server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)

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
            if (botwindow.getMarket().isSale())                                 //проверяем, находимся ли в магазине
                StateDriverRun(new StateGT09(botwindow), new StateGT12(botwindow));
            //StateDriverRun(new StateGT09(botwindow), new StateGT12(botwindow));
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
            StateDriverRun(new StateGT111(botwindow), new StateGT14(botwindow));
            //StateDriverRun(new StateGT111(botwindow), new StateGT12(botwindow));
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
            if (botwindow.getserver().isWork())   //если бот на работе
            {
                StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа - продажа - нет окна)
                botwindow.Pause(2000);
                StateGotoWork();                                           // по паттерну "Состояние".  14-01       (нет окна - логаут - казарма - город - работа)
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
