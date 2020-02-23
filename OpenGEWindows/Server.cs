﻿using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using GEBot.Data;
using System.Diagnostics;

namespace OpenGEWindows
{
    public abstract class Server

    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(UIntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern UIntPtr FindWindowEx(UIntPtr hwndParent, UIntPtr hwndChildAfter, string className, string windowName);

        /// <summary>
        /// если окно загружено на другом компе
        /// </summary>
        public static bool AccountBusy;

        #region общие

        protected botWindow botwindow;
        protected GlobalParam globalParam;
        protected ServerParam serverParam;
        protected BotParam botParam;
        protected int xx;
        protected int yy;

        #endregion

        #region общие 2

        protected Town town;
        protected Town town_begin;
        protected Dialog dialog;

        #endregion

        #region параметры, зависящие от сервера

        protected string pathClient;
        protected bool isActiveServer;                       //это свойство можно использовать везде вместо метода isActive()

        #endregion

        #region No Window
        protected iPointColor pointSafeIP1;
        protected iPointColor pointSafeIP2;
        protected iPointColor pointisSteam1;
        protected iPointColor pointisSteam2;
        protected iPointColor pointisNewSteam1;
        protected iPointColor pointisNewSteam2;
        //protected iPointColor pointisError1;
        //protected iPointColor pointisError2;
        protected iPointColor pointisContinueRunning1;
        protected iPointColor pointisContinueRunning2;
        protected iPoint pointSteamLogin1;
        protected iPoint pointSteamLogin2;
        protected iPoint pointSteamPassword;
        protected iPoint pointSteamSavePassword;
        protected iPoint pointSteamOk;
        protected iPoint pointNewSteamOk;
        protected iPoint pointCancelContinueRunning;

        protected const int WIDHT_WINDOW = 1024;
        protected const int HIGHT_WINDOW = 700;

        #endregion

        #region Logout

        protected iPointColor pointConnect;
        protected iPointColor pointisLogout1;
        protected iPointColor pointisLogout2;
        protected iPointColor pointIsServerSelection1;
        protected iPointColor pointIsServerSelection2;
        protected iPoint pointserverSelection;

        #endregion

        #region Pet

        protected iPointColor pointisSummonPet1;
        protected iPointColor pointisSummonPet2;
        protected iPointColor pointisActivePet1;
        protected iPointColor pointisActivePet2;
        protected iPointColor pointisActivePet3;  //3 и 4 сделаны для европы для проверки корма на месяц
        protected iPointColor pointisActivePet4;
        protected iPointColor pointisOpenMenuPet1;
        protected iPointColor pointisOpenMenuPet2;
        protected iPoint pointCancelSummonPet;
        protected iPoint pointSummonPet1;
        protected iPoint pointSummonPet2;
        protected iPoint pointActivePet;

        #endregion

        #region Top Menu
        protected iPointColor pointisOpenTopMenu21;
        protected iPointColor pointisOpenTopMenu22;
        protected iPointColor pointisOpenTopMenu61;
        protected iPointColor pointisOpenTopMenu62;
        protected iPointColor pointisOpenTopMenu81;
        protected iPointColor pointisOpenTopMenu82;
        protected iPointColor pointisOpenTopMenu91;
        protected iPointColor pointisOpenTopMenu92;
        protected iPointColor pointisOpenTopMenu121;
        protected iPointColor pointisOpenTopMenu122;
        protected iPointColor pointisOpenTopMenu131;
        protected iPointColor pointisOpenTopMenu132;
        protected iPoint pointLogout;
        protected iPoint pointGotoEnd;
        protected iPoint pointGotoBarack;
        protected iPoint pointTeleportFirstLine;
        //        protected iPoint pointTeleportSecondLine;
        protected iPoint pointTeleportExecute;

        #endregion

        #region Shop

        protected iPointColor pointIsSale1;
        protected iPointColor pointIsSale2;
        protected iPointColor pointIsSale21;
        protected iPointColor pointIsSale22;
        protected iPointColor pointIsClickSale1;
        protected iPointColor pointIsClickSale2;
        protected iPoint pointBookmarkSell;
        protected iPoint pointSaleToTheRedBottle;
        protected iPoint pointSaleOverTheRedBottle;
        protected iPoint pointWheelDown;
        protected iPoint pointButtonBUY;
        protected iPoint pointButtonSell;
        protected iPoint pointButtonClose;
        protected iPoint pointBuyingMitridat1;
        protected iPoint pointBuyingMitridat2;
        protected iPoint pointBuyingMitridat3;

        /// <summary>
        /// структура для сравнения товаров в магазине
        /// </summary>
        protected struct Product
        {
            public uint color1;
            public uint color2;
            public uint color3;

            /// <summary>
            /// создаем структуру объекта, состоящую из трех точек
            /// </summary>
            /// <param name="xx">сдвиг окна по оси X</param>
            /// <param name="yy">сдвиг окна по оси Y</param>
            /// <param name="numderOfString">номер строки в магазине, где берется товар</param>
            public Product(int xx, int yy, int numderOfString)
            {
                color1 = new PointColor(149 - 5 + xx, 219 - 5 + yy + (numderOfString - 1) * 27, 3360337, 0).GetPixelColor();
                color2 = new PointColor(146 - 5 + xx, 219 - 5 + yy + (numderOfString - 1) * 27, 3360337, 0).GetPixelColor();
                color3 = new PointColor(165 - 5 + xx, 214 - 5 + yy + (numderOfString - 1) * 27, 3360337, 0).GetPixelColor();
            }

            /// <summary>
            /// сравнение двух товаров по трем точкам
            /// </summary>
            /// <param name="product">товар для сравнения</param>
            /// <returns>true, если два товара одинаковы</returns>
            public bool EqualProduct(Product product)
            {
                return ((product.color1 == color1) &&
                        (product.color2 == color2) &&
                        (product.color3 == color3));
            }
        };

        #endregion

        #region atWork

        protected iPointColor pointisKillHero1;      //если перс убит
        protected iPointColor pointisKillHero2;
        protected iPointColor pointisKillHero3;
        protected iPointColor pointisLiveHero1;      //если перс жив
        protected iPointColor pointisLiveHero2;
        protected iPointColor pointisLiveHero3;
        protected iPointColor pointisBoxOverflow1;
        protected iPointColor pointisBoxOverflow2;
        protected iPointColor pointisBoxOverflow3;
        protected iPointColor pointisBoxOverflow4;
        protected iPoint pointSkillCook;
        protected iPointColor pointisBattleMode1;
        protected iPointColor pointisBattleMode2;
        protected uint[] arrayOfColorsIsWork1;
        protected uint[] arrayOfColorsIsWork2;
        protected iPointColor pointisBulletHalf;      //если патронов половина (желтый значок)
        protected iPointColor pointisBulletOff;       //если патронов почти нет (красный значок)

        #endregion

        #region inTown

        protected iPointColor pointisToken1;      //при входе в город проверяем, открыто ли окно с подарочными токенами.
        protected iPointColor pointisToken2;
        protected iPoint pointToken;            //если окно с подарочными токенами открыто, то закрываем его нажатием на эту точку
        protected iPoint pointCure1;
        protected iPoint pointCure2;
        protected iPoint pointCure3;
        protected iPoint pointMana1;
        protected iPoint pointMana2;
        protected iPoint pointMana3;
        protected iPoint pointGM;
        protected iPoint pointHeadGM;
        //protected iPointColor pointIsTown_RifleFirstDot1;   //проверка по обычному ружью
        //protected iPointColor pointIsTown_RifleFirstDot2;
        //protected iPointColor pointIsTown_RifleSecondDot1;
        //protected iPointColor pointIsTown_RifleSecondDot2;
        //protected iPointColor pointIsTown_RifleThirdDot1;
        //protected iPointColor pointIsTown_RifleThirdDot2;
        //protected iPointColor pointIsTown_ExpRifleFirstDot1;   //проверка по эксп. ружью (флинт)
        //protected iPointColor pointIsTown_ExpRifleFirstDot2;
        //protected iPointColor pointIsTown_ExpRifleSecondDot1;
        //protected iPointColor pointIsTown_ExpRifleSecondDot2;
        //protected iPointColor pointIsTown_ExpRifleThirdDot1;
        //protected iPointColor pointIsTown_ExpRifleThirdDot2;
        //protected iPointColor pointIsTown_DrobFirstDot1;      //проверка по обычному дробовику
        //protected iPointColor pointIsTown_DrobFirstDot2;
        //protected iPointColor pointIsTown_DrobSecondDot1;
        //protected iPointColor pointIsTown_DrobSecondDot2;
        //protected iPointColor pointIsTown_DrobThirdDot1;
        //protected iPointColor pointIsTown_DrobThirdDot2;
        //protected iPointColor pointIsTown_VetDrobFirstDot1;    //проверка по вет. дробовику
        //protected iPointColor pointIsTown_VetDrobFirstDot2;
        //protected iPointColor pointIsTown_VetDrobSecondDot1;
        //protected iPointColor pointIsTown_VetDrobSecondDot2;
        //protected iPointColor pointIsTown_VetDrobThirdDot1;
        //protected iPointColor pointIsTown_VetDrobThirdDot2;
        //protected iPointColor pointIsTown_ExpDrobFirstDot1;    //проверка по эксп. дробовику
        //protected iPointColor pointIsTown_ExpDrobFirstDot2;
        //protected iPointColor pointIsTown_ExpDrobSecondDot1;
        //protected iPointColor pointIsTown_ExpDrobSecondDot2;
        //protected iPointColor pointIsTown_ExpDrobThirdDot1;
        //protected iPointColor pointIsTown_ExpDrobThirdDot2;
        //protected iPointColor pointIsTown_JainaDrobFirstDot1;    //проверка по эксп. дробовику Джаина
        //protected iPointColor pointIsTown_JainaDrobFirstDot2;
        //protected iPointColor pointIsTown_VetSabreFirstDot1;    //проверка по вет сабле
        //protected iPointColor pointIsTown_VetSabreFirstDot2;
        //protected iPointColor pointIsTown_ExpSwordFirstDot1;    //проверка по мечу Дарья
        //protected iPointColor pointIsTown_ExpSwordFirstDot2;   
        //protected iPointColor pointIsTown_VetPistolFirstDot1;    //проверка по двум пистолетам outrange
        //protected iPointColor pointIsTown_VetPistolFirstDot2;
        //protected iPointColor pointIsTown_SightPistolFirstDot1;  //проверка по одному пистолету Sight Shot
        //protected iPointColor pointIsTown_SightPistolFirstDot2;
        //protected iPointColor pointIsTown_UnlimPistolFirstDot1;  //проверка по двум пистолетам эксп стойка Unlimited Shot
        //protected iPointColor pointIsTown_UnlimPistolFirstDot2;
        //protected iPointColor pointIsTown_ExpCannonFirstDot1;   // проверка по эксп пушке Мисы
        //protected iPointColor pointIsTown_ExpCannonFirstDot2;
        protected uint[] arrayOfColorsIsTown1;
        protected uint[] arrayOfColorsIsTown2;

        #endregion

        #region Алхимия

        protected iPointColor pointisAlchemy1;
        protected iPointColor pointisAlchemy2;
        protected iPoint pointAlchemy;
        protected iPointColor pointisInventoryFull1;
        protected iPointColor pointisInventoryFull2;
        protected iPointColor pointisOutOfIngredient1_1;
        protected iPointColor pointisOutOfIngredient1_2;
        protected iPointColor pointisOutOfIngredient2_1;
        protected iPointColor pointisOutOfIngredient2_2;
        protected iPointColor pointisOutOfIngredient3_1;
        protected iPointColor pointisOutOfIngredient3_2;
        protected iPointColor pointOutOfMoney1;
        protected iPointColor pointOutOfMoney2;

        #endregion

        #region Barack

        protected iPoint pointTeamSelection1;
        protected iPoint pointTeamSelection2;
        protected iPoint pointTeamSelection3;
        protected iPoint pointButtonLogoutFromBarack;
        protected iPoint pointChooseChannel;
        protected iPoint pointEnterChannel;
        protected iPoint pointMoveNow;
        protected int sdvigY;
        protected iPointColor pointisBarack1;
        protected iPointColor pointisBarack2;
        protected iPointColor pointisBarack3;
        protected iPointColor pointisBarack4;
        protected iPointColor pointisBarackTeamSelection1;
        protected iPointColor pointisBarackTeamSelection2;
        protected iPoint pointNewPlace;
        protected iPoint pointLastPoint;
        protected iPointColor pointisBHLastPoint1;
        protected iPointColor pointisBHLastPoint2;

        #endregion

        #region создание нового бота
        protected iPoint pointNewName;
        protected iPoint pointButtonCreateNewName;
        protected iPoint pointCreateHeroes;
        protected iPoint pointButtonOkCreateHeroes;
        protected iPoint pointMenuSelectTypeHeroes;
        protected iPoint pointSelectTypeHeroes;
        protected iPoint pointNameOfHeroes;
        protected iPoint pointButtonCreateChar;
        protected iPoint pointSelectMusk;
        protected iPoint pointUnselectMedik;
        protected iPoint pointNameOfTeam;
        protected iPoint pointButtonSaveNewTeam;

        protected iPoint pointRunNunies;
        protected iPoint pointPressNunez;
        protected iPoint ButtonOkDialog;
        protected iPoint PressMedal;
        protected iPoint ButtonCloseMedal;
        protected iPoint pointPressNunez2;

        protected iPoint pointPressLindon1;
        protected iPoint pointPressGMonMap;
        protected iPoint pointPressGM_1;
        protected iPoint pointPressSoldier;
        protected iPoint pointFirstStringSoldier;
        protected iPoint pointRifle;
        protected iPoint pointCoat;
        protected iPoint pointButtonPurchase;
        protected iPoint pointButtonCloseSoldier;
        protected iPoint pointButtonYesSoldier;
        protected iPoint pointFirstItem;
        protected iPoint pointDomingoOnMap;
        protected iPoint pointPressDomingo;
        protected iPoint pointFirstStringDialog;
        protected iPoint pointSecondStringDialog;
        protected iPoint pointDomingoMiss;
        protected iPoint pointPressDomingo2;
        protected iPoint pointLindonOnMap;
        protected iPoint pointPressLindon2;
        protected iPoint pointPetExpert;
        protected iPoint pointPetExpert2;
        protected iPoint pointThirdBookmark;
        protected iPoint pointNamePet;
        protected iPoint pointButtonNamePet;
        protected iPoint pointButtonClosePet;
        protected iPoint pointWayPointMap;
        protected iPoint pointWayPoint;
        protected iPoint pointBookmarkField;
        protected iPoint pointButtonLavaPlato;
        protected Point pointPetBegin;  //для перетаскивания пета
        protected Point pointPetEnd;

        #endregion

        #region кратер

        protected iPoint pointGateCrater;
        protected iPoint pointSecondBookmark;
        protected iPoint pointMitridat;
        protected iPoint pointMitridatTo2;
        protected iPoint pointBookmark3;
        protected iPoint pointButtonYesPremium;

        protected iPoint pointWorkCrater;
        protected iPoint pointButtonSaveTeleport;
        protected iPoint pointButtonOkSaveTeleport;
        #endregion

        #region Ида заточка
        protected iPoint pointAcriveInventory;
        protected iPointColor pointIsActiveInventory;

        protected iPoint pointEquipmentBegin;
        protected iPoint pointEquipmentEnd;
        protected iPointColor pointisMoveEquipment1;
        protected iPointColor pointisMoveEquipment2;

        protected iPoint pointButtonEnhance;
        protected iPointColor pointIsPlus41;
        protected iPointColor pointIsPlus42;
        protected iPointColor pointIsPlus43;
        protected iPointColor pointIsPlus44;

        protected iPoint pointAddShinyCrystall;
        protected iPointColor pointIsAddShinyCrystall1;
        protected iPointColor pointIsAddShinyCrystall2;

        protected iPointColor pointIsIda1;
        protected iPointColor pointIsIda2;
        #endregion

        #region чиповка
        protected iPointColor pointIsEnchant1;
        protected iPointColor pointIsEnchant2;
        protected iPointColor pointisWeapon1;
        protected iPointColor pointisWeapon2;
        protected iPointColor pointisArmor1;
        protected iPointColor pointisArmor2;
        protected iPoint pointMoveLeftPanelBegin;
        protected iPoint pointMoveLeftPanelEnd;
        protected iPoint pointButtonEnchance;
        protected iPointColor pointisDef15;
        protected iPointColor pointisHP1;
        protected iPointColor pointisHP2;
        protected iPointColor pointisHP3;
        protected iPointColor pointisHP4;

        protected iPointColor pointisAtk401;
        protected iPointColor pointisAtk402;
        protected iPointColor pointisSpeed30;

        protected iPointColor pointisAtk391;
        protected iPointColor pointisAtk392;
        protected iPointColor pointisSpeed291;
        protected iPointColor pointisSpeed292;

        protected iPointColor pointisAtk381;
        protected iPointColor pointisAtk382;
        protected iPointColor pointisSpeed281;
        protected iPointColor pointisSpeed282;

        protected iPointColor pointisAtk371;
        protected iPointColor pointisAtk372;
        protected iPointColor pointisSpeed271;
        protected iPointColor pointisSpeed272;

        protected iPointColor pointisWild41;  //строка 4
        protected iPointColor pointisWild42;
        protected iPointColor pointisWild51;  //строка 5
        protected iPointColor pointisWild52;
        protected iPointColor pointisWild61;  //строка 6
        protected iPointColor pointisWild62;
        protected iPointColor pointisWild71;  //строка 7
        protected iPointColor pointisWild72;

        protected iPointColor pointisHuman41;  //строка 4
        protected iPointColor pointisHuman42;
        protected iPointColor pointisHuman51;  //строка 5
        protected iPointColor pointisHuman52;
        protected iPointColor pointisHuman61;  //строка 6
        protected iPointColor pointisHuman62;
        protected iPointColor pointisHuman71;  //строка 7
        protected iPointColor pointisHuman72;

        protected iPointColor pointisDemon41;  //строка 4
        protected iPointColor pointisDemon42;
        protected iPointColor pointisDemon51;  //строка 5
        protected iPointColor pointisDemon52;
        protected iPointColor pointisDemon61;  //строка 6
        protected iPointColor pointisDemon62;
        protected iPointColor pointisDemon71;  //строка 7
        protected iPointColor pointisDemon72;

        protected iPointColor pointisUndead41;  //строка 4
        protected iPointColor pointisUndead42;
        protected iPointColor pointisUndead51;  //строка 5
        protected iPointColor pointisUndead52;
        protected iPointColor pointisUndead61;  //строка 6
        protected iPointColor pointisUndead62;
        protected iPointColor pointisUndead71;  //строка 7
        protected iPointColor pointisUndead72;

        protected iPointColor pointisLifeless41;  //строка 4
        protected iPointColor pointisLifeless42;
        protected iPointColor pointisLifeless51;  //строка 5
        protected iPointColor pointisLifeless52;
        protected iPointColor pointisLifeless61;  //строка 6
        protected iPointColor pointisLifeless62;
        protected iPointColor pointisLifeless71;  //строка 7
        protected iPointColor pointisLifeless72;


        #endregion

        #region для перекладывания песо в торговца

        protected iPointColor pointPersonalTrade1;
        protected iPointColor pointPersonalTrade2;
        protected iPoint pointTrader;
        protected iPoint pointPersonalTrade;
        protected iPoint pointMap;
        protected iPoint pointVis1;
        protected iPoint pointVisMove1;
        protected iPoint pointVisMove2;
        protected iPoint pointVisOk2;
        protected iPoint pointVisTrade;
        protected iPoint pointFood;
        protected iPoint pointButtonFesoBUY;
        protected iPoint pointArrowUp2;
        protected iPoint pointButtonFesoSell;
        protected iPoint pointBookmarkFesoSell;
        protected iPoint pointDealer;
        protected iPoint pointYesTrade;
        protected iPoint pointBookmark4;
        protected iPoint pointFesoBegin;
        protected iPoint pointFesoEnd;
        protected iPoint pointOkSum;
        protected iPoint pointOk;
        protected iPoint pointTrade;

        #endregion

        #region Undressing in Barack

        protected iPoint pointShowEquipment;
        //protected iPoint pointBarack1;
        //protected iPoint pointBarack2;
        //protected iPoint pointBarack3;
        //protected iPoint pointBarack4;

        protected iPoint[] pointBarack = new Point[5];
        protected iPointColor pointEquipment1;
        protected iPointColor pointEquipment2;


        #endregion

        #region BH

        protected iPoint pointGateInfinityBH;
        protected iPointColor pointisBH1;           //правильное место в бх
        protected iPointColor pointisBH2;           //неправильное место в бх
        protected iPointColor pointIsAtak1;
        protected iPointColor pointIsAtak2;
        protected iPointColor pointIsRoulette1;
        protected iPointColor pointIsRoulette2;

        //        protected uint[] arrayOfColors = new uint[17] { 0, 1644051, 725272, 6123117, 3088711, 1715508, 1452347, 6608314, 14190184, 1319739, 2302497, 5275256, 2830124, 1577743, 525832, 2635325, 2104613 };
        protected uint[] arrayOfColors;



        #endregion

        // ===========================================  Методы ==========================================

        #region общие методы

        /// <summary>
        /// Останавливает поток на некоторый период (пауза)
        /// </summary>
        /// <param name="ms"> ms - период в милисекундах </param>
        protected void Pause(int ms)
        {
            Thread.Sleep(ms);
        }

        #endregion

        #region Getters

        public bool IsActiveServer { get { return isActiveServer; } }

        /// <summary>
        /// геттер
        /// </summary>
        /// <returns></returns>
        public Town getTown()
        { return this.town; }

        /// <summary>
        /// возвращает параметр, прочитанный из файла
        /// </summary>
        /// <returns></returns>
        public Town getTownBegin()
        {
            return town_begin;
        }

        #endregion

        #region No Window

        /// <summary>
        /// проверяем, выскочило ли сообщение о несовместимости версии SafeIPs.dll
        /// </summary>
        /// <returns></returns>
        public bool isSafeIP()
        {
            return (pointSafeIP1.isColor() && pointSafeIP2.isColor());
        }

        /// <summary>
        /// проверяем, открыто ли окно Стим для ввода логина и пароля
        /// </summary>
        /// <returns></returns>
        protected bool isSteam()
        {
            return (pointisSteam1.isColor() && pointisSteam2.isColor());
        }

        protected bool isNewSteam()
        {
            return (pointisNewSteam1.isColor() && pointisNewSteam2.isColor());
        }

        protected bool isSystemError()
        {
            iPointColor pointisError1 = new PointColor(1142, 610, 3539040, 0);
            iPointColor pointisError2 = new PointColor(1142, 608, 3539040, 0);
            return pointisError1.isColor() && pointisError2.isColor();
        }

        protected void OkSystemError()
        {
            WriteToLogFile("системная ошибка");
            new Point(1142, 610).PressMouseL();
        }

        ///// <summary>
        ///// проверяем, выскочило ли сообщение, что аккаунт загружен на другом компе и кнопка "Продолжить запуск"
        ///// </summary>
        ///// <returns>true = выскочило сообщение</returns>
        //protected bool isContinueRunning()
        //{
        //    bool ff1 = pointisContinueRunning1.isColor();
        //    bool ff2 = pointisContinueRunning2.isColor();
        //    return pointisContinueRunning1.isColor() && pointisContinueRunning2.isColor();
        //}

        protected abstract bool isContinueRunning();

        protected void NextAccount()
        {
            pointCancelContinueRunning.PressMouseLL();  // жмём кнопку отмена

            iPoint pointMove = new Point(1300,500);
            pointMove.Move();  //убираем мышку в сторону

            //if (botParam.NumberOfInfinity > 0)     //если не обычный режим бота
            //{
            //    RemoveSandboxie();    // удаляем текущую песочницу и к счетчику NumberOfInfinity прибавляем единицу
            //    //botwindow.ReOpenWindow(); //перезапускаем окно

            //}
        }

        protected string GetLogin()
        {
            //string result = botParam.Login;
            //if (globalParam.Infinity)
            //{
            //    //если ходим в Инфинити вместо обычного ботоводства, 
            //    //то здесь надо написать выбор логина из списка
            //    result = botParam.Logins[botParam.NumberOfInfinity];   //получили логин
            //}
            //return result;
            return botParam.Logins[botParam.NumberOfInfinity];
        }

        protected string GetPassword()
        {
            //string result = botParam.Password;
            //if (globalParam.Infinity)
            //{
            //    //если ходим в Инфинити вместо обычного ботоводства, 
            //    //то здесь надо написать выбор пароля из списка
            //    result = botParam.Passwords[botParam.NumberOfInfinity];   //получили пароль
            //}
            //return result;
            return botParam.Passwords[botParam.NumberOfInfinity];
        }

        protected void EnterSteamLogin()
        {
            pointSteamLogin2.Drag(pointSteamLogin1); //выделяем старый пароль, если он есть
            Pause(1000);
            SendKeys.SendWait(GetLogin());
        }
        protected void EnterSteamPassword()
        {
            pointSteamPassword.PressMouseL();
            SendKeys.SendWait(GetPassword());
        }
        protected void CheckSteamSavePassword()
        {
            //if (globalParam.Infinity)
            //{
            //    //если ходим в Инфинити вместо обычного ботоводства, 
             
            //}
            //else   //обычное ботоводство
            //{
            //    pointSteamSavePassword.PressMouseL();
            //}
            if (botParam.NumberOfInfinity == 0)  pointSteamSavePassword.PressMouseL(); //если обычный режим бота
        }
        protected void PressSteamOk()
        {
            pointSteamOk.PressMouseL();
            //botParam.NumberOfInfinity++;   //прибавили индекс, когда нажали Ок (загрузка аккаунта Стим)
        }


        /// <summary>
        /// Вводим в стим логин-пароль и нажимаем Ок
        /// </summary>
        protected void InsertLoginPasswordOk()
        {
            EnterSteamLogin();
            EnterSteamPassword();
            CheckSteamSavePassword();
            PressSteamOk();
            //WriteToLogFile(botParam.NumberOfInfinity + " "+ botParam.Logins[botParam.NumberOfInfinity] + 
                //" " + botParam.Passwords[botParam.NumberOfInfinity] + " " + botParam.Parametrs[botParam.NumberOfInfinity]);
        }

        /// <summary>
        /// удаляем текущую песочницу
        /// </summary>
        public void RemoveSandboxie()
        {
            int res = botParam.NumberOfInfinity + 1; //прибавили индекс, когда удаляем песочницу 
            botParam.NumberOfInfinity = res;
            new Point(1597, 1060).Move();   //перемещаем мышь вниз 
            Pause(400);

            //вариант с песочницей
            //Process process = new Process();
            //process.StartInfo.FileName = @"C:\Program Files\Sandboxie\Start.exe";
            //process.StartInfo.Arguments = @"/box:" + botwindow.getNumberWindow() + " /terminate";
            //process.Start();
            //Pause(2000);

            //process.StartInfo.FileName = @"C:\Program Files\Sandboxie\Start.exe";
            //process.StartInfo.Arguments = @"/box:" + botwindow.getNumberWindow() + " delete_sandbox";
            //process.Start();

            //вариант с чистым клиентом
            Process process = new Process();
            process.StartInfo.FileName = this.pathClient;
            process.StartInfo.Arguments = " -shutdown";
            process.Start();


            Pause(4000);

        }

        public abstract void runClientSteam();
        public abstract void runClient();
        public abstract UIntPtr FindWindowGE();
        public abstract void OrangeButton();
        //public abstract bool isActive();



        #endregion

        #region Logout

        /// <summary>
        /// метод проверяет, находится ли данное окно в режиме логаута, т.е. на стадии ввода логина-пароля
        /// </summary>
        /// <returns></returns>
        public bool isLogout()
        {
            return pointisLogout1.isColor() && pointisLogout2.isColor();
        }

 

        /// <summary>
        /// проверяем, есть ли ошибки при нажатии кнопки Connect
        /// </summary>
        /// <returns></returns>
        public bool isPointConnect()
        {
            return pointConnect.isColor();
        }

        /// <summary>
        /// переключились ли на нужный сервер FERRUCCIO-ESPADA (в режиме логаута)
        /// </summary>
        /// <returns></returns>
        public abstract bool IsServerSelection();

        /// <summary>
        /// выбираем сервер путем нажатия на первую строчку в меню
        /// </summary>
        public void serverSelection()
        {
            //WriteToLogFileBH("выбираем сервер из списка серверов начало");
            while (!IsServerSelection())
            {
                pointserverSelection.PressMouseLL();
                Pause(500);
            }
            //WriteToLogFileBH("выбираем сервер из списка серверов конец метода");
        }

        /// <summary>
        /// нажимаем кооннект без проверки результатов нажатия
        /// </summary>
        public void QuickConnect()
        {
            serverSelection();
            PressConnectButton();
        }

        /// <summary>
        /// нажимаем на кнопку Connect (окно в логауте)
        /// </summary>
        protected void PressConnectButton()
        {
            iPoint pointButtonConnect = new Point(595 - 5 + xx, 485 - 5 + yy);    // кнопка коннект в логауте 
            pointButtonConnect.PressMouseLL();   // Кликаю в Connect
        }

        #endregion

        #region Pet (перенесено в другой класс Pet)

        /// <summary>
        /// выбираем первого пета и нажимаем кнопку Summon в меню пет
        /// </summary>
        public void buttonSummonPet()
        {
            pointSummonPet1.PressMouseL();      //Click Pet
            pointSummonPet1.PressMouseL();
            Pause(500);
            pointSummonPet2.PressMouseL();      //Click кнопку "Summon"
            pointSummonPet2.PressMouseL();
            Pause(1000);
        }

        /// <summary>
        /// нажимаем кнопку Cancel Summon в меню пет
        /// </summary>
        public void buttonCancelSummonPet()
        {
            pointCancelSummonPet.PressMouseL();   //Click Cancel Summon
            pointCancelSummonPet.PressMouseL();
            Pause(1000);
        }

        /// <summary>
        /// метод проверяет, открылось ли меню с петом Alt + P
        /// </summary>
        /// <returns> true, если открыто </returns>
        public bool isOpenMenuPet()
        {
            return (pointisOpenMenuPet1.isColor() && pointisOpenMenuPet2.isColor());
        }

        /// <summary>
        /// проверяет, активирован ли пет (зависит от сервера)
        /// </summary>
        /// <returns></returns>
        public bool isActivePet()
        {
            return ((pointisActivePet1.isColor() && pointisActivePet2.isColor()) || (pointisActivePet3.isColor() && pointisActivePet4.isColor()));
        }

        /// <summary>
        /// активируем уже призванного пета
        /// </summary>
        public void ActivePet()
        {
            pointActivePet.PressMouse(); //Click Button Active Pet
            Pause(2500);
        }

        /// <summary>
        /// проверяет, призван ли пет
        /// </summary>
        /// <returns> true, если призван </returns>
        public bool isSummonPet()
        {
            return (pointisSummonPet1.isColor() && pointisSummonPet2.isColor());
        }

        #endregion

        #region Top Menu 

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
                    result = (pointisOpenTopMenu21.isColor() && pointisOpenTopMenu22.isColor());
                    break;
                case 6:
                    result = (pointisOpenTopMenu61.isColor() && pointisOpenTopMenu62.isColor());
                    break;
                case 8:
                    result = (pointisOpenTopMenu81.isColor() && pointisOpenTopMenu82.isColor());
                    break;
                case 9:
                    result = (pointisOpenTopMenu91.isColor() && pointisOpenTopMenu92.isColor());
                    break;
                case 12:
                    result = (pointisOpenTopMenu121.isColor() && pointisOpenTopMenu122.isColor());
                    break;
                case 13:
                    result = (pointisOpenTopMenu131.isColor() && pointisOpenTopMenu132.isColor());
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }

