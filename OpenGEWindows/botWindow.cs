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
using GEBot.Data;


namespace OpenGEWindows
{
    public class botWindow
    {
        [DllImport("user32.dll")]
        private static extern UIntPtr FindWindow(String ClassName, String WindowName);  //ищет окно с заданным именем и классом

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(UIntPtr myhWnd, int myhwndoptional, int xx, int yy, int cxx, int cyy, uint flagus); // Перемещает окно в заданные координаты с заданным размером

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(UIntPtr hWnd); // Делает окно активным

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(UIntPtr hWnd, int nCmdShow);  //раскрывает окно, если оно было скрыто в трей

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(UIntPtr hWnd); // Перемещает окно в верхний список Z порядка

        // ================ переменные класса =================
        
        private int numberWindow;       //номер окна
        private const int WIDHT_WINDOW = 1024;
        private const int HIGHT_WINDOW = 700;
        private const String KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";
        private int needToChange;

        private DataBot databot;              //начальные данные для бота (заданные пользователем)
        private IScriptDataBot scriptDataBot;

        private ServerInterface server;                 
        private ServerFactory serverFactory;
        private Town town;
        private int counterMitridat;
        private System.DateTime timeMitridat = System.DateTime.Now;

        private iPoint pointButtonClose;
        private iPoint pointOneMode;

        enum TypeLoadUserData {txt, db}
        

        
        /// <summary>
        /// конструктор с пустыми параметрами
        /// </summary>
        public botWindow()
        {
            MessageBox.Show("НУЖНЫ ПАРАМЕТРЫ в botwindow");
        }

        /// <summary>
        /// основной конструктор
        /// </summary>
        /// <param name="number_Window">номер бота (номер окна)</param>
        public botWindow(int number_Window)
        {
            // основные переменные класса
            this.numberWindow = number_Window;     // эта инфа поступает при создании объекта класса

            this.databot = LoadUserDataBot(TypeLoadUserData.txt);       

            #region Вариант 1. переменные класса подчитываются из текстовых файлов
            this.needToChange = NeedToChange();
            #endregion

            #region Вариант 2. переменные класса подчитываются из БД

            //var bots = GetBots(number_Window);  //подчитываем строку из БД BotsNew, соответствующую данному боту
            //this.login = bots.Login;
            //this.password = bots.Password;
            //databot.hwnd = (UIntPtr)uint.Parse(bots.HWND);
            //databot.param = bots.Server;
            //databot.Kanal = bots.Channel;
            //this.nomerTeleport = bots.TeleportForSale;
            //this.needToChange = bots.ChangeVis;

            //var coord = GetCoordinates(number_Window);  //подчитываем список строк из БД CoordinatesNew с координатами расстановки, соответствующих данному боту
            //int ii = 1;
            //foreach (CoordinatesNew c in coord)
            //{
            //    triangleX[ii] = c.X;
            //    triangleY[ii] = c.Y;
            //    ii++;
            //}
            #endregion

            // эти объекты создаются на основании предыдущих переменных класса, а именно param (на каком сервере бот) и nomerTeleport (город продажи)
            this.serverFactory = new ServerFactory(this);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();

            // точки для тыканья. универсально для всех серверов
            this.pointButtonClose = new Point(848 + databot.x, 620 + databot.y);   //(848, 620);
            this.pointOneMode = new Point(123 - 5 + databot.x, 489 - 5 + databot.y);    // 118, 484
        }

        #region геттеры и сеттеры
        // сеттеры
        public void setHwnd(UIntPtr hwnd)
        { 
            databot.hwnd = hwnd; 
            hwnd_to_file(); 
        }
        public void setNeedToChange(int needToChange)
        { this.needToChange = needToChange; }
        public void setNeedToChangeForMainForm(bool checkBox)
        {
            this.needToChange = checkBox ? 1 : 0;
            NeedToChangeToFile();
        }
        

        // геттеры 
        public DataBot getDataBot()
        { return this.databot; }

        public ServerInterface getserver()
        {
            return this.server;
        }
        public UIntPtr getHwnd()
        { return databot.hwnd; }
        public int getNumberWindow()
        { return this.numberWindow; }
        public int getX()
        { return databot.x; }
        public int getY()
        { return databot.y; }
        public String getParam()
        { return databot.param; }
        public int getKanal()
        { return databot.Kanal; }
        public int[] getTriangleX()
        { return databot.triangleX; }
        public int[] getTriangleY()
        { return databot.triangleY; }
        public int getNomerTeleport()
        { return databot.nomerTeleport; }
        public String getLogin()
        {
            return databot.Login;
        }
        public String getPassword()
        {
            return databot.Password;
        }
        public bool getNeedToChangeForMainForm()
        { return this.needToChange == 1 ? true : false; }
        public String getNameOfFamily()
        {
            return databot.nameOfFamily;
        }

