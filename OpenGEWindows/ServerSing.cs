﻿using System;
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
    public class ServerSing : ServerInterface
    {


        /// <summary>
        /// конструктор
        /// town отвечает за методы для конкретного города (паттерн Стратегия). Все различия в действиях, зависящих от города, инкапсулированы в семействе классов Town (в т.ч. AmericaTown)
        /// </summary>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        public ServerSing(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            this.townFactory = new SingTownFactory(botwindow);                                     // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();
            this.pathClient = path_Client();
            this.activeWindow = Sing_active();
            this.pointIsSale1 = new PointColor(907 + xx, 675 + yy, 7200000, 5);
            this.pointIsSale2 = new PointColor(907 + xx, 676 + yy, 7800000, 5);
            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7900000, 5);
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7900000, 5);
            this.pointIsClickSale1 = new PointColor(731 - 5 + xx, 662 - 5 + yy, 7900000, 5);
            this.pointIsClickSale2 = new PointColor(731 - 5 + xx, 663 - 5 + yy, 7900000, 5);

            this.pointIsTown11 = new PointColor(24 + xx, 692 + yy, 11053000, 3);       //точки для проверки стойки с ружьем
            this.pointIsTown12 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown21 = new PointColor(279 + xx, 692 + yy, 11053000, 3);
            this.pointIsTown22 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown31 = new PointColor(534 + xx, 692 + yy, 11053000, 3);
            this.pointIsTown32 = new PointColor(535 + xx, 692 + yy, 10921000, 3);

            this.pointIsTown_11 = new PointColor(24 + xx, 692 + yy, 16777000, 3);       //точки для проверки эксп стойки с дробашом
            this.pointIsTown_12 = new PointColor(25 + xx, 692 + yy, 3552000, 3);
            this.pointIsTown_21 = new PointColor(279 + xx, 692 + yy, 16777000, 3);
            this.pointIsTown_22 = new PointColor(280 + xx, 692 + yy, 3552000, 3);
            this.pointIsTown_31 = new PointColor(534 + xx, 692 + yy, 16777000, 3);
            this.pointIsTown_32 = new PointColor(535 + xx, 692 + yy, 3552000, 3);

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

            this.pointisBoxOverflow1 = new PointColor(522 - 5 + xx, 434 - 5 + yy, 7800000, 5);          //
            this.pointisBoxOverflow2 = new PointColor(522 - 5 + xx, 435 - 5 + yy, 7800000, 5);
            this.pointisSummonPet1 = new PointColor(494 - 5 + xx, 304 - 5 + yy, 13000000, 6);
            this.pointisSummonPet2 = new PointColor(494 - 5 + xx, 305 - 5 + yy, 13000000, 6);
            this.pointisActivePet1 = new PointColor(493 - 5 + xx, 310 - 5 + yy, 13000000, 6);
            this.pointisActivePet2 = new PointColor(494 - 5 + xx, 309 - 5 + yy, 13000000, 6);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //не проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц

            this.pointisLogout1 = new PointColor(565 - 5 + xx, 530 - 5 + yy, 16400000, 5);       // проверено   слово Leave Game
            this.pointisLogout2 = new PointColor(565 - 5 + xx, 531 - 5 + yy, 16400000, 5);       // проверено
            this.pointisBarack1 = new PointColor(64 - 5 + xx, 151 - 5 + yy, 2420000, 4);            //зеленый цвет в слове Barracks  // не проверено
            this.pointisBarack2 = new PointColor(64 - 5 + xx, 154 - 5 + yy, 2420000, 4);            // не проверено
            this.pointisBarack3 = new PointColor(36 - 5 + xx, 53 - 5 + yy, 15100000, 5);             //проверено   Baack Mode
            this.pointisBarack4 = new PointColor(36 - 5 + xx, 54 - 5 + yy, 15500000, 5);             //проверено

            this.pointisWork1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //проверено
            this.pointisWork2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            this.pointisWork_1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork__1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            this.pointisWork__2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);

            this.pointisOpenMenuPet1 = new PointColor(474 - 5 + xx, 219 - 5 + yy, 12000000, 6);      //834 - 5, 98 - 5, 12400000, 835 - 5, 98 - 5, 12400000, 5);             //проверено
            this.pointisOpenMenuPet2 = new PointColor(474 - 5 + xx, 220 - 5 + yy, 12000000, 6);

            this.pointisOpenTopMenu21 = new PointColor(328 + xx, 74 + yy, 13420000, 4);      //333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu22 = new PointColor(329 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu61 = new PointColor(455 + xx, 87 + yy, 13420000, 4);      //460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu62 = new PointColor(456 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu81 = new PointColor(553 + xx, 87 + yy, 13420000, 4);      //558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu82 = new PointColor(554 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu91 = new PointColor(601 + xx, 74 + yy, 13420000, 4);      //606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);            //проверено
            this.pointisOpenTopMenu92 = new PointColor(602 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu121 = new PointColor(502 - 5 + xx, 140 - 5 + yy, 12800000, 5);      //507 - 5, 140 - 5, 12440000, 508 - 5, 140 - 5, 12440000, 4);        //проверено
            this.pointisOpenTopMenu122 = new PointColor(502 - 5 + xx, 141 - 5 + yy, 12800000, 5);
            this.pointisOpenTopMenu131 = new PointColor(404 - 5 + xx, 278 - 5 + yy, 16500000, 5);          //Quest Name                                                         //проверено
            this.pointisOpenTopMenu132 = new PointColor(404 - 5 + xx, 279 - 5 + yy, 16500000, 5);

            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      //360, 537
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //1392 - 875, 438 - 5
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //1392 - 875, 428 - 5

            this.pointGotoEnd = new Point(685 - 5 + xx, 440 - 5 + yy);            //логаут
            //this.pointGotoEnd = new Point(680 + xx, 432 + yy);                  //для CatzMods - logout (только в том случае, если надо сохранять настройки бота)

            this.pointTeamSelection1 = new Point(140 - 5 + xx, 470 - 5 + yy);                   //проверено
            this.pointTeamSelection2 = new Point(70 - 5 + xx, 355 - 5 + yy);                   //проверено
            this.pointTeamSelection3 = new Point(50 - 5 + xx, 620 - 5 + yy);                   //проверено

            this.pointTeleport1 = new Point(400 + xx, 193 + yy);   //400, 193               тыкаем в первую строчку телепорта                          //проверено
            this.pointTeleport2 = new Point(355 + xx, 570 + yy);   //355, 570               тыкаем в кнопку Execute                   //проверено
            this.pointCancelSummonPet = new Point(410 - 5 + xx, 390 - 5 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(540 - 5 + xx, 380 - 5 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(410 - 5 + xx, 360 - 5 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet  = new Point(410 - 5 + xx, 410 - 5 + yy);                   // //Click Button Active Pet                            //проверено

            this.pointTeleportToTownAltW = new Point(801 + xx, 564 + yy + (botwindow.getNomerTeleport() - 1) * 17);   //801, 564 + (botwindow.getNomerTeleport() - 1) * 17);

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
            this.pointButtonLogOut = new Point(955 - 5 + xx, 700 - 5 + yy);               //кнопка логаут в казарме

            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 13000000, 5);  //проверяем открыто ли окно с токенами
            this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 13000000, 5);
            this.pointToken = new Point(755 - 5 + xx, 94 - 5 + yy);                       //крестик в углу окошка с токенами
            this.pointChooseChannel = new Point(820 - 5 + xx, 382 - 5 + yy);                       //переход из меню Alt+Q в меню Alt+F2 (нажатие кнопки Choose a channel)
            this.pointEnterChannel = new Point(646 - 5 + xx, 409 - 5 + yy + (botwindow.getKanal() - 2) * 15);                        //выбор канала в меню Alt+F2
            this.pointMoveNow = new Point(651 - 5 + xx, 591 - 5 + yy);                        //выбор канала в меню Alt+F2
            this.pointCure1 = new Point(215 - 5 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой U
            this.pointCure2 = new Point(215 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой J
            this.pointCure3 = new Point(215 - 5 + 255 * 2 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой M
            this.pointMana1 = new Point(215 - 5 + 30 + xx, 705 - 5 + yy);                        //бутылка маны под буквой I


            this.pointNewName = new Point(490 - 5 + 30 + xx, 280 - 5 + yy);                        //строчка, куда надо вводить имя семьи
            this.pointButtonCreateNewName = new Point(465 - 5 + xx, 510 - 5 + yy);                        //кнопка Create для создания новой семьи
        }                          //конструктор

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
        ///// активируем уже призванного пета
        ///// </summary>
        //public void ActivePet()
        //{
        //    //botwindow.PressMouse(408, 405);  //Click Button Active Pet
        //    pointActivePet.PressMouse(); //Click Button Active Pet
        //    botwindow.Pause(2500);
        //}


        ///// <summary>
        ///// нажимаем кнопку Summon в меню пет
        ///// </summary>
        //public void buttonCancelSummonPet()
        //{
        //    pointCancelSummonPet.PressMouseL();   //Click Cancel Summon
        //    pointCancelSummonPet.PressMouseL();
        //    //botwindow.PressMouseL(408, 390); //Click Cancel Summon
        //    //botwindow.PressMouseL(408, 390);
        //    botwindow.Pause(1000);
        //}


        ///// <summary>
        ///// вызываем телепорт через верхнее меню и телепортируемся по номеру телепорта 
        ///// </summary>
        ///// <param name="numberTeleport"> номер телепорта по порядку </param>
        //public void Teleport()
        //{
        //    botwindow.Pause(400);
        //    TopMenu(12); //Click Teleport menu
        //    //            botwindow.PressMouseL(400, 190 );
        //    pointTeleport1.PressMouseL();
        //    botwindow.Pause(50);
        //    //botwindow.PressMouseL(400, 190 );
        //    pointTeleport1.PressMouseL();
        //    botwindow.Pause(200);
        //    //botwindow.PressMouseL(355, 570); //Click on button Execute in Teleport menu
        //    pointTeleport2.PressMouseL();   //Click on button Execute in Teleport menu
        //    botwindow.Pause(200);
        //}


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
        //    {
        //        counter++;
        //        botwindow.TopMenu(6, 3);
        //        botwindow.Pause(500);
        //        result = !town.isOpenTownTeleport();
        //        if (result) botwindow.PressEscThreeTimes();
        //        botwindow.Pause(500);
        //    }
        //}

        ///// <summary>
        ///// Открыть городской телепорт (Alt + F3) без проверок и while (для паттерна Состояние)  StateGT
        ///// </summary>
        //public void OpenTownTeleportForState()
        //{
        //    TopMenu(6, 3);
        //    botwindow.Pause(1000);
        //}

        ///// <summary>
        ///// Открыть карту местности (Alt + Z). Если карта местности не открывается, то производится повторная попытка     
        ///// </summary>
        //public void OpenMap()
        //{
        //    int counter = 0;
        //    bool result = true;
        //    while ((result) & (counter < 5))
        //    {
        //        counter++;
        //        botwindow.TopMenu(6, 2);
        //        botwindow.Pause(500);
        //        result = !town.isOpenMap();
        //        if (result) botwindow.PressEscThreeTimes();
        //        botwindow.Pause(1000);
        //    }
        //}

        ///// <summary>
        ///// Открыть карту местности (Alt + Z) для группы классов StateGT (паттерн Состояние)
        ///// </summary>
        //public void OpenMapForState()
        //{
        //    //botwindow.TopMenu(6, 2);
        //    //botwindow.Pause(1000);
        //    botwindow.PressMouse(470, 55);
        //    botwindow.PressMouse(458, 117);
        //}

//        /// <summary>
//        /// проверяет, находится ли данное окно в магазине (а точнее на странице входа в магазин) 
//        /// </summary>
//        /// <returns> true, если находится в магазине </returns>
//        public bool isSale()
//        {
////            return botwindow.isColor2(902, 673, 7850000, 903, 673, 7720000, 4);
//            return ((pointIsSale1.isColor()) && (pointIsSale2.isColor()));
//            //            return botwindow.isColor2(902, 673, 7850000, 903, 673, 7850000, 4);

//        }

        ///// <summary>
        ///// проверяет, находится ли данное окно в самом магазине (на закладке BUY или SELL)                                                    // доработать!!!!!
        ///// </summary>
        ///// <returns> true, если находится в магазине </returns>
        //public bool isSale2()
        //{
        //    return true;
        //}                                             // доработать!!!!!


        ///// <summary>
        ///// Покупка митридата в количестве 333 штук
        ///// </summary>
        //public void BuyingMitridat()
        //{
        //    botwindow.PressMouseL(360, 537);          //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //    botwindow.Pause(150);

        //    botwindow.Press333();

        //    botwindow.Botton_BUY();                             // Нажимаем на кнопку BUY 

        //    botwindow.PressMouseL(1392 - 875, 438 - 5);          //кликаем левой кнопкой мыши в кнопку Ок, если переполнение митридата
        //    botwindow.Pause(500);

        //    botwindow.PressMouseL(1392 - 875, 428 - 5);          //кликаем левой кнопкой мыши в кнопку Ок, если нет денег на покупку митридата
        //    botwindow.Pause(500);
        //}

        ///// <summary>
        ///// Покупка ящиков для джеков в магазине 
        ///// </summary>
        //private void BuyingToolsetBox()
        //{
        //            bool ff = true;
        //            int ii = 0;
        //            uint sss;

        //            while (ff)
        //            {
        //                sss = botwindow.GetPixelColor(149 - 5, 219 - 5);                 // проверка цвета текущего товара
        //                if (sss == 7305078) ff = false; // Дошли до ящиков для джеков
        //                else
        //                {
        //                    ii++;
        //                    if (ii >= 230) ff = false;     // Страховка против бесконечного цикла
        //                    else botwindow.PressMouseWheelDown(345 + 30, 190 + 30);
        //                        //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
        //                    botwindow.Pause(150);
        //                }
        //            }  //Конец цикла
        //            botwindow.PressMouseL(335, 220);   //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //            botwindow.Pause(150);

        //            botwindow.Press44444();

        //            botwindow.Pause(1500);
        //            botwindow.Botton_Sell();            // Нажимаем на кнопку BUY (она там же, где и Sell)

        //            botwindow.PressMouseL(1392 - 875, 433);       //кликаем левой кнопкой мыши в кнопку Ок, если переполнение ящиков
        //            botwindow.Pause(1500);
        //}

//        /// <summary>
//        /// проверяет, открыта ли закладка Sell в магазине 
//        /// </summary>
//        /// <returns> true, если закладка Sell в магазине открыта </returns>
//        public bool isClickSell()
//        {
//            return ((pointIsClickSale1.isColor()) && (pointIsClickSale2.isColor()));
//            //return botwindow.isColor2(730, 660, 7390000, 730, 659, 7390000, 4);

////            return botwindow.isColor2(726, 660, 7920000, 726, 659, 7920000, 4);
//        }

        ///// <summary>
        ///// Продажа товара в магазине, после продажи окно с ботом будет закрыто
        ///// </summary>
        //public void CompleteSaleOneWindow()
        //{
        //    town.ClickSellAndOkInTrader(); //нажимает строчку Sell и кнопку Ок при входе в магазин (по разному в разных городах)

        //    BuyingToolsetBox();              //покупка ящиков
        //    //BuyingMitridat();              // покупка митридата

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
        //    //botwindow.PressMouse(680, 432);   // Кликаю "Logout"
        //}

//        /// <summary>
//        /// метод проверяет, находится ли данное окно в городе (проверка по стойке, работает только с ружьем) 
//        /// </summary>
//        /// <returns> true, если бот находится в городе </returns>
//        public bool isTown()
//        {
////            return isColor2(24, 692, 11053000, 25, 692, 10921000, 3);
//            return false;
//        }

//        /// <summary>
//        /// метод проверяет, переполнился ли карман (выскочило ли уже сообщение о переполнении)
//        /// </summary>
//        /// <returns> true, еслм карман переполнен </returns>
//        public bool isBoxOverflow()
//        {
////            return botwindow.isColor2(1392 - 875, 432, 7800000, 1392 - 875, 432, 7800000, 5);

//            //   return botwindow.isColor2(548 - 30, 462 - 30, 7800000, 547 - 30, 458 - 30, 7600000, 5);
//            return (pointisBoxOverflow1.isColor() && pointisBoxOverflow2.isColor());

//        }

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
        //    } while ((!town.isSecondBookmark()) & (counter < 5));

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
        ///// <returns> true, если находится на стадии ввода логина-пароля </returns>
        //public bool isLogout()
        //{
        //    //return botwindow.isColor2(921 - 725, 250 - 155, 16700000, 921 - 725, 250 - 155, 16700000, 5);

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

//        /// <summary>
//        /// проверяем, в бараках ли бот
//        /// </summary>
//        /// <returns> true, если бот в бараках </returns>
//        public bool isBarack()
//        {
////            return botwindow.isColor2(64 - 5, 152 - 5, 2420000, 64 - 5, 153 - 5, 2420000, 4);    //зеленый цвет в слове Barracks

//            //   return botwindow.isColor2(61 - 5, 151 - 5, 2420000, 61 - 5, 154 - 5, 2420000, 4);
//            return (pointisBarack1.isColor() && pointisBarack2.isColor());

//        }

//        ///// <summary>
//        ///// выход из казармы в логаут, т.е. переход из состояния "Барак" в состояние "логаут"
//        ///// </summary>
//        //public void LogOutFromBarack()
//        //{
//        //    botwindow.PressMouseL(933 - 5, 700 - 5); // нажимаем LogOut
//        //    botwindow.PressMouseL(933 - 5, 700 - 5); // нажимаем LogOut
//        //}


//        /// <summary>
//        /// метод проверяет, открылось ли меню с петом Alt + P
//        /// </summary>
//        /// <returns> true, если открыто </returns>
//        public bool isOpenMenuPet()
//        {
////            return botwindow.isColor2(471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
//            //return botwindow.isColor2(471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
//            return (pointisOpenMenuPet1.isColor() && pointisOpenMenuPet2.isColor());

//        }

        

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

        //оставляем их тут
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private String getPathClient()
        //{ return this.pathClient; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private String path_Client()
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\Singapoore_path.txt"); }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int Sing_active()
        { return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\Singapoore_active.txt")); }

        /// <summary>
        /// запуск клиента игры
        /// </summary>
        public override void runClient()
        {
            //для чистого окна
            //Process.Start(getPathClient());                             //запускаем саму игру или бот Catzmods
            //botwindow.Pause(10000);

            //если CatzMods
            Process.Start(getPathClient());                                    //запускаем саму игру или бот Catzmods
            Pause(10000);
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1110, 705, 1);        //нажимаем кнопку "старт" в боте      
            Pause(500);
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1222, 705, 1);        //нажимаем кнопку "Close" в боте
        }

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
                    //                    result = botwindow.isColor2(333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);  //не не проверено
                    result = (pointisOpenTopMenu21.isColor() && pointisOpenTopMenu22.isColor());
                    break;
                case 6:
                    //                    result = botwindow.isColor2(460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);  //не не проверено
                    result = (pointisOpenTopMenu61.isColor() && pointisOpenTopMenu62.isColor());
                    break;
                case 8:
                    //result = botwindow.isColor2(558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);  //не не проверено
                    result = (pointisOpenTopMenu81.isColor() && pointisOpenTopMenu82.isColor());
                    break;
                case 9:
                    //result = botwindow.isColor2(606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);  //не не проверено
                    result = (pointisOpenTopMenu91.isColor() && pointisOpenTopMenu92.isColor());
                    break;
                case 12:
                    //result = botwindow.isColor2(507 - 5, 140 - 5, 12440000, 508 - 5, 140 - 5, 12440000, 4);  //не проверено
                    uint bb = pointisOpenTopMenu121.GetPixelColor();
                    uint dd = pointisOpenTopMenu122.GetPixelColor();
                    result = (pointisOpenTopMenu121.isColor() && pointisOpenTopMenu122.isColor());
                    break;
                case 13:
                    //result = botwindow.isColor2(371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);  //не не проверено
                    //uint bb = pointisOpenTopMenu131.GetPixelColor();
                    //uint dd = pointisOpenTopMenu132.GetPixelColor();
                    result = (pointisOpenTopMenu131.isColor() && pointisOpenTopMenu132.isColor());
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }

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
                botwindow.Pause(1000);
            } while (!isOpenTopMenu(numberOfThePartitionMenu));
        }

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
        }

        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public override void TeleportToTownAltW()
        {
            // отбегаю в сторону. чтобы бот не стрелял               
            botwindow.PressMouseL(300, 300);
            botwindow.Pause(10000);
            botwindow.PressMouseL(350, 350);
            botwindow.Pause(10000);
            //botwindow.PressMouseL(150, 150);
            //botwindow.Pause(10000);

            TopMenu(6, 1);
            botwindow.Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            botwindow.Pause(2000);
        }                                                                                                               

        ///// <summary>
        ///// проверяет, призван ли пет
        ///// </summary>
        ///// <returns> true, если призван </returns>
        //public bool isSummonPet()
        //{
        //    return true;
        //}

        //        /// <summary>
        //        /// метод проверяет, находится ли данное окно на работе (проверка по стойке, работает только с ружьем)
        //        /// </summary>
        //        /// <returns> true, если сейчас на рабочей карте </returns>
        //        public bool isWork()
        //        {
        //            return false; 
        //        }                                                                                                           ///доделать

    }
}

