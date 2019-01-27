using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenGEWindows;

namespace States
{
    public class Check
    {
        private botWindow botwindow;
        private Server server;
        private Market market;
        private Pet pet;
        private Otit otit;
        private MM mm;
        private BHDialog BHdialog;

        private DriversOfState driver;

        public Check()
        { 
        }
        public Check(int numberOfWindow)
        {
            botwindow = new botWindow(numberOfWindow);
            ServerFactory serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            MarketFactory marketFactory = new MarketFactory(botwindow);
            this.market = marketFactory.createMarket();
            PetFactory petFactory = new PetFactory(botwindow);
            this.pet = petFactory.createPet();
            OtitFactory otitFactory = new OtitFactory(botwindow);
            this.otit = otitFactory.createOtit();
            MMFactory mmFactory = new MMFactory(botwindow);
            this.mm = mmFactory.create();
            BHDialogFactory dialogFactory = new BHDialogFactory(botwindow);
            this.BHdialog = dialogFactory.create();  

            driver = new DriversOfState(numberOfWindow);
        }

        /// <summary>
        /// возвращает номер телепорта для продажи
        /// </summary>
        /// <returns></returns>
        public int getNumberTeleport()
        {
            return botwindow.getNomerTeleport();
        }
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


        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        public void checkForProblemsBH()
        {

            if (server.isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                ReOpenWindow();
                Pause(500);
                if (server.isLogout())                // если окно в логауте
                {
                    driver.StateRecoveryBH();         // долетаем до Гильдии Охотников
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
                                server.buttonExitFromBarack();      //StateExitFromBarack();
                            }
                            else
                            {
                                if (server.isTown() && !server.isBH())                     //если стоят в городе (но не в BH)
                                {
                                    driver.StateExitFromTown();          // 12-14 (GotoEnd)
                                    botwindow.PressEscThreeTimes();
                                }
                                else
                                {
                                    if (server.isBH())
                                    {
                                        driver.StateGateBH();            // BH --> Gate
                                    }
                                    else
                                    {
                                        if (BHdialog.isGateBH())
                                        { 
                                            
                                        }

                                    }
                                }
                            } //else isBarack()
                        } // else isKillHero()
                    }
                } //else  isLogout()
            } //if  Active_or_not
        }                                                                  //основной метод для зеленой кнопки


        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        public void checkForProblems()
        {
            if (server.isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
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
        }                                                                  //основной метод для зеленой кнопки

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
        /// определяет, нужно ли работать с этим окном (может быть отключено из-за профилактики на сервере)
        /// </summary>
        /// <returns></returns>
        public bool isActive()
        {
            return server.isActive();
        }

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
        /// тестовая кнопка
        /// </summary>
        public void TestButton()
        {
            int i = 2;   //номер проверяемого окна

            int[] koordX = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 875, 850, 825, 800, 775, 750, 875 };
            int[] koordY = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 5, 30, 55, 80, 105, 130, 5 };

            botWindow botwindow = new botWindow(i);

            Server server = new ServerSing(botwindow);
            //Server server = new ServerAmerica2(botwindow);

            BHDialog BHdialog = new BHDialogSing(botwindow);
            

            //Market market = new MarketSing(botwindow);

//            Pet pet = new PetAmerica2(botwindow);
            //Pet pet = new PetSing(botwindow);
            //MessageBox.Show(" " + pet.isOpenMenuPet());

            //Otit otit = new OtitSing(botwindow);
            //MessageBox.Show(" " + server.is248Items());

            //bool iscolor1 = server.isDef15();
            //MessageBox.Show(" " + iscolor1);
            //bool iscolor1 = server.isActive ();
            //MessageBox.Show(" " + iscolor1);
            //bool iscolor1 = server.isSafeIP();
            //MessageBox.Show(" " + iscolor1);
            bool ttt;
            ttt = BHdialog.isBottonGateBH();
            MessageBox.Show(" " + ttt);
            ttt = BHdialog.isGateBH1();
            MessageBox.Show(" " + ttt);
            ttt = BHdialog.isGateBH2();
            MessageBox.Show(" " + ttt);
            ttt = BHdialog.isGateBH3();
            MessageBox.Show(" " + ttt);
            ttt = BHdialog.isGateBH4();
            MessageBox.Show(" " + ttt);
            ttt = BHdialog.isGateBH5();
            MessageBox.Show(" " + ttt);
            //ttt = BHdialog.isGateBH6();
            //MessageBox.Show(" " + ttt);

            //int[] x = { 0, 0, 130, 260, 390, -70, 60, 190, 320, 450 };
            //int[] y = { 0, 0, 0, 0, 0, 340, 340, 340, 340, 340 };

            int xx, yy;
            xx = koordX[i - 1];
            yy = koordY[i - 1];
            uint color1;
            uint color2;
            uint color3;
            //int x = 483;
            //int y = 292;
            //int i = 4;

            //int j = 1;
//            PointColor point1 = new PointColor(149 - 5 + xx, 219 - 5 + yy + (j - 1) * 27, 1, 1);       // новый товар в магазине в городе
//            PointColor point1 = new PointColor(152 - 5 + xx, 250 - 5 + yy + (j - 1) * 27, 1, 1);       // новый товар в магазине в Катовии

            PointColor point1 = new PointColor(932 - 30 + xx, 700 - 30 + yy, 7000000, 6);
            PointColor point2 = new PointColor(979 - 30 + xx, 390 - 30 + yy, 7000000, 6);
            PointColor point3 = new PointColor(716 - 30 + xx, 249 - 30 + yy, 1, 1);


            color1 = point1.GetPixelColor();
            color2 = point2.GetPixelColor();
            color3 = point3.GetPixelColor();

            MessageBox.Show(" " + color1);
            MessageBox.Show(" " + color2);
            MessageBox.Show(" " + color3);


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