        //private int getNeedToChange()
        //{ return this.needToChange; }
        //public int getNumberOfACOOUNTS()
        //{ return this.NUMBER_OF_ACCOUNTS; }
        #endregion

        #region  М Е Т О Д Ы, которые присваивают начальные значения переменным класса (чтение из текстового файла)

        /// <summary>
        /// Перемещает окно с ботом в заданные координаты. Если окно есть, то result = true, а если вылетело окно, то result = false.
        /// </summary>
        /// <returns></returns>
        public bool isHwnd()
        {
            return SetWindowPos(databot.hwnd, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);  //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
        }

        /// <summary>
        /// метод возвращает значение 1, если нужно передавать песо торговцу. или 0, если не нужно
        /// </summary>
        /// <returns></returns>
        private int NeedToChange()
        {
            int result = 0;
            String LoadString = File.ReadAllText(KATALOG_MY_PROGRAM + this.numberWindow + "\\НужноПередаватьПесо.txt");
            if (LoadString.Equals("1")) result = 1;
            return result;
        }


        /// <summary>
        /// запись значения NeedToChange в файл 
        /// </summary>
        public void NeedToChangeToFile()
        { File.WriteAllText(KATALOG_MY_PROGRAM + this.numberWindow + "\\НужноПередаватьПесо.txt", this.needToChange.ToString()); }

        #endregion

        #region методы Entity Framework, которые читают из БД значения для последующего присваивания переменным класса

        ///// <summary>
        ///// читаем из таблицы параметры ботов
        ///// </summary>
        ///// <returns></returns>
        //private BotsNew GetBots(int i)
        //{
        //    var context = new GEContext();

        //    IQueryable<BotsNew> query = context.BotsNew.Where (c => c.NumberOfWindow == i);

        //    BotsNew bots = query.Single<BotsNew>();

        //    return bots;
        //}

        ///// <summary>
        ///// читаем из базы координаты расстановки ботов на карте
        ///// </summary>
        ///// <returns></returns>
        //private List<CoordinatesNew> GetCoordinates(int i)
        //{
        //    var context = new GEContext();

        //    //IQueryable<CoordinatesNew> query = context.CoordinatesNew.Where(c => c.Id_Bots == i);

        //    var query = from c in context.CoordinatesNew
        //                where c.Id_Bots == i
        //                orderby c.NumberOfHeroes
        //                select c;

        //    var coordinates = query.ToList();

        //    return coordinates;
        //}


        #endregion

        #region неиспользуемые методы

        ///// <summary>
        ///// проверяет цвет двух пикселей и сверяет их с заданными
        ///// </summary>
        ///// <param name="x1"> абсцисса первого пикселя </param>
        ///// <param name="y1"> ордината первого пикселя </param>
        ///// <param name="color1"> цвет для проверки №1 </param>
        ///// <param name="x2"> абсцисса второго пикселя </param>
        ///// <param name="y2"> ордината второго пикселя </param>
        ///// <param name="color2">  цвет для проверки №2 </param>
        ///// <param name="accuracy"> точность для округления цвета пикселя </param>
        ///// <returns> true, если цвета обоих пикселей совпадают с указанными цветами с заданной точностью </returns>
        //public bool isColor2(int x1, int y1, uint color1, int x2, int y2, uint color2, int accuracy)  
        //{
        //    bool result = false;
        //    uint ss, tt;
        //    ss = Okruglenie(GetPixelColor(x1, y1), accuracy);  //  
        //    if (ss == color1)
        //    {  
        //        tt = Okruglenie(GetPixelColor(x2, y2), accuracy);  //  
        //        if (tt == color2) result = true;
        //    }
        //    return result;
        //} 

        ///// <summary>
        ///// нажмает на выбранный раздел верхнего меню 
        ///// </summary>
        ///// <param name="numberOfThePartitionMenu"> ноиер раздела верхнего меню </param>
        //public void TopMenu(int numberOfThePartitionMenu)
        //{
        //    int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
        //    int x = MenukoordX[numberOfThePartitionMenu - 1];
        //    int y = 55;
        //    do {
        //    PressMouse(x, y);
        //    Pause(1000);
        //    } while (!isOpenTopMenu(numberOfThePartitionMenu));
        //}

