using System;
using System.Windows.Forms;
using OpenGEWindows;
using GEBot.Data;

namespace States
{
    public class Check
    {
        private int numberOfWindow;
        private botWindow botwindow;
        private Server server;
        private Market market;
        private KatoviaMarket kMarket;
        private Pet pet;
        private Otit otit;
        private MM mm;
        private BHDialog BHdialog;
        private GlobalParam globalParam;
        private DriversOfState driver;
        private BotParam botParam;
        //public static bool rrr = false;

        private bool isActiveServer;

        public bool IsActiveServer { get => isActiveServer; }

        public Check()
        { 
        }

        public Check(int numberOfWindow)
        {
            this.numberOfWindow = numberOfWindow;
            botwindow = new botWindow(numberOfWindow);
            ServerFactory serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            MarketFactory marketFactory = new MarketFactory(botwindow);
            this.market = marketFactory.createMarket();
            KatoviaMarketFactory kMarketFactory = new KatoviaMarketFactory(botwindow);
            this.kMarket = kMarketFactory.createMarket();
            PetFactory petFactory = new PetFactory(botwindow);
            this.pet = petFactory.createPet();
            OtitFactory otitFactory = new OtitFactory(botwindow);
            this.otit = otitFactory.createOtit();
            MMFactory mmFactory = new MMFactory(botwindow);
            this.mm = mmFactory.create();
            BHDialogFactory dialogFactory = new BHDialogFactory(botwindow);
            this.BHdialog = dialogFactory.create();  

            this.driver = new DriversOfState(numberOfWindow);
            this.globalParam = new GlobalParam();
            this.botParam = new BotParam(numberOfWindow);
            this.isActiveServer = server.IsActiveServer;
        }

        /// <summary>
        /// возвращает номер телепорта для продажи
        /// </summary>
        /// <returns></returns>
        public int getNumberTeleport()
        {
            return botwindow.getNomerTeleport();
        }

        /// <summary>
        /// если находимся на алхимическом столе, то true
        /// </summary>
        /// <returns></returns>
        public bool isAlchemy()
        {
            return server.isAlchemy();
        }

        /// <summary>
        /// выполняет действия по открытию окна с игрой
        /// </summary>
        public void OpenWindow ()
        {
            botwindow.OpenWindow();
        }

