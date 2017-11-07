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
using GEDataBot.BL;


namespace OpenGEWindows
{
    public class botWindow
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

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(UIntPtr myhWnd, int myhwndoptional, int xx, int yy, int cxx, int cyy, uint flagus); // Перемещает окно в заданные координаты с заданным размером

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(UIntPtr hWnd); // Делает окно активным

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(UIntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(UIntPtr hWnd); // Перемещает окно в верхний список Z порядка

        // ================ переменные класса =================
        
        private int numberWindow;       //номер окна
        private const int WIDHT_WINDOW = 1024;
        private const int HIGHT_WINDOW = 700;
        private const String KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";
        private int needToChange;

        private IScriptDataBot scriptDataBot;  
        private DataBot databot;              //начальные данные для бота (заданные пользователем)

        private ServerInterface server;                 
        private ServerFactory serverFactory;
        private Town town;
        private int counterMitridat;
        private System.DateTime timeMitridat = System.DateTime.Now;

        private iPoint pointArrowUp;
        private iPoint pointArrowUp2; 
        private iPoint pointButtonBUY;
        private iPoint pointButtonClose;
        private iPoint pointButtonSell;
        private iPoint pointBookmarkSell;
        private iPoint pointNewPlace;
        private iPoint pointChoiceOfChannel;
        private iPoint pointButtonSelectChannel;
        private iPoint pointButtonConnect;
        private iPoint pointButtonOk;
        private iPoint pointButtonOk2;
        private iPoint pointOneMode;
        private iPoint pointBattleMode;
        private iPoint pointPassword;
        private iPoint pointMap;
        private iPoint pointTrader;
        private iPoint pointPersonalTrade;
        private iPoint pointFeso1;
        private iPoint pointFesoMove1;
        private iPoint pointFesoMove2;
        private iPoint pointFesoOk;
        private iPoint pointFesoOk2;
        private iPoint pointFesoObmen;
        private iPoint pointFirstHeroL;
        private iPoint pointFirstHeroR;
        private iPoint pointSecondHeroL;
        private iPoint pointSecondHeroR;
        private iPoint pointThirdHeroL;
        private iPoint pointThirdHeroR;
        private iPoint pointMitridat;
        private iPoint pointEnterBattleMode;
        private iPoint pointToMoveMouse;
        private iPointColor point5050;
        private iPointColor pointCommandMode;


        //private SqlConnection sqlconnection;
        // ================ конструктор =================
        public botWindow()
        {
            MessageBox.Show("НУЖНЫ ПАРАМЕТРЫ");
        }
        public botWindow(int number_Window)
        {
            // основные переменные класса
            this.numberWindow = number_Window;     // эта инфа поступает при создании объекта класса

            scriptDataBot = new ScriptDataBotText(this.numberWindow);   //делаем объект репозитория с реализацией чтения из тестовых файлов (а можно организовать аналогичный класс с чтением из БД)
            databot = scriptDataBot.GetDataBot();  //в этом объекте все данные по данному окну бота


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
            this.pointArrowUp = new Point(375 + databot.x, 327 + databot.y);   //375, 327);   //шаг = 27 пикселей на одну строчку магазина (на случай если добавят новые строчки)
            this.pointButtonBUY = new Point(725 + databot.x, 620 + databot.y);   //725, 620);
            this.pointButtonClose = new Point(848 + databot.x, 620 + databot.y);   //(848, 620);
            this.pointButtonSell = new Point(725 + databot.x, 620 + databot.y);   //725, 620);
            this.pointArrowUp2 = new Point(375 + databot.x, 246 + databot.y);   //375, 246);
            this.pointBookmarkSell = new Point(226 + databot.x, 196 + databot.y);     //226, 196);
            this.pointNewPlace = new Point(85 + databot.x, 670 + databot.y); //85, 670);
            this.pointButtonSelectChannel = new Point(125 + databot.x, 705 + databot.y); //   125, 705);
            this.pointButtonConnect = new Point(595 - 5 + databot.x, 485 - 5 + databot.y);    // кнопка коннект в логауте (экран еще до казармы)
            this.pointButtonOk = new Point(525 - 5 + databot.x, 425 - 5 + databot.y);    // кнопка коннект в логауте
            this.pointButtonOk2 = new Point(525 - 5 + databot.x, 445 - 5 + databot.y);    // кнопка коннект в логауте
            this.pointOneMode = new Point(123 - 5 + databot.x, 489 - 5 + databot.y);    // 118, 484
            this.pointBattleMode = new Point(190 - 5 + databot.x, 530 - 5 + databot.y);    //  185, 525
            this.pointPassword = new Point(510 - 5 + databot.x, 355 - 5 + databot.y);    //  505, 350
            this.pointMap = new Point(327 - 5 + databot.x, 355 - 5 + databot.y);    //  322, 350
            this.pointTrader = new Point(382 - 5 + databot.x, 262 - 5 + databot.y);    // 377, 257
            this.pointPersonalTrade = new Point(436 - 5 + databot.x, 280 - 5 + databot.y);    // 431, 275
            this.pointFeso1 = new Point(971 - 5 + databot.x, 154 - 5 + databot.y);    // 1666 - 700, 329 - 180
            this.pointFesoMove1 = new Point(801 - 5 + databot.x, 186 - 5 + databot.y);    // 1496 - 700, 361 - 180
            this.pointFesoMove2 = new Point(395 - 5 + databot.x, 361 - 5 + databot.y);    // 1090 - 700, 536 - 180
            this.pointFesoOk = new Point(610 - 5 + databot.x, 398 - 5 + databot.y);    // 1305 - 700, 573 - 180
            this.pointFesoOk2 = new Point(440 - 5 + databot.x, 502 - 5 + databot.y);    // 1135 - 700, 677 - 180
            this.pointFesoObmen = new Point(521 - 5 + databot.x, 502 - 5 + databot.y);    // 1216 - 700, 677 - 180
            this.pointFirstHeroL = new Point(187 - 5 + databot.x, 640 - 5 + databot.y);    // 182, 635
            this.pointFirstHeroR = new Point(187 - 5 + databot.x, 669 - 5 + databot.y);    // 182, 664
            this.pointSecondHeroL = new Point(425 - 5 + databot.x, 640 - 5 + databot.y);    // 420, 635
            this.pointSecondHeroR = new Point(425 - 5 + databot.x, 669 - 5 + databot.y);    // 420, 664
            this.pointThirdHeroL = new Point(675 - 5 + databot.x, 640 - 5 + databot.y);    // 670, 635
            this.pointThirdHeroR = new Point(675 - 5 + databot.x, 669 - 5 + databot.y);    // 670, 664
            this.pointMitridat = new Point(38 - 5 + databot.x, 486 - 5 + databot.y);    // 33, 481
            this.pointEnterBattleMode = new Point(205 - 5 + databot.x, 205 - 5 + databot.y);    // 200, 200
            this.pointToMoveMouse = new Point(205 - 5 + databot.x, 575 - 5 + databot.y);    //

            //точки для проверки цвета
            this.point5050 = new PointColor(55 - 5 + databot.x, 55 - 5 + databot.y, 7800000, 5);
            this.pointCommandMode = new PointColor(123 - 5 + databot.x, 479 - 5 + databot.y, 8000000, 6);

            
        }

        #region геттеры и сеттеры
        // сеттеры
        public void setHwnd(UIntPtr hwnd)
        { databot.hwnd = hwnd;  }
        public void setNeedToChange(int needToChange)
        { this.needToChange = needToChange; }
        public void setNeedToChangeForMainForm(bool checkBox)
        {
            this.needToChange = checkBox ? 1 : 0;
            NeedToChangeToFile();
        }
        

        // геттеры 
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

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindow2()
        {
            UIntPtr New_HWND_GE;
            New_HWND_GE = (UIntPtr)0;

            if (server.isActive())
            {
                Click_Mouse_and_Keyboard.Mouse_Move_and_Click(350, 700, 8);
                Pause(200);
                while (New_HWND_GE == (UIntPtr)0)                
                {
                    Pause(500);
                    New_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");
                }
                setHwnd(New_HWND_GE);
                hwnd_to_file();
                //Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
                //SetWindowPos(New_HWND_GE, 1, getX(), getY(), WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
                SetWindowPos(New_HWND_GE, 1, 825, 5, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
                Pause(500);

            }
            return New_HWND_GE;
        }

        /// <summary>
        /// поиск новых окон с игрой для кнопки "Найти окна Sing"
        /// </summary>
        /// <returns></returns>
        public UIntPtr FindWindow3()
        {
            UIntPtr New_HWND_GE;
            New_HWND_GE = (UIntPtr)0;

            while (New_HWND_GE == (UIntPtr)0)
            {
                Pause(500);
                New_HWND_GE = FindWindow("Granado Espada", "Granado Espada");
            }
            setHwnd(New_HWND_GE);
            hwnd_to_file();
            //Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
            //SetWindowPos(New_HWND_GE, 1, getX(), getY(), WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            SetWindowPos(New_HWND_GE, 1, 825, 5, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
            Pause(500);
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(350, 700, 8);
            Pause(200);

            return New_HWND_GE;
        }

        /// <summary>
        /// восстановливает окно (т.е. переводит из состояния "нет окна" в состояние "логаут", плюс из состояния свернутого окна в состояние развернутого и на нужном месте)
        /// </summary>
        public bool ReOpenWindow()
        {
            //scriptDataBot.SetHwnd(databot.hwnd);       не нужно здесь вроде
            ///setHwnd(Hwnd_in_file());                  //написал 14.11.2016   нуждается в проверке. сначала читаем hwnd из файла, а потом присваиваем его текущему hwnd (databot.hwnd)
            bool result = isHwnd();                                //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
            // 26.04.2017  if (!result) setHwnd(OpenWindow());                             //Если вылетело окно, то открываем новое окно с помощью метода OpenWindow и присваиваем новое hwnd 
            //hwnd_to_file();
            if (result)  //26.04.2017 эта строка
            {
                ShowWindow(databot.hwnd, 9);                                       // Разворачивает окно если свернуто
                SetForegroundWindow(databot.hwnd);                                 // Перемещает окно в верхний список Z порядка     
                BringWindowToTop(databot.hwnd);                                    // Делает окно активным                              
            }
            return result;
        }

        /// <summary>
        /// открывает новое окно бота (т.е. переводит из состояния "нет окна" в состояние "логаут")
        /// </summary>
        /// <returns> hwnd окна </returns>
        public UIntPtr OpenWindow()
        {
            UIntPtr New_HWND_GE, current_HWND_GE;
            Pause(500);
            if (server.isActive())
            {
                current_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");    //hwnd старого окна ге
                server.runClient();  //запускаем нужный клиент игры
                Pause(5000);
                //current_HWND_GE = FindWindow("Granado Espada", "Granado Espada");    //hwnd вновь загруженного окна
                New_HWND_GE = current_HWND_GE;
                while (New_HWND_GE == current_HWND_GE)                             //убрал 09-01-2017. восстановить, если не будет работать америка
                {
                    Pause(500);
                    New_HWND_GE = FindWindow("Granado Espada", "Granado Espada Online");
                }
                Pause(25000);

                //Перемещает вновь открывшиеся окно в заданные координаты, игнорирует размеры окна
                SetWindowPos(New_HWND_GE, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
                Pause(1000);

                EnterLoginAndPasword();

                setHwnd(New_HWND_GE);
            }
            else
            {
                setHwnd((UIntPtr)2222222222);                     // если окно не нужно открывать, то возвращаем это
            }

            hwnd_to_file();     //записали новый hwnd в файл

            return databot.hwnd;
        }//Конец метода OpenWindow

        /// <summary>
        /// вводим логин и пароль в соответствующие поля
        /// </summary>
        public void EnterLoginAndPasword()

        {
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
//            Array_File_IO.Write_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\HWND.txt", (uint)databot.hwnd);
            scriptDataBot.SetHwnd(databot.hwnd);

            // обязательно прописать запись hwnd в базу данных Entity Framework
            //var context = new GEContext();
            //IQueryable<BotsNew> query = context.BotsNew.Where (c => c.NumberOfWindow == this.numberWindow);
            //BotsNew bots = query.Single<BotsNew>();
            //bots.HWND = databot.hwnd.ToString();
            //context.SaveChanges();
        }

        /// <summary>
        /// Нажимаем Коннект (переводим юота из состояния логаут в состояние казарма)
        /// </summary>
        /// <returns></returns>
        public bool Connect()    // возвращает true, если успешно вошли в казарму
        {
            uint Tek_Color1;
            uint Test_Color = 0;
            bool ColorBOOL = true;
            uint currentColor = 0;
            const int MAX_NUMBER_ITERATION = 4;  //максимальное количество итераций

            bool aa = true;

//            Test_Color = GetPixelColor(50, 50);       //запоминаем цвет в координатах 50, 50 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            Test_Color = point5050.GetPixelColor();       //запоминаем цвет в координатах 50, 50 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            Tek_Color1 = Test_Color;

            ColorBOOL = (Test_Color == Tek_Color1);
            int counter = 0; //счетчик

            while ((aa | (ColorBOOL)) & (counter < MAX_NUMBER_ITERATION))
            {
                counter++;  //счетчик

                Tek_Color1 = point5050.GetPixelColor();
                ColorBOOL = (Test_Color == Tek_Color1);
                pointButtonConnect.PressMouse();   // Кликаю в Connect
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
        }


        #region методы для перекладывания песо в торговца
        /// <summary>
        /// открыть карту Alt+Z  через верхнее меню (без проверок)
        /// </summary>
        public void OpenMap2()
        {
            server.TopMenu(6, 2);
            Pause(1000);
        }

        /// <summary>
        /// переход бота к месту передачи песо (для фиолетовой кнопки)
        /// </summary>
        public void GoToChangePlaceForBot()
        {
            while (!server.isTown())         //ожидание загрузки города (передача песо торговцу)
            { Pause(500); }

            PressEscThreeTimes();
            Pause(1000);

            town.MaxHeight();             //с учетом города и сервера
            Pause(500);

            OpenMap2();                  //открываем карту города
            Pause(500);

            pointMap.PressMouseL();   //тыкаем в карту, чтобы добежать до нужного места
            //PressMouseL(322, 350);

            PressEscThreeTimes();       // закрываем карту
            Pause(15000);               // ждем пока добежим
        }

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
            //идем на место передачи песо
            GoToChangePlaceForBot();

            //жмем правой на торговце
            pointTrader.PressMouseL();
            //PressMouseL(377, 257);
            Pause(200);

            pointTrader.PressMouseL();
            //PressMouseR(377, 257);
            Pause(500);
            
            //жмем левой  на пункт "Personal Trade"
            pointPersonalTrade.PressMouseL();
            //PressMouseL(431, 275);
        }

        /// <summary>
        /// обмен песо (часть 2) закрываем сделку со стороны бота
        /// </summary>
        public void ChangeVis2()
        {
            // открываем инвентарь
            server.TopMenu(8, 1);

            // открываем закладку кармана, там где фесо
            
            //PressMouseL(1666 - 700, 329 - 180);
            pointFeso1.PressMouseL();
            Pause(500);

            // перетаскиваем песо
            pointFesoMove1.Drag(pointFesoMove2);                                             // песо берется из первой ячейки на этой закладке  
            //MouseMoveAndDrop(1496 - 700, 361 - 180, 1090 - 700, 536 - 180);                         // песо берется из первой ячейки на этой закладке  
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы песо
            pointFesoOk.PressMouseL();
            //PressMouseL(1305 - 700, 573 - 180);
            

            // нажимаем ок и обмен
            pointFesoOk2.PressMouseL();
            //PressMouseL(1135 - 700, 677 - 180);
            pointFesoObmen.PressMouseL();
            //PressMouseL(1216 - 700, 677 - 180);
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
            pointBookmarkSell.PressMouseL();
            //PressMouseL(226, 196);
            Pause(1500);
        }                                                                            //заменил все точки

        #endregion


        /// <summary>
        /// начать с выхода в город (нажать на кнопку "начать с нового места")
        /// </summary>
        public void NewPlace()
        {
            pointNewPlace.PressMouse();
        }                                                              //метод не привязан к botwindow

        /// <summary>
        /// Нажимаем Выбор канала и группы персов в казарме    // ======================================================================================== по идее должен быть в server
        /// </summary>
        public void SelectChannel()
        {
            //PressMouseL(125, 705);
            pointButtonSelectChannel.PressMouseL();

            //===============================  Выбор канала для выхода в город ==============================
            //switch (getKanal())
            //{
            //    case 1:
            //        pointChoiceOfChannel = new Point(125 + x, 660 + server.sdvig() + y); 
            //        //PressMouseL(125, 660 + server.sdvig());
            //        break;
            //    case 2:
            //        pointChoiceOfChannel = new Point(125 + x, 675 + server.sdvig() + y); 
            //        //PressMouseL(125, 675 + server.sdvig());
            //        break;
            //    case 3:
            //        pointChoiceOfChannel = new Point(125 + x, 690 + server.sdvig() + y); 
            //        //PressMouseL(125, 690 + server.sdvig());
            //        break;
            //    default:
            //        pointChoiceOfChannel = new Point(125 + x, 660 + server.sdvig() + y); 
            //        break;
            //} //END SWiTCH

            //            pointChoiceOfChannel = new Point(125 + x, 660 + server.sdvig() + y);              //выходим в город всегда на первом канале, переход на нужный канал на месте работы
            pointChoiceOfChannel = new Point(125 + databot.x, 660 + (databot.Kanal - 1) * 15 + server.sdvig() + databot.y);    //переход на нужный канал в казарме
            pointChoiceOfChannel.PressMouseL();
        }

        /// <summary>
        /// выбрать первого (левого) бойца из тройки
        /// </summary>
        public void FirstHero()
        {
            //PressMouseR(182, 664);
            //PressMouseR(182, 664);
            pointFirstHeroR.PressMouseR();
            pointFirstHeroR.PressMouseR();
            //PressMouseL(182, 635);
            pointFirstHeroL.PressMouseL();
            
        }

        /// <summary>
        /// выбрать второго (среднего) бойца из тройки
        /// </summary>
        public void SecondHero()
        {
            pointSecondHeroR.PressMouseR();
            pointSecondHeroR.PressMouseR();
//            PressMouseR(420, 664);
  //          PressMouseR(420, 664);
            pointSecondHeroL.PressMouseL();
            //PressMouseL(420, 635);
        }

        /// <summary>
        /// выбрать третьего (правого) бойца из тройки
        /// </summary>
        public void ThirdHero()
        {
            pointThirdHeroR.PressMouseR();
            pointThirdHeroR.PressMouseR();
            pointThirdHeroL.PressMouseL();
            //PressMouseR(670, 664);
            //PressMouseR(670, 664);
            //PressMouseL(670, 635);
        }

        /// <summary>
        /// проверяет включен ли командный режим или нет 
        /// </summary>
        /// <returns> true, если командный режим включен </returns>
        public bool isCommandMode()
        {
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
            //PressMouse(185, 525);   // Кликаю на кнопку "боевой режим"
            pointBattleMode.PressMouse();  // Кликаю на кнопку "боевой режим"
        }

        /// <summary>
        /// Лечение одного окна, если побили часть персов (лечение состоит в закрытии окна с ботом)
        /// </summary>
        public void CureOneWindow2()
        {
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

        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        public void checkForProblems()
        {
            if (server.isActive())      //этот метод проверяет, нужно ли грузить или обрабатывать это окно (профа и прочее)
            {
                bool result = ReOpenWindow();    //если окно не вылетело, то будет true
                Pause(1000);
                if (result)      //если окно не вылетело
                {
                    if (server.isLogout())
                    {
                        StateRecovery();
                    }
                    else
                    {
                        if (server.isSale2())         //если зависли в магазине на любой закладке
                        {
                            StateExitFromShop();            //выход из магазина
                        }
                        else
                        {
                            if (server.isKillHero())                  // если убиты один или несколько персов   
                            {
                                CureOneWindow2();              // сделать End Programm
                                Pause(2000);
                                StateGotoWork();               // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                            }
                            else
                            {
                                if (server.isBarack())                  //если стоят в бараке     
                                {
                                    server.buttonExitFromBarack();      //StateExitFromBarack();
                                }
                                else
                                {
                                    //=========================== если переполнение ==============================
                                    if (server.isBoxOverflow())           // если карман переполнился
                                    {
                                        StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа-продажа-выгрузка окна)
                                        Pause(2000);
                                        StateGotoWork();                                           // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                    }
                                    else
                                    {
                                        //================== если в городе ========================================
                                        if ((server.isTown()) || (server.isTown_2()))          //если стоят в городе (проверка по обоим стойкам - эксп.дробаш и ружье )         //**   было istown2()
                                        {
                                            StateExitFromTown();
                                            PressEscThreeTimes();
                                            Pause(2000);
                                            StateGotoWork();                                    // по паттерну "Состояние".  14-28       (нет окна - логаут - казарма - город - работа)
                                        }
                                        else
                                        {
                                            if (server.isSale())                               // если застряли в магазине на странице входа
                                            { StateExitFromShop2(); }
                                            else
                                            { PressMitridat(); }

                                        } //else isTown2()
                                    } //else isBoxOverflow()
                                } //else isBarack()
                            } // else isKillHero()
                        } // else isSale2()
                    } //else  isLogout()
                } // если окно не вылетело, т.е. result = true
            } //if  Active_or_not
        }                                                                  //основной метод для зеленой кнопки


        #region движки для запуска перехода по состояниям

        /// <summary>
        /// перевод из состояния 01 (на работе) в состояние 14 (нет окна). Цель  - продажа после переполнения инвентаря
        /// </summary>
        public void StateGotoTrade()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                ReOpenWindow();
                StateDriverRun(new StateGT01(this), new StateGT14(this));
            }
        }

        /// <summary>
        /// перевод из состояния 14 (нет окна бота) в состояние 01 (на работе)
        /// </summary>
        public void StateGotoWork()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT14(this), new StateGT01(this)); //
            }
        }