        ///// <summary>
        ///// нажать на выбранный раздел верхнего меню, а далее на пункт раскрывшегося списка
        ///// </summary>
        ///// <param name="numberOfThePartitionMenu"></param>
        ///// <param name="punkt"></param>
        //public void TopMenu(int numberOfThePartitionMenu, int punkt)
        //{
        //    int[] numberOfPunkt = { 0, 8, 4, 5, 0, 3, 2, 6, 9, 0, 0, 0, 0 };
        //    int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
        //    int[] FirstPunktOfMenuKoordY = { 0, 80, 80, 80, 0, 92, 92, 92, 80, 0, 0, 0, 0 };

        //    if (punkt <= numberOfPunkt[numberOfThePartitionMenu - 1])
        //    {
        //        int x = MenukoordX[numberOfThePartitionMenu - 1];
        //        int y = FirstPunktOfMenuKoordY[numberOfThePartitionMenu - 1] + 25 * (punkt - 1);

        //        server.TopMenu(numberOfThePartitionMenu);   //сначала открываем раздел верхнего меню (1-13)
        //        Pause(500);
        //        PressMouse(x, y);  //выбираем конкретный пункт подменю (раскрывающийся список)
        //    }
        //}
 
        ///// <summary>
        ///// округление вверх числа a на количество разрядов b
        ///// если a = 1655, b = 2, то результат равен 1600
        ///// </summary>
        ///// <param name="a"> округляемое число </param>
        ///// <param name="b"> количество разрядов для округления </param>
        ///// <returns> если a = 1655, b = 2, то результат равен 1600 </returns>
        //public uint Okruglenie(uint a, int b)
        //{
        //    uint bb = 1;
        //    for (int j = 1; j <= b; j++) bb = bb * 10;
        //    uint result = a - a % bb;
        //    //result = result * bb;
        //    return result;
        //}

        #endregion

        /// <summary>
        /// читаем данные о боте, заполненные пользователем, из БД или из текстовых файлов
        /// </summary>
        /// <returns></returns>
        private DataBot LoadUserDataBot(TypeLoadUserData select)
        {
            if (select == TypeLoadUserData.txt)
            { this.scriptDataBot = new ScriptDataBotText(this.numberWindow); }    //делаем объект репозитория с реализацией чтения из тестовых файлов
            else
            { this.scriptDataBot = new ScriptDataBotDB(this.numberWindow); }      //делаем объект репозитория с реализацией чтения из базы данных

            return scriptDataBot.GetDataBot();                                    //в этом объекте все данные по данному окну бота
        }

        /// <summary>
        /// нажать мышью в конкретную точку только левой кнопкой
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseL(int x, int y)
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x + databot.x, y + databot.y, 1);    
            Pause(200);
        } 

        /// <summary>
        /// отодвинуть мышку в сторону, чтобы она не загораживала проверяемые точки
        /// </summary>
        public void ToMoveMouse()
        {
            iPoint pointToMoveMouse = new Point(205 - 5 + databot.x, 575 - 5 + databot.y);    //
            pointToMoveMouse.PressMouseR();
        }

        /// <summary>
        /// Останавливает поток на некоторый период
        /// </summary>
        /// <param name="ms"> ms - период в милисекундах </param>
        public void Pause(int ms)
        {
            //Class_Timer.Pause(ms);
            Thread.Sleep(ms);
        }
        
        /// <summary>
        /// эмулирует тройное нажатие кнопки "Esc", тем самым в окне бота убираются все лишние окна (в том числе реклама)
        /// </summary>
        public void PressEscThreeTimes()
        {
            TextSend.SendText2(1);           // нажимаем Esc
            Thread.Sleep(200);
            TextSend.SendText2(1);           // нажимаем Esc
            Thread.Sleep(200);
            TextSend.SendText2(1);           // нажимаем Esc
            Thread.Sleep(200);
        }

//        /// <summary>
//        /// поиск новых окон с игрой для кнопки "Найти окна"
//        /// </summary>
//        /// <returns></returns>
//        public UIntPtr FindWindowEuropa()
//        {
//            UIntPtr HWND = (UIntPtr)0;

//            int count = 0;
//            while (HWND == (UIntPtr)0)
//            {
//                Pause(500);
//                HWND = FindWindow("Granado Espada", "Granado Espada Online");

//                count++; if (count > 5) return (UIntPtr)0;
//            }

//            setHwnd(HWND);
////            hwnd_to_file();

