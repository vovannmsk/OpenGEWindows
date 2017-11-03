using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using GEDataBot.BL;



namespace OpenGEWindows
{
    public partial class MainForm : Form
    {

        [DllImportAttribute("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern UIntPtr GetForegroundWindow();

        public static uint NumberBlueButton = 0;       //сколько раз нажали голубую(красную) кнопку
        public const int MAX_NUMBER_OF_ACCOUNTS = 20;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        
        public botWindow[] botWindowArray = new botWindow[MAX_NUMBER_OF_ACCOUNTS];
        public UIntPtr[] aa = new UIntPtr[21];   //используется в методе "Найти окна"

        public MainForm()
        {
            InitializeComponent();
        }

        //public static string KatalogMyProgram = Directory.GetCurrentDirectory() + "\\";         //                   включаем это, когда компилируем в exe-файл
        public static String KatalogMyProgram = "C:\\!! Суперпрограмма V&K\\";                    //                   включаем это, когда экспериментируем (программируем)!! Суперпрограмма V&K
        public static String DataVersion = "03-11-2017";
//        public static int numberOfAccounts = botWindow.KolvoAkk();
        public static int numberOfAccounts = KolvoAkk();

        /// <summary>
        /// выполняется перез загружкой основной формы (меню)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Программа от " + DataVersion + ".    " + numberOfAccounts + " окон";
            this.Location = new System.Drawing.Point(1315, 1080 - this.Height - 40);

            //инициализация элементов массива
            for (int i = 1; i <= numberOfAccounts; i++)
            {
                botWindow botwindow = new botWindow(i);
                botWindowArray[i] = botwindow;
            }
            for (int i = 0; i <= 20; i++)
            {
                aa[i] = (UIntPtr)0;
            }

            //начальные значения checkbox
            this.checkBox1.Checked = numberOfAccounts >= 1 ? botWindowArray[1].getNeedToChangeForMainForm() : false;
            this.checkBox2.Checked = numberOfAccounts >= 2 ? botWindowArray[2].getNeedToChangeForMainForm() : false;
            this.checkBox3.Checked = numberOfAccounts >= 3 ? botWindowArray[3].getNeedToChangeForMainForm() : false;
            this.checkBox4.Checked = numberOfAccounts >= 4 ? botWindowArray[4].getNeedToChangeForMainForm() : false;
            this.checkBox5.Checked = numberOfAccounts >= 5 ? botWindowArray[5].getNeedToChangeForMainForm() : false;
            this.checkBox6.Checked = numberOfAccounts >= 6 ? botWindowArray[6].getNeedToChangeForMainForm() : false;
            this.checkBox7.Checked = numberOfAccounts >= 7 ? botWindowArray[7].getNeedToChangeForMainForm() : false;
            this.checkBox8.Checked = numberOfAccounts >= 8 ? botWindowArray[8].getNeedToChangeForMainForm() : false;
            this.checkBox9.Checked = numberOfAccounts >= 9 ? botWindowArray[9].getNeedToChangeForMainForm() : false;
            this.checkBox10.Checked = numberOfAccounts >= 10 ? botWindowArray[10].getNeedToChangeForMainForm() : false;
            this.checkBox11.Checked = numberOfAccounts >= 11 ? botWindowArray[11].getNeedToChangeForMainForm() : false;
            this.checkBox12.Checked = numberOfAccounts >= 12 ? botWindowArray[12].getNeedToChangeForMainForm() : false;
            this.checkBox13.Checked = numberOfAccounts >= 13 ? botWindowArray[13].getNeedToChangeForMainForm() : false;
            this.checkBox14.Checked = numberOfAccounts >= 14 ? botWindowArray[14].getNeedToChangeForMainForm() : false;
            this.checkBox15.Checked = numberOfAccounts >= 15 ? botWindowArray[15].getNeedToChangeForMainForm() : false;
            this.checkBox16.Checked = numberOfAccounts >= 16 ? botWindowArray[16].getNeedToChangeForMainForm() : false;
            this.checkBox17.Checked = numberOfAccounts >= 17 ? botWindowArray[17].getNeedToChangeForMainForm() : false;
            this.checkBox18.Checked = numberOfAccounts >= 18 ? botWindowArray[18].getNeedToChangeForMainForm() : false;
            this.checkBox19.Checked = numberOfAccounts >= 19 ? botWindowArray[19].getNeedToChangeForMainForm() : false;

        }


        /// <summary>
        /// действия при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //myThreadGreen.Abort();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        #region Light Coral Button "Найти окна"

        /// <summary>
        /// найти окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)                                                                          //кнопка "найти окна"
        {
            button5.Visible = false;

            Thread myThreadCoral = new Thread(funcCoral);
            myThreadCoral.Start();

            button5.Visible = true;
        }

        /// <summary>
        /// метод задает функционал для потока, организуемого Coral кнопкой
        /// </summary>
        private void funcCoral()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                bool fff = true;
                while (fff)
                {
                    UIntPtr ddd = botWindowArray[j].FindWindow2();
                    botWindowArray[j].Pause(500);
                    if (!funcArray(ddd))
                    {
                        aa[j] = ddd;
                        fff = false;
                    }
                    botWindowArray[j].Pause(500);
                }
            }
        }


