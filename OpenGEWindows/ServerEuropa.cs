using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OpenGEWindows
{
    public class ServerEuropa : Server 
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(UIntPtr myhWnd, int myhwndoptional, int xx, int yy, int cxx, int cyy, uint flagus); // Перемещает окно в заданные координаты с заданным размером

        //[DllImport("user32.dll")]
        //private static extern bool ShowWindow(UIntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern UIntPtr FindWindow(String ClassName, String WindowName);  //ищет окно с заданным именем и классом

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public ServerEuropa(botWindow botwindow)
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region общие 2

            TownFactory townFactory = new EuropaTownFactory(botwindow);                      // здесь выбирается конкретная реализация для фабрики Town
            this.town = townFactory.createTown();                                            // выбирается город с помощью фабрики
            this.pathClient = path_Client();

            #endregion

            #region No window

            

            #endregion

            #region Logout

            this.pointConnect   = new PointColor(522 - 5 + xx, 418 - 5 + yy, 6100000, 5); 
            this.pointisLogout1 = new PointColor(362 - 5 + xx, 315 - 5 + yy, 15000000, 6);    //проверено
            this.pointisLogout2 = new PointColor(362 - 5 + xx, 317 - 5 + yy, 15000000, 6);

            #endregion

            #region Pet

            this.pointisOpenMenuPet1 = new PointColor(829 + xx, 93 + yy, 12000000, 6);   
            this.pointisOpenMenuPet2 = new PointColor(830 + xx, 93 + yy, 12000000, 6);
            this.pointisSummonPet1 = new PointColor(740 - 5 + xx, 237 - 5 + yy, 7400000, 5);
            this.pointisSummonPet2 = new PointColor(741 - 5 + xx, 237 - 5 + yy, 7400000, 5);
            this.pointisActivePet1 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13000000, 6);
            this.pointisActivePet2 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13000000, 6);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц
            this.pointCancelSummonPet = new Point(750 + xx, 265 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(868 + xx, 258 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(748 + xx, 238 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet = new Point(748 + xx, 288 + yy);                   // //Click Button Active Pet                            //проверено

            #endregion

            #region Top Menu

            this.pointGotoEnd = new Point(681 - 5 + xx, 436 - 5 + yy);                              //логаут
//            this.pointGotoEnd = new Point(681 - 5 + xx, 467 - 5 + yy);                              //end
            this.pointLogout = new Point(681 - 5 + xx, 436 - 5 + yy);            //логаут
            this.pointTeleport1 = new Point(400 + xx, 193 + yy);   //400, 193               тыкаем в первую строчку телепорта                          //проверено
            this.pointTeleport2 = new Point(355 + xx, 570 + yy);   //355, 570              тыкаем в кнопку Execute                   //проверено
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

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(903 + xx, 674 + yy, 7590000, 4);
            this.pointIsSale2 = new PointColor(904 + xx, 674 + yy, 7850000, 4);
            this.pointIsSale21 = new PointColor(840 - 5 + xx, 665 - 5 + yy, 7720000, 4);
            this.pointIsSale22 = new PointColor(840 - 5 + xx, 668 - 5 + yy, 7720000, 4);
            this.pointIsClickSale1 = new PointColor(728 + xx, 660 + yy, 7720000, 4);
            this.pointIsClickSale2 = new PointColor(728 + xx, 659 + yy, 7720000, 4);
            this.pointBookmarkSell = new Point(225 + xx, 163 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonClose = new Point(847 + xx, 663 + yy);   //847, 663);

            #endregion

            #region atWork

            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      //360, 537
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //1392 - 875, 438 - 5
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //1392 - 875, 428 - 5
            this.pointisBoxOverflow1 = new PointColor(523 - 5 + xx, 438 - 5 + yy, 7100000, 5);            //     это правильные точки для определения, переполнился карман или нет
            this.pointisBoxOverflow2 = new PointColor(524 - 5 + xx, 438 - 5 + yy, 7600000, 5);
            //this.pointisBoxOverflow1 = new PointColor(573 - 5 + xx, 488 - 5 + yy, 7500000, 5);          //это неправильные точки. сигнализация о наполненном кармане никогда не сработает
            //this.pointisBoxOverflow2 = new PointColor(574 - 5 + xx, 488 - 5 + yy, 7800000, 5);
            this.pointisWork_RifleDot1 = new PointColor(24 + xx, 692 + yy, 11051000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //проверено
            this.pointisWork_RifleDot2 = new PointColor(25 + xx, 692 + yy, 10919000, 3);
            this.pointisWork_ExpRifleDot1 = new PointColor(24 + xx, 692 + yy, 1721000, 3);      //29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);                    //не проверено
            this.pointisWork_ExpRifleDot2 = new PointColor(25 + xx, 692 + yy, 2106000, 3);
            this.pointisWork_DrobDot1 = new PointColor(24 + xx, 692 + yy, 7644000, 3);              //проверка по обычной стойке с дробашем
            this.pointisWork_DrobDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            this.pointisWork_VetDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по вет стойке с дробашем          не проверено
            this.pointisWork_VetDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork_ExpDrobDot1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по эксп стойке с дробашем
            this.pointisWork_ExpDrobDot2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            this.pointisWork_JainaDrobDot1 = new PointColor(24 + xx, 692 + yy, 4278000, 3);              //проверка по эксп стойке с дробашем Jaina
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

            this.pointisKillHero1 = new PointColor(75 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero2 = new PointColor(330 + xx, 631 + yy, 1900000, 4);
            this.pointisKillHero3 = new PointColor(585 + xx, 631 + yy, 1900000, 4);
            this.pointisLiveHero1 = new PointColor(80 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointisLiveHero2 = new PointColor(335 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointisLiveHero3 = new PointColor(590 - 5 + xx, 636 - 5 + yy, 4200000, 5);
            this.pointSkillCook = new Point(183 - 5 + xx, 700 - 5 + yy);
            this.pointisBattleMode1 = new PointColor(173 - 5 + xx, 511 - 5 + yy, 8900000, 5);
            this.pointisBattleMode2 = new PointColor(200 - 5 + xx, 511 - 5 + yy, 8900000, 5);
            this.pointisWork_VetPistolDot1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 13086000, 3);           //проверка по стойке с вет пистолетом Outrange
            this.pointisWork_VetPistolDot2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 12954000, 3);

            #endregion

            #region inTown

            this.pointCure1 = new Point(215 - 5 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой U
            this.pointCure2 = new Point(215 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой J
            this.pointCure3 = new Point(215 - 5 + 255 * 2 + xx, 705 - 5 + yy);                        //бутылка лечения под буквой M
            this.pointMana1 = new Point(215 - 5 + 30 + xx, 705 - 5 + yy);                        //бутылка маны под буквой I
            this.pointMana2 = new Point(245 - 5 + 255 + xx, 705 - 5 + yy);                        //бутылка маны под буквой K
            this.pointMana3 = new Point(245 - 5 + 510 + xx, 705 - 5 + yy);                        //бутылка маны под буквой ,
            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 13000000, 5);  //проверяем открыто ли окно с токенами
            this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 13000000, 5);
            this.pointToken = new Point(755 - 5 + xx, 94 - 5 + yy);                       //крестик в углу окошка с токенами
            this.pointIsTown_RifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 11053000, 3);       //точки для проверки стойки с ружьем
            this.pointIsTown_RifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown_RifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 11053000, 3);
            //this.pointIsTown_RifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 10921000, 3);

            this.pointIsTown_ExpRifleFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с ружьем
            this.pointIsTown_ExpRifleFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpRifleSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpRifleSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_ExpRifleThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_ExpRifleThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_DrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки обычной стойки с дробашом в городе               
            this.pointIsTown_DrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_DrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_DrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

            this.pointIsTown_VetDrobFirstDot1 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки вет стойки с дробашом в городе              не проверено             
            this.pointIsTown_VetDrobFirstDot2 = new PointColor(25 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_VetDrobSecondDot1 = new PointColor(279 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_VetDrobSecondDot2 = new PointColor(280 + xx, 692 + yy, 16711000, 3);
            //this.pointIsTown_VetDrobThirdDot1 = new PointColor(534 + xx, 692 + yy, 7631000, 3);
            //this.pointIsTown_VetDrobThirdDot2 = new PointColor(535 + xx, 692 + yy, 16711000, 3);

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

            this.pointisBarack1 = new PointColor(104 - 5 + xx, 152 - 5 + yy, 2350000, 4);       //проверено
            this.pointisBarack2 = new PointColor(104 - 5 + xx, 155 - 5 + yy, 2350000, 4);
            this.pointisBarack3 = new PointColor(81 - 5 + xx, 63 - 5 + yy, 15300000, 5);       //проверено
            this.pointisBarack4 = new PointColor(81 - 5 + xx, 64 - 5 + yy, 13700000, 5);
            this.pointButtonLogoutFromBarack = new Point(955 - 5 + xx, 700 - 5 + yy);               //кнопка логаут в казарме
            this.pointTeamSelection1 = new Point(140 - 5 + xx, 470 - 5 + yy);                   //проверено
            this.pointTeamSelection2 = new Point(70 - 5 + xx, 355 - 5 + yy);                   //проверено
            this.pointTeamSelection3 = new Point(50 - 5 + xx, 620 - 5 + yy);                   //проверено
            this.sdvigY = 15;
            this.pointChooseChannel = new Point(820 - 5 + xx, 382 - 5 + yy);                       //переход из меню Alt+Q в меню Alt+F2 (нажатие кнопки Choose a channel)
            this.pointEnterChannel = new Point(646 - 5 + xx, 409 - 5 + yy + (botwindow.getKanal() - 2) * 15);                        //выбор канала в меню Alt+F2
            this.pointMoveNow = new Point(651 - 5 + xx, 591 - 5 + yy);                        //выбор канала в меню Alt+F2
            this.pointNewPlace = new Point(85 + xx, 670 + yy);

            #endregion

            #region новые боты

            pointPetBegin = new Point(800 - 5 + xx, 220 - 5 + yy);    // 800-5, 220-5
            pointPetEnd = new Point(520 - 5 + xx, 330 - 5 + yy);    // 520-5, 330-5
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
            this.pointDomingoOnMap = new Point(810 - 5 + xx, 130 - 5 + yy);                        //нажимаем на Доминго на карте Alt+Z
            this.pointPressDomingo = new Point(510 - 5 + xx, 425 - 5 + yy);                        //нажимаем на Доминго
            this.pointFirstStringDialog = new Point(520 - 5 + xx, 660 - 5 + yy);                   //нажимаем Yes в диалоге Доминго (нижняя строчка)
            this.pointSecondStringDialog = new Point(520 - 5 + xx, 640 - 5 + yy);                  //нажимаем Yes в диалоге Доминго второй раз (вторая строчка снизу)
            this.pointDomingoMiss = new Point(396 - 5 + xx, 206 - 5 + yy);                         //нажимаем правой кнопкой по карте миссии Доминго
            this.pointPressDomingo2 = new Point(572 - 5 + xx, 237 - 5 + yy);                       //нажимаем на Доминго после миссии
            this.pointLindonOnMap = new Point(820 - 5 + xx, 385 - 5 + yy);                         //нажимаем на Линдона на карте Alt+Z
            this.pointPressLindon2 = new Point(655 - 5 + xx, 255 - 5 + yy);                        //нажимаем на Линдона
            this.pointPetExpert = new Point(910 - 5 + xx, 415 - 5 + yy);                           //нажимаем на петэксперта
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
            #endregion

            #region заточка
            #endregion

            #region чиповка
            #endregion

            #region передача песо
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

        }  


        // ===============================  Методы ==================================================

        #region Общие методы 2

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

        #endregion

        #region No window

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

        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public override void TeleportToTownAltW(int nomerTeleport)
        {
            iPoint pointTeleportToTownAltW = new Point(806 - 5 + xx, 517 - 5 + yy + (nomerTeleport - 1) * 17);

            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            Pause(2000);
        }

        #endregion

        #region at Work

        /// <summary>
        /// проверяем, находится ли в инвентае 248 вещей 
        /// </summary>
        /// <returns></returns>
        public override bool is248Items()
        {
            return true;
        }

        #endregion


        #region Заточка

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
    }
}