//            #region старый вариант
//            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(350, 700, 8);
//            //Pause(200);
//            //while (New_HWND_GE == (UIntPtr)0)                
//            //{
//            //    Pause(500);
//            //    New_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");
//            //}
//            //setHwnd(New_HWND_GE);
//            //hwnd_to_file();
//            ////Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
//            ////SetWindowPos(New_HWND_GE, 1, getX(), getY(), WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
//            //SetWindowPos(New_HWND_GE, 1, 825, 5, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
//            //Pause(500);
//            #endregion

//            return HWND;
//        }

        /// <summary>
        /// активируем окно
        /// </summary>
        private void ActiveWindow()
        {
            ShowWindow(databot.hwnd, 9);                                       // Разворачивает окно если свернуто
            SetForegroundWindow(databot.hwnd);                                 // Перемещает окно в верхний список Z порядка     
            BringWindowToTop(databot.hwnd);                                    // Делает окно активным                              
            SetWindowPos(databot.hwnd, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001); //перемещаем окно в заданные для него координаты
        }
        /// <summary>
        /// восстановливает окно (т.е. переводит из состояния "нет окна" в состояние "логаут", плюс из состояния свернутого окна в состояние развернутого и на нужном месте)
        /// </summary>
        public void ReOpenWindow()
        {
            bool result = isHwnd();                           //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
            if (!result)
            {
                OpenWindow();

                ActiveWindow();

                while (!server.isLogout())  Pause(1000);    //ожидание логаута
            }
            else
            {
                ActiveWindow();
            }
        }

        /// <summary>
        /// открывает новое окно бота (т.е. переводит из состояния "нет окна" в состояние "логаут")
        /// </summary>
        /// <returns> hwnd окна </returns>
        public void OpenWindow()
        {
            server.runClient();
            while (true)
            {
                Pause(5000);
                UIntPtr hwnd = server.FindWindowGE();          //ищем окно ГЭ с нужными параметрами
                if (hwnd != (UIntPtr)0) break;
            }
            Pause(10000);

            #region старый вариант метода
            //UIntPtr New_HWND_GE, current_HWND_GE;
            //Pause(500);
            //current_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");    //hwnd старого окна ге
            //server.runClient();  //запускаем нужный клиент игры
            //Pause(5000);
            ////current_HWND_GE = FindWindow("Granado Espada", "Granado Espada");    //hwnd вновь загруженного окна
            //New_HWND_GE = current_HWND_GE;
            //while (New_HWND_GE == current_HWND_GE)                             //убрал 09-01-2017. восстановить, если не будет работать америка
            //{
            //    Pause(500);
            //    New_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");
            //}
            //Pause(25000);

            ////Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
            //SetWindowPos(New_HWND_GE, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            //Pause(1000);

            //EnterLoginAndPasword();

            //setHwnd(New_HWND_GE);
            ////            hwnd_to_file();     //записали новый hwnd в файл
            //return databot.hwnd;
            #endregion
        }

        /// <summary>
        /// вводим логин и пароль в соответствующие поля
        /// </summary>
        public void EnterLoginAndPasword()

        {
            iPoint pointPassword = new Point(510 - 5 + databot.x, 355 - 5 + databot.y);    //  505, 350
            // окно открылось, надо вставить логин и пароль
            pointPassword.PressMouseL();   //Кликаю в строчку с паролем
            //PressMouseL(505, 350);       //Кликаю в строчку с паролем
            Pause(500);
            pointPassword.PressMouseL();   //Кликаю в строчку с паролем
            //PressMouseL(505, 350);       //Кликаю в строчку с паролем
            Pause(500);
            SendKeys.SendWait("{TAB}");
            Pause(500);
            SendKeys.SendWait(getLogin());
            Pause(500);
            SendKeys.SendWait("{TAB}");
            Pause(500);
            SendKeys.SendWait(getPassword());
            Pause(500);
        }

        /// <summary>
        /// запись HWND в файл
        /// </summary>
        private void hwnd_to_file()
        {
            scriptDataBot.SetHwnd(databot.hwnd);
        }

        /// <summary>
        /// нажимаем на кнопку Connect (окно в логауте)
        /// </summary>
        private void PressConnectButton()
        {
            iPoint pointButtonConnect = new Point(595 - 5 + databot.x, 485 - 5 + databot.y);    // кнопка коннект в логауте (экран еще до казармы)
            pointButtonConnect.PressMouse();   // Кликаю в Connect
            Pause(500);
        }

        /// <summary>
        /// исправление ошибок при нажатии кнопки Connect (бот в логауте)
        /// </summary>
        private void BugFixes()
        {
            iPoint pointButtonOk  = new Point(525 - 5 + databot.x, 410 - 5 + databot.y);    // кнопка Ok в логауте
            iPoint pointButtonOk2 = new Point(525 - 5 + databot.x, 445 - 5 + databot.y);    // кнопка Ok в логауте

            pointButtonOk.PressMouse();   //кликаю в кнопку  "ОК"
            Pause(500);

            pointButtonOk.PressMouseL();  //кликаю в кнопку  "ОК"  второй раз (их может быть две)
            Pause(500);

            pointButtonOk2.PressMouseL();  //кликаю в кнопку  "ОК" другой формы (где написано про 3 min)

            EnterLoginAndPasword();        //вводим логин и пароль заново
        }

        /// <summary>
        /// проверяем, есть ли проблемы после нажатия кнопки Connect (выскачила форма с кнопкой ОК)
        /// </summary>
        /// <returns></returns>
        private bool isCheckBugs()
        {
            return server.isPointConnect();
        }

        /// <summary>
        /// проверяем, сменилось ли изображение на экране
        /// </summary>
        /// <param name="testColor">тестовая точка</param>
        /// <returns>true, если сменился экран</returns>
        private bool isChangeDisplay(uint testColor)
        {
            iPointColor currentColor = new PointColor(65 - 5 + databot.x, 55 - 5 + databot.y, 7800000, 5);
            uint color = currentColor.GetPixelColor();
            bool result = (color == testColor);
            return !result;
        }


        /// <summary>
        /// Нажимаем Коннект (переводим юота из состояния логаут в состояние казарма)
        /// </summary>
        /// <returns></returns>
        public bool Connect()    // возвращает true, если успешно вошли в казарму
        {
            
            #region новый вариант
            //bool result = true;
            //const int MAX_NUMBER_ITERATION = 4;    //максимальное количество итераций
            //uint count = 0;

            //iPointColor testColor = new PointColor(65 - 5 + databot.x, 55 - 5 + databot.y, 7800000, 5);  //запоминаем цвет в координатах 55, 55 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            //Pause(500);

            //do
            //{
            //    PressConnectButton();
            //    Pause(10000);
            //    if (isCheckBugs()) BugFixes();

            //    count++;
            //    if (count > MAX_NUMBER_ITERATION)
            //    {
            //        result = false;
            //        break;
            //    }
            //    //if (server.isBarack())
            //    //{
            //    //    result = true;
            //    //    break;
            //    //}
            //} while (!isChangeDisplay(testColor.GetPixelColor()));

            //return result;

            #endregion

            #region старый вариант

            iPointColor point5050 = new PointColor(50 - 5 + databot.x, 50 - 5 + databot.y, 7800000, 5);  //запоминаем цвет в координатах 50, 50 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            iPoint pointButtonOk = new Point(525 - 5 + databot.x, 410 - 5 + databot.y);    // кнопка Ok в логауте
            iPoint pointButtonOk2 = new Point(525 - 5 + databot.x, 445 - 5 + databot.y);    // кнопка Ok в логауте

            uint Tek_Color1;
            uint Test_Color = 0;
            bool ColorBOOL = true;
            uint currentColor = 0;
            const int MAX_NUMBER_ITERATION = 4;  //максимальное количество итераций

            bool aa = true;

            Test_Color = point5050.GetPixelColor();       //запоминаем цвет в координатах 50, 50 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            Tek_Color1 = Test_Color;

            ColorBOOL = (Test_Color == Tek_Color1);
            int counter = 0; //счетчик

            while ((aa | (ColorBOOL)) & (counter < MAX_NUMBER_ITERATION))
            {
                counter++;  //счетчик

                Tek_Color1 = point5050.GetPixelColor();
                ColorBOOL = (Test_Color == Tek_Color1);
                PressConnectButton();
                //pointButtonConnect.PressMouse();   // Кликаю в Connect
                Pause(500);

                //если есть ошибки в логине-пароле, то возникает сообщение с кнопкой "OK". 

                if (server.isPointConnect())                                         // Обработка Ошибок.
                {
                    pointButtonOk.PressMouse();  //кликаю в кнопку  "ОК"
                    Pause(500);

                    if (server.isPointConnect())   //проверяем, выскочила ли форма с кнопкой ОК
                    {
                        pointButtonOk.PressMouse();  //кликаю в кнопку  "ОК"
                        Pause(500);
                    }
                    pointButtonOk.PressMouseL();  //кликаю в кнопку  "ОК"

                    pointButtonOk2.PressMouseL();  //кликаю в кнопку  "ОК" 3 min

                    EnterLoginAndPasword();
                }
                else
                {
                    aa = false;
                }

            }

            bool result = true;
            Pause(5000);
            currentColor = point5050.GetPixelColor();
            if (currentColor == Test_Color)      //проверка входа в казарму. 
            {
                //тыкнуть в Quit 
                //PressMouseL(600, 530);          //если не вошли в казарму, то значит зависли и жмем кнопку Quit
                //PressMouseL(600, 530);
                result = false;
            }
            return result;

            #endregion

            
        }


        #region методы для перекладывания песо в торговца


        
        /// <summary>
        /// открыть фесо шоп
        /// </summary>
        public void OpenFesoShop()
        {
            server.TopMenu(2, 2);
            Pause(1000);
        }

        /// <summary>
        /// для передачи песо торговцу. Идем на место и предложение персональной торговли
        /// </summary>
        public void ChangeVis1()
        {
            iPoint pointTrader = new Point(472 - 5 + databot.x, 175 - 5 + databot.y);    
            iPoint pointPersonalTrade = new Point(536 - 5 + databot.x, 203 - 5 + databot.y);
            iPoint pointMap = new Point(405 - 5 + databot.x, 220 - 5 + databot.y);    

            //идем на место передачи песо
            PressEscThreeTimes();
            Pause(1000);

            town.MaxHeight();             //с учетом города и сервера
            Pause(500);

            server.OpenMapForState();                  //открываем карту города
            Pause(500);

            pointMap.DoubleClickL();   //тыкаем в карту, чтобы добежать до нужного места

            PressEscThreeTimes();       // закрываем карту
            Pause(25000);               // ждем пока добежим

            iPointColor pointMenuTrade = new PointColor(588 - 5 + databot.x, 230 - 5 + databot.y, 1710000 , 4);
            while (!pointMenuTrade.isColor())
            {
                //жмем правой на торговце
                pointTrader.PressMouseR();
                Pause(1000);
            }

            //жмем левой  на пункт "Personal Trade"
            pointPersonalTrade.PressMouseL();
            Pause(500);
        }

        /// <summary>
        /// обмен песо (часть 2) закрываем сделку со стороны бота
        /// </summary>
        public void ChangeVis2()
        {
            iPoint pointVis1 = new Point(903 - 5 + databot.x, 151 - 5 + databot.y);    
            iPoint pointVisMove1 = new Point(701 - 5 + databot.x, 186 - 5 + databot.y);
            iPoint pointVisMove2 = new Point(395 - 5 + databot.x, 361 - 5 + databot.y);
            iPoint pointVisOk = new Point(611 - 5 + databot.x, 397 - 5 + databot.y);   
            iPoint pointVisOk2 = new Point(442 - 5 + databot.x, 502 - 5 + databot.y);  
            iPoint pointVisTrade = new Point(523 - 5 + databot.x, 502 - 5 + databot.y);  

            // открываем инвентарь
            server.TopMenu(8, 1);

            // открываем закладку кармана, там где песо
            pointVis1.DoubleClickL();
            Pause(500);

            // перетаскиваем песо
            pointVisMove1.Drag(pointVisMove2);                                             // песо берется из первой ячейки на 4-й закладке  
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы песо
            pointVisOk.DoubleClickL();

            // нажимаем ок
            pointVisOk2.DoubleClickL();
            Pause(500);

            // нажимаем обмен
            pointVisTrade.DoubleClickL();
            Pause(500);
        }

        /// <summary>
        /// телепортируемся по номеру телепорта Америка Alt+W 
        /// </summary>
        /// <param name="numberTeleport"></param>
        public void TeleportWA(int numberTeleport)
        {
            server.TopMenu(6, 1);
            Pause(1000);

            PressMouseL(801, 564 + (numberTeleport - 1) * 17);
            Pause(50);
            PressMouseL(801, 564 + (numberTeleport - 1) * 17);
            Pause(200);
        }                                      // по идее должен быть в server

        /// <summary>
        /// купить 400 еды в фесо шопе                    вообще-то метод должен находится в ServerInterface
        /// </summary>
        public void Buy400PetFood()
        {
            iPoint pointArrowUp = new Point(375 + databot.x, 327 + databot.y);   //375, 327);   //шаг = 27 пикселей на одну строчку магазина (на случай если добавят новые строчки)
            iPoint pointButtonBUY = new Point(725 + databot.x, 620 + databot.y);   //725, 620);

            // тыкаем два раза в стрелочку вверх
            //PressMouseL(375, 327);
            pointArrowUp.PressMouseL();
            Pause(500);
            //PressMouseL(375, 327);
            pointArrowUp.PressMouseL();
            Pause(500);

            // жмем кнопку купить
            pointButtonBUY.PressMouseL();
            //PressMouseL(725, 620);
            Pause(1500);

            //нажимаем кнопку Close
            pointButtonClose.PressMouseL();
            //PressMouseL(848, 620);
            Pause(1500);
        }                                                                         //заменил все точки

        /// <summary>
        /// продать 10 ВК в фесо шопе 
        /// </summary>
        public void SellGrowthStone10()
        {
            iPoint pointArrowUp2 = new Point(375 + databot.x, 246 + databot.y);   //375, 246);
            iPoint pointButtonSell = new Point(725 + databot.x, 620 + databot.y);   //725, 620);

            // 10 раз нажимаем на стрелку вверх, чтобы отсчитать 10 ВК
            for (int i = 1; i <= 10; i++)
            {
                pointArrowUp2.PressMouseL();
                //PressMouseL(375, 246);
                Pause(500);
            }

            //нажимаем кнопку Sell
            pointButtonSell.PressMouseL();
            //PressMouseL(725, 620);
            Pause(500);

            //нажимаем кнопку Close
            pointButtonClose.PressMouseL();
            //PressMouseL(848, 620);
            Pause(500);
        }                                                                               //заменил все точки

        /// <summary>
        /// открыть вкладку Sell в фесо шопе
        /// </summary>
        public void OpenBookmarkSell()
        {
            iPoint pointBookmarkSell = new Point(226 + databot.x, 196 + databot.y);     //226, 196);
            pointBookmarkSell.PressMouseL();
            Pause(1500);
        }                                                                            //заменил все точки

        #endregion


        /// <summary>
        /// начать с выхода в город (нажать на кнопку "начать с нового места")
        /// </summary>
        public void NewPlace()
        {
            iPoint pointNewPlace = new Point(85 + databot.x, 670 + databot.y); //85, 670);
            pointNewPlace.PressMouse();
        }                                                              

        /// <summary>
        /// Нажимаем Выбор канала и группы персов в казарме 
        /// </summary>
        public void SelectChannel()
        {
            iPoint pointChoiceOfChannel = new Point(125 + databot.x, 660 + (databot.Kanal - 1) * 15 + server.sdvig() + databot.y);    //переход на нужный канал в казарме
            iPoint pointButtonSelectChannel = new Point(125 + databot.x, 705 + databot.y); //   125, 705);
            pointButtonSelectChannel.PressMouseL();
            pointChoiceOfChannel.PressMouseL();
        }

        /// <summary>
        /// Нажимаем Выбор канала и группы персов в казарме 
        /// </summary>
        public void SelectChannel(int channel)
        {
            iPoint pointChoiceOfChannel = new Point(125 + databot.x, 660 + (channel - 1) * 15 + server.sdvig() + databot.y);    //переход на указанный канал
            iPoint pointButtonSelectChannel = new Point(125 + databot.x, 705 + databot.y); //   125, 705);
            pointButtonSelectChannel.PressMouseL();
            pointChoiceOfChannel.PressMouseL();
        }

        /// <summary>
        /// выбрать первого (левого) бойца из тройки
        /// </summary>
        public void FirstHero()
        {
            iPoint pointFirstHeroL = new Point(187 - 5 + databot.x, 640 - 5 + databot.y);    // 182, 635
            iPoint pointFirstHeroR = new Point(187 - 5 + databot.x, 669 - 5 + databot.y);    // 182, 664
            pointFirstHeroR.PressMouseR();
            pointFirstHeroR.PressMouseR();
            pointFirstHeroL.PressMouseL();
        }

        /// <summary>
        /// выбрать второго (среднего) бойца из тройки
        /// </summary>
        public void SecondHero()
        {
            iPoint pointSecondHeroL = new Point(425 - 5 + databot.x, 640 - 5 + databot.y);    // 420, 635
            iPoint pointSecondHeroR = new Point(425 - 5 + databot.x, 669 - 5 + databot.y);    // 420, 664
            pointSecondHeroR.PressMouseR();
            pointSecondHeroR.PressMouseR();
            pointSecondHeroL.PressMouseL();
        }

        /// <summary>
        /// выбрать третьего (правого) бойца из тройки
        /// </summary>
        public void ThirdHero()
        {
            iPoint pointThirdHeroL = new Point(675 - 5 + databot.x, 640 - 5 + databot.y);    // 670, 635
            iPoint pointThirdHeroR = new Point(675 - 5 + databot.x, 669 - 5 + databot.y);    // 670, 664
            pointThirdHeroR.PressMouseR();
            pointThirdHeroR.PressMouseR();
            pointThirdHeroL.PressMouseL();
        }

        /// <summary>
        /// проверяет включен ли командный режим или нет 
        /// </summary>
        /// <returns> true, если командный режим включен </returns>
        public bool isCommandMode()
        {
            iPointColor pointCommandMode = new PointColor(123 - 5 + databot.x, 479 - 5 + databot.y, 8000000, 6);
            return pointCommandMode.isColor2();
        } 

        /// <summary>
        /// перевод бота в одиночный режим 
        /// </summary>
        public void OneMode()
        {
            if (isCommandMode())
            {
                // если включен командный режим, то надо нажать 1 раз
                pointOneMode.PressMouse();
                //PressMouse(118, 484); 
            }
            else
            {   // если включен одиночный режим, то надо нажать два раза (в командный режим и обратно)
                pointOneMode.PressMouse();
                //PressMouse(118, 484);
                Pause(500);
                //PressMouse(118, 484);
                pointOneMode.PressMouse();
            }
        }

        /// <summary>
        /// закрыть карту Alt+Z
        /// </summary>
        public void CloseMap()
        {
            PressEscThreeTimes();
        }

        /// <summary>
        /// перевод в командный режим из любого положения 
        /// </summary>
        public void CommandMode()
        {
            if (!isCommandMode())
            {   // если включен одиночный режим
                pointOneMode.PressMouse();
                //PressMouse(118, 484);
                Pause(500);
            }
        }

        /// <summary>
        /// нажимаем пробел (переход в боевой режим)
        /// </summary>
        public void ClickSpace()
        {
            iPoint pointBattleMode = new Point(190 - 5 + databot.x, 530 - 5 + databot.y);    //  185, 525
            pointBattleMode.PressMouse();  // Кликаю на кнопку "боевой режим"
        }

        /// <summary>
        /// Лечение одного окна, если побили часть персов (лечение состоит в закрытии окна с ботом)
        /// </summary>
        public void CureOneWindow2()
        {
            iPoint pointEnterBattleMode = new Point(205 - 5 + databot.x, 205 - 5 + databot.y);    // 200, 200
            // ================================= убирает все лишние окна с экрана =========================================
            PressEscThreeTimes();
            Pause(1000);

            //========================================= в командный режим =================================================
            CommandMode();
            Pause(1000);

            //=============================== выйти из боевого режима (бежим в сторону) ============================================

            pointEnterBattleMode.PressMouseL();//отбегаю ботами в сторону, чтобы они вышли из боевого режима
            pointEnterBattleMode.PressMouseL();
            //PressMouseL(200, 200);  //отбегаю ботами в сторону, чтобы они вышли из боевого режима
            //PressMouseL(200, 200);  //отбегаю ботами в сторону, чтобы они вышли из боевого режима
            Pause(2000);

            server.GoToEnd();
        }

        /// <summary>
        /// Нажать на бутылку митридата, которая лежит во второй ячейке
        /// </summary>
        public void PressMitridat()
        {
            iPoint pointMitridat = new Point(38 - 5 + databot.x, 486 - 5 + databot.y);    // 33, 481
            System.DateTime timeNow = DateTime.Now;  //текущее время
            System.TimeSpan PeriodMitridat = timeNow.Subtract(timeMitridat);   //сколько времени прошло с последнего применения митридата
            uint PeriodMitridatSeconds = (uint)PeriodMitridat.TotalSeconds;          //сколько времени прошло с последнего применения митридата в секундах

            if ((PeriodMitridatSeconds >= 360) | (counterMitridat == 0))
            {
                pointMitridat.PressMouseR();             // Кликаю правой кнопкой в панель с бытылками, чтобы сделать ее активной и поверх всех окон (группа может мешать)
                //PressMouseR(33, 481);                       // Кликаю правой кнопкой в панель с бытылками, чтобы сделать ее активной и поверх всех окон (группа может мешать)
                //Pause(500);
                PressMouseL(31 - 5, 140 - 5);                       // тыкаю в митридат (вторая ячейка)
                //Pause(500);
                PressMouseL(31 - 5, 170 - 5);                       // тыкаю в  (третья ячейка)
                //Pause(500);

                SecondHero();                               //выбираю главным второго героя (это нужно, чтобы не было тормозов) типа надо нажать в экран после митридата
                //Pause(1000);
                Pause(500);
                timeMitridat = DateTime.Now;              //обновляю время, когда был применен митридат
                counterMitridat++;
            }
        }




    }
}