        /// <summary>
        /// возвращает true, если в массиве есть искомое число ddd
        /// </summary>
        /// <param name="ddd"></param>
        /// <returns></returns>
        private bool funcArray(UIntPtr ddd)                                                                                  
        { 
            bool fff = false;
            for (int j = 1; j <= numberOfAccounts; j++)
            { 
                if (aa[j] == ddd) fff = true;
            }

            return fff;
        }

        #endregion

        #region Белая Кнопка "Открытие окон" 

        /// <summary>
        /// ОТКРЫТИЕ ОКОН
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenWindowGE_Click(object sender, EventArgs e)                                                                  // ОТКРЫТИЕ ОКОН
        {
            buttonOpenWindowGE.Visible = false;

            for (int j = 1; j <= numberOfAccounts + 1; j++)
            {
                botWindowArray[j].getserver().runClient();
                botWindowArray[j].Pause(1600);
            }

            buttonOpenWindowGE.Visible = true;
          } // Окончание нажатия кнопки Открытия окон        кнопка белая

        /// <summary>
        /// метод задает функционал для потока, организуемого белой кнопкой
        /// </summary>
        private void funcWhite()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
//                whiteButtonArray[j].run();
                botWindowArray[j].StateReOpen();
            }
        }

        #endregion
        
        #region Оранжевая кнопка

        /// <summary>
        /// ВОССТАНОВЛЕНИЕ ОКОН                                                                                                                 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReOpenWindowGE_Click(object sender, EventArgs e)                                                        // ВОССТАНОВЛЕНИЕ ОКОН               оранжевая кнопка
        {
            buttonReOpenWindowGE.Visible = false;


//            for (int j = 1; j <= numberOfAccounts; j++)      
//            {      
                //orangeButtonArray[j].run();       
                Thread mythread = new Thread(funcOrange);
                mythread.Start();
//            }   
            
            
            buttonReOpenWindowGE.Visible = true;
         } //                                                                                                                        оранжевая кнопка

        /// <summary>
        /// метод задает функционал для потока, организуемого оранжевой кнопкой
        /// </summary>
        private void funcOrange()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                botWindowArray[j].ReOpenWindow();
                botWindowArray[j].Pause(100);
                if (botWindowArray[j].getserver().isLogout())
                {
                    botWindowArray[j].EnterLoginAndPasword();
                }
            }   
        }

        #endregion
        
        #region ЛАЙМ КНОПКА
        /// <summary>
        /// новый аккаунт бежит в кратер, сохраняет там телепорт                                                                         ЛАЙМ КНОПКА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunToCrater_Click(object sender, EventArgs e)
        {
            for (int j = 1; j <= numberOfAccounts; j++)
                botWindowArray[j].StateToCrater();

        }

        #endregion

        #region РОЗОВАЯ КНОПКА
        /// <summary>
        /// создание новых аккаунтов                                                                                                    РОЗОВАЯ КНОПКА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewAcc_Click(object sender, EventArgs e)
        {
            for (int j = 1; j <= numberOfAccounts; j++)
                botWindowArray[j].StateNewAcc();             

        }
        #endregion

        #region AQUA кнопка

        /// <summary>
        /// СУПЕР ПРОДАЖА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSuperSell_Click(object sender, EventArgs e)                                                              //        кнопка цвета морской волны (AQUA)
        {
            buttonSuperSell.Visible = false;
            Thread mythread = new Thread(funcAqua);
            mythread.Start();
            buttonSuperSell.Visible = true;
        }

        /// <summary>
        /// метод задает функционал для потока, организуемого аква кнопкой
        /// </summary>
        private void funcAqua()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
                botWindowArray[j].StateSelling();             // продаёт бота, который стоит в данный момент в магазине (через движок состояний). окно должно быть активным
        }
        
        #endregion

        #region Test Button
        /// <summary>
        /// тестовая кнопка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)                                                                                             // Тестовая кнопка
        {
            button1.Visible = false;
            //public const UInt32 KEYEVENTF_EXTENDEDKEY = 1;
            //public const UInt32 KEYEVENTF_KEYUP = 2;

            //botWindowArray[1].StateDriverRun(new StateGT03(botWindowArray[1]), new StateGT04(botWindowArray[1]));
            //System.DateTime time1 = DateTime.Now;  //текущее время
            //Class_Timer.Pause(1000);
            //Class_Timer.Pause(1000);
            //System.DateTime time2 = DateTime.Now;  //текущее время

            //System.TimeSpan PeriodMitridat = time2.Subtract(time1);   //сколько времени прошло с последнего применения митридата
            //int PeriodMitridatSeconds = PeriodMitridat.Seconds;          //сколько времени прошло с последнего применения митридата в секундах
            //MessageBox.Show(" " + PeriodMitridatSeconds);


//            color1 = Okruglenie(GetPixelColor(907 - 5, 678 - 5), 4);
  //          color2 = Okruglenie(GetPixelColor(908 - 5, 678 - 5), 4);
            // 495 - 5, 310 - 5, 13230000, 496 - 5, 308 - 5, 13620000, 4);
            //this.pointIsTown11 = new PointColor(24 + xx, 692 + yy, 11053000, 3);        //24, 692, 11053000, 25, 692, 10921000, 3);
            //this.pointIsTown12 = new PointColor(25 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown21 = new PointColor(279 + xx, 692 + yy, 11053000, 3);       //279, 692, 11053000, 280, 692, 10921000, 3);
            //this.pointIsTown22 = new PointColor(280 + xx, 692 + yy, 10921000, 3);
            //this.pointIsTown31 = new PointColor(534 + xx, 692 + yy, 11053000, 3);      //534, 692, 11053000, 535, 692, 10921000, 3);
            //this.pointIsTown32 = new PointColor(535 + xx, 692 + yy, 10921000, 3);
            //908 - 5 + xx, 707 - 5 + yy, 16440000, 4)    //проверено
            //523 - 5 + xx, 438 - 5
            // color1 = GetPixelColor(149 - 5, Y_tovar - 5);     
            //color1 = GetPixelColor(149 - 5, 516 - 5); 
            //this.pointisOpenMenuPet1 = new PointColor(475 - 5 + xx, 220 - 5 + yy, 12900000, 5);     //проверено
            //this.pointisOpenMenuPet2 = new PointColor(475 - 5 + xx, 221 - 5 + yy, 12900000, 5);     //проверено
            //this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 5); // для проверки периодической еды на месяц                                      //проверено
            //this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 5); // для проверки периодической еды на месяц

            //uint color1 = botWindowArray[1].getserver().poi
            //this.pointIsTown__11 = new PointColor(24 + xx, 692 + yy, 7631000, 3);       //точки для проверки эксп стойки с ружьем
            //this.pointIsTown__12 = new PointColor(25 + xx, 692 + yy, 16711000, 3);


            int xx, yy;
            //////uint ss, tt, rr,sss;
            xx = 5;
            yy = 5;
            uint color1;
            uint color2;
            //PointColor pointisSummonPet1 = new PointColor(149 - 5 + xx, 516 - 5 + yy, 12000000, 6);      //проверено
            //PointColor pointisSummonPet2 = new PointColor(475 - 5 + xx, 221 - 5 + yy, 12000000, 6);       //проверено
            //uint color1 = pointisSummonPet1.GetPixelColor();
            //uint color2 = pointisSummonPet2.GetPixelColor();
//            this.pointisToken1 = new PointColor(478 - 5 + xx, 92 - 5 + yy, 14400000, 5);  //проверяем открыто ли окно с токенами
  //          this.pointisToken2 = new PointColor(478 - 5 + xx, 93 - 5 + yy, 14200000, 5);
            //this.pointisWork_1 = new PointColor(29 - 5 + xx, 697 - 5 + yy, 11051000, 3);              //проверка по эксп стойке с дробашем
            //this.pointisWork_2 = new PointColor(30 - 5 + xx, 697 - 5 + yy, 10919000, 3);
            //this.pointisWork__1 = new PointColor(24 + xx, 692 + yy, 16777000, 3);              //проверка по обычной стойке с дробашем
            //this.pointisWork__2 = new PointColor(25 + xx, 692 + yy, 3560000, 3);
            PointColor pointisLogout1 = new PointColor(565 - 5 + xx, 530 - 5 + yy, 16400000, 5);       //не проверено   слово Leave Game
            PointColor pointisLogout2 = new PointColor(565 - 5 + xx, 531 - 5 + yy, 16400000, 5);       //не проверено

            PointColor pointisActivePet1 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13100000, 5);
            PointColor pointisActivePet2 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13100000, 5);

            color1 = pointisLogout1.GetPixelColor();
            color2 = pointisLogout2.GetPixelColor();

            //color1 = Win32.Okruglenie(Win32.GetPixelColor(29 - 5 + xx, 697 - 5 + yy), 0);      //стойка на работе
            //color2 = Win32.Okruglenie(Win32.GetPixelColor(30 - 5 + xx, 697 - 5 + yy), 0);      

            //uint color1 = Win32.Okruglenie(Win32.GetPixelColor(829 - 5 + xx, 186 - 5 + yy), 6);      //для проверки периодической еды на месяц

            //uint color1 = Win32.Okruglenie(Win32.GetPixelColor(478 - 5 + xx, 93 - 5 + yy), 0);     
            //uint color2 = Win32.Okruglenie(Win32.GetPixelColor(524 - 5 + xx, 438 - 5 + yy), 4);
            MessageBox.Show(" " + color1);
            MessageBox.Show(" " + color2);
            //MessageBox.Show(" " + color2);

            //botWindow botwindow = new botWindow(1);
            //botwindow.Pause(1000);
            //bool result1 = botwindow.isColor2( 24, 692, 11053000,  25, 692, 10921000, 3);
            //bool result2 = botwindow.isColor2(279, 692, 11053000, 280, 692, 10921000, 3);
            //bool result3 = botwindow.isColor2(534, 692, 11053000, 535, 692, 10921000, 3);
            //bool result4 = result1 & result2 & result3;
            //MessageBox.Show(" " + result4);

            //bool result = StandUp.isActivePet(KatalogMyProgram, 13);
            //MessageBox.Show(" " + result);

