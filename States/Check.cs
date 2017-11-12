using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGEWindows;

namespace States
{

    public class Check
    {
        private botWindow botwindow;
        DriversOfState driver;

        public Check()
        { 
        }
        public Check(int numberOfWindow)
        {
            botwindow = new botWindow(numberOfWindow);
            driver = new DriversOfState(numberOfWindow);
        }

        /////// <summary>
        /////// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /////// </summary>
        ////public void checkForProblems()
        ////{
        ////    botwindow.checkForProblems();
        ////}

        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        public void checkForProblems()
        {
            
            if (botwindow.getserver().isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                bool result = ReOpenWindow();    //если окно не вылетело, то будет true
                Pause(1000);
                if (result)      //если окно не вылетело
                {
                    if (botwindow.getserver().isLogout())
                    {
                        driver.StateRecovery();
                    }
                    else
                    {
                        if (botwindow.getserver().isSale2())         //если зависли в магазине на любой закладке
                        {
                            driver.StateExitFromShop();            //выход из магазина
                        }
                        else
                        {
                            if (botwindow.getserver().isKillHero())                  // если убиты один или несколько персов   
                            {
                                botwindow.CureOneWindow2();              // сделать End Programm
                                Pause(2000);
                                driver.StateGotoWork();               // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                            }
                            else
                            {
                                if (botwindow.getserver().isBarack())                  //если стоят в бараке     
                                {
                                    botwindow.getserver().buttonExitFromBarack();      //StateExitFromBarack();
                                }
                                else
                                {
                                    //=========================== если переполнение ==============================
                                    if ((botwindow.getserver().isBoxOverflow()) && (botwindow.getNomerTeleport() > 0))  // если карман переполнился и нужно продавать(телепорт = 0, тогда не нужно продавать)
                                    {
                                        driver.StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                                        Pause(2000);
                                        driver.StateGotoWork();                                           // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                    }
                                    else
                                    {
                                        //================== если в городе ========================================
                                        if ((botwindow.getserver().isTown()) || (botwindow.getserver().isTown_2()))          //если стоят в городе (проверка по обоим стойкам - эксп.дробаш и ружье )         //**   было istown2()
                                        {
                                            driver.StateExitFromTown();
                                            botwindow.PressEscThreeTimes();
                                            Pause(2000);
                                            driver.StateGotoWork();                                    // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                        }
                                        else
                                        {
                                            if (botwindow.getserver().isSale())                               // если застряли в магазине на странице входа
                                            { driver.StateExitFromShop2(); }
                                            else
                                            { botwindow.PressMitridat(); }

                                        } //else isTown2()
                                    } //else isBoxOverflow()
                                } //else isBarack()
                            } // else isKillHero()
                        } // else isSale2()
                    } //else  isLogout()
                } // если окно не вылетело, т.е. result = true
            } //if  Active_or_not
        }                                                                  //основной метод для зеленой кнопки
                                             

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindow()
        {
            return botwindow.FindWindow2();
        }

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна Sing"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindowSing()
        {
            return botwindow.FindWindow3();
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
        public bool ReOpenWindow()
        {
            return botwindow.ReOpenWindow();
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
    }
}
