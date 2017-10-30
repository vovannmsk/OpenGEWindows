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
    /// Класс описывает процесс перехода к торговле от фарма , начиная от проверки необходимости продажи и заканчивая закрытием окна с ботом (для сервера Америка)
    /// </summary>
    public class ServerAmerica : ServerInterface 
    {
        //int xx;
        //int yy;
        //private Town town;
        //private botWindow botwindow;
        //private TownFactory townFactory;
        //public const String KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";
        //private iPointColor pointIsSale1; 
        //private iPointColor pointIsSale2;
        //private iPointColor pointIsSale21;
        //private iPointColor pointIsSale22;
        //private iPointColor pointIsClickSale1; 
        //private iPointColor pointIsClickSale2;
        //private iPointColor pointIsTown11;
        //private iPointColor pointIsTown12;
        //private iPointColor pointIsTown21;
        //private iPointColor pointIsTown22;
        //private iPointColor pointIsTown31;
        //private iPointColor pointIsTown32;
        //private iPointColor pointisBoxOverflow1;
        //private iPointColor pointisBoxOverflow2;
        //private iPointColor pointisSummonPet1;
        //private iPointColor pointisSummonPet2;
        //private iPointColor pointisActivePet1;
        //private iPointColor pointisActivePet2;
        //private iPointColor pointisLogout1;
        //private iPointColor pointisLogout2;
        //private iPointColor pointisBarack1;
        //private iPointColor pointisBarack2;
        //private iPointColor pointisWork1;
        //private iPointColor pointisWork2;
        //private iPointColor pointisOpenMenuPet1;
        //private iPointColor pointisOpenMenuPet2;
        //private iPointColor pointisOpenTopMenu21;
        //private iPointColor pointisOpenTopMenu22;
        //private iPointColor pointisOpenTopMenu61;
        //private iPointColor pointisOpenTopMenu62;
        //private iPointColor pointisOpenTopMenu81;
        //private iPointColor pointisOpenTopMenu82;
        //private iPointColor pointisOpenTopMenu91;
        //private iPointColor pointisOpenTopMenu92;
        //private iPointColor pointisOpenTopMenu121;
        //private iPointColor pointisOpenTopMenu122;
        //private iPointColor pointisOpenTopMenu131;
        //private iPointColor pointisOpenTopMenu132;


        //private iPoint pointBuyingMitridat1;
        //private iPoint pointBuyingMitridat2;
        //private iPoint pointBuyingMitridat3;
        //private iPoint pointGotoEnd;

        //private iPoint pointTeamSelection1;
        //private iPoint pointTeamSelection2;
        //private iPoint pointTeamSelection3;
        //private iPoint pointTeleport1;
        //private iPoint pointTeleport2;
        //private iPoint pointCancelSummonPet;
        //private iPoint pointSummonPet1;
        //private iPoint pointSummonPet2;
        //private iPoint pointTeleportToTownAltW;
        //private iPoint pointActivePet;

        //private int sdvigY;

        //private String pathClient;
        //private int activeWindow;

        /// <summary>
        /// конструктор
        /// town отвечает за методы для конкретного города (паттерн Стратегия). Все различия в действиях, зависящих от города, инкапсулированы в семействе классов Town (в т.ч. AmericaTown)
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public ServerAmerica(botWindow botwindow) 
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            this.townFactory = new AmericaTownFactory (botwindow);                                     // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();                                                      // выбирается город с помощью фабрики
            this.pathClient = path_Client();
            this.activeWindow = active_window();
            this.pointIsSale1 = new PointColor(902 + xx, 673 + yy, 7850000, 4);          
            this.pointIsSale2 = new PointColor(903 + xx, 673 + yy, 7850000, 4);
            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7390000, 4); 
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7390000, 4);
            this.pointIsClickSale1 = new PointColor(735 - 5 + xx, 665 - 5 + yy, 7390000, 4); 
            this.pointIsClickSale2 = new PointColor(735 - 5 + xx, 664 - 5 + yy, 7390000, 4);

            this.pointIsTown11 = new PointColor(24 + xx, 692 + yy, 11053000, 3);        //точки для проверки стойки с ружьем
            this.pointIsTown12 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown21 = new PointColor(279 + xx, 692 + yy, 11053000, 3);       
            this.pointIsTown22 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown31 = new PointColor(534 + xx, 692 + yy, 11053000, 3);      
            this.pointIsTown32 = new PointColor(535 + xx, 692 + yy, 10921000, 3);

            this.pointIsTown_11 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с дробашом
            this.pointIsTown_12 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_21 = new PointColor(279 + xx, 692 + yy, 7631000, 3);   
            this.pointIsTown_22 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_31 = new PointColor(534 + xx, 692 + yy, 7631000, 3);   
            this.pointIsTown_32 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_11a = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки обычной стойки с дробашом в городе
            this.pointIsTown_12a = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_21a = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_22a = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_31a = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_32a = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown__11 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с ружьем
            this.pointIsTown__12 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown__21 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown__22 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown__31 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown__32 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointisBoxOverflow1 = new PointColor(518 + xx, 432 + yy, 7800000, 5);      //548 - 30, 462 - 30, 7800000, 547 - 30, 458 - 30, 7600000, 5);
            this.pointisBoxOverflow2 = new PointColor(517 + xx, 428 + yy, 7600000, 5);
            this.pointisSummonPet1 = new PointColor(401 - 5 + xx, 362 - 5 + yy, 7630000, 4);      //401 - 5, 362 - 5, 7630000, 401 - 5, 364 - 5, 7560000, 4);
            this.pointisSummonPet2 = new PointColor(401 - 5 + xx, 364 - 5 + yy, 7560000, 4);
            this.pointisActivePet1 = new PointColor(495 - 5 + xx, 310 - 5 + yy, 13200000, 5);      //495 - 5, 310 - 5, 13200000, 496 - 5, 308 - 5, 13600000, 5);
            this.pointisActivePet2 = new PointColor(496 - 5 + xx, 308 - 5 + yy, 13600000, 5);
            this.pointisActivePet3 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13000000, 5);     //для америки пока не нужно
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13100000, 5);     //для америки пока не нужно. еда на месяц
            this.pointisLogout1 = new PointColor(126 - 5 + xx, 66 - 5 + yy, 7460000, 4);      //121, 62, 7460000, 135, 61, 7590000, 4)
            this.pointisLogout2 = new PointColor(126 - 5 + xx, 67 - 5 + yy, 7460000, 4);      //121, 62, 7460000, 135, 61, 7590000, 4)
            this.pointisBarack1 = new PointColor(56 + xx, 146 + yy, 2420000, 4);      //61 - 5, 151 - 5, 2420000, 61 - 5, 154 - 5, 2420000, 4);
            this.pointisBarack2 = new PointColor(56 + xx, 149 + yy, 2420000, 4);
            this.pointisBarack3 = new PointColor(81 - 5 + xx, 63 - 5 + yy, 7700000, 5);       //проверено
            this.pointisBarack4 = new PointColor(81 - 5 + xx, 64 - 5 + yy, 7700000, 5);
            this.pointisWork1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);               //проверка по стойке с ружьем
            this.pointisWork2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            this.pointisWork_1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork__1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            this.pointisWork__2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);

            this.pointisOpenMenuPet1 = new PointColor(466 + xx, 214 + yy, 12500000, 5);      //471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
            this.pointisOpenMenuPet2 = new PointColor(467 + xx, 214 + yy, 12500000, 5);

            this.pointisOpenTopMenu21 = new PointColor(328 + xx, 74 + yy, 13420000, 4);      //333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);  //проверено
            this.pointisOpenTopMenu22 = new PointColor(329 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu61 = new PointColor(455 + xx, 87 + yy, 13420000, 4);      //460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);  //проверено
            this.pointisOpenTopMenu62 = new PointColor(456 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu81 = new PointColor(553 + xx, 87 + yy, 13420000, 4);      //558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);  //проверено
            this.pointisOpenTopMenu82 = new PointColor(554 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu91 = new PointColor(601 + xx, 74 + yy, 13420000, 4);      //606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);  //проверено
            this.pointisOpenTopMenu92 = new PointColor(602 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu121 = new PointColor(406 + xx, 166 + yy, 7590000, 4);      //411 - 5, 171 - 5, 7590000, 412 - 5, 171 - 5, 7850000, 4);  //проверено
            this.pointisOpenTopMenu122 = new PointColor(407 + xx, 166 + yy, 7850000, 4);
            this.pointisOpenTopMenu131 = new PointColor(366 + xx, 273 + yy, 16310000, 4);      //371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);  //проверено
            this.pointisOpenTopMenu132 = new PointColor(367 + xx, 273 + yy, 16510000, 4);

            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);
            //this.pointGotoEnd = new Point(680 + xx, 462 + yy);
            this.pointGotoEnd = new Point(680 + xx, 442 - 5 + yy);          //логаут
            this.pointTeamSelection1 = new Point(135 + xx, 450 + yy);    //кнопка выбора групп в бараке
            this.pointTeamSelection2 = new Point(65 + xx, 355 + yy);
            this.pointTeamSelection3 = new Point(65 + xx, 620 + yy);

            this.pointTeleport1 = new Point(400 + xx, 190 + yy);  //400, 190 
            this.pointTeleport2 = new Point(355 + xx, 570 + yy);   //355, 570
            this.pointCancelSummonPet = new Point(408 + xx, 390 + yy);  //400, 190 
            this.pointSummonPet1 = new Point(569 + xx, 375 + yy);                   // 569, 375   //Click Pet
            this.pointSummonPet2 = new Point(408 + xx, 360 + yy);                   // 408, 360   //Click кнопку "Summon"
            this.pointActivePet = new Point(408 + xx, 405 + yy);                   // 408, 405);  //Click Button Active Pet
            this.pointTeleportToTownAltW = new Point(801 + xx, 564 + yy + (botwindow.getNomerTeleport() - 1) * 17);  


            this.sdvigY = 0;

            this.pointBookmarkSell = new Point(225 + xx, 163 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonClose = new Point(847 + xx, 663 + yy);   //847, 663);
            this.pointisKillHero1 = new PointColor(75 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero2 = new PointColor(330 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero3 = new PointColor(585 + xx, 631 + yy, 1900000, 4);
            this.pointButtonLogOut = new Point(955 - 5 + xx, 700 - 5 + yy);                        //кнопка логаут в казарме

            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 14600000, 5);           //проверяем открыто ли окно с токенами
            this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 14600000, 5);
            this.pointToken = new Point(755 - 5 + xx, 94 - 5 + yy);                                //крестик в углу окошка с токенами
            this.pointChooseChannel = new Point(680 - 5 + xx, 285 - 5 + yy);                       //выбор канала в меню Alt+Q
            this.pointEnterChannel = new Point(460 - 5 + xx, 309 - 5 + yy + (botwindow.getKanal() - 2) * 15);                        //выбор канала в меню Alt+F2
            this.pointMoveNow = new Point(445 - 5 + xx, 490 - 5 + yy);                             //выбор канала в меню Alt+F2
            this.pointCure1 = new Point(215 - 5 + xx, 705 - 5 + yy);                               //бутылка лечения под буквой U
            this.pointCure2 = new Point(215 - 5 + 255 + xx, 705 - 5 + yy);                         //бутылка лечения под буквой J
            this.pointCure3 = new Point(215 - 5 + 255 * 2 + xx, 705 - 5 + yy);                     //бутылка лечения под буквой M
            this.pointMana1 = new Point(215 - 5 + 30 + xx, 705 - 5 + yy);                          //бутылка маны под буквой I

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
            //стартония
            this.pointRunNunies = new Point(920 - 5 + xx, 170 - 5 + yy);                           //нажимаем на зеленую стрелку, чтобы бежать к Нуньесу в Стартонии
            this.pointPressNunez = new Point(830 - 5 + xx, 340 - 5 + yy);                          //нажимаем на Нуньеса
            this.ButtonOkDialog = new Point(910 - 5 + xx, 680 - 5 + yy);                           //нажимаем на Ок в диалоге
            this.PressMedal = new Point(300 - 5 + xx, 210 - 5 + yy);                               //нажимаем на медаль
            this.ButtonCloseMedal = new Point(700 - 5 + xx, 600 - 5 + yy);                         //нажимаем на кнопку Close и закрываем медали
            this.pointPressNunez2 = new Point(700 - 5 + xx, 360 - 5 + yy);                         //нажимаем на Нуньеса после надевания медали
            //ребольдо
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
            this.pointDomingoOnMap = new Point(830 - 5 + xx, 145 - 5 + yy);                        //нажимаем на Доминго на карте Alt+Z
            this.pointPressDomingo = new Point(510 - 5 + xx, 425 - 5 + yy);                        //нажимаем на Доминго
            this.pointFirstStringDialog = new Point(520 - 5 + xx, 660 - 5 + yy);                   //нажимаем Yes в диалоге Доминго (нижняя строчка)
            this.pointSecondStringDialog = new Point(520 - 5 + xx, 640 - 5 + yy);                  //нажимаем Yes в диалоге Доминго второй раз (вторая строчка снизу)
            this.pointDomingoMiss = new Point(396 - 5 + xx, 206 - 5 + yy);                         //нажимаем правой кнопкой по карте миссии Доминго
            this.pointPressDomingo2 = new Point(590 - 5 + xx, 215 - 5 + yy);                       //нажимаем на Доминго после миссии
            this.pointLindonOnMap = new Point(820 - 5 + xx, 340 - 5 + yy);                         //нажимаем на Линдона на карте Alt+Z
            this.pointPressLindon2 = new Point(655 - 5 + xx, 255 - 5 + yy);                        //нажимаем на Линдона
            this.pointPetExpert = new Point(910 - 5 + xx, 415 - 5 + yy);                           //нажимаем на петэксперта
            this.pointPetExpert2 = new Point(815 - 5 + xx, 425 - 5 + yy);                          //нажимаем на петэксперта второй раз 
            this.pointThirdBookmark = new Point(920 - 5 + xx, 150 - 5 + yy);                       //тыкнули в третью закладку в кармане
            this.pointNamePet = new Point(440 - 5 + xx, 440 - 5 + yy);                             //нажимаем на строку, где вводить имя пета
            this.pointButtonNamePet = new Point(520 - 5 + xx, 495 - 5 + yy);                       //тыкнули в кнопку Raise Pet
            this.pointButtonClosePet = new Point(520 - 5 + xx, 535 - 5 + yy);                      //тыкнули в кнопку Close
            this.pointWayPointMap = new Point(820 - 5 + xx, 430 - 5 + yy);                         //тыкнули в строчку телепорт на карте Ребольдо
            this.pointWayPoint = new Point(665 - 5 + xx, 345 - 5 + yy);                            //тыкнули в телепорт
            this.pointBookmarkField = new Point(220 - 5 + xx, 200 - 5 + yy);                       //закладка Field в телепорте
            this.pointButtonLavaPlato = new Point(820 - 5 + xx, 320 - 5 + yy);                     //кнопка лавовое плато в телепорте
            //лавовое плато             
            this.pointGateCrater = new Point(373 - 5 + xx, 605 - 5 + yy);                          //переход (ворота) из лавового плато в кратер
            this.pointMitridat = new Point(800 - 5 + xx, 180 - 5 + yy);                            //митридат в кармане
            this.pointMitridatTo2 = new Point(30 - 5 + xx, 140 - 5 + yy);                          //ячейка, где должен лежать митридат
            this.pointBookmark3 = new Point(155 - 5 + xx, 180 - 5 + yy);                           //третья закладка в спецкармане
            this.pointButtonYesPremium = new Point(470 - 5 + xx, 415 - 5 + yy);                    //третья закладка в спецкармане
            this.pointSecondBookmark = new Point(870 - 5 + xx, 150 - 5 + yy);                      //вторая закладка в кармане

            //кратер
            this.pointWorkCrater = new Point(botwindow.getTriangleX()[0] + xx, botwindow.getTriangleY()[0] + yy);     //бежим на место работы
            this.pointButtonSaveTeleport = new Point(440 -5 + xx, 570 -5 + yy);                   // нажимаем на кнопку сохранения телепорта в текущей позиции
            this.pointButtonOkSaveTeleport = new Point(660 - 5 + xx, 645 - 5 + yy);               // нажимаем на кнопку OK для подтверждения сохранения телепорта 

        }                           //конструктор

        /// <summary>
        /// возвращаем тестовый цвет для сравнения в методе Connect
        /// </summary>
        /// <returns> номер цвета </returns>
        public override uint colorTest()
        {
            return 7859187;
        }


        ///// <summary>
        ///// геттер
        ///// </summary>
        ///// <returns></returns>
        //public Town getTown()
        //{ return this.town; }

        ///// <summary>
        ///// выбираем первого пета и нажимаем кнопку Summon в меню пет
        ///// </summary>
        //public void buttonSummonPet()
        //{
        //    pointSummonPet1.PressMouseL();      //Click Pet
        //    pointSummonPet1.PressMouseL();
        //    //botwindow.PressMouseL(569, 375);  //Click Pet
        //    //botwindow.PressMouseL(569, 375);
        //    botwindow.Pause(500);
        //    //botwindow.PressMouseL(408, 360);  //Click кнопку "Summon"
        //    //botwindow.PressMouseL(408, 360);
        //    pointSummonPet2.PressMouseL();      //Click кнопку "Summon"
        //    pointSummonPet2.PressMouseL();
        //    botwindow.Pause(1000);
        //}


        ///// <summary>
        ///// нажимаем кнопку Cancel Summon в меню пет
        ///// </summary>
        //public void buttonCancelSummonPet()
        //{
        //    pointCancelSummonPet.PressMouseL();   //Click Cancel Summon
        //    pointCancelSummonPet.PressMouseL();
        //    //botwindow.PressMouseL(408, 390); //Click Cancel Summon
        //    //botwindow.PressMouseL(408, 390);
        //    botwindow.Pause(1000);
        //}

//        /// <summary>
//        /// вызываем телепорт через верхнее меню и телепортируемся по первому телепорту
//        /// </summary>
//        public void Teleport()
//        {
//            botwindow.Pause(400);
//            TopMenu(12); //Click Teleport menu
////            botwindow.PressMouseL(400, 190 );
//            pointTeleport1.PressMouseL();
//            botwindow.Pause(50);
//            //botwindow.PressMouseL(400, 190 );
//            pointTeleport1.PressMouseL();
//            botwindow.Pause(200);
//            //botwindow.PressMouseL(355, 570); //Click on button Execute in Teleport menu
//            pointTeleport2.PressMouseL();   //Click on button Execute in Teleport menu
//            botwindow.Pause(200);
//        }

        ///// <summary>
        ///// выбор команды персов из списка в казарме
        ///// </summary>
        //public void TeamSelection()
        //{
        //    //            Class_Timer.Pause(500);
        //    pointTeamSelection1.PressMouse();   // Нажимаем кнопку вызова списка групп
        //    pointTeamSelection2.PressMouseL();  // выбираем нужную группу персов (первую в списке)
        //    pointTeamSelection3.PressMouseL();  // Нажимаем кнопку выбора группы (Select Team) 
        //    //PressMouse(135, 420); // Нажимаем кнопку вызова списка групп
        //    //PressMouseL(65, 355); // выбираем нужную группу персов (первую в списке)
        //    //PressMouseL(65, 620); // Нажимаем кнопку выбора группы (Select Team) 
        //}

        ///// <summary>
        ///// метод проверяет, открылось ли верхнее меню 
        ///// </summary>
        ///// <param name="numberOfThePartitionMenu"></param>
        ///// <returns> true, если меню открылось </returns>
        //public bool isOpenTopMenu(int numberOfThePartitionMenu)
        //{
        //    bool result = false;
        //    switch (numberOfThePartitionMenu)
        //    {
        //        case 2:
        //            result = botwindow.isColor2(333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);
        //            break;
        //        case 6:
        //            result = botwindow.isColor2(460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);
        //            break;
        //        case 8:
        //            result = botwindow.isColor2(558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);
        //            break;
        //        case 9:
        //            result = botwindow.isColor2(606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);
        //            break;
        //        case 12:
        //            result = botwindow.isColor2(411 - 5, 171 - 5, 7590000, 412 - 5, 171 - 5, 7850000, 4);
        //            break;
        //        case 13:
        //            result = botwindow.isColor2(371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);
        //            break;
        //        default:
        //            result = true;
        //            break;
        //    }
        //    return result;
        //}


        ///// <summary>
        ///// Открыть городской телепорт (Alt + F3)
        ///// </summary>
        //public void OpenTownTeleport()  
        //{
        //    bool result = true;
        //    int counter = 0;
        //    while ((result) & (counter < 5))
        //        {
        //            counter++;
        //            botwindow.TopMenu(6, 3);
        //            botwindow.Pause(500);
        //            result = !town.isOpenTownTeleport();
        //            if (result) botwindow.PressEscThreeTimes();
        //            botwindow.Pause(500);
        //        }
        //}

        ///// <summary>
        ///// Открыть городской телепорт (Alt + F3) без проверок и while (для паттерна Состояние)  StateGT
        ///// </summary>
        //public void OpenTownTeleportForState()
        //{
        //        TopMenu(6, 3);
        //        botwindow.Pause(1000);
        //}

        ///// <summary>
        ///// Открыть карту местности (Alt + Z). Если карта местности не открывается, то производится повторная попытка     
        ///// </summary>
        //public void OpenMap()
        //{
        //    int counter = 0;
        //    bool result = true;
        //    while ((result) & (counter < 5))
        //        {
        //            counter++;
        //            botwindow.TopMenu(6, 2);
        //            botwindow.Pause(500);
        //            result = !town.isOpenMap();
        //            if (result) botwindow.PressEscThreeTimes();
        //            botwindow.Pause(1000);
        //        }
        //}

        ///// <summary>
        ///// Открыть карту местности (Alt + Z) для группы классов StateGT (паттерн Состояние)
        ///// </summary>
        //public void OpenMapForState()
        //{
        //    TopMenu(6, 2);
        //    botwindow.Pause(1000);
        //}
        
//        /// <summary>
//        /// проверяет, находится ли данное окно в магазине (а точнее на странице входа в магазин) 
//        /// </summary>
//        /// <returns> true, если находится в магазине </returns>
//        public bool isSale()
//        {
//            return ((pointIsSale1.isColor()) && (pointIsSale2.isColor()));
////            return botwindow.isColor2(902, 673, 7850000, 903, 673, 7850000, 4);
//        }

        ///// <summary>
        ///// проверяет, находится ли данное окно в самом магазине (на закладке BUY или SELL)                                       
        ///// </summary>
        ///// <returns> true, если находится в магазине </returns>
        //public bool isSale2()
        //{
        //    return ((pointIsSale21.isColor()) && (pointIsSale22.isColor()));
        //    //return botwindow.isColor2(841 - 5, 665 - 5, 7390000, 841 - 5, 668 - 5, 7390000, 4);
        //}

        ///// <summary>
        ///// Покупка митридата в количестве 333 штук
        ///// </summary>
        //public void BuyingMitridat()
        //{
        //    //botwindow.PressMouseL(360, 537);          //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //    pointBuyingMitridat1.PressMouseL();             //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //    botwindow.Pause(150);

        //    botwindow.Press333();

        //    botwindow.Botton_BUY();                             // Нажимаем на кнопку BUY 


        //    pointBuyingMitridat2.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если переполнение митридата
        //    //botwindow.PressMouseL(1392 - 875, 438 - 5);         
        //    botwindow.Pause(500);

        //    pointBuyingMitridat3.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если нет денег на покупку митридата
        //    //botwindow.PressMouseL(1392 - 875, 428 - 5);          
        //    botwindow.Pause(500);
        //}

        ///// <summary>
        ///// проверяет, открыта ли закладка Sell в магазине 
        ///// </summary>
        ///// <returns> true, если закладка Sell в магазине открыта </returns>
        //public bool isClickSell()
        //{
        //    return ((pointIsClickSale1.isColor()) && (pointIsClickSale2.isColor()));
        //    //return botwindow.isColor2(730, 660, 7390000, 730, 659, 7390000, 4);
        //}

        ///// <summary>
        ///// Продажа товара в магазине, после продажи окно с ботом будет закрыто
        ///// </summary>
        //public void CompleteSaleOneWindow()
        //{
        //    town.ClickSellAndOkInTrader(); //нажимает строчку Sell и кнопку Ок при входе в магазин (по разному в разных городах)

        //    BuyingMitridat();              // покупка митридата

        //    botwindow.Bookmark_Sell();               //========= тыкаем в закладку SELL =======================

        //    if (isClickSell())             // если закладка Sell точно тыкнулась (а иначе мы скупим весь магазин)
        //    {
        //        botwindow.SaleToTheRedBottle();      // продажа до красной бутылки
        //        botwindow.SaleOverTheRedBottle();    // продажа от красной бутылки до того момента, пока крутится список продажи
        //        botwindow.SaleToEnd();               // продажа до конца, когда список уже не крутится
        //        botwindow.Botton_Sell();             // Нажимаем на кнопку Sell
        //    }

        //    botwindow.Botton_Close();                // Нажимаем на кнопку Close
        //    botwindow.Pause(1500);
        //    town.ExitFromTrader();         // дополнительные нажатия при выходе из магазина
        //}

        ///// <summary>
        ///// Выгружаем окно через верхнее меню 
        ///// </summary>
        //public void GoToEnd()
        //{
        //    botwindow.PressEscThreeTimes();
        //    botwindow.Pause(1000);

        //    TopMenu(13);
        //    botwindow.Pause(200);
        //    pointGotoEnd.PressMouse();
        //    //botwindow.PressMouse(680, 462);   //выбираем пункт end Programm
        //}

        ///// <summary>
        ///// метод проверяет, находится ли данное окно в городе (проверка по стойке, работает только с ружьем) 
        ///// делаем проверку по двум точкам у каждого перса
        ///// </summary>
        ///// <returns> true, если бот находится в городе </returns>
        //public bool isTown()
        //{
        //    //bool result1 = botwindow.isColor2(24, 692, 11053000, 25, 692, 10921000, 3);
        //    //bool result2 = botwindow.isColor2(279, 692, 11053000, 280, 692, 10921000, 3);
        //    //bool result3 = botwindow.isColor2(534, 692, 11053000, 535, 692, 10921000, 3);
        //    return (pointIsTown11.isColor() && pointIsTown12.isColor() && pointIsTown21.isColor() && pointIsTown22.isColor() && pointIsTown31.isColor() && pointIsTown32.isColor());
        //}

        ///// <summary>
        ///// метод проверяет, переполнился ли карман (выскочило ли уже сообщение о переполнении)
        ///// </summary>
        ///// <returns> true, еслм карман переполнен </returns>
        //public bool isBoxOverflow()
        //{
        // //   return botwindow.isColor2(548 - 30, 462 - 30, 7800000, 547 - 30, 458 - 30, 7600000, 5);
        //    return (pointisBoxOverflow1.isColor() && pointisBoxOverflow2.isColor());
        //}

        ///// <summary>
        ///// проверяет, призван ли пет
        ///// </summary>
        ///// <returns> true, если призван </returns>
        //public bool isSummonPet()
        //{
        //    //return botwindow.isColor2(401 - 5, 362 - 5, 7630000, 401 - 5, 364 - 5, 7560000, 4);
        //    return (pointisSummonPet1.isColor() && pointisSummonPet2.isColor());
        //}

        ///// <summary>
        ///// активируем уже призванного пета
        ///// </summary>
        //public void ActivePet()
        //{
        //    //botwindow.PressMouse(408, 405);  //Click Button Active Pet
        //    pointActivePet.PressMouse(); //Click Button Active Pet
        //    botwindow.Pause(2500);
        //}

        ///// <summary>
        ///// проверяет, активирован ли пет (зависит от сервера)
        ///// </summary>
        ///// <returns></returns>
        //public bool isActivePet()
        //{
        //    //return botwindow.isColor2(495 - 5, 310 - 5, 13200000, 496 - 5, 308 - 5, 13600000, 5);
        //    return (pointisActivePet1.isColor() && pointisActivePet2.isColor());
        //}


        ///// <summary>
        ///// главный метод, который организует переход бота в город для продажи и выгружает окно
        ///// </summary>
        //public void run()
        //{
        //    // ================= убирает все лишние окна с экрана =================================
        //    botwindow.PressEscThreeTimes();
        //    botwindow.Pause(500);
        //    //================ переход в тот город, где надо продаться (переход по Alt+W) =================================
        //    TeleportToTownAltW();

        //    int counter = 0;
        //    while ((!isTown()) & (counter < 40))                    //ожидание загрузки города
        //    { botwindow.Pause(500); counter++; }

        //    // ========================== убирает все лишние окна с экрана =================================
        //    botwindow.PressEscThreeTimes();
        //    botwindow.Pause(1000);

        //    // ================= выбираем главным среднего персонажа (для унификации) =================================
        //    botwindow.SecondHero();
        //    botwindow.Pause(1500);

        //    // ================= открывает городской телепорт (ALT + F3) =================================
        //    OpenTownTeleport();

        //    // ============= Удаляем камеру на максимальную высоту =================================================================
        //    town.MaxHeight();
        //    botwindow.Pause(1000);

        //    // ============= Кликаю на кнопку городского телепорта, чтобы перелететь на фиксированную точку (торговую улицу)==============
        //    town.TownTeleportW();
        //    botwindow.Pause(4000);  //время чтобы долететь до точки

        //    counter = 0;
        //    do
        //    {
        //        // ============= открыть карту через верхнее меню ============================================================
        //        OpenMap();   // с проверкой открытия

        //        // тыкаем во вторую закладку карты города
        //        town.SecondBookmark();
        //        if (!town.isSecondBookmark()) { botwindow.PressEscThreeTimes(); }
        //        botwindow.Pause(1000);
        //        counter++;
        //    } while ((!town.isSecondBookmark()) & (counter<5));

        //    if (counter >= 5)
        //    {
        //        GoToEnd();
        //    }
        //    else
        //    {
        //        ////тыкаем в другого торговца (который стоит рядом с нужным нам)
        //        town.GoToTraderMap();

        //        ////тыкаем "Move"
        //        town.ClickMoveMap();

        //        // закрываем все окна
        //        botwindow.PressEscThreeTimes();

        //        //время, чтобы добежать до нужного торговца (разное время для разных городов) 
        //        town.PauseToTrader();

        //        // ============= тыкаем в голову торговца, чтобы войти в магазин  ===================================================
        //        town.Click_ToHeadTrader();
        //        botwindow.Pause(3000);   //время, чтобы загрузился магазин

        //        // ============= продажа в одном окне с закрытием ===================================
        //        if (isSale())                         // если окно находится в магазине (т.е. получилось тыкнуть в торговца), то продаёмся
        //        {
        //            CompleteSaleOneWindow();
        //            botwindow.Pause(1000);
        //        }

        //        GoToEnd();
        //    }
        //}

        ///// <summary>
        ///// метод проверяет, находится ли данное окно в режиме логаута, т.е. на стадии ввода логина-пароля
        ///// </summary>
        ///// <returns></returns>
        //public bool isLogout()
        //{
        //    //return botwindow.isColor2(121, 62, 7460000, 135, 61, 7590000, 4);
        //    return (pointisLogout1.isColor() && pointisLogout2.isColor());
        //}


        ///// <summary>
        ///// сдвиг для правильного выбора канала
        ///// </summary>
        ///// <returns></returns>
        //public int sdvig()
        //{
        //    return sdvigY;
        //}

        ///// <summary>
        ///// проверяем, в бараках ли бот
        ///// </summary>
        ///// <returns> true, если бот в бараках </returns>
        //public bool isBarack()
        //{
        // //   return botwindow.isColor2(61 - 5, 151 - 5, 2420000, 61 - 5, 154 - 5, 2420000, 4);
        //    return (pointisBarack1.isColor() && pointisBarack2.isColor());
        //}

        ///// <summary>
        ///// выход из казармы в логаут, т.е. переход из состояния "Барак" в состояние "логаут"
        ///// </summary>
        //public void LogOutFromBarack()
        //{
        //    botwindow.PressMouseL(933 - 5, 700 - 5); // нажимаем LogOut
        //    botwindow.PressMouseL(933 - 5, 700 - 5); // нажимаем LogOut
        //}

        ///// <summary>
        ///// метод проверяет, находится ли данное окно на работе (проверка по стойке, работает только с ружьем)
        ///// </summary>
        ///// <returns> true, если сейчас на рабочей карте </returns>
        //public bool isWork()
        //{
        //  //  return botwindow.isColor2(29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);
        //    return (pointisWork1.isColor() && pointisWork2.isColor());
        //}

        ///// <summary>
        ///// метод проверяет, открылось ли меню с петом Alt + P
        ///// </summary>
        ///// <returns> true, если открыто </returns>
        //public bool isOpenMenuPet()
        //{
        //    //return botwindow.isColor2(471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
        //    return (pointisOpenMenuPet1.isColor() && pointisOpenMenuPet2.isColor());
        //}

        ///// <summary>
        ///// Определяет, надо ли грузить данное окно с ботом
        ///// </summary>
        ///// <returns> true означает, что это окно (данный бот) должно быть активно и его надо грузить </returns>
        //public override bool isActive()
        //{
        //    bool result = false;
        //    if (getActiveWindow() == 1) result = true;
        //    return result;
        //}                                                                                        


        ///// <summary>
        ///// геттер
        ///// </summary>
        ///// <returns></returns>
        //private int getActiveWindow()
        //{ return this.activeWindow; }

        ///// <summary>
        ///// геттер
        ///// </summary>
        ///// <returns></returns>
        //private String getPathClient()
        //{ return this.pathClient; }

        /// <summary>
        /// запуск клиента игры
        /// </summary>
        public override void runClient()
        {
            Process.Start(getPathClient());
        }                                   //нужен здесь из-за Синга (там другой порядок вызова)

        /// <summary>
        /// возвращает параметр, прочитанный из файла
        /// </summary>
        /// <returns></returns>    
        private String path_Client()
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_path.txt"); }                                         //нужен

        /// <summary>
        /// возвращает параметр, прочитанный из файла
        /// </summary>
        /// <returns></returns>
        private int active_window()
        { return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_active.txt")); }                                          //нужен

        /// <summary>
        /// нажмает на выбранный раздел верхнего меню 
        /// </summary>
        /// <param name="numberOfThePartitionMenu"> ноиер раздела верхнего меню </param>
        public override void TopMenu(int numberOfThePartitionMenu)
        {
            int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
            int x = MenukoordX[numberOfThePartitionMenu - 1];
            int y = 55;
            iPoint pointMenu = new Point(x + botwindow.getX(), y + botwindow.getY());

            do
            {
                pointMenu.PressMouse();
                //PressMouse(x, y);
                Pause(1000);
                if ((isLogout()) || (!botwindow.isHwnd())) break;    //если вылетели в логаут или закрылось окно с игрой, то выход из цикла.  (29.04.2017) 
            } while (!isOpenTopMenu(numberOfThePartitionMenu));
        }            //нужен

        /// <summary>
        /// нажать на выбранный раздел верхнего меню, а далее на пункт раскрывшегося списка
        /// </summary>
        /// <param name="numberOfThePartitionMenu"></param>
        /// <param name="punkt"></param>
        public override void TopMenu(int numberOfThePartitionMenu, int punkt)
        {
            int[] numberOfPunkt = { 0, 8, 4, 5, 0, 3, 2, 6, 9, 0, 0, 0, 0 };
            int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
            int[] FirstPunktOfMenuKoordY = { 0, 80, 80, 80, 0, 92, 92, 92, 80, 0, 0, 0, 0 };

            if (punkt <= numberOfPunkt[numberOfThePartitionMenu - 1])
            {
                int x = MenukoordX[numberOfThePartitionMenu - 1];
                int y = FirstPunktOfMenuKoordY[numberOfThePartitionMenu - 1] + 25 * (punkt - 1);
                iPoint pointMenu = new Point(x + botwindow.getX(), y + botwindow.getY());

                TopMenu(numberOfThePartitionMenu);   //сначала открываем раздел верхнего меню (1-13)
                Pause(500);
                pointMenu.PressMouse();  //выбираем конкретный пункт подменю (раскрывающийся список)
                //PressMouse(x, y);  //выбираем конкретный пункт подменю (раскрывающийся список)
            }
        }    //нужен

        ///// <summary>
        ///// телепортируемся в город продажи по Alt+W (Америка)
        ///// </summary>
        //public override void TeleportToTownAltW()
        //{
        //    TopMenu(6, 1);
        //    botwindow.Pause(1000);
        //    //botwindow.PressMouseL(801, 564 + (botwindow.getNomerTeleport() - 1) * 17);
        //    //botwindow.Pause(50);
        //    //botwindow.PressMouseL(801, 564 + (botwindow.getNomerTeleport() - 1) * 17);
        //    pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
        //    //pointTeleportToTownAltW.PressMouseL();
        //    //pointTeleportToTownAltW.PressMouseL();
        //    botwindow.Pause(2000);
        //}                               //нужен здесь из-за Синга (там другой порядок ухода с карты)

        /// <summary>
        /// метод проверяет, открылось ли верхнее меню 
        /// </summary>
        /// <param name="numberOfThePartitionMenu"></param>
        /// <returns> true, если меню открылось </returns>
        private bool isOpenTopMenu(int numberOfThePartitionMenu)
        {
            bool result = false;
            switch (numberOfThePartitionMenu)
            {
                case 2:
                    //                    result = botwindow.isColor2(333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);  //не проверено
                    result = (pointisOpenTopMenu21.isColor() && pointisOpenTopMenu22.isColor());
                    break;
                case 6:
                    //                    result = botwindow.isColor2(460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);  //не проверено
                    result = (pointisOpenTopMenu61.isColor() && pointisOpenTopMenu62.isColor());
                    break;
                case 8:
                    //result = botwindow.isColor2(558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);  //не проверено
                    result = (pointisOpenTopMenu81.isColor() && pointisOpenTopMenu82.isColor());
                    break;
                case 9:
                    //result = botwindow.isColor2(606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);  //не проверено
                    result = (pointisOpenTopMenu91.isColor() && pointisOpenTopMenu92.isColor());
                    break;
                case 12:
                    //result = botwindow.isColor2(507 - 5, 140 - 5, 12440000, 508 - 5, 140 - 5, 12440000, 4);  //проверено
                    result = (pointisOpenTopMenu121.isColor() && pointisOpenTopMenu122.isColor());
                    break;
                case 13:
                    //result = botwindow.isColor2(371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);  //не проверено
                    result = (pointisOpenTopMenu131.isColor() && pointisOpenTopMenu132.isColor());
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }              //нужен



    }
}