//            StandUp.GoToSaleOneWindow(KatalogMyProgram, 13);
//            StandUp.CompleteSaleOneWindow(KatalogMyProgram, 1);

            
            //StandUp.GoToEnd(KatalogMyProgram, 1);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(670, 664, 8);    // Кликаю правой кнопкой в экран
            //Class_Timer.Pause(1000);
            //StandUp.Cure(KatalogMyProgram, 1);

            //StandUp.RasstanovkaOneWindow(KatalogMyProgram, 1);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(210, 200, 8); 

            // botWindow aa = new botWindow(1); 
            //for (int i = 1; i <= 13;i++ )
            //{
                //aa.TopMenu(6,2);
                //Class_Timer.Pause(500);
                //aa.TopMenu(6, 3);
                //Class_Timer.Pause(500);
                //            }

            //int x, y;
            ////uint ss, tt, rr,sss;
            //x = 5;
            //y = 5;

//            sss = Win32.GetPixelColor(149 - 5 + x, 543 - 27 - 5 + y);
////            sss = Win32.GetPixelColor(149 - 5 + kanal, 219 - 5 + y);                 // проверка цвета текущего товара
//            MessageBox.Show(" " + sss);
            //uint ss2 = Win32.GetPixelColor(149 - 5 + x, 219 + 11 * 27 - 5 + y);
            //MessageBox.Show(" " + ss2);

            //color1 = Win32.Okruglenie(Win32.GetPixelColor(1725 - 800 + kanal, 775 - 80 + y), 5);  //  проверяем точку в слове Ver
            //parametr = Win32.Okruglenie(Win32.GetPixelColor(1726 - 800 + kanal, 775 - 80 + y), 5);  //  проверяем еще одну точку в слове Ver

            ////          hwndActiveWindow = Win32.Okruglenie(Win32.GetPixelColor(590 - 5 + kanal, 636 - 5 + y), 4);  //  проверяем точку в портрете третьего героя 590 636

            //MessageBox.Show(" " + color1);
            //MessageBox.Show(" " + parametr);
 //           MessageBox.Show(" " + hwndActiveWindow);

            ////result = isColor2(KatalogMyProgram, Number_Window, 495 - 5, 310 - 5, 7920000, 501 - 5, 310 - 5, 7920000, 4);
            //// result = isColor2(KatalogMyProgram, Number_Window, 974 - 875, 152 - 5, 12630000, 974 - 875, 155 - 5, 13620000, 4);
            //color1 = Win32.Okruglenie(Win32.GetPixelColor(974 - 875 + kanal, 152 - 5 + y), 4);  //  
            //MessageBox.Show(" " + color1);
            //parametr = Win32.Okruglenie(Win32.GetPixelColor(974 - 875 + kanal, 155 - 5 + y), 4);  //  
            //MessageBox.Show(" " + parametr);

            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(150 + kanal, 150 + y, 8);
            //Class_Timer.Pause(200);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(150 + kanal, 150 + y, 8);
            //Class_Timer.Pause(200);

            //Click_Mouse_and_Keyboard.ClickKey(0x50);
            //Class_Timer.Pause(150);
            //Click_Mouse_and_Keyboard.ClickKey(0x50);

            // bool aa = Class_Timer.PauseMinute(2);

            //color1 = Win32.Okruglenie(Win32.GetPixelColor(548 - 30 + kanal, 462 - 30 + y), 4);  //  проверяем точку в окошке с переполнением                     
            //parametr = Win32.Okruglenie(Win32.GetPixelColor(547 - 30 + kanal, 458 - 30 + y), 4);  //  проверяем еще одну точку в окошке с переполнением

            //StandUp.CloseReklama("C:\\Europa\\", 1);
            //StandUp.Click_Trader(KatalogMyProgram, 1);
            //Class_Timer.Pause(2500);
            //TextSend.SendText2(1);        
            //StandUp.Click_Trader_Map("C:\\Europa\\", 1);
            //Click_Mouse_and_Keyboard.ClickOneKey(0x17);                    
            
            ///color1 = Win32.Okruglenie(Win32.GetPixelColor(1178 - 875 + kanal, 679 - 5 + y), 4);  //  
            //parametr = Win32.Okruglenie(Win32.GetPixelColor(1179 - 875 + kanal, 679 - 5 + y), 4);  //  


            //color1 = Win32.Okruglenie(Win32.GetPixelColor(735 - 5 + kanal, 663 - 5 + y), 4);  //  
            //parametr = Win32.Okruglenie(Win32.GetPixelColor(735 - 5 + kanal, 664 - 5 + y), 4);  //  

            //StandUp.OpenMap(KatalogMyProgram, 1);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(50 + kanal, 50 + y, 8);
            //Class_Timer.Pause(200);
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(50 + kanal, 50 + y, 8);
            //Class_Timer.Pause(200);
            //Class_Timer.Pause(2500);
            //for (int j = 1; j <= 10; j++)
            //{
            //    Click_Mouse_and_Keyboard.ClickOneKey(0x17);
            //    Click_Mouse_and_Keyboard.ClickOneKey((ushort)j);
            //    Class_Timer.Pause(200);
            //    //Click_Mouse_and_Keyboard.UnClickKey((ushort)j);
            //    //Class_Timer.Pause(200);
            //x1 = 1267 - 875 + kanal;
            //y1 = 480 - 5 + y;

            
            //}x1 = 595 - 5 + kanal;                    y1 = 426 - 5 + y
            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(50+kanal, 50+y, 8);
            //Class_Timer.Pause(1500);

            //StandUp.GoSaleOneWindow(KatalogMyProgram, 1);

            //int kanal, y;
            //IntPtr HWND;
            ////uint color1;
            //for (int j = 1; j <= 15; j++)
            //{
            //    kanal = StandUp.Koord_X(KatalogMyProgram, j);
            //    y = StandUp.Koord_Y(KatalogMyProgram, j);
            //    HWND = StandUp.Hwnd_in_file(KatalogMyProgram, j);
            //    ShowWindow(HWND, 9);// Разворачивает окно если свернуто
            //    SetWindowPos(HWND, 0, kanal, y, 1024, 700, 0x0001); //Перемещает в заданные координаты
            //    SetForegroundWindow(HWND); // Перемещает окно в верхний список Z порядка     
            //    BringWindowToTop(HWND); // Делает окно активным 
     

            //    color1 = Win32.Okruglenie(Win32.GetPixelColor(29 - 5 + kanal, 697 - 5 + y), 3);  //  
            //    MessageBox.Show(" " + color1);
            //    parametr = Win32.Okruglenie(Win32.GetPixelColor(30 - 5 + kanal, 697 - 5 + y), 3);  //  
            //    MessageBox.Show(" " + parametr);
                
            //}


            //color1 = Win32.GetPixelColor(1369-850 + kanal, 463 - 30 + y);  //  361 + xx, 65 + yy
            //MessageBox.Show(" " + color1);

            //color1 = Win32.GetPixelColor(1370 -850+ kanal, 464 -30+y);  //  
            //MessageBox.Show(" " + color1);\
            //kanal = 825;
            //y = 55;

            //color1 = Win32.GetPixelColor(1344 - 825 + kanal, 488 - 55 + y);  //  361 + xx, 65 + yy
            //MessageBox.Show(" " + color1);

            //color1 = Win32.GetPixelColor(1345 - 825 + kanal, 489 - 55 + y);  //  
            //MessageBox.Show(" " + color1);

  //          color1 = Win32.GetPixelColor(1369 - 850 + kanal, 463 - 30 + y);  //  361 + xx, 65 + yy
  //          MessageBox.Show(" " + color1);

  //          color1 = Win32.GetPixelColor(1370 - 850 + kanal, 464 - 30 + y);  //  
 //           MessageBox.Show(" " + color1);