        ///// <summary>
        ///// вызываем телепорт через верхнее меню и телепортируемся по первому телепорту
        ///// </summary>
        //public void Teleport()
        //{
        //    Pause(400);
        //    TopMenu(12);                     // Click Teleport menu
        //    pointTeleportFirstLine.DoubleClickL();   // Первая строка в списке телепортов
        //    Pause(200);
        //    pointTeleportExecute.PressMouseL();    // Click on button Execute in Teleport menu
        //    Pause(2000);
        //}

        ///// <summary>
        ///// вызываем телепорт через верхнее меню и телепортируемся по второму телепорту
        ///// </summary>
        //public void TeleportBH()
        //{
        //    Pause(400);
        //    TopMenu(12);                     // Click Teleport menu
        //    pointTeleportSecondLine.DoubleClickL();   // Первая строка в списке телепортов
        //    Pause(200);
        //    pointTeleportExecute.PressMouseL();    // Click on button Execute in Teleport menu
        //    Pause(2000);
        //}


        /// <summary>
        /// Открыть городской телепорт (Alt + F3) без проверок и while (для паттерна Состояние)  StateGT
        /// </summary>
        public void OpenTownTeleportForState()
        {
            TopMenu(6, 3);
            Pause(1000);
        }

        /// <summary>
        /// Открыть карту местности (Alt + Z) для группы классов StateGT (паттерн Состояние)
        /// </summary>
        public void OpenMapForState()
        {
            TopMenu(6, 2);
            Pause(1000);
        }

