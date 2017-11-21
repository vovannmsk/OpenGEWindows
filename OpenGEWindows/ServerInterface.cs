using System;
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

        protected const int WIDHT_WINDOW = 1024;
        protected const int HIGHT_WINDOW = 700;

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

        protected iPointColor pointIsTown_RifleFirstDot1;   //проверка по обычному ружью
        protected iPointColor pointIsTown_RifleFirstDot2;
        protected iPointColor pointIsTown_RifleSecondDot1;
        protected iPointColor pointIsTown_RifleSecondDot2;
        protected iPointColor pointIsTown_RifleThirdDot1;
        protected iPointColor pointIsTown_RifleThirdDot2;

        protected iPointColor pointIsTown_ExpRifleFirstDot1;   //проверка по эксп. ружью (флинт)
        protected iPointColor pointIsTown_ExpRifleFirstDot2;
        protected iPointColor pointIsTown_ExpRifleSecondDot1;
        protected iPointColor pointIsTown_ExpRifleSecondDot2;
        protected iPointColor pointIsTown_ExpRifleThirdDot1;
        protected iPointColor pointIsTown_ExpRifleThirdDot2;

        protected iPointColor pointIsTown_DrobFirstDot1;      //проверка по обычному дробовику
        protected iPointColor pointIsTown_DrobFirstDot2;
        protected iPointColor pointIsTown_DrobSecondDot1;
        protected iPointColor pointIsTown_DrobSecondDot2;
        protected iPointColor pointIsTown_DrobThirdDot1;
        protected iPointColor pointIsTown_DrobThirdDot2;

        protected iPointColor pointIsTown_VetDrobFirstDot1;    //проверка по вет. дробовику
        protected iPointColor pointIsTown_VetDrobFirstDot2;
        protected iPointColor pointIsTown_VetDrobSecondDot1;
        protected iPointColor pointIsTown_VetDrobSecondDot2;
        protected iPointColor pointIsTown_VetDrobThirdDot1;
        protected iPointColor pointIsTown_VetDrobThirdDot2;

        protected iPointColor pointIsTown_ExpDrobFirstDot1;    //проверка по эксп. дробовику
        protected iPointColor pointIsTown_ExpDrobFirstDot2;
        protected iPointColor pointIsTown_ExpDrobSecondDot1;
        protected iPointColor pointIsTown_ExpDrobSecondDot2;
        protected iPointColor pointIsTown_ExpDrobThirdDot1;
        protected iPointColor pointIsTown_ExpDrobThirdDot2;

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

        protected iPointColor pointisWork_RifleDot1;          //проверка стойки с ружьем (проверяются две точки )
        protected iPointColor pointisWork_RifleDot2;
        protected iPointColor pointisWork_DrobDot1;           //проверка стойки с обычным дробашом (проверяются две точки )
        protected iPointColor pointisWork_DrobDot2;
        protected iPointColor pointisWork_VetDrobDot1;        //проверка стойки с вет дробашом (проверяются две точки )
        protected iPointColor pointisWork_VetDrobDot2;
        protected iPointColor pointisWork_ExpDrobDot1;        //проверка стойки с эксп дробашом (проверяются две точки )
        protected iPointColor pointisWork_ExpDrobDot2;


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
        protected Point pointPetBegin;  //для перетаскивания пета
        protected Point pointPetEnd;


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

        protected iPointColor pointConnect;


        protected int sdvigY;
        protected String pathClient;
//        protected int activeWindow;

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

        #region методы для работы кнопок по созданию новых ботов и переходу в кратер

        #region методы для перемещения ботов в кратер
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

