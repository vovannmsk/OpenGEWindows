using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using GEBot.Data;

namespace OpenGEWindows
{
    /// <summary>
    /// Класс описывает процесс перехода к торговле от фарма , начиная от проверки необходимости продажи и заканчивая закрытием окна с ботом (для сервера Америка)
    /// </summary>
    public class ServerAmerica : Server 
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
        public ServerAmerica(botWindow botwindow) 
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();
            this.globalParam = new GlobalParam();
            ServerParamFactory serverParamFactory = new ServerParamFactory(botwindow.getNumberWindow());
            this.serverParam = serverParamFactory.create();

            #endregion

            #region общие 2

            TownFactory townFactory = new AmericaTownFactory(botwindow);                        // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();                                               // выбирается город с помощью фабрики

            #endregion

            #region параметры, зависящие от сервера

            //            this.pathClient = path_Client();
            this.pathClient = serverParam.PathClient;
            this.isActiveServer = serverParam.IsActiveServer;

            #endregion

            #region No window
            #endregion

            #region Logout

            this.pointisLogout1 = new PointColor(126 - 5 + xx, 66 - 5 + yy, 7460000, 4);      //121, 62, 7460000, 135, 61, 7590000, 4)
            this.pointisLogout2 = new PointColor(126 - 5 + xx, 67 - 5 + yy, 7460000, 4);      //121, 62, 7460000, 135, 61, 7590000, 4)
            this.pointConnect = new PointColor(522 - 5 + xx, 418 - 5 + yy, 7800000, 5);

            #endregion

            #region Pet

            this.pointisOpenMenuPet1 = new PointColor(466 + xx, 214 + yy, 12500000, 5);      //471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
            this.pointisOpenMenuPet2 = new PointColor(467 + xx, 214 + yy, 12500000, 5);
            this.pointisSummonPet1 = new PointColor(401 - 5 + xx, 362 - 5 + yy, 7630000, 4);      //401 - 5, 362 - 5, 7630000, 401 - 5, 364 - 5, 7560000, 4);
            this.pointisSummonPet2 = new PointColor(401 - 5 + xx, 364 - 5 + yy, 7560000, 4);
            this.pointisActivePet1 = new PointColor(495 - 5 + xx, 310 - 5 + yy, 13200000, 5);      //495 - 5, 310 - 5, 13200000, 496 - 5, 308 - 5, 13600000, 5);
            this.pointisActivePet2 = new PointColor(496 - 5 + xx, 308 - 5 + yy, 13600000, 5);
            this.pointisActivePet3 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13000000, 5);     //для америки пока не нужно
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13100000, 5);     //для америки пока не нужно. еда на месяц
            this.pointCancelSummonPet = new Point(408 + xx, 390 + yy);  //400, 190 
            this.pointSummonPet1 = new Point(569 + xx, 375 + yy);                   // 569, 375   //Click Pet
            this.pointSummonPet2 = new Point(408 + xx, 360 + yy);                   // 408, 360   //Click кнопку "Summon"
            this.pointActivePet = new Point(408 + xx, 405 + yy);                   // 408, 405);  //Click Button Active Pet

            #endregion

            #region Top menu