        /// <summary>
        /// Выгружаем окно через верхнее меню 
        /// </summary>
        public void GoToEnd()
        {
            //TopMenu(13);
            //Pause(1000);
            //pointGotoEnd.PressMouse();

            systemMenu(7);
        }

        /// <summary>
        /// Выгружаем окно через верхнее меню 
        /// </summary>
        public void Logout()
        {
            //TopMenu(13);
            //Pause(1000);
            //pointLogout.PressMouse();

            systemMenu(6);
        }

        /// <summary>
        /// Идем в казармы через верхнее меню 
        /// </summary>
        public void GotoBarack()
        {
            //TopMenu(13);
            //Pause(1000);
            //pointGotoBarack.PressMouse();

            systemMenu(4);
        }

        /// <summary>
        /// Идем в казармы через верхнее меню 
        /// </summary>
        /// <param name="status"> false, если без проверки открытия системного меню </param>
        public void GotoBarack(bool status)
        {
            //TopMenu(13);
            //Pause(1000);
            //pointGotoBarack.PressMouse();

            systemMenu(4, status);
        }


        /// <summary>
        /// переход по системному меню 
        /// </summary>
        /// <param name="number"> номер пункта меню </param>
        public void systemMenu(int number)
        {
            //if (!isOpenTopMenu(13))
                TopMenu(13);
            Pause(1000);
            iPoint pointCurrentMenu = new Point(685 - 5 + xx, 288 - 5 + (number - 1) * 30 + yy);
            pointCurrentMenu.PressMouse();
        }

        /// <summary>
        /// переход по системному меню 
        /// </summary>
        /// <param name="number"> номер пункта меню </param>
        public void systemMenu(int number, bool status)
        {
            if(!isOpenTopMenu(13))  TopMenu(13, status);       //если не открыто системное меню, то открыть
            Pause(1000);
            iPoint pointCurrentMenu = new Point(685 - 5 + xx, 288 - 5 + (number - 1) * 30 + yy);
            if (!isRouletteBH() && (isOpenTopMenu(13))) pointCurrentMenu.PressMouse();
        }


        /// <summary>
        /// телепортируемся в город продажи по Alt+W (улетаем из БХ или другого города)
        /// </summary>
        public void TeleportAltW_BH()
        {
            int teleport = botwindow.getNomerTeleport();

            if (teleport >= 14)
            {
                teleport = 1;              //если номер телепорта больше 14 (катовия и отит), то летим в Ребольдо
            }

            iPoint pointTeleportToTownAltW = new Point(800 + xx, 517 + yy + (teleport - 1) * 17);

            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            //            botwindow.Pause(2000);
        }