//            color1 = Win32.GetPixelColor(701 + koorX, 65 + koorY);  //  
//            MessageBox.Show(" " + color1);

           //
//            Color newColor = Win32.GetColorHWND(aa, 60, 60); 
//            MessageBox.Show("A=" + newColor.A + "R=" + newColor.R + "G=" + newColor.G + "B=" + newColor.B);



      //      uint Tek_Color = ColorOfPixel.getColor(514, 428, HWND); // Считывает цвет кнопки ОК
            button1.Visible = true;
        }

        #endregion

        #region Green button

        /// <summary>
        /// проверка проблем у ботов и исправление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_StandUp_Click(object sender, EventArgs e)         
        {
            button_StandUp.Visible = false;
            Thread myThreadGreen = new Thread(funcGreen);
            myThreadGreen.Start();

            button_StandUp.Visible = true;
        }

        /// <summary>
        /// метод задает функционал для потока, организуемого аква кнопкой
        /// </summary>
        private void funcGreen()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                botWindowArray[j].checkForProblems();
            }
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                botWindowArray[j].ReOpenWindow();
            }   
        }

        #endregion

        #region Blue button

        /// <summary>
        /// Процедура периодически (раз в минуту) запускает проверку (зеленую кнопку)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWarning_Click(object sender, EventArgs e)                                                //      Голубая кнопка
        {
            buttonWarning.Visible = false;

           NumberBlueButton++;
           if ((NumberBlueButton % 2) == 1)
           {
               this.buttonWarning.Text = "ВКЛЮЧЕН АВТОРЕЖИМ !!!!!!!!!!!!";
               this.buttonWarning.BackColor = Color.OrangeRed;

               // добавлено 08-09-2016
               if (MainForm.NumberBlueButton == 1)    
               {
                   myTimer.Tick += new EventHandler(TimerEventProcessor);
                   myTimer.Interval = 60000;
                   myTimer.Start();
               }
               else myTimer.Start();
               // коенц добавленного 08-09-2016
           }
           else
           {
               this.buttonWarning.Text = "АВТОРЕЖИМ ВЫКЛЮЧЕН";
               this.buttonWarning.BackColor = Color.SkyBlue;
               // добавлено 08-09-2016
               myTimer.Stop();
               // конец добавленного 08-09-2016
           }
            buttonWarning.Visible = true;
        }

        /// <summary>
        /// именно в этом методе организуется поток для выполнения функционала синей кнопки
        /// </summary>
        /// <param name="myObject"> не имею понятия, что это </param>
        /// <param name="myEventArgs"> не имею понятия, что это </param>
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)                                    //ВАЖНЫЙ МЕТОД (ПРОДОЛЖЕНИЕ ГОЛУБОЙ КНОПКИ) (используется)
        {
            myTimer.Stop();

            funcGreen();

            myTimer.Enabled = true;
        }

        #endregion

        #region Magenta button

        /// <summary>
        /// Фиолетовая кнопка                                                                                                                                   Фиолетовая кнопка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)            
        {
            button4.Visible = false;

            // переводим чекбоксы в массив
            CheckBox[] checkboxs = { this.checkBox1, this.checkBox2, this.checkBox3, this.checkBox4, this.checkBox5, this.checkBox6, this.checkBox7, this.checkBox8, this.checkBox9,
                                     this.checkBox10, this.checkBox11, this.checkBox12, this.checkBox13, this.checkBox14, this.checkBox15, this.checkBox16, this.checkBox17, this.checkBox18, this.checkBox19};

            // открываем окно с торговцем
            botWindow dealer = new botWindow(20);   // окно дилера
            dealer.StateDriverDealerRun(new StateCV01(dealer, dealer), new StateCV02(dealer, dealer));    //открывается окно с торговцем

            //для всех окон ботов
            for (int j = 0; j < numberOfAccounts; j++)
            {
                if (checkboxs[j].Checked)
                    botWindowArray[j+1].StateDriverDealerRun(new StateCV02(botWindowArray[j+1], dealer), new StateCV07(botWindowArray[j+1], dealer));  //запускаем движок состояний с 2 по 7 (передача песо с помеченных окон)
            }

            // обновляем чекбоксы
            for (int j = 0; j < numberOfAccounts; j++)
            {  checkboxs[j].Checked = numberOfAccounts >= 1 ? botWindowArray[j+1].getNeedToChangeForMainForm() : false;   }

            button4.Visible = true;
        }

        #endregion

        #region New white button (Sale)
        /// <summary>
        /// метод не работает, т.к. посылает на продажу не то окно. Он посылает на продажу первое же окно, в котором видны все стойки у персов. Если количество окон меньше или равно 12, то норм работает
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGotoTradeTest_Click(object sender, EventArgs e)                                                     //новая белая кнопка (продажа). 
        {
            buttonGotoTradeTest.Visible = false;

            //ServerTest a = new ServerTest();
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                GotoTradeTest gototradetest = new GotoTradeTest(botWindowArray[j]);
                gototradetest.gotoTradeTestDrive();
            }

            buttonGotoTradeTest.Visible = true;

        }
        #endregion

        #region checkbox
        // =================================== Чекбоксы ===================================================================================
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int i = 1;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox1.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            int i = 2;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox2.Checked);

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            int i = 3;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox3.Checked);

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            int i = 4;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox4.Checked);

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            int i = 5;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox5.Checked);

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            int i = 6;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox6.Checked);

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            int i = 7;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox7.Checked);

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            int i = 8;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox8.Checked);

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            int i = 9;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox9.Checked);

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            int i = 10;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox10.Checked);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            int i = 11;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox11.Checked);

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            int i = 12;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox12.Checked);

        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            int i = 13;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox13.Checked);

        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            int i = 14;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox14.Checked);

        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            int i = 15;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox15.Checked);

        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            int i = 16;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox16.Checked);

        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            int i = 17;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox17.Checked);

        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            int i = 18;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox18.Checked);

        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            int i = 19;
            if (numberOfAccounts >= i) botWindowArray[i].setNeedToChangeForMainForm(this.checkBox19.Checked);
        }

        #endregion

        #region неиспользуемые кнопки
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //GotoTradeTest gototradetest = new GotoTradeTest();
            //gototradetest.gotoWorkTestDrive();
        }

        /// <summary>
        /// Aion  прокачка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Win32.Pause(2000);
            //Win32.PressMouseL(1000,500);
            //Win32.Pause(1000);
            //Win32.PressMouseL(1100, 600); 
            //Win32.Pause(1000);
            //Win32.PressMouseL(1200, 600); 
            //Win32.Pause(1000);
            //Win32.PressMouseL(1200, 600); 
            //Win32.Pause(1000);
            //Win32.PressMouseL(1100, 600);
            //SendKeys.SendWait("1");
            TextSend.SendText2(0x16);  //1
            //Win32.Pause(1000);
            //SendKeys.SendWait("1");
            //Win32.Pause(1000);
            //SendKeys.SendWait("3");
        }

        #endregion

        /// <summary>
        /// возвращаеи количество аккаунтов
        /// </summary>
        /// <returns>кол-во акков всего</returns>
        public static int KolvoAkk()
        { 
            int dd = int.Parse(File.ReadAllText(KatalogMyProgram + "\\Аккаунтов всего.txt"));
            return dd;
        }


        #region Silver button
        private void findWindowSing_Click(object sender, EventArgs e)
        {
            findWindowSing.Visible = false;

            Thread myThreadSilver = new Thread(funcSilver);
            myThreadSilver.Start();

            findWindowSing.Visible = true;
        }

        /// <summary>
        /// метод задает функционал для потока, организуемого Silver кнопкой
        /// </summary>
        private void funcSilver()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                bool fff = true;
                while (fff)
                {
                    UIntPtr ddd = botWindowArray[j].FindWindow3();
                    botWindowArray[j].Pause(500);
                    if (!funcArray(ddd))
                    {
                        aa[j] = ddd;
                        fff = false;
                    }
                    botWindowArray[j].Pause(500);
                }
            }
        }

        #endregion



    }// END class MainForm : Form
}// END namespace OpenGEWindows