#endregion

        #region методы для создания новы ботов

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

            pointPetBegin.Drag(pointPetEnd);            // берем коробку с кокошкой и переносим в куб для вылупления у петэксперта
            //botwindow.MouseMoveAndDrop(800-5, 220-5, 520-5, 330-5);            // берем коробку с кокошкой и переносим в куб для вылупления у петэксперта
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
                ButtonOkDialog.PressMouse();           // Нажимаем на Ok в диалоге
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
                ButtonOkDialog.PressMouse();           // Нажимаем на Ok в диалоге
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
            Pause(2000);

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
//            pointRunNunies.PressMouseL();   // Нажимаем кнопку вызова списка групп
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

        #endregion

        #region Методы для управления ботами

        /// <summary>
        /// открывает новое окно бота (т.е. переводит из состояния "нет окна" в состояние "логаут")
        /// </summary>
        /// <returns> hwnd окна </returns>
        public void OpenWindow()
        {
            runClient();
            FindWindowGE();
        }


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
            uint bb = pointisOpenMenuPet1.GetPixelColor();
            uint dd = pointisOpenMenuPet2.GetPixelColor();
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
            //uint bb = pointisActivePet1.GetPixelColor();
            //uint dd = pointisActivePet2.GetPixelColor();
            //MessageBox.Show(" " + bb);
            //MessageBox.Show(" " + dd);
            //uint ee = pointisActivePet3.GetPixelColor();
            //uint ff = pointisActivePet4.GetPixelColor();
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
        /// метод проверяет, находится ли данное окно в городе (проверка по стойкам с ружьем) 
        /// делаем проверку по двум точкам у каждого перса
        /// </summary>
        /// <returns> true, если бот находится в городе </returns>
        public bool isTown()
        {
            //проверка по двум персам обычная стойка
            bool result = (pointIsTown_RifleFirstDot1.isColor() && pointIsTown_RifleFirstDot2.isColor() && pointIsTown_RifleSecondDot1.isColor() && pointIsTown_RifleSecondDot2.isColor());
            //проверка по двум персам эксп стойка
            bool resultExp = (pointIsTown_ExpRifleFirstDot1.isColor() && pointIsTown_ExpRifleFirstDot2.isColor() && pointIsTown_ExpRifleSecondDot1.isColor() && pointIsTown_ExpRifleSecondDot2.isColor());  

            return (result || resultExp);  
        }

        /// <summary>
        /// метод проверяет, находится ли данное окно в городе (проверка по стойкам с дробовиком) 
        /// делаем проверку по двум точкам у каждого перса
        /// </summary>
        /// <returns> true, если бот находится в городе </returns>
        public bool isTown_2()
        {

            bool result = (pointIsTown_DrobFirstDot1.isColor() && pointIsTown_DrobFirstDot2.isColor());           //проверка по первому персу обычная стойка
            bool resultVet = (pointIsTown_VetDrobFirstDot1.isColor() && pointIsTown_VetDrobFirstDot2.isColor());  //проверка по первому персу вет стойка
            bool resultExp = (pointIsTown_ExpDrobFirstDot1.isColor() && pointIsTown_ExpDrobFirstDot2.isColor());  //проверка по первому персу эксп стойка
            
            return ( result || resultVet || resultExp ); 
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
            bool resultRifle = (pointisWork_RifleDot1.isColor() && pointisWork_RifleDot2.isColor());
            bool resultDrob = (pointisWork_DrobDot1.isColor() && pointisWork_DrobDot2.isColor());
            bool resultVetDrob = (pointisWork_VetDrobDot1.isColor() && pointisWork_VetDrobDot2.isColor());
            bool resultExpDrob = (pointisWork_ExpDrobDot1.isColor() && pointisWork_ExpDrobDot2.isColor());

            return (resultRifle || resultDrob || resultVetDrob || resultExpDrob);  //проверка только по первому персу
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
        /// геттер
        /// </summary>
        /// <returns></returns>
        public String getPathClient()
        { return this.pathClient; }

        ///// <summary>
        ///// телепортируемся в город продажи по Alt+W (Америка)
        ///// </summary>
        //public void TeleportToTownAltW()
        //{
        //    TopMenu(6, 1);
        //    Pause(1000);
        //    pointTeleportToTownAltW.PressMouse();           //было два нажатия левой, решил попробовать RRL
        //    Pause(2000);
        //}

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

        /// <summary>
        /// проверяем, является ли товар в первой строке магазина маленькой красной бутылкой
        /// </summary>
        /// <param name="numberOfString">номер строки, в которой проверяем товар</param>
        /// <returns> true, если в первой строке маленькая красная бутылка </returns>
        public bool isRedBottle(int numberOfString)
        {
            PointColor pointFirstString = new PointColor(147 - 5 + xx, 224 - 5 + yy + (numberOfString - 1) * 27, 3360337, 0);
            return pointFirstString.isColor();
        }

        /// <summary>
        /// добавляем товар из указанной строки в корзину 
        /// </summary>
        /// <param name="numberOfString">номер строки</param>
        public void AddToCart(int numberOfString)
        {
            Point pointAddProduct = new Point(380 - 5 + botwindow.getX(), 220 - 5 + (numberOfString - 1) * 27 + botwindow.getY());
            pointAddProduct.PressMouseL();
            pointAddProduct.PressMouseWheelDown();
        }

          
        /// <summary>
        /// определяет, анализируется ли нужный товар либо данный товар можно продавать
        /// </summary>
        /// <param name="color"> цвет полностью определяет товар, который поступает на анализ </param>
        /// <returns> true, если анализируемый товар нужный и его нельзя продавать </returns>
        public bool NeedToSellProduct(uint color)
        {
            bool result = true;

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
                case 13865807:    // blue ore                   **
                case 4670431:     // Red ore                 **
                case 13291199:    // Diamond Ore                   **
                case 1063140:     // Stone of Philos                   **
                case 8486756:     // Ice Crystal                  **
                case 4143156:     // bulk of Coal                   **
                case 9472397:     // Steel piece                 **
                case 7187897:     // Mustang ore
                case 1381654:     // стрелы эксп
                case 11258069:     // пули эксп
                case 2569782:     // дробь эксп
                case 5137276:     // сундук деревянный как у сфер древней звезды

//              case 7645105:     // Quartz                 **
                result = false;
                break;
            }

            return result;
        }

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


        /// <summary>
        /// добавляем товар из указанной строки в корзину 
        /// </summary>
        /// <param name="numberOfString">номер строки</param>
        public void GoToNextproduct(int numberOfString)
        {
            Point pointAddProduct = new Point(380 - 5 + botwindow.getX(), 225 - 5 + (numberOfString - 1) * 27 + botwindow.getY());
            pointAddProduct.PressMouseWheelDown();   //прокручиваем список

        }

        /// <summary>
        /// добавляем товар из указанной строки в корзину 
        /// </summary>
        /// <param name="numberOfString">номер строки</param>
        public void AddToCartLotProduct(int numberOfString)
        {
            Point pointAddProduct = new Point(360 - 5 + botwindow.getX(), 220 - 5 + (numberOfString - 1) * 27 + botwindow.getY());  //305 + 30, 190 + 30)
            pointAddProduct.PressMouseL();  //тыкаем в строчку с товаром
            Pause(150);
            Press44444();                   // пишем 444444 , чтобы максимальное количество данного товара попало в корзину 
            pointAddProduct.PressMouseWheelDown();   //прокручиваем список
        }

        /// <summary>
        /// Продажа товаров в магазине вплоть до маленькой красной бутылки 
        /// </summary>
        public void SaleToTheRedBottle()
        {
            uint count = 0;
            while (!isRedBottle(1))
            {
                AddToCart(1);
                count++;
                if (count > 220) break;   // защита от бесконечного цикла
            }
        }

        /// <summary>
        /// Продажа товара после маленькой красной бутылки, до момента пока прокручивается список продажи
        /// </summary>
        public void SaleOverTheRedBottle()
        {
            Product previousProduct;
            Product currentProduct;

            currentProduct = new Product(xx, yy, 1);  //создаем структуру "текущий товар" из трёх точек, которые мы берем у товара в первой строке магазина

            do
            {
                previousProduct = currentProduct;
                if (NeedToSellProduct(currentProduct.color1))
                    AddToCartLotProduct(1); 
                else
                    GoToNextproduct(1);

                Pause(200);  //пауза, чтобы ГЕ успела выполнить нажатие. Можно и увеличить     
                currentProduct = new Product(xx, yy, 1);
            } while (!currentProduct.EqualProduct(previousProduct));          //идет проверка по трем точкам
        }

        /// <summary>
        /// Продажа товара, когда список уже не прокручивается 
        /// </summary>
        public void SaleToEnd()
        {
            iPointColor pointCurrentProduct;
            for (int j = 2; j <= 12; j++)
            {
                pointCurrentProduct = new PointColor(149 - 5 + xx, 219 - 5 + yy + (j - 1) * 27, 3360337, 0);   //проверяем цвет текущего продукта
                Pause(50);
                if (NeedToSellProduct(pointCurrentProduct.GetPixelColor()))       //если нужно продать товар
                    AddToCartLotProduct(j);                                       //добавляем в корзину весь товар в строке j
            }
        }

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

        /// <summary>
        /// проверяем, есть ли ошибки при нажатии кнопки Connect
        /// </summary>
        /// <returns></returns>
        public bool isPointConnect()
        {
            return pointConnect.isColor();
        }

        #endregion


        public abstract void TopMenu(int numberOfThePartitionMenu);
        public abstract void TopMenu(int numberOfThePartitionMenu, int punkt);
        public abstract void runClient();
        public abstract void TeleportToTownAltW();
        public abstract void OrangeButton();
        public abstract bool isActive();
//        public abstract void OpenWindow();
        public abstract UIntPtr FindWindowGE();

        //        public abstract uint colorTest();

    }
}
