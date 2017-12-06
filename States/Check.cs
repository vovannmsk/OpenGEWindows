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
        DriversOfState driver;

        public Check()
        { 
        }
        public Check(int numberOfWindow)
        {
            botwindow = new botWindow(numberOfWindow);
            server = botwindow.getserver();
            market = botwindow.getMarket();
            driver = new DriversOfState(numberOfWindow);
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
        public void checkForProblems()
        {
            
            if (server.isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                ReOpenWindow();    
                Pause(1000);
                if (server.isLogout())                // если окно в логауте
                {
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
                        if (server.isKillHero())                  // если убиты один или несколько персов   
                        {
                            botwindow.CureOneWindow2();              // сделать End Programm
                            Pause(2000);
                            //driver.StateGotoWork();               // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                        }
                        else
                        {
                            if (server.isBarack())                  //если стоят в бараке     
                            {
                                server.buttonExitFromBarack();      //StateExitFromBarack();
                            }
                            else
                            {
                                //=========================== если переполнение ==============================
                                if ((server.isBoxOverflow()) && (botwindow.getNomerTeleport() > 0))  // если карман переполнился и нужно продавать(телепорт = 0, тогда не нужно продавать)
                                {
                                    driver.StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                                    Pause(2000);
                                    driver.StateGotoWork();                                           // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                }
                                else
                                {
                                    //================== если в городе ========================================
                                    if ((server.isTown()) || (server.isTown_2()))          //если стоят в городе (проверка по обоим стойкам - эксп.дробаш и ружье )         //**   было istown2()
                                    {
                                        driver.StateExitFromTown();
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
                                            if (server.isOpenMenuPet())                  //если открыто меню с петом, значит пет не выпущен
                                            {
                                                driver.StateActivePet();
                                            }
                                            else
                                            {

                                            }
                                        }
                                        //else
                                        //{ botwindow.PressMitridat(); }

                                    } //else isTown2()
                                } //else isBoxOverflow()
                            } //else isBarack()
                        } // else isKillHero()
                    } // else isSale2()
                } //else  isLogout()
                
            } //if  Active_or_not
        }                                                                  //основной метод для зеленой кнопки

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindow()
        {
//            return botwindow.FindWindowEuropa();
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
            return botwindow.getserver().isLogout();
        }

        /// <summary>
        /// ввод в форму логина и пароля
        /// </summary>
        public void EnterLoginAndPasword()
        {
            botwindow.EnterLoginAndPasword();
        }

        /// <summary>
        /// действия для оранжевой кнопки
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



        /// <summary>
        /// тестовая кнопка
        /// </summary>
        public void TestButton()
        {
            botWindow botwindow = new botWindow(2);
            Server server = new ServerSing(botwindow);
            Market market = new MarketSing(botwindow);
            MessageBox.Show(" " + market.isSale2());

            //bool iscolor1 = server.isSafeIP();
            //MessageBox.Show(" " + iscolor1);

            int xx, yy;
            xx = 5;
            yy = 5;
            uint color1;
            uint color2;

            PointColor point1 = new PointColor(565 - 5 + xx, 530 - 5 + yy, 1710000, 4);
            PointColor point2 = new PointColor(565 - 5 + xx, 531 - 5 + yy, 1710000, 4);

            color1 = point1.GetPixelColor();
            color2 = point2.GetPixelColor();

            MessageBox.Show(" " + color1);
            MessageBox.Show(" " + color2);

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