        /// <summary>
        /// перевод из состояния 15 (логаут) в состояние 01 (на работе)
        /// </summary>
        public void StateRecovery()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT15(this), new StateGT01(this));
            }
        }

        /// <summary>
        /// перевод из состояния 14 (нет окна) в состояние 15 (логаут)                 // оранжевая кнопка
        /// </summary>
        public void StateReOpen()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT14(this), new StateGT15(this));
            }
        }

        /// <summary>
        /// перевод из состояния 09 (в магазине) в состояние 12 (всё продано, в городе)                 // аква кнопка
        /// </summary>
        public void StateSelling()
        {
            if ((server.isActive()) && (server.isSale()))                                 //проверяем, нужно ли грузить окно и находимся ли в магазине
                        StateDriverRun(new StateGT09(this), new StateGT12(this));
        }

        /// <summary>
        /// создание новой семьи, выход в ребольдо, получение и надевание брони 35 лвл, выполнение квеста Доминго, разговор с Линдоном, получение Кокошки и еды 100 шт.
        /// перевод из состояния 30 (логаут) в состояние 41 (пет Кокошка получен)          // розовая кнопка
        /// </summary>
        public void StateNewAcc()
        {
                StateDriverRun(new StateGT30(this), new StateGT41(this));
        }

        /// <summary>
        /// вновь созданный аккаунт бежит через лавовое плато в кратер, становится на выделенное ему место, сохраняет телепорт и начинает ботить
        /// </summary>
        public void StateToCrater()
        {
            StateDriverRun(new StateGT42(this), new StateGT58(this));
        }

        /// <summary>
        /// перевод из состояния 10 (в магазине) в состояние 14 (нет окна)
        /// </summary>
        public void StateExitFromShop()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT10(this), new StateGT14(this));
            }
        }

        /// <summary>
        /// перевод из состояния 09 (в магазине, на странице входа) в состояние 14 (нет окна)
        /// </summary>
        public void StateExitFromShop2()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT09(this), new StateGT14(this));
            }
        }

        /// <summary>
        /// перевод из состояния 12 (в городе) в состояние 14 (нет окна) 
        /// </summary>
        public void StateExitFromTown()
        {
            if (server.isActive())                                 //проверяем, нужно ли грузить окно
            {
                StateDriverRun(new StateGT12(this), new StateGT14(this));
            }
        }

        /// <summary>
        /// запускает движок состояний StateDriver от пункта stateBegin до stateEnd
        /// </summary>
        /// <param name="stateBegin"> начальное состояние </param>
        /// <param name="stateEnd"> конечное состояние </param>
        public void StateDriverRun(State stateBegin, State stateEnd)
        {
            StateDriver stateDriver = new StateDriver(this, stateBegin, stateEnd);    //this в данном случае есть экземпляр класса botWindow, здесь stateDriver - это начальное состояние движка
            while (!stateDriver.Equals(stateEnd))
            {
                stateDriver.run();
                stateDriver.setState();
            }
        }

        /// <summary>
        /// запускает движок состояний StateDriver от пункта stateBegin до stateEnd
        /// </summary>
        /// <param name="stateBegin"> начальное состояние </param>
        /// <param name="stateEnd"> конечное состояние </param>
        public void StateDriverDealerRun(State stateBegin, State stateEnd)
        {
            StateDriverTrader stateDriverTrader = new StateDriverTrader(stateBegin);
            while (!stateDriverTrader.Equals(stateEnd))
            {
                stateDriverTrader.run();
                stateDriverTrader.setState();
            }
        }


        #endregion



    }
}