        #region Гильдия охотников BH

     
        /// <summary>
        /// проверяем, если ли проблемы при работе в БХ и возвращаем номер проблемы
        /// </summary>
        /// <returns>порядковый номер проблемы</returns>
        public int NumberOfProblemBH()
        {
            //            int statusOfSale = botwindow.getStatusOfSale();
            int statusOfSale = globalParam.StatusOfSale;
            int statusOfAtk  = botwindow.getStatusOfAtk();

            //ворота
            if (BHdialog.isGateBH()) return 7;                 //если стоим в воротах в положении 1 или 3
            if (BHdialog.isGateBH2()) return 8;                //ворота состояние 2
            if (BHdialog.isGateBH4()) return 9;                //ворота состояние 4
            if (BHdialog.isGateBH5()) return 10;               //ворота состояние 5
            if (BHdialog.isBottonGateBH()) return 19;          //ворота прочий диалог 


            //город или БХ
            if (server.isTown())   //если в городе
            {
                if (server.isBH())     //в БХ
                {
                    if (server.isBH2()) return 18;   //стоим в БХ в неправильном месте
                    if (statusOfSale == 1)     
                    {
                        // если нужно бежать продаваться
                        return 3;
                    }
                    else
                    {
                        // если не нужно бежать продаваться
                        return 4;
                    }
                }
                else   // в городе, но не в БХ
                {
                    if (statusOfSale == 1)
                    {
                        // если нужно бежать продаваться
                        return 5;
                    }
                    else
                    {
                        // если не нужно бежать продаваться
                        return 6;
                    }
                }
            }

            //магазин
            if (market.isSale())
            {
                if (!((server.isTown()) || (server.isWork()))) return 11;        //если стоим в магазине на экране входа, но не проходит проверка, что мы в городе и что мы на работе (защита от ложных срабатываний)
            }
            if (market.isClickPurchase())
            {
                if (!((server.isTown()) || (server.isWork()))) return 12;         //если стоим в магазине на закладке Purchase, но не проходит проверка, что мы в городе и что мы на работе (защита от ложных срабатываний)
            }
            if (market.isClickSell())
            {
                if (!((server.isTown()) || (server.isWork()))) return 15;         //если стоим в магазине на закладке Purchase, но не проходит проверка, что мы в городе и что мы на работе (защита от ложных срабатываний)
            }

            //в миссии
            if (server.isWork())
            {
                if (statusOfAtk == 0)                                            //еще не атаковали босса в миссии
                {
                    if (server.isMissionBH())                                         //если миссия определилась
                    {
                        return 13;
                    }
                    else
                    {
                        return 21;                                                    //миссия не определилась
                    }
                }
                else                                                                  //после начала атаки босса
                {
                    if (server.isRouletteBH())
                    {
                        return 20;
                    }
                    else
                    {
                        if (!server.isAtakBH()) return 14;
                        //если находимся в миссии, но уже не в начале и не атакуем босса и не крутится рулетка (значит бой окончен, либо заблудились и надо выходить из миссии) 
                    }
                }
            }
            if (server.isLogout()) return 1;               // если окно в логауте
            if (server.isBarack())                         //если стоят в бараке 
            {
                if (server.isBarackLastPoint())            //если начиная со старого места попадаем в БХ
                { return 16;} 
                else 
                { return 2; }                
            }

            if (server.isBarackTeamSelection()) return 17;    //если в бараках на стадии выбора группы

            if (server.isKillAllHero()) return 29;            // если убиты все
            if (server.isKillHero()) return 30;               // если убиты не все 

            return 0;
        }

