﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace OpenGEWindows
{
    public abstract class ServerInterface

    {
        [DllImport("user32.dll")]
        private static extern UIntPtr FindWindow(String ClassName, String WindowName);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        static extern bool PostMessage(UIntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern UIntPtr FindWindowEx(UIntPtr hwndParent, UIntPtr hwndChildAfter, string className, string windowName);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        public static extern UIntPtr DestroyWindow(UIntPtr hWnd);

        protected int xx;
        protected int yy;
        protected Town town;
        protected Town town_begin;
        protected botWindow botwindow;
        protected TownFactory townFactory;
        protected const String KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";
        protected iPointColor pointIsSale1;
        protected iPointColor pointIsSale2;
        protected iPointColor pointIsSale21;
        protected iPointColor pointIsSale22;
        protected iPointColor pointIsClickSale1;
        protected iPointColor pointIsClickSale2;
        protected iPointColor pointIsTown11;   //проверка по обычному ружью
        protected iPointColor pointIsTown12;
        protected iPointColor pointIsTown21;
        protected iPointColor pointIsTown22;
        protected iPointColor pointIsTown31;
        protected iPointColor pointIsTown32;
        
        protected iPointColor pointIsTown_11;   //проверка по эксп. дробовику
        protected iPointColor pointIsTown_12;
        protected iPointColor pointIsTown_21;
        protected iPointColor pointIsTown_22;
        protected iPointColor pointIsTown_31;
        protected iPointColor pointIsTown_32;

        protected iPointColor pointIsTown_11a;   //проверка по обычному дробовику
        protected iPointColor pointIsTown_12a;
        protected iPointColor pointIsTown_21a;
        protected iPointColor pointIsTown_22a;
        protected iPointColor pointIsTown_31a;
        protected iPointColor pointIsTown_32a;

        protected iPointColor pointIsTown__11;   //проверка по эксп. ружью (флинт)
        protected iPointColor pointIsTown__12;
        protected iPointColor pointIsTown__21;
        protected iPointColor pointIsTown__22;
        protected iPointColor pointIsTown__31;
        protected iPointColor pointIsTown__32;

        protected iPointColor pointisBoxOverflow1;
        protected iPointColor pointisBoxOverflow2;
        protected iPointColor pointisSummonPet1;
        protected iPointColor pointisSummonPet2;
        protected iPointColor pointisActivePet1;
        protected iPointColor pointisActivePet2;
        protected iPointColor pointisActivePet3;  //3 и 4 сделаны для европы для проверки корма на месяц
        protected iPointColor pointisActivePet4;
        protected iPointColor pointisLogout1;
        protected iPointColor pointisLogout2;
        protected iPointColor pointisBarack1;
        protected iPointColor pointisBarack2;
        protected iPointColor pointisBarack3;
        protected iPointColor pointisBarack4;
        protected iPointColor pointisWork1;         //проверка стойки с ружьем (проверяются две точки )
        protected iPointColor pointisWork2;
        protected iPointColor pointisWork_1;        //проверка стойки с эксп дробашом (проверяются две точки )
        protected iPointColor pointisWork_2;
        protected iPointColor pointisWork__1;       //проверка стойки с обычным дробашом (проверяются две точки )
        protected iPointColor pointisWork__2;
        protected iPointColor pointisOpenMenuPet1;
        protected iPointColor pointisOpenMenuPet2;
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
        protected iPointColor pointisKillHero1;
        protected iPointColor pointisKillHero2;
        protected iPointColor pointisKillHero3;
        protected iPointColor pointisToken1;      //при входе в город проверяем, открыто ли окно с подарочными токенами.
        protected iPointColor pointisToken2;
        protected iPoint pointToken;            //если окно с подарочными токенами открыто, то закрываем его нажатием на эту точку
        protected iPoint pointBuyingMitridat1;
        protected iPoint pointBuyingMitridat2;
        protected iPoint pointBuyingMitridat3;
        protected iPoint pointGotoEnd;
        protected iPoint pointTeamSelection1;
        protected iPoint pointTeamSelection2;
        protected iPoint pointTeamSelection3;
        protected iPoint pointTeleport1;
        protected iPoint pointTeleport2;
        protected iPoint pointCancelSummonPet;
        protected iPoint pointSummonPet1;
        protected iPoint pointSummonPet2;
        protected iPoint pointTeleportToTownAltW;
        protected iPoint pointActivePet;
        protected iPoint pointBookmarkSell;
        protected iPoint pointSaleToTheRedBottle;
        protected iPoint pointSaleOverTheRedBottle;
        protected iPoint pointWheelDown;
        protected iPoint pointButtonBUY;
        protected iPoint pointButtonSell;
        protected iPoint pointButtonClose;
        protected iPoint pointButtonLogOut;
        protected iPoint pointChooseChannel;
        protected iPoint pointEnterChannel;
        protected iPoint pointMoveNow;
        protected iPoint pointCure1;
        protected iPoint pointCure2;
        protected iPoint pointCure3;
        protected iPoint pointMana1;

        //создание нового бота
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

        //Стартония
        protected iPoint pointRunNunies;
        protected iPoint pointPressNunez;
        protected iPoint ButtonOkDialog;
        protected iPoint PressMedal;
        protected iPoint ButtonCloseMedal;
        protected iPoint pointPressNunez2;

        //ребольдо
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
        //лавовое плато
        protected iPoint pointGateCrater;
        protected iPoint pointSecondBookmark;
        protected iPoint pointMitridat;
        protected iPoint pointMitridatTo2;
        protected iPoint pointBookmark3;
        protected iPoint pointButtonYesPremium;


        //кратер
        protected iPoint pointWorkCrater;
        protected iPoint pointButtonSaveTeleport;
        protected iPoint pointButtonOkSaveTeleport;



        protected int sdvigY;
        protected String pathClient;
        protected int activeWindow;


        #region методы для работы кнопок по созданию новых ботов и переходу в кратер
        /// <summary>
        /// перекладываем митридат в ячейку под цифрой 2
        /// </summary>
        public void PutMitridat()
        {
            TopMenu(8,1);
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

        /// <summary>
        /// бежим к петэксперту
        /// </summary>
        public void RunToPetExpert()
        {
            pointPetExpert.PressMouseL();            //тыкнули в эксперта по петам
            Pause(3000);

            pointPetExpert2.PressMouseL();            //тыкнули в эксперта по петам второй раз
            Pause(3000);

        }

        /// <summary>
        /// диалог с петэкспертом
        /// </summary>
        public void DialogPetExpert()
        {
            pointFirstStringDialog.PressMouseL();       //нижняя строчка в диалоге
            Pause(1500);

            ButtonOkDialog.PressMouseL();               // Нажимаем на Ok в диалоге
            Pause(1500);

            pointFirstStringDialog.PressMouseL();       //нижняя строчка в диалоге
            Pause(1500);

            ButtonOkDialog.PressMouseL();               // Нажимаем на Ok в диалоге
            Pause(1500);

            pointThirdBookmark.PressMouseL();           //тыкнули в третью закладку в кармане
            Pause(1500);

            botwindow.MouseMoveAndDrop(800-5, 220-5, 520-5, 330-5);            // берем коробку с кокошкой и переносим в куб для вылупления у петэксперта
            botwindow.Pause(2500);

            pointNamePet.PressMouseL();                // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            SendKeys.SendWait("Koko");                 //вводим имя пета
            Pause(1500);

            pointButtonNamePet.PressMouseL();          // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            pointButtonClosePet.PressMouseL();          // Нажимаем на строчку, где надо написать имя пета
            Pause(1500);

            //жмем Ок 3 раза
            for (int j = 1; j <= 3; j++)
            {
                ButtonOkDialog.PressMouseL();           // Нажимаем на Ok в диалоге
                Pause(1500);
            }
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
                ButtonOkDialog.PressMouseL();           // Нажимаем на Ok в диалоге
                Pause(1500);
            }
        }


        /// <summary>
        /// бежим к доминго
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

            pointPressLindon2.PressMouseL();           //нажимаем на голову Доминго
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
                ButtonOkDialog.PressMouseL();           // Нажимаем на Ok в диалоге
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
            Pause(120000);

        }

        /// <summary>
        /// диалог с Доминго
        /// </summary>
        public void DomingoDialog()
        {
            //жмем Ок 3 раза
            for (int j = 1; j <= 3; j++)
            {
                ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
                Pause(1500);
            }

            pointFirstStringDialog.PressMouseL();   //YES
            Pause(1000);

            ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
            Pause(1000);

            pointSecondStringDialog.PressMouseL();   //YES во второй раз
            Pause(1000);

            ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
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
            Pause(30000);

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
            Pause(2000);
            pointPressLindon1.PressMouseL();
            Pause(4000);

            for (int j = 1; j <= 4; j++)
            {
                ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
                Pause(1000);
            }
            Pause(2000);
        }

        /// <summary>
        /// разговор с Нуьесом
        /// </summary>
        public void TalkRunToNunez()
        {
            pointPressNunez.PressMouseL();   // Нажимаем на Нуньеса
            Pause(2000);

            for (int j = 1; j <= 7; j++)
            {
                ButtonOkDialog.PressMouseL();    // Нажимаем на Ok в диалоге
                Pause(1000);
            }

            PressMedal.DoubleClickL();        //нажимаем на медаль 1 (медаль новичка)  двойной щелчок
            Pause(1500);

            ButtonCloseMedal.PressMouseL();    // Нажимаем на Close и закрываем медали
            Pause(2500);

            pointPressNunez2.PressMouseL();   // Нажимаем на Нуньеса
            Pause(3000);

            for (int j = 1; j <= 5; j++)
            {
                ButtonOkDialog.PressMouse();    // Нажимаем на Ok в диалоге
                Pause(1500);
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
//            pointRunNunies.PressMouseL();   // Нажимаем кнопку вызова списка групп
            pointRunNunies.PressMouseL();   // Нажимаем кнопку вызова списка групп
            Pause(20000);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateOfTeam()
        {
            pointTeamSelection1.PressMouse();   // Нажимаем кнопку вызова списка групп
            Pause(500);
            pointUnselectMedik.PressMouseL();   // выкидывание медика из команды
            Pause(500);
            pointSelectMusk.PressMouseL();       // выбор мушкетера в команду
            Pause(500);
            pointNameOfTeam.PressMouseL();      //тыкаем в строку, где надо вводить имя группы героев
            Pause(500);

            Random random = new Random();
            int temp;
            temp = random.Next(99999);        //случайное число от 0 до 9999
            string sss = temp.ToString();     //число в строку
            SendKeys.SendWait(sss);
            Pause(500);
            pointButtonSaveNewTeam.PressMouseL();
            Pause(2500);

        }

        /// <summary>
        /// Ввод имени новой семьи
        /// </summary>
        public void NewName()
        {
            pointNewName.PressMouseL();
            Pause(500);

            Random random = new Random();
            int temp;
            temp = random.Next(9999);        //случайное число от 0 до 9999
            string sss = temp.ToString();    //число в строку

            string sss2 = botwindow.NameOfFamily();
            SendKeys.SendWait(sss2 + sss);

            Pause(500);
            pointButtonCreateNewName.PressMouseL();
            Pause(2000);
        }

        /// <summary>
        /// вводим имена героев (участников семьи) 
        /// </summary>
        public void NamesOfHeroes()
        {
            //medik
            pointCreateHeroes.PressMouse();    //создали медика
            Pause(500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(500);

            //musketeer #1
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(500);
            pointNameOfHeroes.PressMouseL();
            Pause(500);
            SendKeys.SendWait("Musk1");
            Pause(500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(500);

            //musketeer #2
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(500);
            pointNameOfHeroes.PressMouseL();
            Pause(500);
            SendKeys.SendWait("Musk2");
            Pause(500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(500);

            //musketeer #3

            pointButtonCreateChar.PressMouseL();
            Pause(500);
            pointMenuSelectTypeHeroes.PressMouse();
            Pause(500);
            pointSelectTypeHeroes.PressMouseL();
            Pause(500);
            pointNameOfHeroes.PressMouseL();
            Pause(500);
            SendKeys.SendWait("Musk3");
            Pause(500);
            pointCreateHeroes.PressMouse();    //создали мушкетера
            Pause(500);
            pointButtonOkCreateHeroes.PressMouse(); //нажали Ок
            Pause(500);

        }

        #endregion

        /// <summary>
        /// нажимаем на кнопку логаут в казарме, тем самым покидаем казарму
        /// </summary>
        public void buttonExitFromBarack()
        {
            pointButtonLogOut.PressMouse();
            Pause(500);

        }

        /// <summary>
        /// Останавливает поток на некоторый период (пауза)
        /// </summary>
        /// <param name="ms"> ms - период в милисекундах </param>
        protected void Pause(int ms)
        {
            Thread.Sleep(ms);
        }


        /// <summary>
        /// выбираем первого пета и нажимаем кнопку Summon в меню пет
        /// </summary>
        public void buttonSummonPet()
        {
            pointSummonPet1.PressMouseL();      //Click Pet
            pointSummonPet1.PressMouseL();
            //botwindow.PressMouseL(569, 375);  //Click Pet
            //botwindow.PressMouseL(569, 375);
            Pause(500);
            //botwindow.PressMouseL(408, 360);  //Click кнопку "Summon"
            //botwindow.PressMouseL(408, 360);
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
            //uint bb = pointisOpenMenuPet1.GetPixelColor();
            //uint dd = pointisOpenMenuPet2.GetPixelColor();
            return (pointisOpenMenuPet1.isColor() && pointisOpenMenuPet2.isColor());
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


        /// <summary>
        /// вызываем телепорт через верхнее меню и телепортируемся по первому телепорту
        /// </summary>
        public void Teleport()
        {
            Pause(400);
            TopMenu(12); //Click Teleport menu
            //            botwindow.PressMouseL(400, 190 );
            pointTeleport1.PressMouseL();
            Pause(50);
            //botwindow.PressMouseL(400, 190 );
            pointTeleport1.PressMouseL();
            Pause(200);
            //botwindow.PressMouseL(355, 570); //Click on button Execute in Teleport menu
            pointTeleport2.PressMouseL();   //Click on button Execute in Teleport menu
            Pause(2000);
        }

        /// <summary>
        /// выбор команды персов из списка в казарме
        /// </summary>
        public void TeamSelection()
        {
            //            Class_Timer.Pause(500);
            pointTeamSelection1.PressMouse();   // Нажимаем кнопку вызова списка групп
            pointTeamSelection2.PressMouseL();  // выбираем нужную группу персов (первую в списке)
            pointTeamSelection3.PressMouseL();  // Нажимаем кнопку выбора группы (Select Team) 
        }

        /// <summary>
        /// Покупка митридата в количестве 333 штук
        /// </summary>
        public void BuyingMitridat()
        {
            //botwindow.PressMouseL(360, 537);          //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
            pointBuyingMitridat1.PressMouseL();             //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
            Pause(150);

            Press333();

            Botton_BUY();                             // Нажимаем на кнопку BUY 


            pointBuyingMitridat2.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если переполнение митридата
            //botwindow.PressMouseL(1392 - 875, 438 - 5);         
            Pause(500);

            pointBuyingMitridat3.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если нет денег на покупку митридата
            //botwindow.PressMouseL(1392 - 875, 428 - 5);          
            Pause(500);
        }

        /// <summary>
        /// Открыть городской телепорт (Alt + F3) без проверок и while (для паттерна Состояние)  StateGT
        /// </summary>
        public void OpenTownTeleportForState()
        {
            TopMenu(6, 3);
            Pause(1000);
        }

        /// <summary>
        /// проверяет, находится ли данное окно в магазине (а точнее на странице входа в магазин) 
        /// </summary>
        /// <returns> true, если находится в магазине </returns>
        public bool isSale()
        {
            //uint bb = pointIsSale1.GetPixelColor();
            //uint dd = pointIsSale2.GetPixelColor();
            return ((pointIsSale1.isColor()) && (pointIsSale2.isColor()));
        }

        /// <summary>
        /// проверяет, находится ли данное окно в самом магазине (на закладке BUY или SELL)                                       
        /// </summary>
        /// <returns> true, если находится в магазине </returns>
        public bool isSale2()
        {
            //uint bb = pointIsSale21.GetPixelColor();
            //uint dd = pointIsSale22.GetPixelColor();
            return ((pointIsSale21.isColor()) && (pointIsSale22.isColor()));
            //return botwindow.isColor2(841 - 5, 665 - 5, 7390000, 841 - 5, 668 - 5, 7390000, 4);
        }

        /// <summary>
        /// проверяет, активирован ли пет (зависит от сервера)
        /// </summary>
        /// <returns></returns>
        public bool isActivePet()
        {
            //return botwindow.isColor2(495 - 5, 310 - 5, 13200000, 496 - 5, 308 - 5, 13600000, 5);
            uint bb = pointisActivePet1.GetPixelColor();
            uint dd = pointisActivePet2.GetPixelColor();
            uint ee = pointisActivePet3.GetPixelColor();
            uint ff = pointisActivePet4.GetPixelColor();
            return ((pointisActivePet1.isColor() && pointisActivePet2.isColor()) || (pointisActivePet3.isColor() && pointisActivePet4.isColor()));
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
        /// геттер
        /// </summary>
        /// <returns></returns>
        public Town getTown()
        { return this.town; }

        /// <summary>
        /// Выгружаем окно через верхнее меню 
        /// </summary>
        public void GoToEnd()
        {
            botwindow.PressEscThreeTimes();
            Pause(1000);

            TopMenu(13);
            Pause(1000);
            pointGotoEnd.PressMouse();
            //pointGotoEnd.PressMouse();      //добавил 01-12-2016
            //botwindow.PressMouse(680, 462);   //выбираем пункт end Programm

            //пробуем окончательно убить окно
            //Pause(2000);
            //UIntPtr ddd = botwindow.getHwnd();
            //DestroyWindow(ddd);
        }

        /// <summary>
        /// метод проверяет, находится ли данное окно в городе (проверка по стойке, работает только с ружьем) 
        /// делаем проверку по двум точкам у каждого перса
        /// </summary>
        /// <returns> true, если бот находится в городе </returns>
        public bool isTown()
        {
            //bool result1 = botwindow.isColor2(24, 692, 11053000, 25, 692, 10921000, 3);
            //bool result2 = botwindow.isColor2(279, 692, 11053000, 280, 692, 10921000, 3);
            //bool result3 = botwindow.isColor2(534, 692, 11053000, 535, 692, 10921000, 3);
//            return (pointIsTown11.isColor() && pointIsTown12.isColor() && pointIsTown21.isColor() && pointIsTown22.isColor() && pointIsTown31.isColor() && pointIsTown32.isColor());  //проверка по трем персам
            return (pointIsTown11.isColor() && pointIsTown12.isColor() && pointIsTown21.isColor() && pointIsTown22.isColor());  //проверка по двум персам
        }

        /// <summary>
        /// метод проверяет, находится ли данное окно в городе (проверка по эксп. стойке с дробашом и по обычной стойке с дробашём) 
        /// делаем проверку по двум точкам у каждого перса
        /// </summary>
        /// <returns> true, если бот находится в городе </returns>
        public bool isTown_2()
        {
            //uint ff11 = pointIsTown_11.GetPixelColor();
            //uint ff12 = pointIsTown_12.GetPixelColor();
            //uint ff21 = pointIsTown_21.GetPixelColor();
            //uint ff22 = pointIsTown_22.GetPixelColor();
            //uint ff31 = pointIsTown_31.GetPixelColor();
            //uint ff32 = pointIsTown_32.GetPixelColor();

            //bool bb = (pointIsTown_11.isColor() && pointIsTown_12.isColor() && pointIsTown_21.isColor() && pointIsTown_22.isColor());  //проверка по двум персам эксп стойка
            //bool cc = (pointIsTown_11a.isColor() && pointIsTown_12a.isColor() && pointIsTown_21a.isColor() && pointIsTown_22a.isColor());  //проверка по двум персам обычная стойка стойка

            bool bb = (pointIsTown_11.isColor() && pointIsTown_12.isColor());  //проверка по одному персу эксп стойка
            bool cc = (pointIsTown_11a.isColor() && pointIsTown_12a.isColor());  //проверка по одному персу обычная стойка

//            return (pointIsTown_11.isColor() && pointIsTown_12.isColor() && pointIsTown_21.isColor() && pointIsTown_22.isColor() && pointIsTown_31.isColor() && pointIsTown_32.isColor());
            return (bb || cc); 
        }

        /// <summary>
        /// метод проверяет, переполнился ли карман (выскочило ли уже сообщение о переполнении)
        /// </summary>
        /// <returns> true, еслм карман переполнен </returns>
        public bool isBoxOverflow()
        {
            //   return botwindow.isColor2(548 - 30, 462 - 30, 7800000, 547 - 30, 458 - 30, 7600000, 5);
            //uint ff = pointisBoxOverflow1.GetPixelColor();
            //uint gg = pointisBoxOverflow2.GetPixelColor();
            return (pointisBoxOverflow1.isColor() && pointisBoxOverflow2.isColor());
        }

        /// <summary>
        /// метод проверяет, находится ли данное окно в режиме логаута, т.е. на стадии ввода логина-пароля
        /// </summary>
        /// <returns></returns>
        public bool isLogout()
        {
            //return botwindow.isColor2(121, 62, 7460000, 135, 61, 7590000, 4);
            //uint ff = pointisLogout1.GetPixelColor();
            //uint gg = pointisLogout2.GetPixelColor();
            return (pointisLogout1.isColor() && pointisLogout2.isColor());
        }

        /// <summary>
        /// проверяет, открыта ли закладка Sell в магазине 
        /// </summary>
        /// <returns> true, если закладка Sell в магазине открыта </returns>
        public bool isClickSell()
        {
            //uint bb = pointIsClickSale1.GetPixelColor();
            //uint dd = pointIsClickSale2.GetPixelColor();
            return ((pointIsClickSale1.isColor()) && (pointIsClickSale2.isColor()));
        }

        /// <summary>
        /// проверяет, призван ли пет
        /// </summary>
        /// <returns> true, если призван </returns>
        public bool isSummonPet()
        {
            //uint ff = pointisSummonPet1.GetPixelColor();
            //uint gg = pointisSummonPet2.GetPixelColor();
            //bool dfg = pointisSummonPet1.isColor();
            //bool dfg2 = pointisSummonPet2.isColor();
            return (pointisSummonPet1.isColor() && pointisSummonPet2.isColor());
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
            //   return botwindow.isColor2(61 - 5, 151 - 5, 2420000, 61 - 5, 154 - 5, 2420000, 4);
            //uint ff = pointisBarack1.GetPixelColor();
            //uint gg = pointisBarack2.GetPixelColor();
            return (pointisBarack1.isColor() && pointisBarack2.isColor()) || (pointisBarack3.isColor() && pointisBarack4.isColor());
        }

        /// <summary>
        /// метод проверяет, находится ли данное окно на работе (проверка по стойке)  две стойки
        /// </summary>
        /// <returns> true, если сейчас на рабочей карте </returns>
        public bool isWork()
        {
            //  return botwindow.isColor2(29 - 5, 697 - 5, 11051000, 30 - 5, 697 - 5, 10919000, 3);
            //uint ff_1 = pointisWork_1.GetPixelColor();
            //uint ff_2 = pointisWork_2.GetPixelColor();

            return ((pointisWork1.isColor() && pointisWork2.isColor()) || (pointisWork_1.isColor() && pointisWork_2.isColor()) || (pointisWork__1.isColor() && pointisWork__2.isColor()));  //проверка только по первому персу
        }

        /// <summary>
        /// активируем уже призванного пета
        /// </summary>
        public void ActivePet()
        {
            //botwindow.PressMouse(408, 405);  //Click Button Active Pet
            pointActivePet.PressMouse(); //Click Button Active Pet
            Pause(2500);
        }

        /// <summary>
        /// Определяет, надо ли грузить данное окно с ботом
        /// </summary>
        /// <returns> true означает, что это окно (данный бот) должно быть активно и его надо грузить </returns>
        public bool isActive()
        {
            bool result = false;
            if (getActiveWindow() == 1) result = true;
            return result;
        }

        /// <summary>
        /// геттер
        /// </summary>
        /// <returns></returns>
        public int getActiveWindow()
        { return this.activeWindow; }

        /// <summary>
        /// геттер
        /// </summary>
        /// <returns></returns>
        public String getPathClient()
        { return this.pathClient; }

        /// <summary>
        /// телепортируемся в город продажи по Alt+W (Америка)
        /// </summary>
        public void TeleportToTownAltW()
        {
            TopMenu(6, 1);
            Pause(1000);
            pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
            Pause(2000);
        }

        /// <summary>
        /// переход на нужный канал после телепорта на работу ===================================================================================================================
        /// </summary>
        public void GoToChannel()
        {
            if (botwindow.getKanal() > 1)
            {
                TopMenu(13);
                Pause(1000);
                
                pointChooseChannel.PressMouse();
                Pause(1000);
                
                pointEnterChannel.PressMouse();
                Pause(1000);
                
                pointMoveNow.PressMouse();
                Pause(15000);
            }
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// лечение персов нажатием на красную бутылку
        /// </summary>
        public void Cure()
        {
            for (int j = 1; j <= 4; j++)
            {
                pointCure1.PressMouseL();
                pointCure2.PressMouseL();
                pointCure3.PressMouseL();

                //PressMouseL(210, 700);
                //PressMouseL(210 + 255, 700);
                //PressMouseL(210 + 255 * 2, 700);
            }
            Pause(2000);
            for (int j = 1; j <= 3; j++)    
            {
                pointMana1.PressMouseL();   //жрем патроны (или то, что будет лежать на этом месте под буквой I)
                Pause(2000);
                //PressMouseL(210 + 30, 700);
            }

        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Кликаем в закладку Sell  в магазине 
        /// </summary>
        public void Bookmark_Sell()
        {
            //PressMouseL(225, 163);
            //PressMouseL(225, 163);
            //PressMouseL(225, 163);
            pointBookmarkSell.PressMouseL();
            pointBookmarkSell.PressMouseL();
            pointBookmarkSell.PressMouseL();
            Pause(1500);
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Продажа товаров в магазине вплоть до маленькой красной бутылки 
        /// </summary>
        public void SaleToTheRedBottle()
        {
            bool ff = true;
            int ii = 0;
            uint color1;
            while (ff)
            {
                color1 = GetPixelColor(142, 219);                 // проверка цвета. бутылка или нет
                if (color1 == 3360337) { ff = false; }            // Дошли до маленькой бутылки        
                else
                {
                    ii++;
                    if (ii >= 230) ff = false;     // Страховка против бесконечного цикла
                    else
                    {
                        Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 5);        //тыканье в стрелочку вверх + колесо вниз (количество продаваемого товара увеличивается на 1)
                    }
                }
            }//Конец цикла
            Pause(150);
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 4); // колесо вверх
            Pause(150);
            //            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(305 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 1); // Делаем левый клик по стрелке количества товара(уменьшаем на один)
            //            Pause(150);
            //PressMouseL(305 + 30, 190 + 30);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(305 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 1); // Делаем левый клик по стрелке количества товара(уменьшаем на один)
            //Pause(150);
            //PressMouseL(305 + 30, 190 + 30);
            pointSaleToTheRedBottle.PressMouseL();            // Делаем левый клик по стрелке количества товара(уменьшаем на один)
            pointSaleToTheRedBottle.PressMouseL();
        }

        /// <summary>
        /// определяет, анализируется ли нужный товар либо данный товар можно продавать
        /// </summary>
        /// <param name="color"> цвет полностью определяет товар, который поступает на анализ </param>
        /// <returns> true, если анализируемый товар нужный и его нельзя продавать </returns>
        public bool NeedTovarOrNot(uint color)
        {
            bool result = false;

            switch (color)                                             // Хорошая вещь или нет, сверяем по картотеке
            {
                case 394901:      // soul crystal                **
                case 3947742:     // красная бутылка 1200HP      **
                case 2634708:     // красная бутылка 2500HP      **
                case 7171437:     // devil whisper               **
                case 5933520:     // маленькая красная бутылка   **
                case 1714255:     // митридат                    **
                case 7303023:     // чугун                       **
                case 4487528:     // honey                       **
                case 1522446:     // green leaf                  **
                case 2112641:     // red leaf                    **
                case 1533304:     // yelow leaf                  **
                case 13408291:    // shiny                       **
                case 3303827:     // карта перса                 **
                case 6569293:     // warp                        **
                case 662558:      // head of Mantis              **
                case 4497887:     // Mana Stone                  **
                case 7305078:     // Ящики для джеков            **
                case 15420103:    // Бутылка хрина               **
                case 9868940:     // композитная сталь           **
                case 5334831:     // магическая сфера            **
                case 13164006:    // свекла                      **
                case 16777215:    // Wheat flour                 **
                case 13565951:    // playtoken                   **
                case 10986144:    // Hinge                       **
                case 3481651:     // Tube                        **
                case 6593716:     // Clock                       **
                case 13288135:    // Spring                      **
                case 7233629:     // Cogwheel                    **
                case 13820159:    // Family Support Token        **
                case 4222442:     // Wolf meat                   **
                case 4435935:     // Yellow ore                  **
                case 5072004:     // Bone Stick                   **
                case 3559777:     // Dragon Lether                   **
                case 1712711:     // Dragon Horn                   **
                case 6719975:     // Wild Boar Meat                   **
                case 4448154:     // Green ore                   **
                case 13865807:     // blue ore                   **
                case 4670431:     // Red ore                 **
                case 13291199:     // Diamond Ore                   **
                case 1063140:     // Stone of Philos                   **
                case 8486756:     // Ice Crystal                  **
                case 4143156:     // bulk of Coal                   **
                case 9472397:     // Steel piece                 **
//                case 7645105:     // Quartz                 **

                result = true;
                break;
            }

            return result;
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Посылаем нажатие числа 333 в окно с ботом с помощью команды PostMessage
        /// </summary>
        public void Press333()
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            UIntPtr lParam = new UIntPtr();
            UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

            for (int i = 1; i <= 3; i++)
            {
                uint dd = 0x00400001;
                lParam = (UIntPtr)dd;
                PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D3, lParam);
                Pause(150);

                dd = 0xC0400001;
                lParam = (UIntPtr)dd;
                PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D3, lParam);
                Pause(150);
            }
        }

        /// <summary>
        /// Посылаем нажатие числа 44444 в окно с ботом с помощью команды PostMessage
        /// </summary>
        public void Press44444()
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            UIntPtr lParam = new UIntPtr();
            UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

            for (int i = 1; i <= 5; i++)
            {
                uint dd = 0x00500001;
                lParam = (UIntPtr)dd;
                PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D4, lParam);
                Pause(150);

                dd = 0xC0500001;
                lParam = (UIntPtr)dd;
                PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D4, lParam);
                Pause(150);
            }
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Продажа товара после маленькой красной бутылки, до момента пока прокручивается список продажи
        /// </summary>
        public void SaleOverTheRedBottle()
        {
            bool ff = true;
            uint ss2 = 0;
            uint ss3 = 0;
            uint ss4 = 0;

            uint sss2 = 0;
            uint sss3 = 0;
            uint sss4 = 0;

            while (ff)
            {
                ss2 = GetPixelColor(149 - 5, 219 - 5);                 // проверка цвета первой точки текущего товара
                ss3 = GetPixelColor(146 - 5, 219 - 5);                 // проверка цвета третьей точки текущего товара
                ss4 = GetPixelColor(165 - 5, 214 - 5);                 // проверка цвета второй точки текущего товара
                Pause(50);
                if ((ss2 == sss2) & (ss3 == sss3) & (ss4 == sss4)) ff = false;           // если текущий цвет равен предыдущему текущему цвету, значит список не двигается и надо выходить из цикла
                else
                {
                    if (NeedTovarOrNot(ss2))   //если нужный товар
                    {
                    }
                    else // товар не нужен, значит продаем
                    {
                        pointSaleOverTheRedBottle.PressMouseL();                               //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
                        //PressMouseL(305 + 30, 190 + 30);                                      //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
                        Pause(150);
                        Press44444();
                    }
                    pointWheelDown.PressMouseWheelDown();
                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
                    Pause(200);  //пауза, чтобы ГЕ успела выполнить нажатие. Можно и увеличить     
                    sss2 = ss2;
                    sss3 = ss3;
                    sss4 = ss4;
                }
            }//Конец цикла

        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Продажа товара, когда список уже не прокручивается 
        /// </summary>
        public void SaleToEnd()
        {
            uint color1;
            int Y_tovar = 219 + 27;     //координата Y товара, у которого проверяем цвет
            for (int j = 1; j <= 11; j++)
            {
                color1 = GetPixelColor(149 - 5, Y_tovar - 5);                 // проверка цвета текущего товара
                Pause(50);
                if (NeedTovarOrNot(color1))   //если нужный товар
                {
                }
                else // товар не нужен, значит продаем
                {
                    botwindow.PressMouseL(360 - 5, Y_tovar - 5);              //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
                    Pause(150);
                    Press44444();
                }
                Y_tovar = Y_tovar + 27;   //переходим к следующей строке
            }//Конец цикла
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Кликаем в кнопку BUY  в магазине 
        /// </summary>
        public void Botton_BUY()
        {
            //PressMouseL(725, 663);
            //PressMouseL(725, 663);
            pointButtonBUY.PressMouseL();
            pointButtonBUY.PressMouseL();
            Pause(2000);
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Кликаем в кнопку Sell  в магазине 
        /// </summary>
        public void Botton_Sell()
        {
            //PressMouseL(725, 663);
            //PressMouseL(725, 663);
            pointButtonSell.PressMouseL();
            pointButtonSell.PressMouseL();
            Pause(2000);
        }

        //=============================================================================================== кандидат в абстрактный класс server
        /// <summary>
        /// Кликаем в кнопку Close в магазине
        /// </summary>
        public void Button_Close()
        {
            pointButtonClose.PressMouse();
            Pause(2000);
            //PressMouse(847, 663);
        }

        /// <summary>
        /// возвращает цвет пикселя экрана, т.е. не в конкретном окне, а на общем экране 1920х1080.            
        /// </summary>
        /// <param name="x"> x - первая координата проверяемой точки </param>
        /// <param name="y"> y - вторая координата проверяемой точки </param>
        /// <returns> цвет пикселя экрана </returns>
        public uint GetPixelColor(int x, int y)
        {
            IntPtr hwnd = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hwnd, x + botwindow.getX(), y + botwindow.getY());
            ReleaseDC(IntPtr.Zero, hwnd);

            return pixel;
        }

        /// <summary>
        /// функция проверяет, убит ли хоть один герой из пати (проверка проходит на карте)
        /// </summary>
        /// <returns></returns>
        public bool isKillHero()
        {
            //bool result = false;
            //uint ss, tt, rr = 0;
            //ss = Okruglenie(GetPixelColor(80 - 5, 636 - 5), 4);  //  проверяем точку в портрете первого героя 
            //tt = Okruglenie(GetPixelColor(335 - 5, 636 - 5), 4);  //  проверяем точку в портрете второго героя 
            //rr = Okruglenie(GetPixelColor(590 - 5, 636 - 5), 4);  //  проверяем точку в портрете третьего героя
            //if (ss == 1900000) result = true;     //если черный цвет, т.е. убит первый перс, то возвращаем true.
            //if (tt == 1900000) result = true;     //если черный цвет, т.е. убит второй перс, то возвращаем true.
            //if (rr == 1900000) result = true;     //если черный цвет, т.е. убит третий перс, то возвращаем true.
            //return result;
            return (pointisKillHero1.isColor() || pointisKillHero2.isColor() || pointisKillHero3.isColor());
        }


        public abstract void TopMenu(int numberOfThePartitionMenu);
        public abstract void TopMenu(int numberOfThePartitionMenu, int punkt);
        public abstract void runClient();
        public abstract uint colorTest();


    }
}