//            this.pointGotoEnd = new Point(680 + xx, 442 - 5 + yy);          //логаут
            this.pointGotoEnd = new Point(680 + xx, 472 - 5 + yy);          //end
            this.pointLogout = new Point(685 - 5 + xx, 440 - 5 + yy);            //логаут
            this.pointTeleportFirstLine = new Point(400 + xx, 190 + yy);  //400, 190 
            //this.pointTeleportSecondLine = new Point(400 + xx, 205 + yy);   //              тыкаем во вторую строчку телепорта                          //проверено
            this.pointTeleportExecute = new Point(355 + xx, 570 + yy);   //355, 570
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

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(902 + xx, 673 + yy, 7850000, 4);
            this.pointIsSale2 = new PointColor(903 + xx, 673 + yy, 7850000, 4);
            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7390000, 4);
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7390000, 4);
            this.pointIsClickSale1 = new PointColor(735 - 5 + xx, 665 - 5 + yy, 7390000, 4);
            this.pointIsClickSale2 = new PointColor(735 - 5 + xx, 664 - 5 + yy, 7390000, 4);
            this.pointBookmarkSell = new Point(225 + xx, 163 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonClose = new Point(847 + xx, 663 + yy);   //847, 663);

            #endregion

            #region atWork

            this.pointisBoxOverflow1 = new PointColor(518 + xx, 432 + yy, 7800000, 5);      //548 - 30, 462 - 30, 7800000, 547 - 30, 458 - 30, 7600000, 5);
            this.pointisBoxOverflow2 = new PointColor(517 + xx, 428 + yy, 7600000, 5);
            this.pointisBoxOverflow3 = new PointColor(379 - 5 + xx, 497 - 5 + yy, 5600000, 5);          //проверка оранжевой надписи
            this.pointisBoxOverflow4 = new PointColor(379 - 5 + xx, 498 - 5 + yy, 5600000, 5);
            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);
            //this.pointisWork_RifleDot1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);               //проверка по стойке с ружьем
            //this.pointisWork_RifleDot2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            //this.pointisWork_DrobDot1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            //this.pointisWork_DrobDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointisWork_VetDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по вет стойке с дробашем          не проверено
            //this.pointisWork_VetDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            //this.pointisWork_ExpDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            //this.pointisWork_ExpDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            //this.pointisWork_ExpSwordDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 3693000, 3);           //проверка по стойке с мечом Дарья
            //this.pointisWork_ExpSwordDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 10258000, 3);
            this.pointisKillHero1 = new PointColor(75 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero2 = new PointColor(330 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero3 = new PointColor(585 + xx, 631 + yy, 1900000, 4);
            //this.pointisWork_VetPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 13086000, 3);           //проверка по стойке с вет пистолетом Outrange
            //this.pointisWork_VetPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 12954000, 3);
            //this.pointisWork_UnlimPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 15824000, 3);      //проверка по стойке с эксп пистолетом Unlimited Shot
            //this.pointisWork_UnlimPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 15767000, 3);

            //this.arrayOfColorsIsWork1 = new uint[13] { 0, 11051000, 1721000, 7644000, 2764000, 16777000, 4278000, 5138000, 3693000, 66000, 5068000, 15824000, 8756000 };
            //this.arrayOfColorsIsWork2 = new uint[13] { 0, 10919000, 2106000, 16711000, 7243000, 3560000, 5401000, 9747000, 10258000, 0, 9350000, 15767000, 8162000 };
            this.arrayOfColorsIsWork1 = new uint[12] { 11051, 1721, 7644, 2764, 16777, 4278, 5138, 3693, 66, 5068, 15824, 8756 };
            this.arrayOfColorsIsWork2 = new uint[12] { 10919, 2106, 16711, 7243, 3560, 5401, 9747, 10258, 0, 9350, 15767, 8162 };
            #endregion

            #region inTown

            this.pointCure1 = new Point(215 - 5 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой U
            this.pointCure2 = new Point(215 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой J
            this.pointCure3 = new Point(215 - 5 + 255 * 2 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой M
            this.pointMana1 = new Point(215 - 5 + 30 + xx, 705 - 5 + yy);                        //бутылка маны под буквой I
            this.pointMana2 = new Point(245 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка маны под буквой K
            this.pointMana3 = new Point(245 - 5 + 510 + xx, 705 - 5 + yy);                        //бутылка маны под буквой ,

            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 14600000, 5);           //проверяем открыто ли окно с токенами
            this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 14600000, 5);
            this.pointToken = new Point(755 - 5 + xx, 94 - 5 + yy);                                //крестик в углу окошка с токенами

            this.arrayOfColorsIsTown1 = new uint[12] { 11053, 1710, 7631, 2763, 16777, 4276, 5131, 3684, 65, 5066, 15856, 8750 };
            this.arrayOfColorsIsTown2 = new uint[12] { 10921, 2105, 16711, 7237, 3552, 5395, 9737, 10263, 0, 9342, 15790, 8158 };

            //this.pointIsTown_RifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 11053000, 3);        //точки для проверки стойки с ружьем
            //this.pointIsTown_RifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 10921000, 3);

            //this.pointIsTown_ExpRifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с ружьем
            //this.pointIsTown_ExpRifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpRifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpRifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpRifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpRifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            //this.pointIsTown_DrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки обычной стойки с дробашом в городе
            //this.pointIsTown_DrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            //this.pointIsTown_VetDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки вет стойки с дробашом в городе            не проверено          
            //this.pointIsTown_VetDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_VetDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_VetDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_VetDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_VetDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_VetPistolFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 65000, 3);       //точки для проверки вет стойки с пистолетом Outrange
            //this.pointIsTown_VetPistolFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 0, 0);
            //this.pointIsTown_UnlimPistolFirstDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 15856000, 3);      //точки для проверки эксп стойки с пистолетами Unlimited Shot
            //this.pointIsTown_UnlimPistolFirstDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 15790000, 3);

            //this.pointIsTown_ExpDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с дробашом
            //this.pointIsTown_ExpDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            #endregion

            #region алхимия

            this.pointisAlchemy1 = new PointColor(513 - 5 + xx, 565 - 5 + yy, 7925494, 0);
            this.pointisAlchemy2 = new PointColor(513 - 5 + xx, 566 - 5 + yy, 7925494, 0);
            this.pointAlchemy = new Point(522 - 5 + xx, 521 - 5 + yy);                                           //кнопка "Start Alchemy"
            this.pointisInventoryFull1 = new PointColor(647 - 130 + xx, 559 - 130 + yy, 7727344, 0);             //переполнение инвентаря при алхимии
            this.pointisInventoryFull2 = new PointColor(647 - 130 + xx, 560 - 130 + yy, 7727344, 0);
            this.pointisOutOfIngredient1_1 = new PointColor(570 - 130 + xx, 645 - 130 + yy, 1973790, 0);             //закончился ОДИН ИЗ ингредиентов
            this.pointisOutOfIngredient1_2 = new PointColor(570 - 130 + xx, 646 - 130 + yy, 1973790, 0);             //
            this.pointOutOfMoney1 = new PointColor(647 - 130 + xx, 540 - 130 + yy, 7700000, 5);
            this.pointOutOfMoney2 = new PointColor(647 - 130 + xx, 541 - 130 + yy, 7700000, 5);

            #endregion

            #region Barack

            this.pointButtonLogoutFromBarack = new Point(955 - 5 + xx, 700 - 5 + yy);                        //кнопка логаут в казарме
            this.sdvigY = 0;
            this.pointisBarack1 = new PointColor(56 + xx, 146 + yy, 2420000, 4);      //61 - 5, 151 - 5, 2420000, 61 - 5, 154 - 5, 2420000, 4);
            this.pointisBarack2 = new PointColor(56 + xx, 149 + yy, 2420000, 4);
            this.pointisBarack3 = new PointColor(81 - 5 + xx, 63 - 5 + yy, 7700000, 5);       //проверено
            this.pointisBarack4 = new PointColor(81 - 5 + xx, 64 - 5 + yy, 7700000, 5);
            this.pointChooseChannel = new Point(680 - 5 + xx, 285 - 5 + yy);                       //выбор канала в меню Alt+Q
            this.pointEnterChannel = new Point(460 - 5 + xx, 309 - 5 + yy + (botwindow.getKanal() - 2) * 15);                        //выбор канала в меню Alt+F2
            this.pointMoveNow = new Point(445 - 5 + xx, 490 - 5 + yy);                             //выбор канала в меню Alt+F2
            this.pointTeamSelection1 = new Point(135 + xx, 450 + yy);    //кнопка выбора групп в бараке
            this.pointTeamSelection2 = new Point(65 + xx, 355 + yy);
            this.pointTeamSelection3 = new Point(65 + xx, 620 + yy);
            this.pointNewPlace = new Point(85 + xx, 670 + yy);

            #endregion

            #region новые боты

            this.pointPetBegin = new Point(800 - 5 + xx, 220 - 5 + yy);    // 800-5, 220-5
            this.pointPetEnd = new Point(520 - 5 + xx, 330 - 5 + yy);    // 520-5, 330-5
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

            #endregion

            #region кратер

            //лавовое плато             
            this.pointGateCrater = new Point(373 - 5 + xx, 605 - 5 + yy);                          //переход (ворота) из лавового плато в кратер
            this.pointMitridat = new Point(800 - 5 + xx, 180 - 5 + yy);                            //митридат в кармане
            this.pointMitridatTo2 = new Point(30 - 5 + xx, 140 - 5 + yy);                          //ячейка, где должен лежать митридат
            this.pointBookmark3 = new Point(155 - 5 + xx, 180 - 5 + yy);                           //третья закладка в спецкармане
            this.pointButtonYesPremium = new Point(470 - 5 + xx, 415 - 5 + yy);                    //третья закладка в спецкармане
            this.pointSecondBookmark = new Point(870 - 5 + xx, 150 - 5 + yy);                      //вторая закладка в кармане

            //кратер
            this.pointWorkCrater = new Point(botwindow.getTriangleX()[0] + xx, botwindow.getTriangleY()[0] + yy);     //бежим на место работы
            this.pointButtonSaveTeleport = new Point(440 - 5 + xx, 570 - 5 + yy);                   // нажимаем на кнопку сохранения телепорта в текущей позиции
            this.pointButtonOkSaveTeleport = new Point(660 - 5 + xx, 645 - 5 + yy);               // нажимаем на кнопку OK для подтверждения сохранения телепорта 

            #endregion

            #region заточка
            #endregion

            #region чиповка
            #endregion

            #region передача песо торговцу
            #endregion

            #region Undressing in Barack

            this.pointShowEquipment = new Point(145 - 5 + xx, 442 - 5 + yy);
            //this.pointBarack1 = new Point( 80 - 5 + xx, 152 - 5 + yy);
            //this.pointBarack2 = new Point(190 - 5 + xx, 152 - 5 + yy);
            //this.pointBarack3 = new Point( 80 - 5 + xx, 177 - 5 + yy);
            //this.pointBarack4 = new Point(190 - 5 + xx, 177 - 5 + yy);

            this.pointBarack[1] = new Point(80 - 5 + xx, 152 - 5 + yy);
            this.pointBarack[2] = new Point(190 - 5 + xx, 152 - 5 + yy);
            this.pointBarack[3] = new Point(80 - 5 + xx, 177 - 5 + yy);
            this.pointBarack[4] = new Point(190 - 5 + xx, 177 - 5 + yy);

            this.pointEquipment1 = new PointColor(300 - 5 + xx, 60 - 5 + yy, 12600000, 5);
            this.pointEquipment2 = new PointColor(302 - 5 + xx, 60 - 5 + yy, 12600000, 5);


            #endregion

            #region BH

            this.pointGateInfinityBH = new Point(410 - 5 + xx, 430 - 5 + yy);
            this.pointisBH1 = new PointColor(985 - 30 + xx, 91 - 30 + yy, 10353000, 3);                    // желтый ободок на миникарте (в BH миникарты нет)
            this.pointisBH2 = new PointColor(975 - 30 + xx, 95 - 30 + yy, 5700000, 5);                 //синий ободок на миникарте (в BH миникарты нет)
            this.arrayOfColors = new uint[17] { 0, 1644051, 725272, 6123117, 3088711, 1715508, 1452347, 6608314, 14190184, 1319739, 2302497, 5275256, 2830124, 1577743, 525832, 2635325, 2104613 };

            #endregion


        }

        public ServerAmerica (int numberOfWindow) : this (new botWindow(numberOfWindow))
        {
        }

        //==================================== Методы ===================================================

        #region общие методы 2

        ///// <summary>
        ///// возвращает параметр, прочитанный из файла
        ///// </summary>
        ///// <returns></returns>    
        //private String path_Client()
        //{ return File.ReadAllText(globalParam.DirectoryOfMyProgram + "\\America_path.txt"); }

        ///// <summary>
        ///// возвращает параметр, прочитанный из файла
        ///// </summary>
        ///// <returns></returns>
        //private int AmericaActive()
        //{ return int.Parse(File.ReadAllText(globalParam.DirectoryOfMyProgram + "\\America_active.txt")); }                                     

        #endregion

        #region No window

        /// <summary>
        /// запуск клиента Steam
        /// </summary>
        public override void runClientSteam()
        {
            #region для песочницы

            //запускаем steam в песочнице
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files\Sandboxie\Start.exe";
            process.StartInfo.Arguments = @"/box:" + botwindow.getNumberWindow() + " " + this.pathClient;
            process.Start();

            Pause(30000);


            #endregion

        }

        /// <summary>
        /// запуск клиента игры
        /// </summary>
        public override void runClient()
        {
            Process.Start(this.pathClient);
        }                                

        /// <summary>
        /// действия для оранжевой кнопки
        /// </summary>
        public override void OrangeButton()
        {
            botwindow.ReOpenWindow();
            Pause(100);
            if (isLogout())
            {
                botwindow.EnterLoginAndPasword();
            }
        }

        ///// <summary>
        ///// Определяет, надо ли грузить данное окно с ботом
        ///// </summary>
        ///// <returns> true означает, что это окно (данный бот) должно быть активно и его надо грузить </returns>
        //public override bool isActive()
        //{
        //    bool result = false;
        //    if (AmericaActive() == 1) result = true;
        //    return result;
        //}


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
                HWND = FindWindow("Granado Espada", "Granado Espada Online");

                count++; if (count > 5) return (UIntPtr)0;
            }

            botwindow.setHwnd(HWND);

            SetWindowPos(HWND, 1, xx, yy, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            //            ShowWindow(HWND, 2);   //скрыть окно в трей

            Pause(500);


            #region старый вариант метода
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(350, 700, 8);
            //Pause(200);
            //while (New_HWND_GE == (UIntPtr)0)                
            //{
            //    Pause(500);
            //    New_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");
            //}
            //setHwnd(New_HWND_GE);
            //hwnd_to_file();
            ////Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
            ////SetWindowPos(New_HWND_GE, 1, getX(), getY(), WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            //SetWindowPos(New_HWND_GE, 1, 825, 5, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            //Pause(500);
            #endregion

            return HWND;
        }

        #endregion

        #region LogOut

        /// <summary>
        /// выбираем сервер путем нажатия на первую строчку в меню
        /// </summary>
        public override void serverSelection()
        {
        }

        #endregion

        #region Top Menu

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


        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public override void TeleportToTownAltW(int nomerTeleport)
        {
            iPoint pointTeleportToTownAltW = new Point(801 + xx, 564 + yy + (nomerTeleport - 1) * 17);

            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            Pause(2000);
        }

        /// <summary>
        /// вызываем телепорт через верхнее меню и телепортируемся по указанному номеру телепорта
        /// </summary>
        /// <param name="NumberOfLine"></param>
        public override void Teleport(int NumberOfLine)
        {
            Pause(400);
            TopMenu(12);                     // Click Teleport menu

            Point pointTeleportNumbertLine = new Point(405 - 5 + xx, 198 - 5 + (NumberOfLine - 1) * 15 + yy);    //              тыкаем в указанную строчку телепорта  405 - 5 + xx, 198 - 5 + yy

            pointTeleportNumbertLine.DoubleClickL();   // Указанная строка в списке телепортов
            Pause(500);

            pointTeleportExecute.PressMouseL();        // Click on button Execute in Teleport menu
            Pause(2000);
        }


        #endregion

        #region at Work

        /// <summary>
        /// пополняем патроны в кармане на 10 000 штук
        /// </summary>
        public override void AddBullet10000()
        {
        }

        /// <summary>
        /// проверяем, находится ли в инвентае 248 вещей 
        /// </summary>
        /// <returns></returns>
        public override bool is248Items()
        {
            return true;
        }

        #endregion

        #region заточка

        /// <summary>
        /// переносим (DragAndDrop) одну из частей экипировки на место для заточки
        /// </summary>
        /// <param name="numberOfEquipment">номер экипировки п/п</param>
        public override void MoveToSharpening(int numberOfEquipment)
        {
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

        #region BH

        /// <summary>
        /// проверка миссии по цвету контрольной точки
        /// </summary>
        /// <returns> номер цвета </returns>
        public override uint ColorOfMissionBH()
        {
            return new PointColor(700 - 30 + xx, 500 - 30 + yy, 0, 0).GetPixelColor();
        }

        /// <summary>
        /// отбегаю в сторону, чтобы бот не стрелял
        /// </summary>
        public override void runAway()
        {
            iPoint pointNotToShoot = new Point(300 - 5 + xx, 300 - 5 + yy);
            // отбегаю в сторону. чтобы бот не стрелял  
            pointNotToShoot.DoubleClickL();
            botwindow.Pause(4000);
        }


        ///// <summary>
        ///// нажать указанную строку в диалоге в воротах Infinity BH. Отсчет снизу вверх
        ///// </summary>
        ///// <param name="number"></param>
        //public override void PressStringInfinityGateBH(int number)
        //{
        //    iPoint pointString = new Point(839 - 30 + xx, 363 - 30 + yy - (number - 1) * 19);
        //    pointString.PressMouse();
        //    Pause(2000);
        //}

        #endregion


    }
}


