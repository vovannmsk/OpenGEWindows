using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace OpenGEWindows
{
    /// <summary>
    /// Класс описывает процесс перехода к торговле от фарма , начиная от проверки необходимости продажи и заканчивая закрытием окна с ботом (для сервера Сингапур (Avalon))
    /// </summary>
    public class ServerAmerica2 : Server
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(UIntPtr myhWnd, int myhwndoptional, int xx, int yy, int cxx, int cyy, uint flagus); // Перемещает окно в заданные координаты с заданным размером

        //[DllImport("user32.dll")]
        //private static extern bool ShowWindow(UIntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern UIntPtr FindWindow(String ClassName, String WindowName);  //ищет окно с заданным именем и классом

        /// <summary>
        /// конструктор
        /// town отвечает за методы для конкретного города (паттерн Стратегия). Все различия в действиях, зависящих от города, инкапсулированы в семействе классов Town (в т.ч. AmericaTown)
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public ServerAmerica2(botWindow botwindow)
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region общие 2

            this.townFactory = new America2TownFactory(botwindow);                                     // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();
            this.pathClient = path_Client();

            #endregion

            #region No Window
            this.pointSafeIP1 = new PointColor(941, 579, 13600000, 5);
            this.pointSafeIP2 = new PointColor(942, 579, 13600000, 5);
            #endregion

            #region Logout

            this.pointConnect = new PointColor(696 - 5 + xx, 148 - 5 + yy, 7800000, 5);
            //this.pointisLogout1 = new PointColor(565 - 5 + xx, 532 - 5 + yy, 16000000, 6);       // проверено   слово Leave Game буква L
            //this.pointisLogout2 = new PointColor(565 - 5 + xx, 531 - 5 + yy, 16000000, 6);       // проверено
            this.pointisLogout1 = new PointColor(913 - 5 + xx, 698 - 5 + yy, 7793136, 0);       // проверено   слово Ver буква V
            this.pointisLogout2 = new PointColor(913 - 5 + xx, 699 - 5 + yy, 7726828, 0);       // проверено

            #endregion

            #region Pet

            this.pointisSummonPet1 = new PointColor(494 - 5 + xx, 304 - 5 + yy, 13000000, 6);
            this.pointisSummonPet2 = new PointColor(494 - 5 + xx, 305 - 5 + yy, 13000000, 6);
            this.pointisActivePet1 = new PointColor(493 - 5 + xx, 310 - 5 + yy, 13000000, 6);
            this.pointisActivePet2 = new PointColor(494 - 5 + xx, 309 - 5 + yy, 13000000, 6);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //не проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц
            this.pointisOpenMenuPet1 = new PointColor(474 - 5 + xx, 219 - 5 + yy, 12000000, 6);      //834 - 5, 98 - 5, 12400000, 835 - 5, 98 - 5, 12400000, 5);             //проверено
            this.pointisOpenMenuPet2 = new PointColor(474 - 5 + xx, 220 - 5 + yy, 12000000, 6);
            this.pointCancelSummonPet = new Point(410 - 5 + xx, 390 - 5 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(540 - 5 + xx, 380 - 5 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(410 - 5 + xx, 360 - 5 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet = new Point(410 - 5 + xx, 410 - 5 + yy);                   // //Click Button Active Pet                            //проверено

            #endregion

            #region Top Menu

            this.pointisOpenTopMenu21  = new PointColor(328 + xx, 74 + yy, 13420000, 4);      //333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu22  = new PointColor(329 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu61  = new PointColor(455 + xx, 87 + yy, 13420000, 4);      //460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu62  = new PointColor(456 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu81  = new PointColor(553 + xx, 87 + yy, 13420000, 4);      //558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu82  = new PointColor(554 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu91  = new PointColor(601 + xx, 74 + yy, 13420000, 4);      //606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);            //проверено
            this.pointisOpenTopMenu92  = new PointColor(602 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu121 = new PointColor(502 - 5 + xx, 120 - 5 + yy, 12000000, 6);            //проверено
            this.pointisOpenTopMenu122 = new PointColor(502 - 5 + xx, 121 - 5 + yy, 12000000, 6);
            this.pointisOpenTopMenu131 = new PointColor(404 - 5 + xx, 278 - 5 + yy, 16000000, 6);          //Quest Name                                                         //проверено
            this.pointisOpenTopMenu132 = new PointColor(404 - 5 + xx, 279 - 5 + yy, 16000000, 6);
            this.pointGotoEnd =          new Point(685 - 5 + xx, 470 - 5 + yy);            //end
            this.pointLogout =           new Point(685 - 5 + xx, 440 - 5 + yy);            //логаут
            this.pointGotoBarack =       new Point(685 - 5 + xx, 380 - 5 + yy);            //в барак
            this.pointTeleport1 =        new Point(400 + xx, 178 + yy);   //400, 193               тыкаем в первую строчку телепорта                          //проверено
            this.pointTeleport2 =        new Point(355 + xx, 580 + yy);   //             тыкаем в кнопку Execute                   //проверено

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(907 + xx, 675 + yy, 7200000, 5);
            this.pointIsSale2 = new PointColor(907 + xx, 676 + yy, 7800000, 5);
            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7900000, 5);
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7900000, 5);
            this.pointIsClickSale1 = new PointColor(731 - 5 + xx, 662 - 5 + yy, 7900000, 5);
            this.pointIsClickSale2 = new PointColor(731 - 5 + xx, 663 - 5 + yy, 7900000, 5);

            this.pointBookmarkSell = new Point(225 + xx, 163 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonClose = new Point(847 + xx, 663 + yy);   //847, 663);
            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      //360, 537
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //1392 - 875, 438 - 5
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //1392 - 875, 428 - 5

            #endregion

            #region atWork

            this.pointisBoxOverflow1 = new PointColor(522 - 5 + xx, 434 - 5 + yy, 7700000, 5);          //
            this.pointisBoxOverflow2 = new PointColor(522 - 5 + xx, 435 - 5 + yy, 7700000, 5);
            this.pointisWork_RifleDot1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //проверено
            this.pointisWork_RifleDot2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            this.pointisWork_ExpRifleDot1 = new PointColor(24 + xx, 692 + yy, 1721000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //проверено
            this.pointisWork_ExpRifleDot2 = new PointColor(25 + xx, 692 + yy, 2106000, 3);
            this.pointisWork_DrobDot1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            this.pointisWork_DrobDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointisWork_VetDrobDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 2764000, 3);           //проверка по вет стойке с дробашем        
            this.pointisWork_VetDrobDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 7243000, 3);
            this.pointisWork_ExpDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_ExpDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork_JainaDrobDot1 = new PointColor(24 + xx, 692 + yy, 4278000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_JainaDrobDot2 = new PointColor(25 + xx, 692 + yy, 5401000, 3);
            this.pointisWork_VetSabreDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 5138000, 3);           //проверка по вет стойке с саблей
            this.pointisWork_VetSabreDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 9747000, 3);
            this.pointisWork_ExpSwordDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 3693000, 3);           //проверка по стойке с мечом Дарья
            this.pointisWork_ExpSwordDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 10258000, 3);
            this.pointisWork_VetPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 66000, 3);           //проверка по стойке с вет пистолетом Outrange
            this.pointisWork_VetPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 72, 0);
            this.pointisWork_SightPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 5068000, 3);       //точки для проверки вет стойки с пистолетом Sight Shot
            this.pointisWork_SightPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 9350000, 3);
            this.pointisWork_UnlimPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 15824000, 3);      //проверка по стойке с эксп пистолетом Unlimited Shot
            this.pointisWork_UnlimPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 15767000, 3);
            //пушка
            this.pointisWork_ExpCannonDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 8756000, 3);       //точки для проверки пушки Мисы
            this.pointisWork_ExpCannonDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 8162000, 3);

            this.pointisKillHero1 = new PointColor(80 - 5 + xx, 636 - 5 + yy, 1900000, 5);
            this.pointisKillHero2 = new PointColor(335 - 5 + xx, 636 - 5 + yy, 1900000, 5);
            this.pointisKillHero3 = new PointColor(590 - 5 + xx, 636 - 5 + yy, 1900000, 5);
            this.pointisLiveHero1 = new PointColor( 80 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointisLiveHero2 = new PointColor(335 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointisLiveHero3 = new PointColor(590 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointSkillCook = new Point(183 - 5 + xx, 700 - 5 + yy);
            this.pointisBattleMode1 = new PointColor(173 - 5 + xx, 511 - 5 + yy, 8900000, 5);
            this.pointisBattleMode2 = new PointColor(200 - 5 + xx, 511 - 5 + yy, 8900000, 5);

            #endregion

            #region inTown

            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 13000000, 5);  //проверяем открыто ли окно с токенами
            this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 13000000, 5);
            this.pointToken = new Point(755 - 5 + xx, 94 - 5 + yy);                       //крестик в углу окошка с токенами
            this.pointCure1 = new Point(215 - 5 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой U
            this.pointCure2 = new Point(215 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой J
            this.pointCure3 = new Point(215 - 5 + 255 * 2 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой M
            this.pointMana1 = new Point(245 - 5 + xx, 705 - 5 + yy);                        //бутылка маны под буквой I
            this.pointMana2 = new Point(245 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка маны под буквой K
            this.pointMana3 = new Point(245 - 5 + 510 + xx, 705 - 5 + yy);                        //бутылка маны под буквой ,
            this.pointIsTown_RifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 11053000, 3);        //точки для проверки обычной стойки с ружьем
            this.pointIsTown_RifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown_ExpRifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 1710000, 4);       //точки для проверки эксп стойки с ружьем
            this.pointIsTown_ExpRifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 2100000, 4);
            //this.pointIsTown_ExpRifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 1710000, 4);
            //this.pointIsTown_ExpRifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 2100000, 4);
            //this.pointIsTown_ExpRifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 1710000, 4);
            //this.pointIsTown_ExpRifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 2100000, 4);
            this.pointIsTown_DrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки обычной стойки с дробашом в городе               
            this.pointIsTown_DrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_VetDrobFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 2763000, 3);       //точки для проверки вет стойки с дробашом в городе            
            this.pointIsTown_VetDrobFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 7237000, 3);
            //this.pointIsTown_VetDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 2763000, 3);
            //this.pointIsTown_VetDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 7237000, 3);
            //this.pointIsTown_VetDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 2763000, 3);
            //this.pointIsTown_VetDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 7237000, 3);
            this.pointIsTown_ExpDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);       //точки для проверки эксп стойки с дробашом
            this.pointIsTown_ExpDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 3552000, 3);
            //this.pointIsTown_ExpDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 16777000, 3);
            //this.pointIsTown_ExpDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 3552000, 3);
            //this.pointIsTown_ExpDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 16777000, 3);
            //this.pointIsTown_ExpDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 3552000, 3);
            this.pointIsTown_JainaDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 4276000, 3);       //точки для проверки эксп стойки с дробашом Джейн
            this.pointIsTown_JainaDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 5395000, 3);
            this.pointIsTown_VetSabreFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 5131000, 3);       //точки для проверки вет стойки с саблей (повар)
            this.pointIsTown_VetSabreFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 9737000, 3);
            this.pointIsTown_ExpSwordFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 3684000, 3);       //точки для проверки эксп стойки с мечом (дарья)
            this.pointIsTown_ExpSwordFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 10263000, 3);
            this.pointIsTown_VetPistolFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 65000, 3);       //точки для проверки вет стойки с пистолетом Outrange
            this.pointIsTown_VetPistolFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 0, 0);
            this.pointIsTown_SightPistolFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 5066000, 3);       //точки для проверки вет стойки с пистолетом Sight Shot
            this.pointIsTown_SightPistolFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 9342000, 3);
            this.pointIsTown_UnlimPistolFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 15856000, 3);      //точки для проверки эксп стойки с пистолетами Unlimited Shot
            this.pointIsTown_UnlimPistolFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 15790000, 3);
            //пушка Миса
            this.pointIsTown_ExpCannonFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 8750000, 3);       //точки для проверки пушки Миса
            this.pointIsTown_ExpCannonFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 8158000, 3);

            #endregion

            #region Barack

            this.sdvigY = 0;
            this.pointMoveNow = new Point(651 - 5 + xx, 591 - 5 + yy);                        //выбор канала в меню Alt+F2
            this.pointisBarack1 = new PointColor(65 - 5 + xx, 153 - 5 + yy, 2400000, 5);            //зеленый цвет в слове Barracks  // не проверено
            this.pointisBarack2 = new PointColor(65 - 5 + xx, 154 - 5 + yy, 2400000, 5);            // проверено
            this.pointisBarack3 = new PointColor(36 - 5 + xx, 56 - 5 + yy, 15500000, 5);             //проверено   Barrack Mode
            this.pointisBarack4 = new PointColor(36 - 5 + xx, 57 - 5 + yy, 15100000, 5);             //проверено
            this.pointTeamSelection1 = new Point(140 - 5 + xx, 496 - 5 + yy);                   //проверено
            this.pointTeamSelection2 = new Point(70 - 5 + xx, 355 - 5 + yy);                   //проверено
            this.pointTeamSelection3 = new Point(50 - 5 + xx, 620 - 5 + yy);                   //проверено
            this.pointButtonLogoutFromBarack = new Point(785 - 5 + xx, 700 - 5 + yy);               //кнопка логаут в казарме
            this.pointChooseChannel = new Point(820 - 5 + xx, 382 - 5 + yy);                       //переход из меню Alt+Q в меню Alt+F2 (нажатие кнопки Choose a channel)
            this.pointEnterChannel = new Point(646 - 5 + xx, 409 - 5 + yy + (botwindow.getKanal() - 2) * 15);                        //выбор канала в меню Alt+F2
            this.pointNewPlace = new Point(85 + xx, 670 + yy);

            #endregion

            #region  новые боты

            this.pointNewName = new Point(490 - 5 + xx, 280 - 5 + yy);                             //строчка, куда надо вводить имя семьи
            this.pointButtonCreateNewName = new Point(465 - 5 + xx, 510 - 5 + yy);                 //кнопка Create для создания новой семьи
            this.pointCreateHeroes = new Point(800 - 5 + xx, 635 - 5 + yy);                        //кнопка Create для создания нового героя (перса)
            this.pointButtonOkCreateHeroes = new Point(520 - 5 + xx, 420 - 5 + yy);                //кнопка Ok для подтверждения создания героя
            this.pointMenuSelectTypeHeroes = new Point(810 - 5 + xx, 260 - 5 + yy);                //меню выбора типа героя в казарме
            this.pointSelectTypeHeroes = new Point(800 - 5 + xx, 320 - 5 + yy);                    //выбор мушкетера в меню типо героев в казарме
            this.pointNameOfHeroes = new Point(800 - 5 + xx, 180 - 5 + yy);                        //нажимаем на строчку, где вводится имя героя (перса)
            this.pointButtonCreateChar = new Point(450 - 5 + xx, 700 - 5 + yy);                    //нажимаем на зеленую кнопку создания нового перса
            this.pointSelectMusk = new Point(320 - 5 + xx, 250 - 5 + yy);                          //нажимаем на строчку, где вводится имя героя (перса)
            this.pointUnselectMedik = new Point(450 - 5 + xx, 250 - 5 + yy);                       //нажимаем на медика и выкидываем из команды
            this.pointNameOfTeam = new Point(30 - 5 + xx, 660 - 5 + yy);                           //нажимаем на строчку, где вводится имя команды героев (в казарме)
            this.pointButtonSaveNewTeam = new Point(190 - 5 + xx, 660 - 5 + yy);                   //нажимаем на кнопку сохранения команды (в казарме)

            this.pointRunNunies = new Point(920 - 5 + xx, 170 - 5 + yy);                           //нажимаем на зеленую стрелку, чтобы бежать к Нуньесу в Стартонии
            this.pointPressNunez = new Point(830 - 5 + xx, 340 - 5 + yy);                          //нажимаем на Нуньеса
            this.ButtonOkDialog = new Point(910 - 5 + xx, 680 - 5 + yy);                           //нажимаем на Ок в диалоге
            this.PressMedal = new Point(300 - 5 + xx, 210 - 5 + yy);                               //нажимаем на медаль
            this.ButtonCloseMedal = new Point(740 - 5 + xx, 600 - 5 + yy);                         //нажимаем на кнопку Close и закрываем медали
            this.pointPressNunez2 = new Point(700 - 5 + xx, 360 - 5 + yy);                         //нажимаем на Нуньеса после надевания медали

            this.town_begin = new AmericaTownReboldo(botwindow);                                   //город взят по умолчанию, как Ребольдо. 
            this.pointPressLindon1 = new Point(590 - 5 + xx, 210 - 5 + yy);                        //нажимаем на Линдона
            this.pointPressGMonMap = new Point(840 - 5 + xx, 235 - 5 + yy);                        //нажимаем на строчку GM на карте Alt+Z
            this.pointPressGM_1 = new Point(555 - 5 + xx, 425 - 5 + yy);                           //нажимаем на голову GM 
            this.pointPressSoldier = new Point(570 - 5 + xx, 315 - 5 + yy);                        //нажимаем на голову солдата
            this.pointFirstStringSoldier = new Point(520 - 5 + xx, 545 - 5 + yy);                  //нажимаем на первую строчку в диалоге
            this.pointRifle = new Point(380 - 5 + xx, 320 - 5 + yy);                               //нажимаем на ружье
            this.pointCoat = new Point(380 - 5 + xx, 345 - 5 + yy);                                //нажимаем на плащ
            this.pointButtonPurchase = new Point(740 - 5 + xx, 590 - 5 + yy);                      //нажимаем на кнопку купить
            this.pointButtonCloseSoldier = new Point(860 - 5 + xx, 590 - 5 + yy);                  //нажимаем на кнопку Close
            this.pointButtonYesSoldier = new Point(470 - 5 + xx, 430 - 5 + yy);                    //нажимаем на кнопку Yes
            this.pointFirstItem = new Point(35 - 5 + xx, 210 - 5 + yy);                            //нажимаем дважды на первую вещь в спецкармане
            this.pointDomingoOnMap = new Point(820 - 5 + xx, 115 - 5 + yy);                        //нажимаем на Доминго на карте Alt+Z
            this.pointPressDomingo = new Point(508 - 5 + xx, 404 - 5 + yy);                        //нажимаем на Доминго
            this.pointFirstStringDialog = new Point(520 - 5 + xx, 660 - 5 + yy);                   //нажимаем Yes в диалоге Доминго (нижняя строчка)
            this.pointSecondStringDialog = new Point(520 - 5 + xx, 640 - 5 + yy);                  //нажимаем Yes в диалоге Доминго второй раз (вторая строчка снизу)
            this.pointDomingoMiss = new Point(396 - 5 + xx, 206 - 5 + yy);                         //нажимаем правой кнопкой по карте миссии Доминго
            this.pointPressDomingo2 = new Point(572 - 5 + xx, 237 - 5 + yy);                       //нажимаем на Доминго после миссии
            this.pointLindonOnMap = new Point(820 - 5 + xx, 370 - 5 + yy);                         //нажимаем на Линдона на карте Alt+Z
            this.pointPressLindon2 = new Point(652 - 5 + xx, 257 - 5 + yy);                        //нажимаем на Линдона
            this.pointPetExpert = new Point(908 - 5 + xx, 423 - 5 + yy);                           //нажимаем на петэксперта
            this.pointPetExpert2 = new Point(815 - 5 + xx, 425 - 5 + yy);                          //нажимаем на петэксперта второй раз 
            this.pointThirdBookmark = new Point(842 - 5 + xx, 150 - 5 + yy);                       //тыкнули в третью закладку в кармане
            this.pointNamePet = new Point(440 - 5 + xx, 440 - 5 + yy);                             //нажимаем на строку, где вводить имя пета
            this.pointButtonNamePet = new Point(520 - 5 + xx, 495 - 5 + yy);                       //тыкнули в кнопку Raise Pet
            this.pointButtonClosePet = new Point(520 - 5 + xx, 535 - 5 + yy);                      //тыкнули в кнопку Close
            this.pointWayPointMap = new Point(820 - 5 + xx, 430 - 5 + yy);                         //тыкнули в строчку телепорт на карте Ребольдо
            this.pointWayPoint = new Point(665 - 5 + xx, 345 - 5 + yy);                            //тыкнули в телепорт
            this.pointBookmarkField = new Point(220 - 5 + xx, 200 - 5 + yy);                       //закладка Field в телепорте
            this.pointButtonLavaPlato = new Point(820 - 5 + xx, 320 - 5 + yy);                     //кнопка лавовое плато в телепорте
            
            #endregion

            #region кратер
            this.pointGateCrater = new Point(373 - 5 + xx, 605 - 5 + yy);                          //переход (ворота) из лавового плато в кратер
            this.pointMitridat = new Point(800 - 5 + xx, 180 - 5 + yy);                            //митридат в кармане
            this.pointMitridatTo2 = new Point(30 - 5 + xx, 140 - 5 + yy);                          //ячейка, где должен лежать митридат
            this.pointBookmark3 = new Point(155 - 5 + xx, 180 - 5 + yy);                           //третья закладка в спецкармане
            this.pointButtonYesPremium = new Point(470 - 5 + xx, 415 - 5 + yy);                    //третья закладка в спецкармане
            this.pointSecondBookmark = new Point(870 - 5 + xx, 150 - 5 + yy);                      //вторая закладка в кармане

            this.pointWorkCrater = new Point(botwindow.getTriangleX()[0] + xx, botwindow.getTriangleY()[0] + yy);     //бежим на место работы
            this.pointButtonSaveTeleport = new Point(440 - 5 + xx, 570 - 5 + yy);                   // нажимаем на кнопку сохранения телепорта в текущей позиции
            this.pointButtonOkSaveTeleport = new Point(660 - 5 + xx, 645 - 5 + yy);               // нажимаем на кнопку OK для подтверждения сохранения телепорта 
            this.pointPetBegin = new Point(855 + 39 - 5 + xx, 180 - 5 + yy);    // 
            this.pointPetEnd = new Point(520 - 5 + xx, 330 - 5 + yy);    // 
            #endregion

            #region заточка Ида 
            this.pointAcriveInventory = new Point(905 - 5 + xx, 425 - 5 + yy);
            this.pointIsActiveInventory = new PointColor(696 - 5 + xx, 146 - 5 + yy, 16000000, 6);
            this.pointisMoveEquipment1 = new PointColor(493 - 5 + xx, 281 - 5 + yy, 7400000, 5);
            this.pointisMoveEquipment2 = new PointColor(493 - 5 + xx, 282 - 5 + yy, 7400000, 5);
            this.pointButtonEnhance = new Point(525 - 5 + xx, 625 - 5 + yy);
            this.pointIsPlus41 = new PointColor(469 - 5 + xx, 461 - 5 + yy, 15700000, 5);
            this.pointIsPlus42 = new PointColor(470 - 5 + xx, 462 - 5 + yy, 16700000, 5);
            this.pointIsPlus43 = new PointColor(469 - 5 + xx, 489 - 5 + yy, 15700000, 5);
            this.pointIsPlus44 = new PointColor(470 - 5 + xx, 490 - 5 + yy, 16700000, 5);
            this.pointAddShinyCrystall = new Point(472 - 5 + xx, 487 - 5 + yy);                                   //max button
            this.pointIsAddShinyCrystall1 = new PointColor(653 - 5 + xx, 316 - 5 + yy, 15000000, 5);
            this.pointIsAddShinyCrystall2 = new PointColor(654 - 5 + xx, 316 - 5 + yy, 15000000, 5);
            this.pointIsIda1 = new PointColor(487 - 5 + xx, 143 - 5 + yy, 16700000, 5);
            this.pointIsIda2 = new PointColor(487 - 5 + xx, 144 - 5 + yy, 16700000, 5);
            #endregion

            #region чиповка

            this.pointIsEnchant1 = new PointColor(513 - 5 + xx, 189 - 5 + yy, 13000000, 5);
            this.pointIsEnchant2 = new PointColor(514 - 5 + xx, 189 - 5 + yy, 13000000, 5);
            this.pointisWeapon1 = new PointColor(584 - 5 + xx, 365 - 5 + yy, 10700000, 5);
            this.pointisWeapon2 = new PointColor(585 - 5 + xx, 366 - 5 + yy, 10700000, 5);
            this.pointisArmor1 = new PointColor(586 - 5 + xx, 367 - 5 + yy, 6100000, 5);
            this.pointisArmor2 = new PointColor(586 - 5 + xx, 373 - 5 + yy, 6100000, 5);
            this.pointMoveLeftPanelBegin = new Point(161 - 5 + xx, 130 - 5 + yy);
            this.pointMoveLeftPanelEnd = new Point(161 - 5 + xx, 592 - 5 + yy);
            this.pointButtonEnchance = new Point(630 - 5 + xx, 490 - 5 + yy);
            this.pointisDef15 = new PointColor(388 - 5 + xx, 247 - 5 + yy, 12000000, 6);  //проверено
            this.pointisHP1 = new PointColor(355 - 5 + xx, 277 - 5 + yy, 7000000, 6);     //проверено
            this.pointisHP2 = new PointColor(355 - 5 + xx, 292 - 5 + yy, 7000000, 6);     //проверено
            this.pointisHP3 = new PointColor(355 - 5 + xx, 307 - 5 + yy, 7000000, 6);     //проверено
            this.pointisHP4 = new PointColor(355 - 5 + xx, 322 - 5 + yy, 7000000, 6);     //проверено

            this.pointisAtk401 = new PointColor(373 - 5 + xx, 247 - 5 + yy, 13300000, 5);   //проверено
            this.pointisAtk402 = new PointColor(373 - 5 + xx, 256 - 5 + yy, 13700000, 5);   //проверено
            this.pointisSpeed30 = new PointColor(390 - 5 + xx, 269 - 5 + yy, 15500000, 5);   //проверено

            this.pointisAtk391 = new PointColor(378 - 5 + xx, 252 - 5 + yy, 14800000, 5);  //проверено
            this.pointisAtk392 = new PointColor(381 - 5 + xx, 252 - 5 + yy, 13300000, 5);  //проверено
            this.pointisSpeed291 = new PointColor(394 - 5 + xx, 267 - 5 + yy, 14800000, 5);   //проверено
            this.pointisSpeed292 = new PointColor(397 - 5 + xx, 267 - 5 + yy, 13300000, 5);   //проверено

            this.pointisAtk381 = new PointColor(378 - 5 + xx, 251 - 5 + yy, 14600000, 5);   //проверено
            this.pointisAtk382 = new PointColor(381 - 5 + xx, 251 - 5 + yy, 13500000, 5);   //проверено
            this.pointisSpeed281 = new PointColor(394 - 5 + xx, 266 - 5 + yy, 14600000, 5);  //проверено
            this.pointisSpeed282 = new PointColor(397 - 5 + xx, 266 - 5 + yy, 13500000, 5);  //проверено

            this.pointisAtk371 = new PointColor(377 - 5 + xx, 247 - 5 + yy, 14300000, 5);    //проверено
            this.pointisAtk372 = new PointColor(382 - 5 + xx, 247 - 5 + yy, 14600000, 5);    //проверено
            this.pointisSpeed271 = new PointColor(393 - 5 + xx, 262 - 5 + yy, 14300000, 5);  //проверено
            this.pointisSpeed272 = new PointColor(398 - 5 + xx, 262 - 5 + yy, 14600000, 5);  //проверено

            this.pointisWild41 = new PointColor(415 - 5 + xx, 292 - 5 + yy, 7900000, 5);   //проверено
            this.pointisWild42 = new PointColor(415 - 5 + xx, 301 - 5 + yy, 7900000, 5);   //проверено
            this.pointisWild51 = new PointColor(415 - 5 + xx, 307 - 5 + yy, 7900000, 5);   //проверено
            this.pointisWild52 = new PointColor(415 - 5 + xx, 316 - 5 + yy, 7900000, 5);   //проверено
            this.pointisWild61 = new PointColor(415 - 5 + xx, 322 - 5 + yy, 7900000, 5);   //проверено
            this.pointisWild62 = new PointColor(415 - 5 + xx, 331 - 5 + yy, 7900000, 5);   //проверено

            this.pointisHuman41 = new PointColor(403 - 5 + xx, 292 - 5 + yy, 7600000, 5);   //проверено
            this.pointisHuman42 = new PointColor(403 - 5 + xx, 301 - 5 + yy, 7700000, 5);   //проверено
            this.pointisHuman51 = new PointColor(403 - 5 + xx, 307 - 5 + yy, 7600000, 5);   //проверено
            this.pointisHuman52 = new PointColor(403 - 5 + xx, 316 - 5 + yy, 7700000, 5);   //проверено
            this.pointisHuman61 = new PointColor(403 - 5 + xx, 322 - 5 + yy, 7600000, 5);   //проверено
            this.pointisHuman62 = new PointColor(403 - 5 + xx, 331 - 5 + yy, 7700000, 5);   //проверено

            this.pointisDemon41 = new PointColor(398 - 5 + xx, 292 - 5 + yy, 7900000, 5);   //проверено
            this.pointisDemon42 = new PointColor(399 - 5 + xx, 292 - 5 + yy, 7900000, 5);   //проверено
            this.pointisDemon51 = new PointColor(398 - 5 + xx, 307 - 5 + yy, 7900000, 5);   //проверено
            this.pointisDemon52 = new PointColor(399 - 5 + xx, 307 - 5 + yy, 7900000, 5);   //проверено
            this.pointisDemon61 = new PointColor(398 - 5 + xx, 322 - 5 + yy, 7900000, 5);   //проверено
            this.pointisDemon62 = new PointColor(399 - 5 + xx, 322 - 5 + yy, 7900000, 5);   //проверено

            this.pointisUndead41 = new PointColor(397 - 5 + xx, 292 - 5 + yy, 7100000, 5);   //проверено
            this.pointisUndead42 = new PointColor(397 - 5 + xx, 293 - 5 + yy, 7400000, 5);   //проверено
            this.pointisUndead51 = new PointColor(397 - 5 + xx, 307 - 5 + yy, 7100000, 5);   //проверено
            this.pointisUndead52 = new PointColor(397 - 5 + xx, 308 - 5 + yy, 7400000, 5);   //проверено
            this.pointisUndead61 = new PointColor(397 - 5 + xx, 322 - 5 + yy, 7100000, 5);   //проверено
            this.pointisUndead62 = new PointColor(397 - 5 + xx, 323 - 5 + yy, 7400000, 5);   //проверено

            this.pointisLifeless41 = new PointColor(398 - 5 + xx, 292 - 5 + yy, 7500000, 5);   //проверено
            this.pointisLifeless42 = new PointColor(398 - 5 + xx, 301 - 5 + yy, 7600000, 5);   //проверено
            this.pointisLifeless51 = new PointColor(398 - 5 + xx, 307 - 5 + yy, 7500000, 5);   //проверено
            this.pointisLifeless52 = new PointColor(398 - 5 + xx, 316 - 5 + yy, 7600000, 5);   //проверено
            this.pointisLifeless61 = new PointColor(398 - 5 + xx, 322 - 5 + yy, 7500000, 5);   //проверено
            this.pointisLifeless62 = new PointColor(398 - 5 + xx, 331 - 5 + yy, 7600000, 5);   //проверено

            #endregion

            #region передача песо торговцу
            this.pointPersonalTrade1 = new PointColor(472 - 5 + xx, 251 - 5 + yy, 12800000, 5);
            this.pointPersonalTrade2 = new PointColor(472 - 5 + xx, 252 - 5 + yy, 12800000, 5);

            this.pointTrader = new Point(472 - 5 + xx, 175 - 5 + yy);
            this.pointPersonalTrade = new Point(536 - 5 + xx, 203 - 5 + yy);
            this.pointMap = new Point(405 - 5 + xx, 220 - 5 + yy);

            this.pointVis1 = new Point(903 - 5 + xx, 151 - 5 + yy);
            this.pointVisMove1 = new Point(701 - 5 + xx, 186 - 5 + yy);
            this.pointVisMove2 = new Point(395 - 5 + xx, 361 - 5 + yy);

            this.pointFood = new Point(361 - 5 + xx, 331 - 5 + yy);     //шаг = 27 пикселей на одну строчку магазина (на случай если добавят новые строчки)
            this.pointButtonFesoBUY = new Point(730 - 5 + xx, 625 - 5 + yy);
            this.pointArrowUp2 = new Point(379 - 5 + xx, 250 - 5 + yy);
            this.pointButtonFesoSell = new Point(730 - 5 + xx, 625 - 5 + yy);
            this.pointBookmarkFesoSell = new Point(245 - 5 + xx, 201 - 5 + yy);
            this.pointDealer = new Point(405 - 5 + xx, 210 - 5 + yy);

            this.pointYesTrade = new Point(1161 - 700 + xx, 595 - 180 + yy);
            this.pointBookmark4 = new Point(903 - 5 + xx, 151 - 5 + yy);
            this.pointFesoBegin = new Point(740 - 5 + xx, 186 - 5 + yy);
            this.pointFesoEnd = new Point(388 - 5 + xx, 343 - 5 + yy);
            this.pointOkSum = new Point(611 - 5 + xx, 397 - 5 + yy);
            this.pointOk = new Point(441 - 5 + xx, 502 - 5 + yy);
            this.pointTrade = new Point(522 - 5 + xx, 502 - 5 + yy);

            #endregion

            #region Undressing in Barack

                this.pointShowEquipment = new Point(145 - 5 + xx, 442 - 5 + yy);
                //this.pointBarack1 = new Point( 80 - 5 + xx, 152 - 5 + yy);
                //this.pointBarack2 = new Point(190 - 5 + xx, 152 - 5 + yy);
                //this.pointBarack3 = new Point( 80 - 5 + xx, 177 - 5 + yy);
                //this.pointBarack4 = new Point(190 - 5 + xx, 177 - 5 + yy);

                this.pointBarack[1] = new Point(80 - 5 + xx, 152 - 5 + yy);
                this.pointBarack[2] = new Point(190 - 5 + xx, 152 - 5 + yy);
                this.pointBarack[3] = new Point( 80 - 5 + xx, 177 - 5 + yy);
                this.pointBarack[4] = new Point(190 - 5 + xx, 177 - 5 + yy);

                this.pointEquipment1 = new PointColor(300 - 5 + xx, 60 - 5 + yy, 12600000, 5);
                this.pointEquipment2 = new PointColor(302 - 5 + xx, 60 - 5 + yy, 12600000, 5);
            #endregion


        }


        //==================================== Методы ===================================================

        #region общие методы 2

        /// <summary>
        /// путь к исполняемому файлу игры (сервер сингапур)
        /// </summary>
        /// <returns></returns>
        private String path_Client()
        { return File.ReadAllText(KATALOG_MY_PROGRAM + "\\America2_path.txt"); }

        /// <summary>
        /// считываем параметр, отвечающий за то, надо ли загружать окна на сервере сингапур
        /// </summary>
        /// <returns></returns>
        private bool America2Active()
        { return bool.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\America2_active.txt")); }


        #endregion

        #region No window

        /// <summary>
        /// Определяет, надо ли грузить данное окно с ботом
        /// </summary>
        /// <returns> true означает, что это окно (данный бот) должно быть активно и его надо грузить </returns>
        public override bool isActive()
        {
            //bool result = false;
            //if (America2Active() == 1) result = true;
            //return result;
            return America2Active();
        }

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна"
        /// </summary>
        /// <returns></returns>
        public override UIntPtr FindWindowGE()
        {
            UIntPtr HWND = (UIntPtr)0;

            int count = 0;
            while (HWND == (UIntPtr)0)
            {
                Pause(500);
                HWND = FindWindow("Sandbox:" + botwindow.getNumberWindow().ToString() + ":Granado Espada", "[#] Granado Espada [#]");

                count++; if (count > 5) return (UIntPtr)0;
            }

            botwindow.setHwnd(HWND);

            SetWindowPos(HWND, 0, xx, yy, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            //            ShowWindow(HWND, 2);   //скрыть окно в трей
            Pause(500);

            return HWND;
        }

        /// <summary>
        /// проверяю белый цвет в загружающемся окне
        /// </summary>
        /// <returns></returns>
        public bool isWhiteWindow()
        {
            iPointColor pointisWhiteWindow = new PointColor(1000, 500, 16700000, 5);            //проверяю белый цвет в загружающемся окне
            return pointisWhiteWindow.isColor();
        }
        /// <summary>
        /// запуск клиента игры
        /// </summary>
        public override void runClient()
        {
            #region для песочницы

            iPoint pointOkSafeIP = new Point(966, 582);
            iPoint pointOkReklamaSteam = new Point(1251, 894);
            iPoint pointRunGE = new Point(1263, 584);
            iPoint pointCloseSteam = new Point(1897, 397);

            //запускаем steam в песочнице
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files\Sandboxie\Start.exe";
            process.StartInfo.Arguments = @"/box:" + botwindow.getNumberWindow() + " " + path_Client() + " -applaunch 663090 -silent";
            process.Start();

            //while ((!isWhiteWindow()) && (!isSafeIP()))
            //{
            //    Pause(2000);
            //}
            //if (isSafeIP())
            //{
            //    pointOkSafeIP.PressMouseL();       //тыкаем в Ок и закрываем сообщение об ошибке
            //}
            Pause(30000);

            //if (isSafeIP())
            //{
            //    pointOkSafeIP.PressMouse();       //тыкаем в Ок и закрываем сообщение об ошибке
            //}

            //int i = 0;
            //while (true)
            //{
            //    Pause(2000);
            //    if (this.isSafeIP())
            //    {
            //        pointOkSafeIP.PressMouseL();       //тыкаем в Ок и закрываем сообщение об ошибке
            //        break;
            //    }
            //    i++; if (i > 50) break;
            //}


            //if (isReklamaSteam())
            //{
            //    pointOkReklamaSteam.PressMouseL();   //закрываем рекламу стим
            //    Pause(5000);
            //}

            //pointRunGE.PressMouseL();            //нажимаем на кнопку запуска ГЭ
            //Pause(1000);

            //pointCloseSteam.PressMouseL();      //закрываем крестиком окно Steam и ждем открытия окна ГЭ
            //Pause(60000);

            #endregion

            #region для чистого окна
            //Process.Start(getPathClient());                             //запускаем саму игру или бот Catzmods
            //botwindow.Pause(10000);
            #endregion

            #region если CatzMods
            //Process.Start(getPathClient());                                    //запускаем саму игру или бот Catzmods
            //Pause(10000);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1110, 705, 1);        //нажимаем кнопку "старт" в боте      
            //Pause(500);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1222, 705, 1);        //нажимаем кнопку "Close" в боте
            #endregion

        }

        /// <summary>
        /// действия для оранжевой кнопки
        /// </summary>
        public override void OrangeButton()
        {
            botwindow.ReOpenWindow();
            Pause(100);
        }



        #endregion

        #region LogOut

        /// <summary>
        /// выбираем сервер путем нажатия на первую строчку в меню
        /// </summary>
        public override void serverSelection()
        {
            iPoint pointserverSelection = new Point(480 - 5 + xx, 370 - 5 + yy);
            pointserverSelection.PressMouseLL();
            Pause(500);
        }

        #endregion

        #region at Work

        /// <summary>
        /// Открываем инвентарь (Alt+V)
        /// </summary>
        private void OpenInventory()
        {
            TopMenu(8, 1);
            Pause(1000);
        }

        /// <summary>
        /// проверяем, находится ли в инвентае 248 вещей 
        /// </summary>
        /// <returns></returns>
        public override bool is248Items()
        {
            bool result = false;
            iPointColor pointis248_1 = new PointColor(684 - 5 + xx, 561 - 5 + yy, 12000000, 6);
            iPointColor pointis248_2 = new PointColor(694 - 5 + xx, 561 - 5 + yy, 12000000, 6);
            iPointColor pointis248_3 = new PointColor(703 - 5 + xx, 559 - 5 + yy, 12000000, 6);

            OpenInventory();

            //проверяем, что в инвентаре 248 вещей
            result = (pointis248_1.isColor()) && (pointis248_2.isColor()) && (pointis248_3.isColor());

            botwindow.PressEscThreeTimes();

            return result;
        }

        #endregion

        #region Top Menu

        /// <summary>
        /// нажмает на выбранный раздел верхнего меню 
        /// </summary>
        /// <param name="numberOfThePartitionMenu"> ноиер раздела верхнего меню </param>
        public override void TopMenu(int numberOfThePartitionMenu)
        {
            //int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
//            int[] MenukoordX = { 283, 316, 349, 382, 415, 453, 500, 547, 588, 620, 653, 683, 715, 748 };
            int[] MenukoordX = { 305, 339, 371, 402, 435, 475, 522, 570, 610, 642, 675, 705, 738 };

            int x = MenukoordX[numberOfThePartitionMenu - 1];
            int y = 55;
            iPoint pointMenu = new Point(x - 5 + xx, y - 5 + yy);

            int count = 0;
            do
            {
                pointMenu.PressMouse();
                botwindow.Pause(2000);
                count++; if (count > 3) break;
            } while (!isOpenTopMenu(numberOfThePartitionMenu));
        }

        /// <summary>
        /// нажать на выбранный раздел верхнего меню, а далее на пункт раскрывшегося списка
        /// </summary>
        /// <param name="numberOfThePartitionMenu"></param>
        /// <param name="punkt"></param>
        public override void TopMenu(int numberOfThePartitionMenu, int punkt)
        {
            //          int[] numberOfPunkt = { 0, 8, 4, 5, 0, 3, 2, 6, 9, 0, 0, 0, 0, 0 };
            int[] numberOfPunkt = { 0, 8, 4, 2, 0, 3, 2, 6, 9, 0, 0, 0, 0, 0 };
            //          int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
//          int[] MenukoordX = { 283, 316, 349, 382, 415, 453, 500, 547, 588, 620, 653, 683, 715, 748 };
            int[] MenukoordX = { 305, 339, 371, 402, 435, 475, 522, 570, 610, 642, 675, 705, 738 };

            int[] FirstPunktOfMenuKoordY = { 0, 85, 85, 85, 0, 97, 97, 97, 85, 0, 0, 0, 0 };

            if (punkt <= numberOfPunkt[numberOfThePartitionMenu - 1])
            {
                int x = MenukoordX[numberOfThePartitionMenu - 1] + 25;
                int y = FirstPunktOfMenuKoordY[numberOfThePartitionMenu - 1] + 25 * (punkt - 1);
                iPoint pointMenu = new Point(x - 5 + xx, y - 5 + yy);

                TopMenu(numberOfThePartitionMenu);   //сначала открываем раздел верхнего меню (1-14)
                Pause(500);
                pointMenu.PressMouse();  //выбираем конкретный пункт подменю (раскрывающийся список)
            }
        }

        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public override void TeleportToTownAltW(int nomerTeleport)
        {
            iPoint pointNotToShoot = new Point(300 - 5 + xx, 300 - 5 + yy);
            iPoint pointNotToShoot2 = new Point(350 - 5 + xx, 350 - 5 + yy);
            iPoint pointTeleportToTownAltW;
            if (botwindow.getNomerTeleport() < 14)
            {
                pointTeleportToTownAltW = new Point(800 + xx, 517 + yy + (nomerTeleport - 1) * 17);
            }
            else
            {
                pointTeleportToTownAltW = new Point(800 + xx, 517 + yy);   //ребольдо
            }

            // отбегаю в сторону. чтобы бот не стрелял  
            pointNotToShoot.DoubleClickL();
            botwindow.Pause(3000);
            pointNotToShoot2.DoubleClickL();
            botwindow.Pause(3000);

            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            botwindow.Pause(2000);
        }

        #endregion

        #region заточка

        /// <summary>
        /// переносим (DragAndDrop) одну из частей экипировки на место для заточки
        /// </summary>
        /// <param name="numberOfEquipment">номер экипировки п/п</param>
        public override void MoveToSharpening(int numberOfEquipment)
        {
            pointAcriveInventory.PressMouseR();
            Pause(500);
            iPoint pointEquipmentBegin = new Point(701 - 5 + xx + (numberOfEquipment - 1) * 39, 183 - 5 + yy);
            iPoint pointEquipmentEnd = new Point(521 - 5 + xx, 208 - 5 + yy);
            pointEquipmentBegin.Drag(pointEquipmentEnd);
        }

        #endregion

        #region чиповка

        /// <summary>
        /// переносим (DragAndDrop) одну из частей экипировки на место для чиповки
        /// </summary>
        /// <param name="numberOfEquipment">номер экипировки п/п</param>
        public override void MoveToNintendo(int numberOfEquipment)
        {
            iPoint pointEquipmentBegin = new Point(701 - 5 + xx + (numberOfEquipment - 1) * 39, 183 - 5 + yy);
            iPoint pointEquipmentEnd = new Point(631 - 5 + xx, 367 - 5 + yy);
            pointEquipmentBegin.Drag(pointEquipmentEnd);
        }

        #endregion

    }
}