//        //=================================================== PostMessage ===============================================================
//        //===============================================================================================================================

//        private void button2_Click(object sender, EventArgs e)
//        {
//            button2.Visible = false;
////            BOOL WINAPI PostMessage(
////  _In_opt_ HWND   hWnd,
////  _In_     UINT   Msg,
////  _In_     WPARAM wParam,
////  _In_     LPARAM lParam
////);
//            //const int BM_SETSTATE = 243;
//            //const int WM_LBUTTONDOWN = 513;
//            //const int WM_LBUTTONUP = 514;
//            const int WM_KEYDOWN = 0x0100;
//            //const int WM_CHAR = 0x0102;
//            const int WM_KEYUP = 0x0101;
//            //const int WM_SETFOCUS = 7;
//            //const int WM_SYSCOMMAND = 274;
//            //const int SC_MINIMIZE = 32;


//            IntPtr VK_NUMPAD2 = (IntPtr)0x62;
////            IntPtr wParam = new IntPtr();
//            UIntPtr lParam = new UIntPtr();


//            //SendMessage(HWND, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);

//            //wParam = VK_NUMPAD2;
//            //lParam = (IntPtr)0x00500001;
//            //PostMessage(HWND, WM_KEYDOWN, (IntPtr)0x62, (IntPtr)0x00500001);