        /// <summary>
        /// нажмает на выбранный раздел верхнего меню (используется для БХ)
        /// </summary>
        /// <param name="numberOfThePartitionMenu"></param>
        /// <param name="status">если false, то не проверяется сработало ли нажатие</param>
        public void TopMenu(int numberOfThePartitionMenu, bool status)
        {
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
            } while ((!isOpenTopMenu(numberOfThePartitionMenu)) && (status));
        }

        /// <summary>
        /// вызываем телепорт через верхнее меню и телепортируемся по указанному номеру телепорта (используется для БХ)
        /// </summary>
        /// <param name="NumberOfLine"></param>
        /// <param name="status">если false, то не проверяется сработало ли нажатие</param>
        public void Teleport(int NumberOfLine, bool status)
        {
            TopMenu(12, status);                     // Click Teleport menu

            Point pointTeleportNumbertLine = new Point(405 - 5 + xx, 180 - 5 + (NumberOfLine - 1) * 15 + yy);    //    тыкаем в указанную строчку телепорта 

            pointTeleportNumbertLine.DoubleClickL();   // Указанная строка в списке телепортов
            Pause(500);

            pointTeleportExecute.PressMouseL();        // Click on button Execute in Teleport menu
        }




        public abstract void Teleport(int numberOfLine);
        public abstract void TopMenu(int numberOfThePartitionMenu);
        public abstract void TopMenu(int numberOfThePartitionMenu, int punkt);
        public abstract void TeleportToTownAltW(int i);

        #endregion

        #region Shop

        ///// <summary>
        ///// проверяет, находится ли данное окно в магазине (а точнее на странице входа в магазин) 
        ///// </summary>
        ///// <returns> true, если находится в магазине </returns>
        //public bool isSale()
        //{
        //    //uint bb = pointIsSale1.GetPixelColor();
        //    //uint dd = pointIsSale2.GetPixelColor();
        //    return ((pointIsSale1.isColor()) && (pointIsSale2.isColor()));
        //}

        ///// <summary>
        ///// проверяет, находится ли данное окно в самом магазине (на закладке BUY или SELL)                                       
        ///// </summary>
        ///// <returns> true, если находится в магазине </returns>
        //public bool isSale2()
        //{
        //    //uint bb = pointIsSale21.GetPixelColor();
        //    //uint dd = pointIsSale22.GetPixelColor();
        //    return ((pointIsSale21.isColor()) && (pointIsSale22.isColor()));
        //    //return botwindow.isColor2(841 - 5, 665 - 5, 7390000, 841 - 5, 668 - 5, 7390000, 4);
        //}

        ///// <summary>
        ///// проверяет, открыта ли закладка Sell в магазине 
        ///// </summary>
        ///// <returns> true, если закладка Sell в магазине открыта </returns>
        //public bool isClickSell()
        //{
        //    //uint bb = pointIsClickSale1.GetPixelColor();
        //    //uint dd = pointIsClickSale2.GetPixelColor();
        //    return ((pointIsClickSale1.isColor()) && (pointIsClickSale2.isColor()));
        //}

        ///// <summary>
        ///// Кликаем в закладку Sell  в магазине 
        ///// </summary>
        //public void Bookmark_Sell()
        //{
        //    pointBookmarkSell.DoubleClickL();
        //    Pause(1500);
        //}

        ///// <summary>
        ///// проверяем, является ли товар в первой строке магазина маленькой красной бутылкой
        ///// </summary>
        ///// <param name="numberOfString">номер строки, в которой проверяем товар</param>
        ///// <returns> true, если в первой строке маленькая красная бутылка </returns>
        //public bool isRedBottle(int numberOfString)
        //{
        //    PointColor pointFirstString = new PointColor(147 - 5 + xx, 224 - 5 + yy + (numberOfString - 1) * 27, 3360337, 0);
        //    return pointFirstString.isColor();
        //}

        ///// <summary>
        ///// добавляем товар из указанной строки в корзину 
        ///// </summary>
        ///// <param name="numberOfString">номер строки</param>
        //public void AddToCart(int numberOfString)
        //{
        //    Point pointAddProduct = new Point(380 - 5 + botwindow.getX(), 220 - 5 + (numberOfString - 1) * 27 + botwindow.getY());
        //    pointAddProduct.PressMouseL();
        //    pointAddProduct.PressMouseWheelDown();
        //}

        ///// <summary>
        ///// определяет, анализируется ли нужный товар либо данный товар можно продавать
        ///// </summary>
        ///// <param name="color"> цвет полностью определяет товар, который поступает на анализ </param>
        ///// <returns> true, если анализируемый товар нужный и его нельзя продавать </returns>
        //public bool NeedToSellProduct(uint color)
        //{
        //    bool result = true;

        //    switch (color)                                             // Хорошая вещь или нет, сверяем по картотеке
        //    {
        //        case 394901:      // soul crystal                **
        //        case 3947742:     // красная бутылка 1200HP      **
        //        case 2634708:     // красная бутылка 2500HP      **
        //        case 7171437:     // devil whisper               **
        //        case 5933520:     // маленькая красная бутылка   **
        //        case 1714255:     // митридат                    **
        //        case 7303023:     // чугун                       **
        //        case 4487528:     // honey                       **
        //        case 1522446:     // green leaf                  **
        //        case 2112641:     // red leaf                    **
        //        case 1533304:     // yelow leaf                  **
        //        case 13408291:    // shiny                       **
        //        case 3303827:     // карта перса                 **
        //        case 6569293:     // warp                        **
        //        case 662558:      // head of Mantis              **
        //        case 4497887:     // Mana Stone                  **
        //        case 7305078:     // Ящики для джеков            **
        //        case 15420103:    // Бутылка хрина               **
        //        case 9868940:     // композитная сталь           **
        //        case 5334831:     // магическая сфера            **
        //        case 13164006:    // свекла                      **
        //        case 16777215:    // Wheat flour                 **
        //        case 13565951:    // playtoken                   **
        //        case 10986144:    // Hinge                       **
        //        case 3481651:     // Tube                        **
        //        case 6593716:     // Clock                       **
        //        case 13288135:    // Spring                      **
        //        case 7233629:     // Cogwheel                    **
        //        case 13820159:    // Family Support Token        **
        //        case 4222442:     // Wolf meat                   **
        //        case 4435935:     // Yellow ore                  **
        //        case 5072004:     // Bone Stick                   **
        //        case 3559777:     // Dragon Lether                   **
        //        case 1712711:     // Dragon Horn                   **
        //        case 6719975:     // Wild Boar Meat                   **
        //        case 4448154:     // Green ore                   **
        //        case 13865807:    // blue ore                   **
        //        case 4670431:     // Red ore                 **
        //        case 13291199:    // Diamond Ore                   **
        //        case 1063140:     // Stone of Philos                   **
        //        case 8486756:     // Ice Crystal                  **
        //        case 4143156:     // bulk of Coal                   **
        //        case 9472397:     // Steel piece                 **
        //        case 7187897:     // Mustang ore
        //        case 1381654:     // стрелы эксп
        //        case 11258069:     // пули эксп
        //        case 2569782:     // дробь эксп
        //        case 5137276:     // сундук деревянный как у сфер древней звезды

        //            //              case 7645105:     // Quartz                 **
        //            result = false;
        //            break;
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Посылаем нажатие числа 333 в окно с ботом с помощью команды PostMessage
        ///// </summary>
        //public void Press333()
        //{
        //    const int WM_KEYDOWN = 0x0100;
        //    const int WM_KEYUP = 0x0101;
        //    UIntPtr lParam = new UIntPtr();
        //    UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

        //    for (int i = 1; i <= 3; i++)
        //    {
        //        uint dd = 0x00400001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D3, lParam);
        //        Pause(150);

        //        dd = 0xC0400001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D3, lParam);
        //        Pause(150);
        //    }
        //}

        ///// <summary>
        ///// Посылаем нажатие числа 44444 в окно с ботом с помощью команды PostMessage
        ///// </summary>
        //public void Press44444()
        //{
        //    const int WM_KEYDOWN = 0x0100;
        //    const int WM_KEYUP = 0x0101;
        //    UIntPtr lParam = new UIntPtr();
        //    UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        uint dd = 0x00500001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D4, lParam);
        //        Pause(150);

        //        dd = 0xC0500001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D4, lParam);
        //        Pause(150);
        //    }
        //}

        ///// <summary>
        ///// добавляем товар из указанной строки в корзину 
        ///// </summary>
        ///// <param name="numberOfString">номер строки</param>
        //public void GoToNextproduct(int numberOfString)
        //{
        //    Point pointAddProduct = new Point(380 - 5 + botwindow.getX(), 225 - 5 + (numberOfString - 1) * 27 + botwindow.getY());
        //    pointAddProduct.PressMouseWheelDown();   //прокручиваем список

        //}

        ///// <summary>
        ///// добавляем товар из указанной строки в корзину 
        ///// </summary>
        ///// <param name="numberOfString">номер строки</param>
        //public void AddToCartLotProduct(int numberOfString)
        //{
        //    Point pointAddProduct = new Point(360 - 5 + botwindow.getX(), 220 - 5 + (numberOfString - 1) * 27 + botwindow.getY());  //305 + 30, 190 + 30)
        //    pointAddProduct.PressMouseL();  //тыкаем в строчку с товаром
        //    Pause(150);
        //    SendKeys.SendWait("33000");
        //    Pause(100);
        //    //Press44444();                   // пишем 444444 , чтобы максимальное количество данного товара попало в корзину 
        //    pointAddProduct.PressMouseWheelDown();   //прокручиваем список
        //}

        ///// <summary>
        ///// Продажа товаров в магазине вплоть до маленькой красной бутылки 
        ///// </summary>
        //public void SaleToTheRedBottle()
        //{
        //    uint count = 0;
        //    while (!isRedBottle(1))
        //    {
        //        AddToCart(1);
        //        count++;
        //        if (count > 220) break;   // защита от бесконечного цикла
        //    }
        //}

        ///// <summary>
        ///// Продажа товара после маленькой красной бутылки, до момента пока прокручивается список продажи
        ///// </summary>
        //public void SaleOverTheRedBottle()
        //{
        //    Product previousProduct;
        //    Product currentProduct;

        //    currentProduct = new Product(xx, yy, 1);  //создаем структуру "текущий товар" из трёх точек, которые мы берем у товара в первой строке магазина

        //    do
        //    {
        //        previousProduct = currentProduct;
        //        if (NeedToSellProduct(currentProduct.color1))
        //            AddToCartLotProduct(1);
        //        else
        //            GoToNextproduct(1);

        //        Pause(200);  //пауза, чтобы ГЕ успела выполнить нажатие. Можно и увеличить     
        //        currentProduct = new Product(xx, yy, 1);
        //    } while (!currentProduct.EqualProduct(previousProduct));          //идет проверка по трем точкам
        //}

        ///// <summary>
        ///// Продажа товара, когда список уже не прокручивается 
        ///// </summary>
        //public void SaleToEnd()
        //{
        //    iPointColor pointCurrentProduct;
        //    for (int j = 2; j <= 12; j++)
        //    {
        //        pointCurrentProduct = new PointColor(149 - 5 + xx, 219 - 5 + yy + (j - 1) * 27, 3360337, 0);   //проверяем цвет текущего продукта
        //        Pause(50);
        //        if (NeedToSellProduct(pointCurrentProduct.GetPixelColor()))       //если нужно продать товар
        //            AddToCartLotProduct(j);                                       //добавляем в корзину весь товар в строке j
        //    }
        //}

        ///// <summary>
        ///// Кликаем в кнопку BUY  в магазине 
        ///// </summary>
        //public void Botton_BUY()
        //{
        //    pointButtonBUY.PressMouseL();
        //    pointButtonBUY.PressMouseL();
        //    Pause(2000);
        //}

        ///// <summary>
        ///// Кликаем в кнопку Sell  в магазине 
        ///// </summary>
        //public void Botton_Sell()
        //{
        //    pointButtonSell.PressMouseL();
        //    pointButtonSell.PressMouseL();
        //    Pause(2000);
        //}

        ///// <summary>
        ///// Кликаем в кнопку Close в магазине
        ///// </summary>
        //public void Button_Close()
        //{
        //    pointButtonClose.PressMouse();
        //    Pause(2000);
        //    //PressMouse(847, 663);
        //}

        ///// <summary>
        ///// Покупка митридата в количестве 333 штук
        ///// </summary>
        //public void BuyingMitridat()
        //{
        //    //botwindow.PressMouseL(360, 537);          //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //    pointBuyingMitridat1.PressMouseL();             //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
        //    Pause(150);

        //    //Press333();
        //    SendKeys.SendWait("333");

        //    Botton_BUY();                             // Нажимаем на кнопку BUY 


        //    pointBuyingMitridat2.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если переполнение митридата
        //    //botwindow.PressMouseL(1392 - 875, 438 - 5);         
        //    Pause(500);

        //    pointBuyingMitridat3.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если нет денег на покупку митридата
        //    //botwindow.PressMouseL(1392 - 875, 428 - 5);          
        //    Pause(500);
        //}

        #endregion

        #region atWork

        public abstract bool is248Items();

        public abstract void AddBullet10000();

        /// <summary>
        /// метод проверяет, закончились ли экспертные патроны (красный значок индикатора)
        /// </summary>
        /// <returns> true, если красный значок </returns>
        public bool isBulletOff()
        {
            return (pointisBulletOff.isColor());
        }

        /// <summary>
        /// метод проверяет, ополовинились ли экспертные патроны (желтый значок индикатора)
        /// </summary>
        /// <returns> true, если желтый значок </returns>
        public bool isBulletHalf()
        {
            return (pointisBulletHalf.isColor());
        }

        /// <summary>
        /// метод проверяет, переполнился ли карман (выскочило ли уже сообщение о переполнении)
        /// </summary>
        /// <returns> true, еслм карман переполнен </returns>
        public bool isBoxOverflow()
        {
            return (pointisBoxOverflow1.isColor() && pointisBoxOverflow2.isColor()) 
                || (pointisBoxOverflow3.isColor() && pointisBoxOverflow4.isColor());          //оранжевая надпись (эта строка)
        }

        /// <summary>
        /// функция проверяет, убит ли хоть один герой из пати (проверка проходит на карте)
        /// </summary>
        /// <returns></returns>
        public bool isKillHero()
        {
            return (pointisKillHero1.isColor() || pointisKillHero2.isColor() || pointisKillHero3.isColor());
        }

        /// <summary>
        /// функция проверяет, убиты ли все герои
        /// </summary>
        /// <returns></returns>
        public bool isKillAllHero()
        {
            return (pointisKillHero1.isColor() && pointisKillHero2.isColor() && pointisKillHero3.isColor());
        }

        ///// <summary>
        ///// метод проверяет, находится ли данное окно на работе (проверка по стойке)  две стойки
        ///// </summary>
        ///// <returns> true, если сейчас на рабочей карте </returns>
        //public bool isWork()
        //{
        //    bool resultRifle = (pointisWork_RifleDot1.isColor() && pointisWork_RifleDot2.isColor());
        //    bool resultExpRifle = (pointisWork_ExpRifleDot1.isColor() && pointisWork_ExpRifleDot2.isColor());
        //    bool resultDrob = (pointisWork_DrobDot1.isColor() && pointisWork_DrobDot2.isColor());
        //    bool resultVetDrob = (pointisWork_VetDrobDot1.isColor() && pointisWork_VetDrobDot2.isColor());
        //    bool resultExpDrob = (pointisWork_ExpDrobDot1.isColor() && pointisWork_ExpDrobDot2.isColor());
        //    bool resultJainaDrob = (pointisWork_JainaDrobDot1.isColor() && pointisWork_JainaDrobDot2.isColor());
        //    bool resultVetSabre = (pointisWork_VetSabreDot1.isColor() && pointisWork_VetSabreDot2.isColor());
        //    bool resultExpSword = (pointisWork_ExpSwordDot1.isColor() && pointisWork_ExpSwordDot2.isColor());
        //    bool resultVetPistol2 = (pointisWork_VetPistolDot1.isColor() && pointisWork_VetPistolDot2.isColor());
        //    bool resultVetPistol1 = (pointisWork_SightPistolDot1.isColor() && pointisWork_SightPistolDot2.isColor());
        //    bool resultExpPistol = (pointisWork_UnlimPistolDot1.isColor() && pointisWork_UnlimPistolDot2.isColor());
        //    bool resultExpCannon = (pointisWork_ExpCannonDot1.isColor() && pointisWork_ExpCannonDot2.isColor());

        //    return (resultRifle || resultExpRifle || resultDrob || resultVetDrob || resultExpDrob || resultVetSabre || resultExpSword || resultJainaDrob || resultVetPistol2 || resultVetPistol1 || resultExpPistol || resultExpCannon);  //проверка только по первому персу
        //}

        /// <summary>
        /// проверяем, находимся ли на работе
        /// </summary>
        /// <returns></returns>
        public bool isWork()
        {
            //проверяем одну и ту же точку на экране в миссии
            //каждой стойке соответствует свой цвет этой точки
            //проверяем две точки для надежности

            // в массиве arrayOfColorsIsWork1 хранятся все цвета первой контрольной точки, которые есть у проверяемых стоек 
            // в массиве arrayOfColorsIsWork2 хранятся все цвета второй контрольной точки, которые есть у проверяемых стоек 

            uint color1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 0, 0).GetPixelColor() / 1000;                 // проверяем номер цвета в контрольной точке и округляем
            uint color2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 0, 0).GetPixelColor() / 1000;                 // проверяем номер цвета в контрольной точке и округляем

            return ((this.arrayOfColorsIsWork1.Contains(color1)) && (this.arrayOfColorsIsWork2.Contains(color2)));                // проверяем, есть ли цвет контрольной точки в массивах цветов
        }


        ///// <summary>
        ///// метод проверяет является ли первый перс поваром
        ///// </summary>
        ///// <returns> true, если сейчас на рабочей карте </returns>
        //public bool isCook()
        //{
        //    return (pointisWork_VetSabreDot1.isColor() && pointisWork_VetSabreDot2.isColor());
        //}

        /// <summary>
        /// нажимаем на спец. умение повару
        /// </summary>
        public void ClickSkillCook()
        {
            pointSkillCook.PressMouseL();
        }

        /// <summary>
        /// проверяем, включен ли боевой режим
        /// </summary>
        /// <returns></returns>
        public bool isBattleMode()
        {
            return (pointisBattleMode1.isColor() && pointisBattleMode1.isColor());
        }


        #endregion

        #region inTown

        /// <summary>
        /// проверяем, открыто ли хотя бы одно задание (список справа экрана)
        /// </summary>
        /// <returns></returns>
        public bool isTask()
        {
            return new PointColor(1008 - 5 + xx, 509 - 5 + yy, 15335423, 0).isColor();
        }

        /// <summary>
        /// перетаскивание коробки с подарком в ячейку Mana1
        /// </summary>
        public void TaskOff()
        {
            for (int i = 1; i <= 5; i++)
            {
                if (isTask())
                {
                    //закрываем задание
                    new Point(1011 - 5 + xx, 512 - 5 + yy).PressMouseL();
                    new Point(500 - 5 + xx, 300 - 5 + yy).Move();
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// перетаскивание коробки с подарком в ячейку Mana1
        /// </summary>
        public void MoveGiftBox()
        {
            iPointColor pointmovegift = new PointColor(237 - 5 + xx, 701 - 5 + yy, 16700000, 5);
            if (!pointmovegift.isColor())    //если нет на месте коробки с подарком
            {
                //перекладываем коробку с подарком в ячейку Mana1
                TopMenu(8, 1);
                new Point(778 - 5 + xx, 152 - 5 + yy).PressMouseLL();   //вторая закладка инвентаря

                for (int i = 1; i <= 5; i++)         //строки в инвентаре
                {
                    for (int j = 1; j <= 8; j++)        // столбцы в инвентаре
                    {
                        if (new PointColor(694 - 5 + xx + (j - 1) * 39, 182 - 5 + yy + (i - 1) * 38, 16700000, 5).isColor())
                        {
                            new Point(694 - 5 + xx + (j - 1) * 39, 182 - 5 + yy + (i - 1) * 38).Drag(new Point(237 - 5 + xx, 701 - 5 + yy));
                            Pause(1000);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// получение подарков из журнала
        /// </summary>
        public void GetGifts()
        {
            bool result = false;
            for (int i = 1; i <= 5; i++)
            {
                iPointColor pointgifts = new PointColor(709 - 5 + xx, 229 - 5 + yy + (i - 1) * 97, 16000000, 5);
                result = result || pointgifts.isColor();
            }
            if (!result) botwindow.PressEsc();      //если журнал с подарками не открыт, то жмём Esc, чтобы убрать рекламу

            result = true;
            while (result)
            {
                result = false;
                for (int i = 1; i <= 5; i++)
                {
                    iPointColor pointgifts = new PointColor(709 - 5 + xx, 229 - 5 + yy + (i - 1) * 97, 16000000, 5);
                    result = result || pointgifts.isColor();

                    if (pointgifts.isColor())
                    {
                        new Point(709 - 5 + xx, 229 - 5 + yy + (i - 1) * 97).PressMouseL();
                        Pause(1000);
                    }
                }
            }
        }

        /// <summary>
        /// проверяем, открыто ли окно с подарочными окнами
        /// </summary>
        /// <returns></returns>
        public bool isToken()
        {
            //uint bb = pointisOpenMenuPet1.GetPixelColor();
            //uint dd = pointisOpenMenuPet2.GetPixelColor();
            return (pointisToken1.isColor() && pointisToken2.isColor());
        }

        /// <summary>
        /// закрываем окно с подарочными токенами
        /// </summary>
        public void TokenClose()
        {
            pointToken.PressMouse();
        }

        ///// <summary>
        ///// метод проверяет, находится ли данное окно в городе (проверка по стойкам - серые в городе, цветные - на рабочих картах) 
        ///// </summary>
        ///// <returns> true, если бот находится в городе </returns>
        //public bool isTown()
        //{
        //    //ружье
        //    bool resultRifle = (pointIsTown_RifleFirstDot1.isColor() && pointIsTown_RifleFirstDot2.isColor());
        //    bool resultRifleExp = (pointIsTown_ExpRifleFirstDot1.isColor() && pointIsTown_ExpRifleFirstDot2.isColor());
        //    //дробовик
        //    bool resultShotgun = (pointIsTown_DrobFirstDot1.isColor() && pointIsTown_DrobFirstDot2.isColor());           //проверка по первому персу обычная стойка
        //    bool resultShotgunVet = (pointIsTown_VetDrobFirstDot1.isColor() && pointIsTown_VetDrobFirstDot2.isColor());  //проверка по первому персу вет стойка
        //    bool resultShotgunExp = (pointIsTown_ExpDrobFirstDot1.isColor() && pointIsTown_ExpDrobFirstDot2.isColor());  //проверка по первому персу эксп стойка
        //    bool resultShotgunJaina = (pointIsTown_JainaDrobFirstDot1.isColor() && pointIsTown_JainaDrobFirstDot2.isColor());  //проверка по первому персу Джаина
        //    //сабля
        //    bool resultVetSabre = (pointIsTown_VetSabreFirstDot1.isColor() && pointIsTown_VetSabreFirstDot2.isColor());  //проверка по первому персу вет сабля
        //    //меч
        //    bool resultExpSword = (pointIsTown_ExpSwordFirstDot1.isColor() && pointIsTown_ExpSwordFirstDot2.isColor());  //проверка по первому персу эксп меч 
        //    //пистолет
        //    bool resultVetPistol2 = (pointIsTown_VetPistolFirstDot1.isColor() && pointIsTown_VetPistolFirstDot2.isColor());   //два пистолета
        //    bool resultVetPistol1 = (pointIsTown_SightPistolFirstDot1.isColor() && pointIsTown_SightPistolFirstDot2.isColor());   //один пистолет
        //    bool resultExpPistol = (pointIsTown_UnlimPistolFirstDot1.isColor() && pointIsTown_UnlimPistolFirstDot2.isColor());   //один пистолет
        //    //пушка Миса
        //    bool resultExpCannon = (pointIsTown_ExpCannonFirstDot1.isColor() && pointIsTown_ExpCannonFirstDot2.isColor());   //пушка Миса

        //    return (resultRifle || resultRifleExp || resultShotgun || resultShotgunVet || resultShotgunExp || resultVetSabre || resultExpSword || resultShotgunJaina || resultVetPistol2 || resultVetPistol1 || resultExpPistol || resultExpCannon);
        //}

        /// <summary>
        /// проверяем, находимся ли в городе
        /// </summary>
        /// <returns></returns>
        public bool isTown()
        {
            //проверяем одну и ту же точку на экране в миссии
            //каждой стойке соответствует свой цвет этой точки
            //проверяем две точки для надежности

            // в массиве arrayOfColorsIsTown1 хранятся все цвета первой контрольной точки, которые есть у проверяемых стоек 
            // в массиве arrayOfColorsIsTown2 хранятся все цвета второй контрольной точки, которые есть у проверяемых стоек 

            uint color1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 0, 0).GetPixelColor() / 1000;                 // проверяем номер цвета в контрольной точке и округляем
            uint color2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 0, 0).GetPixelColor() / 1000;                 // проверяем номер цвета в контрольной точке и округляем

            return ((this.arrayOfColorsIsTown1.Contains(color1)) && (this.arrayOfColorsIsTown2.Contains(color2)));                // проверяем, есть ли цвет контрольной точки в массивах цветов
        }

        /// <summary>
        /// лечение персов нажатием на красную бутылку и выпивание бутылок маны
        /// </summary>
        public void Cure(int n)
        {
            for (int j = 1; j <= n; j++)
            {
                pointCure1.PressMouseL();  
                pointMana1.PressMouseL();

                pointCure2.PressMouseL(); 
                pointMana2.PressMouseL(); 
                
                pointCure3.PressMouseL(); 
                pointMana3.PressMouseL(); 
            }
            Pause(500);

            iPoint pointFourthBox = new Point(31 - 5 + xx, 200 - 5 + yy);
            pointFourthBox.PressMouseL();                // тыкаю в  (четвертая ячейка)

        }

        /// <summary>
        /// "быстрое лечение". Применение коробок патронов в ячейках с маной
        /// </summary>
        public void QuickCure()
        {
            pointMana1.PressMouseL();
            //pointMana2.PressMouseL();
            //pointMana3.PressMouseL();
        }

        /// <summary>
        /// идём к высокой бабе GM
        /// </summary>
        public abstract void GotoGM();
        public abstract void PressToHeadGM();

        #endregion

        #region Алхимия

        /// <summary>
        /// проверяем, закончились ли ингредиенты для алхимии (проверяем по две точки на каждую из трех ячеек алхимии)
        /// </summary>
        /// <returns></returns>
        public bool isOutOfIngredients()
        {
            return (pointisOutOfIngredient1_1.isColor() && pointisOutOfIngredient1_2.isColor());
        }

        /// <summary>
        /// проверяем, закончились ли ингредиенты для алхимии (проверяем по две точки на каждую из трех ячеек алхимии)
        /// </summary>
        /// <returns></returns>
        public bool isOutOfMoney()
        {
            return (pointOutOfMoney1.isColor() && pointOutOfMoney2.isColor());
        }


        /// <summary>
        /// проверяет, заполнился ли инвентарь при производстве на алхимическом столе
        /// </summary>
        /// <returns></returns>
        public bool isInventoryFull()
        {
            return (pointisInventoryFull1.isColor() && pointisInventoryFull2.isColor());
        }

        /// <summary>
        /// нажимаем кнопку "Start Alchemy" на алхимическом столе
        /// </summary>
        /// <returns></returns>
        public void PressButtonAlchemy()
        {
            pointAlchemy.PressMouseL();
        }

        /// <summary>
        /// проверяем, открыт ли алхимический стол
        /// </summary>
        /// <returns></returns>
        public bool isAlchemy()
        {
            return (pointisAlchemy1.isColor() && pointisAlchemy2.isColor());
        }

        #endregion

        #region Barack

        /// <summary>
        /// нажимаем на кнопку логаут в казарме, тем самым покидаем казарму
        /// </summary>
        public void buttonExitFromBarack()
        {
            pointButtonLogoutFromBarack.DoubleClickL(); 
            Pause(500);

        }

        /// <summary>
        /// выбор команды персов из списка в казарме
        /// </summary>
        public void TeamSelection()
        {
            pointTeamSelection1.PressMouse();   // Нажимаем кнопку вызова списка групп
            pointTeamSelection2.PressMouseL();  // выбираем нужную группу персов (первую в списке)
            pointTeamSelection3.PressMouseL();  // Нажимаем кнопку выбора группы (Select Team) 
        }

        /// <summary>
        /// сдвиг для правильного выбора канала
        /// </summary>
        /// <returns></returns>
        public int sdvig()
        {
            return sdvigY;
        }

        /// <summary>
        /// проверяем, в бараках ли бот
        /// </summary>
        /// <returns> true, если бот в бараках </returns>
        public bool isBarack()
        {
            return (pointisBarack1.isColor() && pointisBarack2.isColor()) || (pointisBarack3.isColor() && pointisBarack4.isColor());
        }

        /// <summary>
        /// проверяем, в бараках ли бот (на стадии выбора группы)
        /// </summary>
        /// <returns> true, если бот в бараках на стадии выбора группы  </returns>
        public bool isBarackTeamSelection()
        {
            return (pointisBarackTeamSelection1.isColor() && pointisBarackTeamSelection2.isColor());
        }



        /// <summary>
        /// начать с выхода в город (нажать на кнопку "начать с нового места")
        /// </summary>
        public void NewPlace()
        {
            iPoint pointNewPlace = new Point(54 - 5 + xx, 685 - 5 + yy);
            pointNewPlace.DoubleClickL();
        }

        /// <summary>
        /// начинаем со старого места (из барака)
        /// </summary>
        public void barackLastPoint()
        {
            pointLastPoint.PressMouseL();
        }

        /// <summary>
        /// проверяем, попадём ли мы в BH, если в бараке начинаем со старого места
        /// </summary>
        public bool isBarackLastPoint()
        {
            pointLastPoint.PressMouseR();    // наводим мышку на кнопку Last Point в бараке
            return (pointisBHLastPoint1.isColor() && pointisBHLastPoint2.isColor());
        }


        #endregion

        #region создание новых ботов

        /// <summary>
        /// бежим к петэксперту
        /// </summary>
        public void RunToPetExpert()
        {
            botwindow.ThirdHero();
            Pause(500);
            pointPetExpert.PressMouseL();            //тыкнули в эксперта по петам
            Pause(3000);
            pointPetExpert.PressMouseL();            //тыкнули в эксперта по петам второй раз
            Pause(3000);

            //pointPetExpert2.PressMouseL();            //тыкнули в эксперта по петам второй раз
            //Pause(3000);

        }

        /// <summary>
        /// диалог с петэкспертом
        /// </summary>
        public void DialogPetExpert()
        {
            //pointFirstStringDialog.PressMouseL();       //нижняя строчка в диалоге
            //Pause(1500);
            //ButtonOkDialog.PressMouseL();               // Нажимаем на Ok в диалоге
            //Pause(1500);
            dialog.PressStringDialog(2);
            dialog.PressOkButton(1);

            //pointFirstStringDialog.PressMouseL();       //нижняя строчка в диалоге
            //Pause(1500);

            //ButtonOkDialog.PressMouseL();               // Нажимаем на Ok в диалоге
            //Pause(1500);
            dialog.PressStringDialog(1);
            dialog.PressOkButton(1);

            pointThirdBookmark.PressMouseL();           //тыкнули в третью закладку в кармане
            Pause(1500);

            pointPetBegin.Drag(pointPetEnd);            // берем коробку с кокошкой и переносим в куб для вылупления у петэксперта
            //botwindow.MouseMoveAndDrop(800-5, 220-5, 520-5, 330-5);            // берем коробку с кокошкой и переносим в куб для вылупления у петэксперта
            botwindow.Pause(2500);

            pointNamePet.PressMouseL();                // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            SendKeys.SendWait("Koko");                 //вводим имя пета
            Pause(1500);

            pointButtonNamePet.PressMouseL();          // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            pointButtonClosePet.PressMouseL();         // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            ////жмем Ок 3 раза
            //for (int j = 1; j <= 3; j++)
            //{
            //    ButtonOkDialog.PressMouse();           // Нажимаем на Ok в диалоге
            //    Pause(1500);
            //}
            dialog.PressOkButton(3);

            Pause(2500);
        }


        /// <summary>
        /// диалог с Линдоном для получения лицензии
        /// </summary>
        public void LindonDialog2()
        {
            //жмем Ок 30 раз
            for (int j = 1; j <= 30; j++)
            {
                ButtonOkDialog.PressMouse();           // Нажимаем на Ok в диалоге
                Pause(1500);
            }
        }


        /// <summary>
        /// бежим к линдону после Доминго
        /// </summary>
        public void RunToLindon2()
        {
            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1500);

            pointLindonOnMap.PressMouseL();            //выбрали Линдона
            Pause(1000);

            town_begin.ClickMoveMap();                 //нажимаем на кнопку Move на карте
            Pause(30000);

            botwindow.PressEscThreeTimes();
            Pause(1000);

            pointPressLindon2.PressMouseL();           //нажимаем на голову Линдона
            Pause(3000);


        }

        /// <summary>
        /// бежим к Линдону
        /// </summary>
        public void RunToDomingo2()
        {
            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1500);

            pointDomingoOnMap.PressMouseL();           //выбрали Доминго
            Pause(1000);

            town_begin.ClickMoveMap();                 //нажимаем на кнопку Move на карте
            Pause(10000);

            botwindow.PressEscThreeTimes();
            Pause(1000);

            pointPressDomingo2.PressMouseL();           //нажимаем на голову Доминго
            Pause(3000);


        }


        /// <summary>
        /// диалог с Доминго после миссии
        /// </summary>
        public void DomingoDialog2()
        {
            //жмем Ок 9 раз
            for (int j = 1; j <= 9; j++)
            {
                ButtonOkDialog.PressMouse();           // Нажимаем на Ok в диалоге
                Pause(1500);
            }
        }

        /// <summary>
        /// Миссия у Доминго
        /// </summary>
        public void MissionDomingo()
        {
            TopMenu(6, 2);
            Pause(1000);

            pointDomingoMiss.PressMouseR();
            Pause(5000);


            botwindow.PressEscThreeTimes();
            Pause(180000);

        }

        /// <summary>
        /// диалог с Доминго
        /// </summary>
        public void DomingoDialog()
        {
            //жмем Ок 3 раза
            for (int j = 1; j <= 3; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
                Pause(1500);
            }

            pointFirstStringDialog.PressMouse();   //YES
            Pause(1000);

            ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
            Pause(1000);

            pointSecondStringDialog.PressMouse();   //YES во второй раз
            Pause(1000);

            ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
            Pause(10000);


        }

        /// <summary>
        /// бежим к доминго
        /// </summary>
        public void RunToDomingo()
        {
            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1000);

            pointDomingoOnMap.PressMouseL();           //выбрали Доминго
            Pause(1000);

            town_begin.ClickMoveMap();                 //нажимаем на кнопку Move на карте
            Pause(33000);

            botwindow.PressEscThreeTimes();
            Pause(1000);

            pointPressDomingo.PressMouseL();           //нажимаем на голову Доминго
            Pause(2000);

        }
        /// <summary>
        /// надеваем оружие и броню
        /// </summary>
        public void Arm()
        {
            //открываем карман со спецвещами
            TopMenu(8, 2);
            Pause(1000);

            //надеваем обмундирование на первого перса, тыкая в первую вещь 25 раз (хватило бы 21, но с запасом)
            for (int j = 1; j <= 23; j++)
            {
                pointFirstItem.DoubleClickL();      //нажимаем дважды на первую вещь в спецкармане
                Pause(1000);
            }

            //надеваем обмундирование на второго перса, тыкая в первую вещь 17 раз (хватило бы 14, но с запасом)
            botwindow.SecondHero();
            Pause(1000);
            for (int j = 1; j <= 15; j++)
            {
                pointFirstItem.DoubleClickL();
                Pause(1000);
            }

            //надеваем обмундирование на третьего перса, тыкая в первую вещь 10 раз (хватило бы 7, но с запасом)
            botwindow.ThirdHero();
            Pause(1000);
            for (int j = 1; j <= 8; j++)
            {
                pointFirstItem.DoubleClickL();
                Pause(1000);
            }

            Pause(2000);
            botwindow.PressEscThreeTimes();
            Pause(1000);

        }

        /// <summary>
        /// говорим с солдатом для получения оружия
        /// </summary>
        public void TalkToSoldier()
        {
            pointPressSoldier.PressMouseL();  //нажимаем на голову солдата
            Pause(2000);

            pointFirstStringSoldier.PressMouseL();   //нажимаем на первую строчку в диалоге
            Pause(1500);

            ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
            Pause(1500);

            pointRifle.PressMouseL();    // Нажимаем на ружье
            Pause(500);
            pointRifle.PressMouseL();    // Нажимаем на ружье
            Pause(500);
            pointRifle.PressMouseL();    // Нажимаем на ружье
            Pause(500);

            //крутим колесо мыши вниз 35 раз
            for (int j = 1; j <= 35; j++)
            {
                pointRifle.PressMouseWheelDown();
                Pause(500);
            }

            pointCoat.PressMouseL();    // Нажимаем на плащ
            Pause(500);
            pointCoat.PressMouseL();    // Нажимаем на плащ
            Pause(500);
            pointCoat.PressMouseL();    // Нажимаем на плащ
            Pause(1000);

            pointButtonPurchase.PressMouseL();
            Pause(2000);
            pointButtonYesSoldier.PressMouseL();
            Pause(2000);
            pointButtonCloseSoldier.PressMouseL();
            Pause(3000);

        }

        /// <summary>
        /// говорим с GM для получения бижутерии и купонов на оружие-броню
        /// </summary>
        public void TalkToGM()
        {
            Pause(1000);
            //нажимаем на голову GM 
            pointPressGM_1.PressMouseL();
            Pause(3000);

            //жмем Ок 10 раз
            for (int j = 1; j <= 10; j++)
            {
                ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
                Pause(1500);
            }

            Pause(2500);
        }
        /// <summary>
        /// бежим к персу для получения оружия и брони 35 лвл
        /// </summary>
        public void RunToGetWeapons()
        {
            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1000);
            pointPressGMonMap.PressMouseL();           //нажимаем на строчку GM на карте Alt+Z
            Pause(1000);
            town_begin.ClickMoveMap();                 //нажимаем на кнопку Move на карте
            Pause(10000);
            botwindow.PressEscThreeTimes();
            Pause(1000);
        }

        /// <summary>
        /// первый разговор с Линдоном после Стартонии
        /// </summary>
        public void TalkToLindon1()
        {
            //Pause(4000);
            pointPressLindon1.PressMouseL(); //нажимаем на Линдона
            Pause(4000);

            for (int j = 1; j <= 4; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
                Pause(2000);
            }
            Pause(2000);
        }

        /// <summary>
        /// разговор с Нуьесом
        /// </summary>
        public void TalkRunToNunez()
        {
            pointPressNunez.PressMouseL();   // Нажимаем на Нуньеса
            Pause(3000);
            pointPressNunez.PressMouseL();   // Нажимаем на Нуньеса
            Pause(3000);

            for (int j = 1; j <= 7; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
                Pause(2000);
            }

            PressMedal.DoubleClickL();        //нажимаем на медаль 1 (медаль новичка)  двойной щелчок
            Pause(1500);

            botwindow.PressEscThreeTimes();    //закрываем лишние окна
            Pause(3000);
            //ButtonCloseMedal.PressMouseL();    // Нажимаем на Close и закрываем медали
            //Pause(2500);

            pointPressNunez2.PressMouseL();   // Нажимаем на Нуньеса
            Pause(5000);

            for (int j = 1; j <= 5; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
                Pause(2500);
            }

            Pause(1500);

            //ожидаем ребольдо

            //PressMedal_1.PressMouseL();          //нажимаем на медаль 1 (медаль новичка)  двойной щелчок
            //Pause(50);
            //PressMedal_1.PressMouseL();
            //Pause(500);


        }

        /// <summary>
        /// бежим в Стартонии до Нуньеса
        /// </summary>
        public void RunToNunez()
        {
            
            pointRunNunies.DoubleClickL();   // Нажимаем кнопку вызова списка групп
            Pause(25000);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateOfTeam()
        {
            pointTeamSelection1.PressMouse();   // Нажимаем кнопку вызова списка групп
            Pause(1500);
            pointUnselectMedik.PressMouseL();   // выкидывание медика из команды
            Pause(1500);
            pointSelectMusk.PressMouseL();       // выбор мушкетера в команду
            Pause(1500);
            pointNameOfTeam.PressMouseL();      //тыкаем в строку, где надо вводить имя группы героев
            Pause(1500);

            Random random = new Random();
            int temp;
            temp = random.Next(99999);        //случайное число от 0 до 9999
            string randomNumber = temp.ToString();     //число в строку
            SendKeys.SendWait(randomNumber);
            Pause(1500);
            pointButtonSaveNewTeam.PressMouseL();
            Pause(2500);

        }

        /// <summary>
        /// Ввод имени новой семьи
        /// </summary>
        public void NewName()
        {
            pointNewName.PressMouse();   //тыкнули в строчку, где нужно вводить имя семьи
            Pause(1500);

            Random random = new Random();
            int temp;
            temp = random.Next(9999);        //случайное число от 0 до 9999
            string randomNumber = temp.ToString();    //число в строку

            string newFamily = botwindow.getNameOfFamily();
            SendKeys.SendWait(newFamily + randomNumber);

            Pause(1500);
            pointButtonCreateNewName.DoubleClickL();    //тыкнули в кнопку создания новой семьи
            Pause(3000);
        }

        /// <summary>
        /// вводим имена героев (участников семьи) 
        /// </summary>
        public void NamesOfHeroes()
        {
            //medik
            pointCreateHeroes.PressMouse();    //создали медика
            Pause(1500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(1500);

            //musketeer #1
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(1500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(1500);
            pointNameOfHeroes.PressMouseL();
            Pause(1500);
            SendKeys.SendWait("Musk1");
            Pause(1500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(1500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(1500);

            //musketeer #2
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(1500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(1500);
            pointNameOfHeroes.PressMouseL();
            Pause(1500);
            SendKeys.SendWait("Musk2");
            Pause(1500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(1500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(1500);

            //musketeer #3

            pointButtonCreateChar.PressMouseL();
            Pause(1500);
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(1500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(1500);
            pointNameOfHeroes.PressMouseL();
            Pause(1500);
            SendKeys.SendWait("Musk3");
            Pause(1500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(1500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(1500);

        }

        #endregion

        #region кратер
        /// <summary>
        /// перекладываем митридат в ячейку под цифрой 2
        /// </summary>
        public void PutMitridat()
        {
            TopMenu(8, 1);
            Pause(2000);

            pointSecondBookmark.PressMouseL();
            Pause(2000);

            pointMitridat.Drag(pointMitridatTo2);
            Pause(2000);

            botwindow.PressEscThreeTimes();
            Pause(1000);
        }

        /// <summary>
        /// включаем родные стены
        /// </summary>
        public void OnPremium()
        {
            TopMenu(8, 2);
            Pause(2000);

            pointBookmark3.PressMouseL();
            Pause(1000);

            pointFirstItem.DoubleClickL();      //нажимаем дважды на первую вещь в спецкармане
            Pause(1000);

            pointButtonYesPremium.PressMouseL();  //подтверждаем
            Pause(2000);

            botwindow.PressEscThreeTimes();
            Pause(1000);
        }

        /// <summary>
        /// сохраняем телепорт в месте работы
        /// </summary>
        public void SaveTeleport()
        {
            TopMenu(12);
            Pause(1000);

            pointButtonSaveTeleport.PressMouseL();
            Pause(1000);

            pointButtonOkSaveTeleport.PressMouseL();
            Pause(1000);

            botwindow.PressEscThreeTimes();
            Pause(1000);
        }

        /// <summary>
        /// бежим к месту работы в Кратере
        /// </summary>
        public void RunToWork()
        {
            botwindow.PressMitridat();
            Pause(1000);

            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1000);

            pointWorkCrater.PressMouseR();             //бежим к месту работы в кратере
            Pause(1000);

            botwindow.PressEscThreeTimes();
            Pause(botwindow.getNumberWindow() * 10000);     // пауза зависит от номера окна, так как первые боты будут ближе ко входу стоять
        }

        /// <summary>
        /// бежим к телепорту WP
        /// </summary>
        public void RunToCrater()
        {
            botwindow.PressMitridat();
            Pause(1000);

            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1000);

            pointGateCrater.PressMouseL();             //переход (ворота) из лавового плато в кратер
            Pause(1000);

            botwindow.PressEscThreeTimes();
            Pause(65000);


        }


        /// <summary>
        /// выбираем лавовое плато на телепорте
        /// </summary>
        public void DialogWaypoint()
        {
            pointBookmarkField.PressMouseL();            //выбрали телепорт (последняя строчка)
            Pause(1000);

            pointButtonLavaPlato.PressMouseL();          //выбрали лавовое плато
            Pause(10000);

        }

        /// <summary>
        /// бежим к телепорту WP
        /// </summary>
        public void RunToWaypoint()
        {
            Pause(5000);
            botwindow.PressEscThreeTimes();
            Pause(1000);

            OpenMapForState();                         //открыли карту Alt+Z
            Pause(1000);

            pointWayPointMap.PressMouseL();            //выбрали телепорт (последняя строчка)
            Pause(1000);

            town_begin.ClickMoveMap();                 //нажимаем на кнопку Move на карте
            Pause(5000);

            botwindow.PressEscThreeTimes();
            Pause(1000);

            pointWayPoint.PressMouseL();           //нажимаем на телепорт
            Pause(1000);
        }

        #endregion

        #region "Заточка у Иды" 

        /// <summary>
        /// делаем активным инвентарь
        /// </summary>
        public void InventoryActive()
        {
            pointAcriveInventory.PressMouseR();
        }

        /// <summary>
        /// проверяем, стал ли активным инвентарь (по слову Equip)
        /// </summary>
        /// <returns></returns>
        public bool isActiveInventory()
        {
            return pointIsActiveInventory.isColor();
        }
        
        /// <summary>
        /// проверяем, был ли переложен предмет экипировки на место для заточки
        /// </summary>
        /// <returns></returns>
        public bool isMoveEquipment()
        {
            return (pointisMoveEquipment1.isColor() && pointisMoveEquipment2.isColor());
        }

        /// <summary>
        /// нажимаем на кнопку Enhance
        /// </summary>
        public void PressButtonEnhance()
        {
            pointButtonEnhance.PressMouseL();
        }

        /// <summary>
        /// проверяем, заточилась ли вещь на +4
        /// </summary>
        /// <returns></returns>
        public bool isPlus4()
        {
            return ((pointIsPlus41.isColor() && pointIsPlus42.isColor()) || (pointIsPlus43.isColor() && pointIsPlus44.isColor()));  //либо одни две точки либо другие две
        }

        /// <summary>
        /// нажимаем на кнопку Max (добавляем Shiny Crystal для заточки на +5 или +6)
        /// </summary>
        public void AddShinyCrystall()
        {
            pointAddShinyCrystall.PressMouseL();
        }

        /// <summary>
        /// проверяем, прибавились ли шайники к заточке на +6 (проверка по голубой полоске)
        /// </summary>
        /// <returns></returns>
        public bool isAddShinyCrystall()
        {
            return (pointIsAddShinyCrystall1.isColor() && pointIsAddShinyCrystall2.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в магазине у Иды (заточка)
        /// </summary>
        /// <returns></returns>
        public bool isIda()
        {
            //bool ff = pointIsIda1.isColor();
            //bool gg = pointIsIda2.isColor();
            return (pointIsIda1.isColor() && pointIsIda2.isColor());
        }

        public abstract void MoveToSharpening(int numberOfEquipment);

        #endregion

        #region чиповка

        /// <summary>
        /// проверяем, находимся ли в магазине у Чиповщицы
        /// </summary>
        /// <returns></returns>
        public bool isEnchant()
        {
            return (pointIsEnchant1.isColor() && pointIsEnchant2.isColor());
        }

        /// <summary>
        /// проверяем, является ли предмет для чиповки оружием
        /// </summary>
        /// <returns></returns>
        public bool isWeapon()
        {
            return (pointisWeapon1.isColor() && pointisWeapon2.isColor());
        }

        /// <summary>
        /// проверяем, является ли предмет для чиповки брони
        /// </summary>
        /// <returns></returns>
        public bool isArmor()
        {
            return (pointisArmor1.isColor() && pointisArmor2.isColor());
        }

        /// <summary>
        /// переносим (DragAndDrop) левую панель, чтобы она не мешала
        /// </summary>
        /// <param name="numberOfEquipment">номер экипировки п/п</param>
        public void MoveLeftPanel()
        {
            pointMoveLeftPanelBegin.Drag(pointMoveLeftPanelEnd);
        }

        /// <summary>
        /// нажимаем на кнопку Enchance для чипования
        /// </summary>
        public void PressButtonEnchance()
        {
            pointButtonEnchance.PressMouseL();
        }

        /// <summary>
        /// проверяем, является ли предмет для чиповки брони
        /// </summary>
        /// <returns></returns>
        public bool isGoodChipArmor()
        {
            bool result = false;

            if (isDef15() && isHP()) result = true;
//            if (isDef15()) result = true;
//            if (isHP()) result = true;


            return result;
        }

        /// <summary>
        /// проверяем, зачиповалась ли броня на +15 def
        /// </summary>
        /// <returns></returns>
        public bool isDef15()
        {
            return pointisDef15.isColor();
        }

        /// <summary>
        /// проверяем, зачиповалась ли броня на +15 def
        /// </summary>
        /// <returns></returns>
        public bool isHP()
        {
            return (pointisHP1.isColor() || pointisHP2.isColor() || pointisHP3.isColor() || pointisHP4.isColor());
        }

        /// <summary>
        /// метод возвращает параметр, отвечающий за тип чиповки оружия
        /// </summary>
        /// <returns></returns>
        public int TypeOfNintendo()
        { return int.Parse(File.ReadAllText(globalParam.DirectoryOfMyProgram + "\\Чиповка.txt")); }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на АТК + 40%
        /// </summary>
        /// <returns></returns>
        public bool isAtk40()
        {
            return (pointisAtk401.isColor() && pointisAtk402.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на АТК + 39%
        /// </summary>
        /// <returns></returns>
        public bool isAtk39()
        {
            return (pointisAtk391.isColor() && pointisAtk392.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на АТК + 38%
        /// </summary>
        /// <returns></returns>
        public bool isAtk38()
        {
            return (pointisAtk381.isColor() && pointisAtk382.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на АТК + 37%
        /// </summary>
        /// <returns></returns>
        public bool isAtk37()
        {
            return (pointisAtk371.isColor() && pointisAtk372.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на скорость + 30%
        /// </summary>
        /// <returns></returns>
        public bool isAtkSpeed30()
        {
            return pointisSpeed30.isColor();
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на скорость + 29%
        /// </summary>
        /// <returns></returns>
        public bool isAtkSpeed29()
        {
            return (pointisSpeed291.isColor() && pointisSpeed292.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на скорость + 28%
        /// </summary>
        /// <returns></returns>
        public bool isAtkSpeed28()
        {
            return (pointisSpeed281.isColor() && pointisSpeed282.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на скорость + 28%
        /// </summary>
        /// <returns></returns>
        public bool isAtkSpeed27()
        {
            return (pointisSpeed271.isColor() && pointisSpeed272.isColor());
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на атаку по животным
        /// </summary>
        /// <returns></returns>
        public bool isWild()
        {
            return (
                    (pointisWild41.isColor() && pointisWild42.isColor()) ||
                    (pointisWild51.isColor() && pointisWild52.isColor()) || 
                    (pointisWild61.isColor() && pointisWild62.isColor()) ||
                    (pointisWild71.isColor() && pointisWild72.isColor())
                   );
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на атаку по людям
        /// </summary>
        /// <returns></returns>
        public bool isHuman()
        {
            return (
                    (pointisHuman41.isColor() && pointisHuman42.isColor()) ||
                    (pointisHuman51.isColor() && pointisHuman52.isColor()) ||
                    (pointisHuman61.isColor() && pointisHuman62.isColor()) ||
                    (pointisHuman71.isColor() && pointisHuman72.isColor())
                    );
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на атаку по демонам
        /// </summary>
        /// <returns></returns>
        public bool isDemon()
        {
            return (
                    (pointisDemon41.isColor() && pointisDemon42.isColor()) ||
                    (pointisDemon51.isColor() && pointisDemon52.isColor()) ||
                    (pointisDemon61.isColor() && pointisDemon62.isColor()) ||
                    (pointisDemon71.isColor() && pointisDemon72.isColor())
                    );
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на атаку по Undead
        /// </summary>
        /// <returns></returns>
        public bool isUndead()
        {
            return (
                    (pointisUndead41.isColor() && pointisUndead42.isColor()) ||
                    (pointisUndead51.isColor() && pointisUndead52.isColor()) ||
                    (pointisUndead61.isColor() && pointisUndead62.isColor()) ||
                    (pointisUndead71.isColor() && pointisUndead72.isColor())
                    );
        }

        /// <summary>
        /// проверяем, зачиповалось ли оружие на атаку по Lifeless
        /// </summary>
        /// <returns></returns>
        public bool isLifeless()
        {
            return (
                    (pointisLifeless41.isColor() && pointisLifeless42.isColor()) ||
                    (pointisLifeless51.isColor() && pointisLifeless52.isColor()) ||
                    (pointisLifeless61.isColor() && pointisLifeless62.isColor()) ||
                    (pointisLifeless71.isColor() && pointisLifeless72.isColor())
                    );
        }

        /// <summary>
        /// проверяем, является ли предмет для чиповки оружия
        /// 1 - без рассы
        /// 2 - wild
        /// 3 - LifeLess
        /// 4 - wild or Human
        /// 5 - Undeed
        /// 6 - Demon
        /// 7 - Human
        /// </summary>
        /// <returns></returns>
        public bool isGoodChipWeapon()
        {
            bool result = false;
            int parametr = globalParam.Nintendo;
            switch (parametr)
            {
                case 1:
                    if ((isAtk40() || isAtk39()) && (isAtkSpeed30() || isAtkSpeed29())) result = true;
                    break;
                case 2:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isWild())) result = true;
                    //if ((isAtk40() || isAtk39()) && (isAtkSpeed30() || isAtkSpeed29())) result = true;
                    break;
                case 3:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isLifeless())) result = true;
                    break;
                case 4:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isWild() || isHuman())) result = true;
                    if ((isAtk40() || isAtk39()) && (isAtkSpeed30() || isAtkSpeed29())) result = true;
                    break;
                case 5:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isUndead())) result = true;
                    if ((isAtk40() || isAtk39()) && (isAtkSpeed30() || isAtkSpeed29())) result = true;
                    break;
                case 6:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isDemon())) result = true;
                    break;
                case 7:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) && (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && (isHuman())) result = true;
                    break;
                case 10:
                    if ((isAtk40() || isAtk39() || isAtk38() || isAtk37()) &&
                        (isAtkSpeed30() || isAtkSpeed29() || isAtkSpeed28() || isAtkSpeed27()) && 
                        (isHuman() || (isWild()) || (isLifeless()) || (isUndead()) || (isDemon()) )) result = true;
                    break;
            }
            return result;
        }

        public abstract void MoveToNintendo(int numberOfEquipment);

        #endregion

        #region Personal Trade
        /// <summary>
        /// определяет открыто ли окно для персональной торговли
        /// </summary>
        /// <returns></returns>
        public bool isPersonalTrade()
        {
            return (pointPersonalTrade1.isColor() && pointPersonalTrade2.isColor());
        }


        #endregion

        #region методы для перекладывания песо в торговца

        /// <summary>
        /// для передачи песо торговцу. Идем на место и предложение персональной торговли                        
        /// </summary>
        public void GotoPlaceTradeBot()      
        {
            //идем на место передачи песо
            botwindow.PressEscThreeTimes();
            Pause(1000);

            town.MaxHeight();                    // с учетом города и сервера
            Pause(500);

            OpenMapForState();                   // открываем карту города
            Pause(500);

            pointMap.DoubleClickL();             // тыкаем в карту, чтобы добежать до нужного места

            botwindow.PressEscThreeTimes();      // закрываем карту
            Pause(25000);                        // ждем пока добежим

            iPointColor pointMenuTrade = new PointColor(588 - 5 + xx, 230 - 5 + yy, 1710000, 4);
            while (!pointMenuTrade.isColor())
            {
                //жмем правой на торговце
                pointTrader.PressMouseR();
                Pause(1000);
            }

            //жмем левой  на пункт "Personal Trade"
            pointPersonalTrade.PressMouseL();
            Pause(500);
        }        //проверено

        /// <summary>
        /// обмен песо на фесо (часть 1 со стороны торговца) 
        /// </summary>
        public void ChangeVisDealer()
        {
            // наживаем Yes, подтверждая открытие торговли
            PressButtonYesTrade();

            // открываем сундук (карман)
            TopMenu(8, 1);

            // открываем закладку кармана, там где фесо
            OpenFourthBookmark();

            // перетаскиваем фесо на стол торговли
            MoveFesoToTrade();

            // нажимаем ок и обмен
            PressOkTrade();
        }                //проверено

        /// <summary>
        /// обмен песо. закрываем сделку со стороны бота
        /// </summary>
        public void ChangeVisBot()
        {
            // открываем инвентарь
            TopMenu(8, 1);

            // открываем закладку кармана, там где песо
            OpenFourthBookmark();
            //pointVis1.DoubleClickL();
            //Pause(500);


            //// перетаскиваем песо на стол торговли
            MoveVisToTrade();

            // нажимаем ок и обмен
            PressOkTrade();
        }                  // проверено

        /// <summary>
        /// подтверждает согласие на персональную торговлю
        /// </summary>
        public void PressButtonYesTrade()
        {
            pointYesTrade.DoubleClickL();
            Pause(500);        
        }

        /// <summary>
        /// нажимаем Ок и Обмен в персональной торговле
        /// </summary>
        public void PressOkTrade()
        {
            // нажимаем ок
            pointOk.DoubleClickL();
            Pause(500);

            // нажимаем обмен
            pointTrade.DoubleClickL();
            Pause(500);
        }

        /// <summary>
        /// открываем четвертую закладку в инвентаре
        /// </summary>
        public void OpenFourthBookmark()
        {
            pointBookmark4.DoubleClickL();
            Pause(500);
        }

        /// <summary>
        /// перемещаем фесо 125 000 из инвентаря на стол торговли
        /// </summary>
        public void MoveFesoToTrade()
        {
            // перетаскиваем фесо на стол торговли
            pointFesoBegin.Drag(pointFesoEnd);
            Pause(500);

            SendKeys.SendWait("300000");
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы фесо
            pointOkSum.DoubleClickL();
        
        }

        /// <summary>
        /// перемещаем всё песо из инвентаря на стол торговли
        /// </summary>
        public void MoveVisToTrade()
        {
            // перетаскиваем песо
            pointVisMove1.Drag(pointVisMove2);                                             // песо берется из первой ячейки на 4-й закладке  
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы песо
            pointOkSum.DoubleClickL();

        }

        /// <summary>
        /// купить 125 еды в фесо шопе                    
        /// </summary>
        public void Buy125PetFood()
        {
            // тыкаем два раза в стрелочку вверх
            pointFood.DoubleClickL();
            Pause(500);

            //нажимаем 125
            SendKeys.SendWait("125");

            // жмем кнопку купить
            pointButtonFesoBUY.DoubleClickL();
            Pause(1500);

            //нажимаем кнопку Close
            botwindow.PressEscThreeTimes();
            //pointButtonClose.DoubleClickL();
            Pause(1500);
        }

        /// <summary>
        /// продать 3 ВК (GS) в фесо шопе 
        /// </summary>
        public void SellGrowthStone3pcs()
        {
            // 3 раза нажимаем на стрелку вверх, чтобы отсчитать 3 ВК
            for (int i = 1; i <= 3; i++)
            {
                pointArrowUp2.PressMouseL();
                Pause(700);
            }

            //нажимаем кнопку Sell
            pointButtonFesoSell.PressMouseL();
            Pause(1000);

            //нажимаем кнопку Close
            pointButtonClose.PressMouseL();
            Pause(2500);
        }

        /// <summary>
        /// открыть вкладку Sell в фесо шопе
        /// </summary>
        public  void OpenBookmarkSell()
        {
            pointBookmarkFesoSell.DoubleClickL();
            Pause(1500);
        }

        /// <summary>
        /// переход торговца к месту передачи песо (внутри города)
        /// </summary>
        public void GoToChangePlace()
        {
            pointDealer.DoubleClickL();
        }

        /// <summary>
        /// открыть фесо шоп
        /// </summary>
        public void OpenFesoShop()
        {
            TopMenu(2, 2);
            Pause(1000);
        }


        #endregion

        #region общие2

        /// <summary>
        /// определяем, есть ли в команде второй перс (герой)
        /// </summary>
        public bool isSecondHero()
        {
            return (pointisKillHero2.isColor() || pointisLiveHero2.isColor());
        }

        /// <summary>
        /// определяем, есть ли в команде третий перс (герой)
        /// </summary>
        public bool isThirdHero()
        {
            return (pointisKillHero3.isColor() || pointisLiveHero3.isColor());
        }

        #endregion

        #region Undressing in Barack

        /// <summary>
        /// раздевание персонажей в выбранной казарме
        /// </summary>
        private void UnDressingInCurrentBarack()
        {
            int[] x = { 0, 0, 130, 260, 390, -70, 60, 190, 320, 450 };
            int[] y = { 0, 0, 0, 0, 0, 340, 340, 340, 340, 340 };

            for (int i = 1; i <= 9; i++)            //перебор героев в текущей казарме
            {
                iPointColor pointHatC = new PointColor(285 - 5 + xx + x[i], 119 - 5 + yy + y[i], 2434089, 0);
                iPointColor pointGlassesC = new PointColor(369 - 5 + xx + x[i], 119 - 5 + yy + y[i], 131588, 0);
                iPointColor pointMedalC = new PointColor(345 - 5 + xx + x[i], 159 - 5 + yy + y[i], 5398113, 0);
                iPointColor pointWingsC = new PointColor(285 - 5 + xx + x[i], 199 - 5 + yy + y[i], 197640, 0);
                iPointColor pointArmorC = new PointColor(325 - 5 + xx + x[i], 199 - 5 + yy + y[i], 2960436, 0);
                iPointColor pointCostumeC = new PointColor(365 - 5 + xx + x[i], 199 - 5 + yy + y[i], 2960436, 0);
                iPointColor pointWeaponC = new PointColor(290 - 5 + xx + x[i], 241 - 5 + yy + y[i], 855313, 0);
                iPointColor pointGlovesC = new PointColor(325 - 5 + xx + x[i], 279 - 5 + yy + y[i], 1644571, 0);
                iPointColor pointBootsC = new PointColor(328 - 5 + xx + x[i], 325 - 5 + yy + y[i], 6513252, 0);

                iPoint pointHat = new Point(285 - 5 + xx + x[i], 119 - 5 + yy + y[i]);
                iPoint pointGlasses = new Point(365 - 5 + xx + x[i], 119 - 5 + yy + y[i]);
                iPoint pointMedal = new Point(345 - 5 + xx + x[i], 159 - 5 + yy + y[i]);
                iPoint pointWings = new Point(285 - 5 + xx + x[i], 199 - 5 + yy + y[i]);
                iPoint pointArmor = new Point(325 - 5 + xx + x[i], 199 - 5 + yy + y[i]);
                iPoint pointCostume = new Point(365 - 5 + xx + x[i], 199 - 5 + yy + y[i]);
                iPoint pointWeapon = new Point(285 - 5 + xx + x[i], 239 - 5 + yy + y[i]);
                iPoint pointGloves = new Point(325 - 5 + xx + x[i], 279 - 5 + yy + y[i]);
                iPoint pointBoots = new Point(325 - 5 + xx + x[i], 319 - 5 + yy + y[i]);

                if (!pointGlassesC.isColor()) { pointGlasses.DoubleClickL(); Pause(200); }
                if (!pointHatC.isColor()) { pointHat.DoubleClickL(); Pause(200); }
                if (!pointMedalC.isColor()) { pointMedal.DoubleClickL(); Pause(200); }
                if (!pointWingsC.isColor()) { pointWings.DoubleClickL(); Pause(200); }
                if (!pointArmorC.isColor()) { pointArmor.DoubleClickL(); Pause(200); }
                if (!pointCostumeC.isColor()) { pointCostume.DoubleClickL(); Pause(200); }
                if (!pointWeaponC.isColor()) { pointWeapon.DoubleClickL(); Pause(200); }
                if (!pointGlovesC.isColor()) { pointGloves.DoubleClickL(); Pause(200); }
                if (!pointBootsC.isColor()) { pointBoots.DoubleClickL(); Pause(200); }
            }

        }

        /// <summary>
        /// раздевание в первых четырёх казармах (36 персонажей)
        /// </summary>
        public void UnDressing()
        {
            bool ff = true;
            while (ff)
            {
                pointShowEquipment.PressMouseL();      //нажимаем на кнопку "Show Equipment"
                Pause(1000);
                ff = (pointEquipment1.isColor()) && (pointEquipment2.isColor());
                ff = ! ff;
            }

            for (int i = 1; i <= 4; i++)
            {
                pointBarack[i].PressMouseL();            //выбираем i-ю казарму
                Pause(1000);
                UnDressingInCurrentBarack();
            }

            //pointBarack1.PressMouseL();            //выбираем первую казарму
            //Pause(1000);
            //UnDressingInCurrentBarack();

            //pointBarack2.PressMouseL();            //выбираем вторую казарму
            //Pause(1000);
            //UnDressingInCurrentBarack();


            //pointBarack3.PressMouseL();            //выбираем третью казарму
            //Pause(1000);
            //UnDressingInCurrentBarack();


            //pointBarack4.PressMouseL();            //выбираем четвертую казарму
            //Pause(1000);
            //UnDressingInCurrentBarack();

        }

        #endregion

        #region Гильдия Охотников BH

        /// <summary>
        /// проверяем, крутится ли рулетка после убийства босса
        /// </summary>
        /// <returns></returns>
        public bool isRouletteBH()
        {
            return (pointIsRoulette1.isColor() && pointIsRoulette2.isColor());
        }

        ///// <summary>
        ///// метод возвращает значение статуса, 1 - мы направляемся на продажу товара в магазин, 0 - нет (обычный режим работы)
        ///// </summary>
        ///// <returns></returns>
        //private int GetStatusOfSale()
        //{ return int.Parse(File.ReadAllText(globalParam.DirectoryOfMyProgram + "\\StatusOfSale.txt")); }

        ///// <summary>
        ///// метод возвращает значение статуса, 1 - мы направляемся на продажу товара в магазин, 0 - нет (обычный режим работы)
        ///// </summary>
        ///// <returns></returns>
        //private void SetStatusOfSale(int status)
        //{
        //    File.WriteAllText(globalParam.DirectoryOfMyProgram + "\\StatusOfSale.txt", status.ToString());
        //}


        /// <summary>
        /// проверяем, атакуем ли сейчас босса
        /// если точки серые т.е. pointIsAtak1.isColor() = true или pointIsAtak1.isColor() = true, то значит не атакуем. Поэтому стоит отрицание
        /// </summary>
        /// <returns></returns>
        public bool isAtakBH()
        {
            return (pointIsAtak1.isColor() && pointIsAtak2.isColor());
        }

        /// <summary>
        /// ожидание прекращения атаки
        /// </summary>
        public void waitToCancelAtak()
        {
            //while (isAtakBH())
            //{
            //    Pause(500);
            //}
        }

        ///// <summary>
        ///// опускаем камеру (опускаем максимально вниз)                           
        ///// </summary>
        //public void MinHeight()
        //{
        //    Point pointMinHeight = new Point(514 - 30 + xx, 352 - 30 + yy);
        //    for (int j = 1; j <= 5; j++)
        //    {
        //        pointMinHeight.PressMouseWheelDown();
        //        Pause(300);
        //    }
        //}

        /// <summary>
        /// записываем в лог-файл инфу по прохождению программы
        /// </summary>
        /// <param name="strLog"></param>
        public void WriteToLogFileBH(string strLog)
        {
            //StreamWriter writer = new StreamWriter(globalParam.DirectoryOfMyProgram + "\\BH.log", true);
            //string timeNow = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss | ");

            //writer.WriteLine(timeNow + botwindow.getNumberWindow().ToString() + " " + strLog);
            //writer.Close();
        }

        /// <summary>
        /// записываем в лог-файл инфу по прохождению программы
        /// </summary>
        /// <param name="strLog"></param>
        public void WriteToLogFile(string strLog)
        {
            StreamWriter writer = new StreamWriter(globalParam.DirectoryOfMyProgram + "\\Error.log", true);
            string timeNow = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss | ");

            writer.WriteLine(timeNow + botwindow.getNumberWindow().ToString() + " " + strLog);
            writer.Close();
        }



        /// <summary>
        /// нажимаем левой кнопкой мыши на точку с указанными координатами
        /// </summary>
        /// <param name="x">коорд X</param>
        /// <param name="y">коорд Y</param>
        public void FightToPoint(int x, int y, int t)
        {
             Point pointSabreBottonBH = new Point(92 - 5 + xx, 525 - 5 + yy);         //нажимаем на кнопку с саблей на боевой панели (соответствует нажатию Ctrl)
             Point pointFightBH = new Point(x - 30 + xx, y - 30 + yy);                //нажимаем конкретную точку, куда надо бежать и бить всех по пути

             pointSabreBottonBH.PressMouseL();
             pointFightBH.PressMouseL();
             Pause(t * 1000);
        }

        /// <summary>
        /// проверка миссии по цвету контрольной точки
        /// </summary>
        /// <returns> номер цвета </returns>
        public abstract uint ColorOfMissionBH();                                                                    //может быть перенести из синга сюда

        /// <summary>
        /// отбегаем в сторону, чтобы бот перестал стрелять
        /// </summary>
        public abstract void runAway();

        /// <summary>
        /// проверяем, находимся ли в миссии Гильдии Охотников (проверяется 16 миссий по контрольной точке)
        /// </summary>
        /// <returns></returns>
        public bool isMissionBH()
        {
            //проверяем одну и ту же точку на экране в миссии
            //каждой карте в теории соответствует свой цвет этой точки

            // в массиве arrayOfColors хранятся все цвета контрольной точки, которые могут быть миссиях. Один цвет - одна миссия

            uint color = new PointColor(700 - 30 + xx, 500 - 30 + yy, 0, 0).GetPixelColor();                 // проверяем номер цвета в контрольной точке
            color = color / 1000;
            //int tt = Array.IndexOf(arrayOfColors, color);
            
            bool result = this.arrayOfColors.Contains(color);                                                         // проверяем, есть ли цвет контрольной точки в массиве цветов
            //if (!result) WriteToLogFileBH("неизвестная миссия, цвет " + color);

            return result;                                                   // проверяем, есть ли цвет контрольной точки в массиве цветов

        }

        /// <summary>
        /// если миссия в БХ не найдена. Нет в списке
        /// </summary>
        public void MissionNotFoundBH()
        {
            //botwindow.setStatusOfAtk(1);
            
            uint color = new PointColor(700 - 30 + xx, 500 - 30 + yy, 0, 0).GetPixelColor();                 // проверяем номер цвета в контрольной точке
            //color = color / 1000;
            WriteToLogFileBH("неизвестная миссия!!!, цвет " + color);

            //FightToPoint(1000, 100, 0);    //бежим с атакой в сторону (вдруг зацепим Босса)

            //сохраняем скриншот
            string timeNow = DateTime.Now.ToString(" ddMMMMyyyy HH-mm-ss");
            Size screenSz = Screen.PrimaryScreen.Bounds.Size;
            Bitmap screenshot = new Bitmap(screenSz.Width, screenSz.Height);
            Graphics gr = Graphics.FromImage(screenshot);
            gr.CopyFromScreen(0, 0, 0, 0, screenSz);
            string filepath = globalParam.DirectoryOfMyProgram + "\\ScreenShot"+ timeNow + ".bmp" ;
            screenshot.Save(filepath);    
        }

        /// <summary>
        /// проверяем контрольную точку в миссии БХ и находим, в какой миссии мы находимся 
        /// </summary>
        /// <returns>номер миссии по порядку</returns>
        public int NumberOfMissionBH()
        {
            uint color = new PointColor(700 - 30 + xx, 500 - 30 + yy, 0, 0).GetPixelColor();                // проверяем номер цвета в контрольной точке
            color = color / 1000;
            int result = Array.IndexOf(this.arrayOfColors, color);                                               // номер миссии соответствует порядковому номеру цвета в массиве arrayOfColors
            //if (result == 0) WriteToLogFileBH("неизвестная миссия, цвет " + color);
            return result;
        }

        /// <summary>
        /// проверяем, находимся ли в Гильдии Охотников
        /// </summary>
        /// <returns></returns>
        public bool isBH()
        {
//            return (isTown() && !pointisBH1.isColor());
            return (!pointisBH1.isColor());
        }

        /// <summary>
        /// проверяем, находимся ли в Гильдии Охотников на неправильном месте (вылет из миссии после убийства босса через минуту бездействия)
        /// </summary>
        /// <returns></returns>
        public bool isBH2()
        {
            return (pointisBH2.isColor());
        }



        /// <summary>
        /// тыкаем в ворота Infinity (Гильдии Охотников)
        /// </summary>
        /// <returns></returns>
        public void GoToInfinityGateBH()
        {
            //MinHeight();
            pointGateInfinityBH.PressMouseL();
        }

        /// <summary>
        /// поворот на 180 градусов через левое плечо
        /// </summary>
        /// <returns></returns>
        public void Turn180()
        {
            //если begin больше на 24, то вправо на 90
            //если begin меньше на 24, то влево на 90
            //если begin больше на 16, то на 180 градусов

            //Point pointBegin = new Point(560 + 16 + - 30 + xx, 430 - 30 + yy);
            //Point pointEnd = new Point(560 - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);

            TurnL(16);


            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);

            ////ровно 135 градусов за 4 поворота (33,75)
            //Point pointBegin = new Point(560 + 1 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd   = new Point(560     - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);

            //ровно 270 градусов за 4 поворота (67,5)
            //Point pointBegin = new Point(560 + 2 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd   = new Point(560     - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);

            ////ровно 360+45 градусов за 4 поворота (101,25 )
            //Point pointBegin = new Point(560 + 3 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd =   new Point(560     - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);

            ////ровно 360+180=540 градусов за 4 поворота (135)
            //Point pointBegin = new Point(560 + 4 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd   = new Point(560     - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);
            //pointBegin.Turn(pointEnd);

        }

        /// <summary>
        /// поворот на 90 градусов вправо (по часовой стрелке)
        /// </summary>
        /// <returns></returns>
        public void Turn90L()
        {
            //если begin больше на 24, то влево на 90
            //если begin меньше на 24, то вправо на 90
            //если begin больше на 16, то на 180 градусов

            //Point pointBegin = new Point(560 + 24 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd   = new Point(560 - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);

            TurnL(24);
        }

        /// <summary>
        /// поворот на 90 градусов влево (против часовой стрелки)
        /// </summary>
        /// <returns></returns>
        public void Turn90R()
        {
            //если begin больше на 24, то влево на 90
            //если begin меньше на 24, то вправо на 90
            //если begin больше на 16, то на 180 градусов

            //Point pointBegin = new Point(560 - 24 - 30 + xx, 430 - 30 + yy);
            //Point pointEnd = new Point(560 - 30 + xx, 430 - 30 + yy);
            //pointBegin.Turn(pointEnd);

            TurnR(24);
        }


        /// <summary>
        /// поворот, чтобы смотреть более горизонтально (снизу вверх)
        /// </summary>
        public void TurnUp()
        {
            Point pointBegin = new Point(560 - 30 + xx, 430 - 30 + 1 + yy);
            Point pointEnd   = new Point(560 - 30 + xx, 430 - 30 + yy);
            pointBegin.Turn(pointEnd);
        }

        /// <summary>
        /// поворот, чтобы смотреть более вертикально (сверху вниз)
        /// </summary>
        public void TurnDown()
        {
            Point pointBegin = new Point(560 - 30 + xx, 430 - 30 - 1 + yy);
            Point pointEnd   = new Point(560 - 30 + xx, 430 - 30 + yy);
            pointBegin.Turn(pointEnd);
        }


        /// <summary>
        /// поворот влево (против часовой стрелки)
        /// </summary>
        /// <returns></returns>
        public void TurnL(int gradus)
        {
            Point pointBegin = new Point(560 + gradus - 30 + xx, 430 - 30 + yy);
            Point pointEnd   = new Point(560          - 30 + xx, 430 - 30 + yy);
            pointBegin.Turn(pointEnd);
        }

        /// <summary>
        /// поворот вправо (по часовой стрелке)
        /// </summary>
        /// <returns></returns>
        public void TurnR(int gradus)
        {
            Point pointBegin = new Point(560 - gradus - 30 + xx, 430 - 30 + yy);
            Point pointEnd   = new Point(560          - 30 + xx, 430 - 30 + yy);
            pointBegin.Turn(pointEnd);
        }

        #endregion



        ///// <summary>
        ///// переход на нужный канал после телепорта на работу 
        ///// </summary>
        //public void GoToChannel()
        //{
        //    if (botwindow.getKanal() > 1)
        //    {
        //        TopMenu(13);
        //        Pause(1000);

        //        pointChooseChannel.PressMouse();
        //        Pause(1000);

        //        pointEnterChannel.PressMouse();
        //        Pause(1000);

        //        pointMoveNow.PressMouse();
        //        Pause(15000);
        //    }
        //}

    }
}
