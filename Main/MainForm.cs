using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using States;

namespace Main
{
    public partial class MainForm : Form
    {
        public static uint NumberBlueButton = 0;       //сколько раз нажали голубую(красную) кнопку
        public const int MAX_NUMBER_OF_ACCOUNTS = 20;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        
        public UIntPtr[] arrayOfHwnd = new UIntPtr[21];   //используется в методе "Найти окна"

        public MainForm()
        {
            InitializeComponent();
        }

        //public static string KatalogMyProgram = Directory.GetCurrentDirectory() + "\\";         //                   включаем это, когда компилируем в exe-файл
        public static String KatalogMyProgram = "C:\\!! Суперпрограмма V&K\\";                    //                   включаем это, когда экспериментируем (программируем)!! Суперпрограмма V&K
        public static String DataVersion = "20-11-2017";
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

            for (int i = 0; i <= 20; i++)
            {
                arrayOfHwnd[i] = (UIntPtr)0;
            }

        }


        /// <summary>
        /// действия при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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
            for (int j = 1; j <= numberOfAccounts; j++)                //на европе и америке может быть только одно окно
            {
                Check check = new Check(j);

                if (check.isActive())
                {
                    UIntPtr hwnd = check.FindWindow();
                    if (hwnd == (UIntPtr)0)  MessageBox.Show("Не найдено окно ГЕ с номером " + j); 
                    check.Pause(500);

                    //bool result = true;
                    //while (result)
                    //{
                    //    UIntPtr hwnd = check.FindWindow();
                    //    check.Pause(500);
                        
                    //    if (Array.IndexOf(arrayOfHwnd,hwnd) == -1)     //если нет в массиве такого hwnd
                    //    {
                    //        arrayOfHwnd[j] = hwnd;           //добавляем в массив
                    //        result = false;
                    //    }
                    //}
                }
            }
        }

        #endregion

        #region Silver button Найти окна сингапур
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
            int begin = BeginSing();
            for (int j = begin; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                {
                    check.FindWindowSing();
                }
            }
        }

        #endregion

        #region Оранжевая кнопка (выравнивание окон)

        /// <summary>
        /// ВОССТАНОВЛЕНИЕ ОКОН                                                                                                                 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReOpenWindowGE_Click(object sender, EventArgs e)                                                        
        {
            buttonReOpenWindowGE.Visible = false;

            Thread mythread = new Thread(funcOrange);
            mythread.Start();
            
            buttonReOpenWindowGE.Visible = true;
         }

        /// <summary>
        /// метод задает функционал для потока, организуемого оранжевой кнопкой
        /// </summary>
        private void funcOrange()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                    check.OrangeButton();
            }   
        }

        #endregion
        
        #region ЛАЙМ КНОПКА (бежим до кратера)

        /// <summary>
        /// новый аккаунт бежит в кратер, сохраняет там телепорт                                                                         ЛАЙМ КНОПКА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunToCrater_Click(object sender, EventArgs e)
        {
            RunToCrater.Visible = false;
            Thread myThreadLime = new Thread(funcLime);
            myThreadLime.Start();

            RunToCrater.Visible = true;

        }

        /// <summary>
        /// метод задает функционал для потока, организуемого лайм кнопкой
        /// </summary>
        private void funcLime()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
//            for (int j = 2; j <= 2; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                {
                    DriversOfState driver = new DriversOfState(j);
                    driver.StateToCrater();
                }
            }
        }


        #endregion

        #region РОЗОВАЯ КНОПКА (новые аккаунты)

        /// <summary>
        /// создание новых аккаунтов (создание потока)              
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewAcc_Click(object sender, EventArgs e)
        {
            buttonNewAcc.Visible = false;
            Thread myThreadPink = new Thread(funcPink);
            myThreadPink.Start();
            buttonNewAcc.Visible = true;
        }


        /// <summary>
        /// создание новых аккаунтов
        /// </summary>
        private void funcPink()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
//            for (int j = 2; j <= 2; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                {
                    DriversOfState driver = new DriversOfState(j);
                    driver.StateNewAcc();
                }
            }
        }

        #endregion

        #region AQUA кнопка (продажа одного окна)

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
        /// </summary>*
        private void funcAqua()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                {
                    DriversOfState driver = new DriversOfState(j);
                    driver.StateSelling();             // продаёт бота, который стоит в данный момент в магазине (через движок состояний). окно должно быть активным
                }
            }
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


            Check check = new Check(1);

            check.TestButton();


            button1.Visible = true;
        }

        #endregion

        #region Green button (исправление проблем)

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
                Check check = new Check(j);
                if (check.isActive())  check.checkForProblems();
            }
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive())   check.ReOpenWindow();
            }   
        }

        #endregion

        #region Blue button  (запускает по таймеру проверку состояния ботов)

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
            //Thread myThreadGreen = new Thread(funcGreen);    //запускаем новый процесс
            //myThreadGreen.Start();


            myTimer.Enabled = true;
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

            Thread mythreadNewWhite = new Thread(funcNewWhite);
            mythreadNewWhite.Start();

            buttonGotoTradeTest.Visible = true;

        }

        /// <summary>
        /// метод задает функционал для потока, организуемого White Button (Sale)
        /// </summary>*
        private void funcNewWhite()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive())
                {
                    DriversOfState drive = new DriversOfState(j);
                    drive.StateGotoTradeAndWork();
                }
            }
        }
        
        #endregion

        #region золотая кнопка (открыть окна ге)

        /// <summary>
        /// Золотая кнопка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenWindow_Click(object sender, EventArgs e)
        {
            ButtonOpenWindow.Visible = false;

            Thread myThreadGold = new Thread(funcGold);
            myThreadGold.Start();

            ButtonOpenWindow.Visible = true;
        }

        /// <summary>
        /// метод задает функционал для потока, организуемого gold кнопкой
        /// </summary>
        private void funcGold()
        {
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive()) check.OpenWindow();
            }
            for (int j = 1; j <= numberOfAccounts; j++)
            {
                Check check = new Check(j);
                if (check.isActive()) check.ReOpenWindow();
            }
        }

        #endregion



        #region дополнительные методы

        /// <summary>
        /// возвращаеи количество аккаунтов
        /// </summary>
        /// <returns>кол-во акков всего</returns>
        public static int KolvoAkk()
        { 
            return int.Parse(File.ReadAllText(KatalogMyProgram + "\\Аккаунтов всего.txt"));
        }

        /// <summary>
        /// читаем из файла значение
        /// </summary>
        /// <returns>с какого номера начинаются аккаунты Сингапура</returns>
        public static int BeginSing()
        {
            return int.Parse(File.ReadAllText(KatalogMyProgram + "\\началоАкковСинга.txt"));
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