        /// <summary>
        /// разрешение выявленных проблем в БХ
        /// </summary>
        public void problemResolutionBH()
        {
            ReOpenWindow();
            Pause(500);

            int numberOfProblem = NumberOfProblemBH();             //проверили, какие есть проблемы (на какой стадии находится бот)

            switch (numberOfProblem)
            {
                case 1: driver.StateFromLogoutToBarackBH();       // Logout-->Barack
                    break;
                case 2: driver.StateFromBarackToTownBH();         // идем в город
                    break;
                case 3: driver.StateGotoTradeStep1BH();           // BH-->Town (первый этап продажи)
                    break;
                case 4: driver.StateFromBHToGateBH();             // BH --> Gate
                    break;
                case 5: driver.StateGotoTradeStep2BH();           // если стоят в городе и надо продаваться, то второй этап продажи
                    break;
                case 6: driver.StateFromTownToBH();               // town --> BH
                    break;
                case 7: driver.StateFromGateToMissionBH();        // Gate --> Mission
                    break;
                case 8: driver.StateFromGate2ToMissionBH();       // Gate2 --> Mission (из состояния 2)
                    break;
                case 9: driver.StateFromGate4ToGate5BH();         // Gate4 --> Gate 5
                    break;
                case 10: driver.StateFromGate5ToBH();             // Gate5 --> BH
                    break;
                case 11: driver.StateGotoTradeStep3BH();          // третий этап продажи
                    break;
                case 12: driver.StateGotoTradeStep4BH();          // четвертый этап продажи
                    break;
                case 13: driver.StateFromMissionToFightBH();      // Mission--> Fight!!!
                    break;
                case 14: driver.StateFromMissionToBarackBH();     // в барак 
                       //driver.StateFromMissionToBH();           // в бх
                    break;
                case 15: driver.StateGotoTradeStep5BH();          // пятый этап продажи
                    break;
                case 16: server.barackLastPoint();                // начинаем со старого места
                    break;
                case 17: botwindow.PressEsc();                    // нажимаем Esc
                    break;
                case 18: server.systemMenu(3, true);              // переход в стартовый город
                    break;
                case 19: BHdialog.PressOkButton(1);               // в диалоге ворот нажали кнопку "Ок"
                    break;
                case 20: //server.FightToPoint(180, 270, 0);        // бежим в сторону с атакой, чтобы не улететь в барак, пока крутится рулетка
                    break;
                case 21: server.MissionNotFoundBH();              // миссия не найдена. записываем в файл данные и отбегаем в сторону
                    break;
                case 29: botwindow.CureOneWindow();               // logout
                    break;
                case 30: botwindow.CureOneWindow2();              // отбегаем в сторону и логаут
                    break;
            }


        }

        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)                                //старый метод. не используется
        /// </summary>
        public void checkForProblemsBH()
        {

            if (isActiveServer)      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                ReOpenWindow();
                Pause(500);
                if (server.isLogout())                // если окно в логауте
                {
                    //driver.StateFromLogoutToBH();         // долетаем до Гильдии Охотников
                    driver.StateFromLogoutToBarackBH();         // Logout-->Barack
                }
                else
                {
                    if (server.isKillAllHero())                 // если убиты все
                    {
                        botwindow.CureOneWindow();              // logout
                    }
                    else
                    {
                        if (server.isKillHero())                // если убиты не все 
                        {
                            botwindow.CureOneWindow2();         // отбегаем в сторону и логаут
                        }
                        else
                        {
                            if (server.isBarack())                  //если стоят в бараке     
                            {
                                driver.StateFromBarackToTownBH();     //идем в город
                            }
                            else
                            {
                                if (server.isTown() && !server.isBH() && (globalParam.StatusOfSale == 0))                     //если стоят в городе (но не в BH) и не надо идти продаваться
                                {
                                    driver.StateFromTownToBH();            //town --> BH
                                }
                                else
                                {
                                    if (  (server.isBH())   &&   (globalParam.StatusOfSale == 0)  )          //если стоим в БХ и если мы не собираемся идти продаваться
                                    {
                                        driver.StateFromBHToGateBH();            // BH --> Gate
                                    }
                                    else
                                    {
                                        if ((server.isBH()) && (globalParam.StatusOfSale == 1))          //если стоим в БХ и если собираемся идти продаваться
                                        {
                                            //int ff = botwindow.getNomerTeleport();
                                            //if (botwindow.getNomerTeleport() >= 100)           // продажа в снежке
                                            //{
                                            //    driver.StateGotoTradeKatovia();
                                            //}
                                            //else                                               // продажа в городах
                                            //{
                                            driver.StateGotoTradeStep1BH();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                                            //}
                                        }
                                        else
                                        {
                                            if (BHdialog.isGateBH())
                                            {
                                                driver.StateFromGateToMissionBH();            // Gate --> Mission
                                            }
                                            else
                                            {
                                                if (server.isWork() && server.isMissionBH())                            //если находимся в миссии, в самом начале
                                                {
                                                    driver.StateFromMissionToFightBH();                                 // Mission--> Fight!!!
                                                }
                                                else
                                                {
                                                    if (server.isWork() && !server.isMissionBH() && !server.isAtakBH())
                                                    //если находимся в миссии, но уже не в начале и не атакуем босса (значит бой окончен, либо заблудились и надо выходить в БХ) 
                                                    {
                                                        driver.StateFromMissionToBH();         //в бх
                                                    }
                                                    else
                                                    {
                                                        if (BHdialog.isGateBH4())
                                                        {
                                                            driver.StateFromGate4ToGate5BH();
                                                        }
                                                        else
                                                        {
                                                            if (BHdialog.isGateBH5())
                                                            {
                                                                driver.StateFromGate5ToBH();
                                                            }
                                                            else
                                                            {
                                                                if ((server.isTown()) && !server.isBH() && (globalParam.StatusOfSale == 1))    //если стоят в городе (но не в BH) и надо идти продаваться
                                                                {
                                                                    driver.StateGotoTradeStep2BH();
                                                                }
                                                                else
                                                                {
                                                                    if (market.isSale())                                   //если стоим в магазине на экране входа
                                                                    {
                                                                        driver.StateGotoTradeStep3BH();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (market.isSale2())                                        //если стоим в магазине
                                                                        {
                                                                            driver.StateGotoTradeStep4BH();
                                                                        }
                                                                        else
                                                                        {



                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }//если стоим в БХ и если собираемся идти продаваться
                                    }//если стоим в БХ и если мы не собираемся идти продаваться
                                } //если стоят в городе (но не в BH)
                            } //else isBarack()
                        } // else isKillHero()
                    }
                } //else  isLogout()
            } //if  Active_or_not
        }                                                                  //  (старый рабочий вариант)

        #endregion

        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        /// <returns>нпорядковый номер проблемы</returns>
        public int NumberOfProblem()
        {
            if (server.isLogout()) { server.WriteToLogFile("Логаут");  return 1; }                         // если окно в логауте
            if (server.isKillAllHero()) { server.WriteToLogFile("Все убиты"); return 2; }                  // если убиты все
            if (server.isKillHero())    { server.WriteToLogFile("Убиты не все"); return 3; }               // если убиты не все 
            int numberTeleport = this.botwindow.getNomerTeleport();
            if (server.isBoxOverflow())                             // если карман переполнился и нужно продавать 
            {
                if (numberTeleport > 0)                            // (телепорт = 0, тогда не нужно продавать)
                {
                    if (server.is248Items())                       //проверяем реально ли карман переполнился
                    {
                        if (numberTeleport >= 100)                 // продажа в снежке
                            {  server.WriteToLogFile("Продажа в снежке"); return 5; }
                        else                                 // продажа в городах
                            { server.WriteToLogFile("Продажа не в снежке"); return 6; }

                    }
                }
            }
            if (pet.isOpenMenuPet()) { server.WriteToLogFile("открыто меню пет"); return 4; } //если открыто меню с петом, значит пет не выпущен
            if (market.isSale()) { server.WriteToLogFile("В магазине"); return 7; }           // если бот стоит в магазине на странице входа
            if (market.isSale2()) return 8;                         //если зависли в магазине на любой закладке
            if (kMarket.isSale()) return 12;                     // если бот стоит в магазине на странице входа
            if (kMarket.isClickSell()) return 13;                //если зависли в катовичевском магазине на закладке Sell
            if (kMarket.isSaleIn()) return 14;                //если зависли в катовичевском магазине на закладке BUY 
            if (server.isBarack()) return 9;                        //если стоят в бараке     
            if (server.isBarackTeamSelection()) return 9;           //если стоят в бараке  на странице выбора группы
            if (server.isTown()) return 10;                       //если стоят в городе
            if (mm.isMMSell() || (mm.isMMBuy())) return 11;     //если бот стоит на рынке
            if (server.isBulletHalf()|| server.isBulletOff()) return 15;      // если заканчиваются экспертные патроны

            return 0;
        }

        /// <summary>
        /// решение проблем с ботами
        /// </summary>
        public void problemResolution()
        { 
            ReOpenWindow();
            Pause(500);

            int numberOfProblem = NumberOfProblem();             //проверили, какие есть проблемы (на какой стадии находится бот)

            switch (numberOfProblem)
            {
                case 1: driver.StateRecovery();
                    break;
                case 2: botwindow.CureOneWindow();              //logout
                    break;
                case 3: botwindow.CureOneWindow2();              // отбегаем в сторону и логаут
                    break;
                case 4: driver.StateActivePet();
                    break;
                case 5: driver.StateGotoTradeKatovia();            //Pause(2000);
                    break;
                case 6: driver.StateGotoTrade();                //Pause(2000);                        // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                    break;
                case 7: driver.StateExitFromShop2();           //продаемся и логаут   09-14
                    break;
                case 8: driver.StateExitFromShop();            //продаемся и логаут   10-14                                                  
                    break;
                case 9: server.buttonExitFromBarack();          //StateExitFromBarack();
                    break;
                case 10: driver.StateExitFromTown();          // 12-14 (GotoEnd)
                    break;
                case 11: SellProduct();                     // выставление товаров на рынок
                    break;
                case 12: driver.StateSelling2();          //продажа в катовичевском магазине      
                    break;
                case 13: driver.StateSelling4();          //продажа в катовичевском магазине
                    break;
                case 14: driver.StateSelling3();          //продажа в катовичевском магазине   
                    break;
                case 15:
                    server.AddBullet10000();              //открываем коробку с патронами 10 000 штук
                    break;
            }
        }

        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)                           //старый метод. не используется
        /// </summary>
        public void checkForProblems()
        {
            if (isActiveServer)      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                ReOpenWindow();    
                Pause(500);
                if (server.isLogout())                // если окно в логауте
                {
                    //server.serverSelection();
                    driver.StateRecovery();
                }
                else
                {
                    if (market.isSale2())         //если зависли в магазине на любой закладке
                    {
                        driver.StateExitFromShop();            //выход из магазина
                    }
                    else
                    {
                        if (server.isKillAllHero())                  // если убиты все
                        {
                            botwindow.CureOneWindow();              //logout
                        }
                        else
                        {
                            //if ( (server.isKillHero()) || (!server.isBattleMode()) )                // если убиты не все или стоят не в боевом режиме
                            if  (server.isKillHero())               // если убиты не все 
                            {
                                botwindow.CureOneWindow2();              // отбегаем в сторону и логаут
                            }
                            else
                            {
                                if (server.isBarack())                  //если стоят в бараке     
                                {
                                    server.buttonExitFromBarack();      //StateExitFromBarack();
                                }
                                else
                                {
                                    if ((server.isBoxOverflow()) && (botwindow.getNomerTeleport() > 0))  // если карман переполнился и нужно продавать (телепорт = 0, тогда не нужно продавать)
                                    {
                                        if (server.is248Items())
                                        {
                                            int ff = botwindow.getNomerTeleport();
                                            if (botwindow.getNomerTeleport() >= 100)           // продажа в снежке
                                            {
                                                driver.StateGotoTradeKatovia();
                                                Pause(2000);
                                            }
                                            else                                               // продажа в городах
                                            {
                                                driver.StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                                                Pause(2000);
                                                driver.StateGotoWork();                                           // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (mm.isMMSell() || (mm.isMMBuy()))   //если бот стоит на рынке
                                        {
                                            SellProduct();                     // выставление товаров на рынок
                                        }
                                        else
                                        {
                                            if (server.isTown())          //если стоят в городе
                                            {
                                                driver.StateExitFromTown();          // 12-14 (GotoEnd)
                                                botwindow.PressEscThreeTimes();
                                                Pause(2000);
                                                driver.StateGotoWork();                                    // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                            }
                                            else
                                            {
                                                if (market.isSale())                               // если застряли в магазине на странице входа
                                                { driver.StateExitFromShop2(); }
                                                else
                                                {
                                                    if (pet.isOpenMenuPet())                  //если открыто меню с петом, значит пет не выпущен
                                                    {
                                                        driver.StateActivePet();
                                                    }
                                                    else
                                                    {
                                                        //if ((server.isCook()) && (!server.isBattleMode()))   //если повар и он не в боевом режиме
                                                        //{
                                                        //    botwindow.ClickSpace();
                                                        //}
                                                        //else
                                                        //{
                                                        //    //botwindow.PressMitridat();
                                                        //}
                                                    }
                                                }
                                            }
                                        }
                                    } //else isBoxOverflow()
                                } //else isBarack()
                            } // else isKillHero()
                        }
                    } // else isSale2()
                } //else  isLogout()
            } //if  Active_or_not
        }                                                                  //старый метод. не используется

         /// <summary>
        /// выставляем на рынок продукт, если у нас на рынке не лучшая цена
        /// </summary>
        public void SellProduct()
        {
            mm.SellProduct();
        }

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindow()
        {
            return server.FindWindowGE();
        }

        /// <summary>
        /// пауза в милисекундах
        /// </summary>
        /// <param name="ms">милисекунды</param>
        public void Pause(int ms)
        {
            botwindow.Pause(ms);
        }

        /// <summary>
        /// проверка открыто ли окно и перемещение его в заданные координаты 
        /// </summary>
        /// <returns></returns>
        public void ReOpenWindow()
        {
            botwindow.ReOpenWindow();
        }

        /// <summary>
        /// проверка, находится ли окно в состоянии Logout
        /// </summary>
        /// <returns></returns>
        public bool isLogout()
        {
            return server.isLogout();
        }

        /// <summary>
        /// ввод в форму логина и пароля
        /// </summary>
        public void EnterLoginAndPasword()
        {
            botwindow.EnterLoginAndPasword();
        }

        /// <summary>
        /// оранжевая кнопка. разные действия по серверам . не удалять
        /// </summary>
        public void OrangeButton()
        {
            server.OrangeButton();
        }

        /// <summary>
        /// Желтая кнопка. Загрузка Стимов без загркзки окон ГЭ. Разные действия по серверам . не удалять
        /// </summary>
        public void YellowButton()
        {
            server.runClientSteam();
        }

        /// <summary>
        /// боты заходят в город, получают подарок у ГМ и окно выгружается
        /// </summary>
        public void ChangingAccounts()
        {
            for (int j = botParam.NumberOfInfinity; j < botParam.Logins.Length; j++)
            {
                server.WriteToLogFile("номер окна = " + j);
                driver.StateInputOutput(); //вход и выход из игры
            }
        }

        /// <summary>
        /// создать новые аккаунты в одном окне бота
        /// </summary>
        public void NewAccountsTwo()
        {
            for (int j = botParam.NumberOfInfinity; j < botParam.Logins.Length; j++)
                driver.StateNewAcc2(); //новые акки
        }

        ///// <summary>
        ///// определяет, нужно ли работать с этим окном (может быть отключено из-за профилактики на сервере)
        ///// </summary>
        ///// <returns></returns>
        //public bool isActive()
        //{
        //    return IsActiveServer;
        //}

        /// <summary>
        /// проверяем, находимся ли в магазине у Иды (заточка)
        /// </summary>
        /// <returns></returns>
        public bool isIda()
        {
            return server.isIda();
        }

        /// <summary>
        /// проверяем, находимся ли в магазине у Чиповщицы
        /// </summary>
        /// <returns></returns>
        public bool isEnchant()
        {
            return server.isEnchant();
        }

        ///// <summary>
        ///// проверяем, находимся ли мы в диалоге со старым мужиком в Лос Толдосе
        ///// </summary>
        //public bool isOldMan()
        //{
        //    return otit.isOldMan();
        //}
        
        /// <summary>
        /// раздевание в казарме
        /// </summary>
        public void UnDressing()
        {
            server.UnDressing();
        }

        /// <summary>
        /// выбор сервера (синг или америка2)
        /// </summary>
        public void serverSelection()
        {
            server.serverSelection();
        }

        /// <summary>
        /// номер аккаунта, номер логина по порядку
        /// </summary>
        /// <returns></returns>
        public string NumberOfInfinity()
        {
            return botParam.NumberOfInfinity.ToString();
        }


        /// <summary>
        /// тестовая кнопка
        /// </summary>
        public void TestButton()
        {
            int i = 1;   //номер проверяемого окна

            int[] koordX = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 875, 850, 825, 800, 775, 750, 875 };
            int[] koordY = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 5, 30, 55, 80, 105, 130, 5 };

            //botWindow botwindow = new botWindow(i);
            //botwindow.ReOpenWindow();

            //MessageBox.Show(" " + botwindow.getNomerTeleport());
            //botwindow.Pause(1000);

            //Server server = new ServerSing(botwindow);
            //MessageBox.Show(" " + server.isBoxOverflow());
            //Server server = new ServerAmerica2(botwindow);

            //BHDialog BHdialog = new BHDialogSing(botwindow);
            //Dialog dialog = new DialogSing(botwindow);
            //bool ttt = dialog.isDialog();
            //MessageBox.Show(" " + ttt);

            //KatoviaMarket kMarket = new KatoviaMarketSing (botwindow);
            //Market market = new MarketSing(botwindow);

            //            Pet pet = new PetAmerica2(botwindow);
            //Pet pet = new PetSing(botwindow);
            //MessageBox.Show(" " + pet.isOpenMenuPet());

            //Otit otit = new OtitSing(botwindow);
            //MessageBox.Show(" " + server.is248Items());

            //server.MissionNotFoundBH();
            //bool iscolor1 = pet.isActivePet();
            //MessageBox.Show(" " + iscolor1);

            //bool iscolor1 = kMarket.isSaleIn();
            //MessageBox.Show(" " + iscolor1);

            //bool iscolor1 = market.isClickPurchase();
            //MessageBox.Show(" " + iscolor1);
            //bool iscolor2 = market.isClickSell();
            //MessageBox.Show(" " + iscolor2);

            //bool iscolor1 = server.isUndead();
            //MessageBox.Show(" " + iscolor1);
            //bool ttt;
            //ttt = BHdialog.isBottonGateBH();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH1();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH2();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH3();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH4();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH5();
            //MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH6();
            //MessageBox.Show(" " + ttt);

            //int[] x = { 0, 0, 130, 260, 390, -70, 60, 190, 320, 450 };
            //int[] y = { 0, 0, 0, 0, 0, 340, 340, 340, 340, 340 };

            //int[] aa = new int[17] { 0, 1644051, 725272, 6123117, 3088711, 1715508, 1452347, 6608314, 14190184, 1319739, 2302497, 5275256, 2830124, 1577743, 525832, 2635325, 2104613 };
            //bool ff = aa.Contains(725272);
            //int tt = Array.IndexOf(aa, 7272);
            //MessageBox.Show(" " + ff + " " + tt);

            //server.FightToPoint(997 + 25, 160 + 25, 3);
            //server.Turn180();
            //server.TurnUp();
            //server.Turn90R();
            //server.TurnL(1);
            //server.FightToPoint(595, 125, 3);
            //server.TurnDown();
            //server.TurnR(1);
            //server.FightToPoint(545, 110, 3);

            //server.TurnL(1); 
            //server.TurnUp();



            int xx, yy;
            xx = koordX[i - 1];
            yy = koordY[i - 1];
            uint color1;
            uint color2;
            uint color3;
            //int x = 483;
            //int y = 292;
            //int i = 4;

            //int j = 12;
            //PointColor point1 = new PointColor(149 - 5 + xx, 219 - 5 + yy + (j - 1) * 27, 1, 1);       // новый товар в магазине в городе
            //            PointColor point1 = new PointColor(152 - 5 + xx, 250 - 5 + yy + (j - 1) * 27, 1, 1);       // новый товар в магазине в Катовии

            //PointColor point1 = new PointColor(1142, 610, 1, 1);
            //PointColor point2 = new PointColor(1142, 608, 1, 1);
            PointColor point1 = new PointColor(1008 - 5 + xx, 509 - 5 + yy, 1, 1);
            PointColor point2 = new PointColor(1008 - 5 + xx, 556 - 5 + yy, 1, 1);
            //PointColor point3 = new PointColor(930 - 5 + xx, 703 - 5 + yy, 1, 1);


            color1 = point1.GetPixelColor();
            color2 = point2.GetPixelColor();
            //color3 = point3.GetPixelColor();

            //server.WriteToLogFile("цвет " + color1);
            //server.WriteToLogFile("цвет " + color2);
            MessageBox.Show(" " + color1);
            MessageBox.Show(" " + color2);
            //MessageBox.Show(" " + color3);


            //if ((color1 > 2000000) && (color2 > 2000000)) MessageBox.Show(" больше ");


            //string str = "";
            //if (server.isHuman()) str += "Human ";
            //if (server.isWild()) str += "Wild ";
            //if (server.isLifeless()) str += "Life ";
            //if (server.isUndead()) str += "Undead ";
            //if (server.isDemon()) str += "Demon ";

            //MessageBox.Show(str);
        }
    }
}
