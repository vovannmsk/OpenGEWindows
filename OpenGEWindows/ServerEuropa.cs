﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace OpenGEWindows
{
    public class ServerEuropa : ServerInterface 
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public ServerEuropa(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            TownFactory townFactory = new EuropaTownFactory(botwindow);                      // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();                                            // выбирается город с помощью фабрики
            this.pathClient = path_Client();
//            this.activeWindow = Europa_active();
            this.pointIsSale1 = new PointColor(903 + xx, 674 + yy, 7590000, 4);          
            this.pointIsSale2 = new PointColor(904 + xx, 674 + yy, 7850000, 4);
            this.pointIsSale21 = new PointColor(840 - 5 + xx, 665 - 5 + yy, 7720000, 4); 
            this.pointIsSale22 = new PointColor(840 - 5 + xx, 668 - 5 + yy, 7720000, 4);
            this.pointIsClickSale1 = new PointColor(728 + xx, 660 + yy, 7720000, 4);     
            this.pointIsClickSale2 = new PointColor(728 + xx, 659 + yy, 7720000, 4);

            this.pointIsTown_RifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 11053000, 3);       //точки для проверки стойки с ружьем
            this.pointIsTown_RifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown_RifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 11053000, 3);       
            this.pointIsTown_RifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            this.pointIsTown_RifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 11053000, 3);      
            this.pointIsTown_RifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 10921000, 3);

            this.pointIsTown_ExpRifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с ружьем
            this.pointIsTown_ExpRifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_ExpRifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_ExpRifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_ExpRifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_ExpRifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_DrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки обычной стойки с дробашом в городе               
            this.pointIsTown_DrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_DrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_DrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_DrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_DrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_VetDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки вет стойки с дробашом в городе              не проверено             
            this.pointIsTown_VetDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_VetDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_VetDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            this.pointIsTown_VetDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            this.pointIsTown_VetDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_ExpDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);       //точки для проверки эксп стойки с дробашом
            this.pointIsTown_ExpDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 3552000, 3);
            this.pointIsTown_ExpDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 16777000, 3);
            this.pointIsTown_ExpDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 3552000, 3);
            this.pointIsTown_ExpDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 16777000, 3);
            this.pointIsTown_ExpDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 3552000, 3);

            //this.pointisBoxOverflow1 = new PointColor(523 - 5 + xx, 438 - 5 + yy, 7500000, 5);        это правильные точки для определения, переполнился карман или нет
            //this.pointisBoxOverflow2 = new PointColor(524 - 5 + xx, 438 - 5 + yy, 7800000, 5);
            this.pointisBoxOverflow1 = new PointColor(573 - 5 + xx, 488 - 5 + yy, 7500000, 5);          //это неправильные точки. сигнализация о наполненном кармане никогда не сработает
            this.pointisBoxOverflow2 = new PointColor(574 - 5 + xx, 488 - 5 + yy, 7800000, 5);
            this.pointisSummonPet1 = new PointColor(740 - 5 + xx, 237 - 5 + yy, 7400000, 5);      
            this.pointisSummonPet2 = new PointColor(741 - 5 + xx, 237 - 5 + yy, 7400000, 5);
            this.pointisActivePet1 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13100000, 5);     
            this.pointisActivePet2 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13100000, 5);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц

            //this.pointisLogout1 = new PointColor(211 - 5 + xx, 93 - 5 + yy, 14400000, 5);                                                                           //проверено
            //this.pointisLogout2 = new PointColor(220 - 5 + xx, 93 - 5 + yy, 14400000, 5);
            //this.pointisLogout1 = new PointColor(240 - 5 + xx, 133 - 5 + yy, 13600000, 5);    
            //this.pointisLogout2 = new PointColor(245 - 5 + xx, 133 - 5 + yy, 13600000, 5);
            this.pointisLogout1 = new PointColor(362 - 5 + xx, 315 - 5 + yy, 15000000, 6);    //проверено
            this.pointisLogout2 = new PointColor(362 - 5 + xx, 317 - 5 + yy, 15000000, 6);
            this.pointisBarack1 = new PointColor(104 - 5 + xx, 152 - 5 + yy, 2420000, 4);       //проверено
            this.pointisBarack2 = new PointColor(104 - 5 + xx, 155 - 5 + yy, 2420000, 4);
            this.pointisBarack3 = new PointColor(81 - 5 + xx, 63 - 5 + yy, 7700000, 5);       //проверено
            this.pointisBarack4 = new PointColor(81 - 5 + xx, 64 - 5 + yy, 7700000, 5);

            this.pointisWork_RifleDot1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //проверено
            this.pointisWork_RifleDot2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            this.pointisWork_DrobDot1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            this.pointisWork_DrobDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointisWork_VetDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по вет стойке с дробашем          не проверено
            this.pointisWork_VetDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork_ExpDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_ExpDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);

            this.pointisOpenMenuPet1 = new PointColor(829 + xx, 93 + yy, 12000000, 6);      //834 - 5, 98 - 5, 12400000, 835 - 5, 98 - 5, 12400000, 5);             //проверено
            this.pointisOpenMenuPet2 = new PointColor(830 + xx, 93 + yy, 12000000, 6);
            this.pointisOpenTopMenu21 = new PointColor(328 + xx, 74 + yy, 13420000, 4);      //333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu22 = new PointColor(329 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu61 = new PointColor(455 + xx, 87 + yy, 13420000, 4);      //460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu62 = new PointColor(456 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu81 = new PointColor(553 + xx, 87 + yy, 13420000, 4);      //558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);            //не проверено
            this.pointisOpenTopMenu82 = new PointColor(554 + xx, 87 + yy, 13420000, 4);
            this.pointisOpenTopMenu91 = new PointColor(601 + xx, 74 + yy, 13420000, 4);      //606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);            //проверено
            this.pointisOpenTopMenu92 = new PointColor(602 + xx, 74 + yy, 13420000, 4);
            this.pointisOpenTopMenu121 = new PointColor(502 + xx, 135 + yy, 12440000, 4);      //507 - 5, 140 - 5, 12440000, 508 - 5, 140 - 5, 12440000, 4);        //проверено
            this.pointisOpenTopMenu122 = new PointColor(503 + xx, 135 + yy, 12440000, 4);
            this.pointisOpenTopMenu131 = new PointColor(539 - 5 + xx, 374 - 5 + yy, 16100000, 5);                                                                   //проверено
            this.pointisOpenTopMenu132 = new PointColor(540 - 5 + xx, 374 - 5 + yy, 16500000, 5);

            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      //360, 537
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //1392 - 875, 438 - 5
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //1392 - 875, 428 - 5
            //this.pointGotoEnd = new Point(817 - 5 + xx, 562 - 5 + yy);                              //энд программ
            this.pointGotoEnd = new Point(817 - 5 + xx, 542 - 5 + yy);                              //логаут
            this.pointTeamSelection1 = new Point(140 - 5 + xx, 470 - 5 + yy);                   //проверено
            this.pointTeamSelection2 = new Point(70 - 5 + xx, 355 - 5 + yy);                   //проверено
            this.pointTeamSelection3 = new Point(50 - 5 + xx, 620 - 5 + yy);                   //проверено

            this.pointTeleport1 = new Point(400 + xx, 193 + yy);   //400, 193               тыкаем в первую строчку телепорта                          //проверено
            this.pointTeleport2 = new Point(355 + xx, 570 + yy);   //355, 570              тыкаем в кнопку Execute                   //проверено
            this.pointCancelSummonPet = new Point(750 + xx, 265 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(868 + xx, 258 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(748 + xx, 238 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet = new Point(748 + xx, 288 + yy);                   // //Click Button Active Pet                            //проверено

            this.pointTeleportToTownAltW = new Point(801 + xx, 564 + yy + (botwindow.getNomerTeleport() - 1) * 17);   //801, 564 + (botwindow.getNomerTeleport() - 1) * 17);

            this.sdvigY = 15;

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

            pointPetBegin = new Point(800 - 5 + xx, 220 - 5 + yy);    // 800-5, 220-5
            pointPetEnd = new Point(520 - 5 + xx, 330 - 5 + yy);    // 520-5, 330-5

            this.pointConnect = new PointColor(522 - 5 + xx, 418 - 5 + yy, 6600000, 5);


        }  //конструктор

        /// <summary>
        /// возвращаем тестовый цвет для сравнения в методе Connect
        /// </summary>
        /// <returns> номер цвета </returns>
        public override uint colorTest()
        {
            return 6670287;
        }

         /// <summary>
        /// возвращает параметр, прочитанный из файла
        /// </summary>
        /// <returns></returns>
        private int EuropaActive()
        { return int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\Europa_active.txt")); }

        /// <summary>
        /// возвращает параметр, прочитанный из файла
        /// </summary>
        /// <returns></returns>
        private String path_Client()
        { return File.ReadAllText(KATALOG_MY_PROGRAM + "\\Europa_path.txt"); }

        /// <summary>
        /// запуск клиента игры
        /// </summary>
        public override void runClient()
        {
            Process.Start(getPathClient());
        }

        /// <summary>
        /// метод проверяет, открылось ли верхнее меню 
        /// </summary>
        /// <param name="numberOfThePartitionMenu"></param>
        /// <returns> true, если меню открылось </returns>
        public bool isOpenTopMenu(int numberOfThePartitionMenu)
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
                    //bool ff = pointisOpenTopMenu91.isColor();
                    //bool gg = pointisOpenTopMenu92.isColor();
                    //uint bb = pointisOpenTopMenu91.GetPixelColor();
                    //uint dd = pointisOpenTopMenu92.GetPixelColor();
                    result = (pointisOpenTopMenu91.isColor() && pointisOpenTopMenu92.isColor());
                    break;
                case 12:
                    //result = botwindow.isColor2(507 - 5, 140 - 5, 12440000, 508 - 5, 140 - 5, 12440000, 4);  //проверено
                    result = (pointisOpenTopMenu121.isColor() && pointisOpenTopMenu122.isColor());
                    break;
                case 13:
                    //result = botwindow.isColor2(371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);  //проверено
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

            //do
            //    {
                    pointMenu.PressMouse();
                    //PressMouse(x, y);
                    Pause(1000);
                    //if ((isLogout()) || (!botwindow.isHwnd())) break;    //если вылетели в логаут или закрылось окно с игрой, то выход из цикла.  (29.04.2017) 
                //} while (!isOpenTopMenu(numberOfThePartitionMenu));
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
        //}

        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public override void TeleportToTownAltW()
        {
            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            Pause(2000);
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

        /// <summary>
        /// Определяет, надо ли грузить данное окно с ботом
        /// </summary>
        /// <returns> true означает, что это окно (данный бот) должно быть активно и его надо грузить </returns>
        public override bool isActive()
        {
            bool result = false;
            if (EuropaActive() == 1) result = true;
            return result;
        }

    }
}