//            ////wParam = (IntPtr)0x050;
//            ////lParam = (IntPtr)0x00500001;
//            ////PostMessage(HWND, WM_CHAR, wParam, lParam);
//            //wParam = VK_NUMPAD2;


//            Class_Timer.Pause(3000);

//            ////////// 1 /////////////////////////
//            //UIntPtr HWND = (UIntPtr)0x151B0372;
//            UIntPtr HWND2 = (UIntPtr)1769954;
//            UIntPtr HWND = FindWindowEx(HWND2, UIntPtr.Zero , "Edit", "");
//            //SendMessage(HWND, WM_SETFOCUS, UIntPtr.Zero, UIntPtr.Zero);

//            uint dd = 0x00500001;
//            lParam = (UIntPtr)dd;
//            PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D2, lParam);

//            Class_Timer.Pause(150);

//            dd = 0xC0500001;
//            lParam = (UIntPtr)dd;
//            PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D2, lParam);

//            ////////// 3 /////////////////////////
//            //UIntPtr HWND = (UIntPtr)853492;


//            //uint dd = 0x00170001;
//            //lParam = (UIntPtr)dd;

//            //PostMessage(HWND, WM_CHAR, (UIntPtr)Keys.I, lParam);
//            //Class_Timer.Pause(150);

//            //dd = 0xC0170001;
//            //lParam = (UIntPtr)dd;
//            //PostMessage(HWND, WM_CHAR, (UIntPtr)Keys.I, lParam);


//            ////////////////6//////////////////////////
//            //SendKeys.SendWait("i");                    //идёт неправильный сигнал в программу, но что-то идёт

//            ////////// 4 /////////////////////////
//            //Click_Mouse_and_Keyboard.ClickKey(0x50);
//            //Class_Timer.Pause(150);
//            //Click_Mouse_and_Keyboard.UnClickKey(0x50);

//            ////////// 5 /////////////////////////                // в прогу(ге) ничего не идёт
//            //UIntPtr HWND = (UIntPtr)853492;
//            //SendMessage(HWND, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
//            //Click_Mouse_and_Keyboard.GenerateKey(1,true);



//    /////////2/////////

//    //Class_Timer.Pause(2500);
//    //StandUp.PressKey(Keys.I);
//    //Class_Timer.Pause(500);
//    //StandUp.PressKey(Keys.K);



//    //PostMessage(HWND, WM_KEYUP, (IntPtr)0x72, (IntPtr)0xC03D0001);

//    button2.Visible = true;
//}

