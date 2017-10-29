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
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Drawing;

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
        // SELECT * FROM [Bots] WHERE Id = 1
        // SELECT X FROM Bots, coordinates WHERE Bots.Id = 1 AND Bots.Id = coordinates.IdBots   столбец с координатами X
        // SELECT Y FROM Bots, coordinates WHERE Bots.Id = 1 AND Bots.Id = coordinates.IdBots   столбец с координатами Y
        private int x;
        private int y;
        private int numberWindow;       //номер окна
        private String param;           //америка или европа или синг
        private String login;
        private String password;
        private UIntPtr hwnd;
        private const String KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";
        private const int WIDHT_WINDOW = 1024;
        private const int HIGHT_WINDOW = 700;
        private int Kanal;
        private int[] triangleX;
        private int[] triangleY;
        private int nomerTeleport;
        private int NUMBER_OF_ACCOUNTS;
        private int needToChange;
        private ServerInterface server;                 
        private ServerFactory serverFactory;
        private Town town;
        private int counterMitridat;
        private System.DateTime timeMitridat = DateTime.Now;

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

            //всего количество ботов
            this.NUMBER_OF_ACCOUNTS = KolvoAkk();
            this.triangleX = new int[4];
            this.triangleY = new int[4];


            #region Вариант 1. переменные класса подчитываются из текстовых файлов
            this.login = Login();
            this.password = Pass();
            this.hwnd = Hwnd_in_file();
            this.param = Parametr();
            this.Kanal = Channal();
            this.nomerTeleport = NomerTeleporta();
            this.needToChange = NeedToChange();
            this.triangleX = LoadTriangleX();
            this.triangleY = LoadTriangleY();
            #endregion

            #region Вариант 2. переменные класса подчитываются из текстовых файлов

            //var bots = GetBots(number_Window);  //подчитываем строку из БД BotsNew, соответствующую данному боту
            //this.login = bots.Login;
            //this.password = bots.Password;
            //this.hwnd = (UIntPtr)uint.Parse(bots.HWND);
            //this.param = bots.Server;
            //this.Kanal = bots.Channel;
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

            //константы, считанные из массива (const)
            this.x = Koord_X(); //координаты верхней левой точки окна
            this.y = Koord_Y();

            // эти объекты создаются на основании предыдущих переменных класса, а именно param (на каком сервере бот) и nomerTeleport (город продажи)
            this.serverFactory = new ServerFactory(this);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.town = server.getTown();

            // точки для тыканья. универсально для всех серверов
            this.pointArrowUp = new Point(375 + x, 327 + y);   //375, 327);   //шаг = 27 пикселей на одну строчку магазина (на случай если добавят новые строчки)
            this.pointButtonBUY = new Point(725 + x, 620 + y);   //725, 620);
            this.pointButtonClose = new Point(848 + x, 620 + y);   //(848, 620);
            this.pointButtonSell = new Point(725 + x, 620 + y);   //725, 620);
            this.pointArrowUp2 = new Point(375 + x, 246 + y);   //375, 246);
            this.pointBookmarkSell = new Point(226 + x, 196 + y);     //226, 196);
            this.pointNewPlace = new Point(85 + x, 670 + y); //85, 670);
            this.pointButtonSelectChannel = new Point(125 + x, 705 + y); //   125, 705);
            this.pointButtonConnect = new Point(595 - 5 + x, 485 - 5 + y);    // кнопка коннект в логауте (экран еще до казармы)
            this.pointButtonOk = new Point(525 - 5 + x, 425 - 5 + y);    // кнопка коннект в логауте
            this.pointButtonOk2 = new Point(525 - 5 + x, 445 - 5 + y);    // кнопка коннект в логауте

        }

        #region геттеры и сеттеры
        // сеттеры
        public void setHwnd(UIntPtr hwnd)
        { this.hwnd = hwnd; }
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
        { return this.hwnd; }
        public int getNumberWindow()
        { return this.numberWindow; }
        public int getX()
        { return this.x; }
        public int getY()
        { return this.y; }
        public String getParam()
        { return this.param; }
        public int getKanal()
        { return this.Kanal; }
        public int[] getTriangleX()
        { return this.triangleX; }
        public int[] getTriangleY()
        { return this.triangleY; }
        public int getNomerTeleport()
        { return this.nomerTeleport; }
        public bool getNeedToChangeForMainForm()
        { return this.needToChange == 1 ? true : false; }

        //private String getKATALOG_MY_PROGRAM()
        //{ return KATALOG_MY_PROGRAM; }
        //public String getLogin()
        //{
        //    return this.login;
        //}
        //public String getPassword()
        //{
        //    return this.password;
        //}
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
            return SetWindowPos(this.hwnd, 0, this.x, this.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);  //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
        }
        // ========== функция возвращает номер телепорта, по которому летим продаваться и берется из файла "ТелепортПродажа.txt"   =====================
        private int NomerTeleporta()  
        {  return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\ТелепортПродажа.txt"));  }

        // ========== функция возвращает логин окна номер Number_Window ======================
        private String Login()   // каталог и номер окна
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\Логины.txt"); }
        // ========== функция возвращает пароль окна номер Number_Window ======================
        private String Pass()   // каталог и номер окна
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\Пароли.txt"); }
        // ========== функция возвращает hwnd в папке с номером Number_Window ======================
        private UIntPtr Hwnd_in_file()   
        {
            UIntPtr ff;
            String ss = Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\HWND.txt");
            if (ss.Equals(""))
            { ff = (UIntPtr)2222222; }   //если пусто в файле, то hwnd = 2222222;
            else
            {
                uint dd = uint.Parse(ss);
                ff = (UIntPtr)dd;
            }
            return ff;
        }
        // ========== функция возвращает смещение окна по оси Х ======================
        private int Koord_X()
        {
            int[] koordX = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 875, 850, 825, 800, 775, 750, 725, 700 };
            return koordX[this.numberWindow - 1];
        }
        // ========== функция возвращает смещение окна по оси Y ======================
        private int Koord_Y()   // каталог и номер окна
        {
            int[] koordY = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 5, 30, 55, 80, 105, 130, 155, 180 };
            return koordY[this.numberWindow - 1];
        }
        // ========== функция возвращает Параметр из файла Параметр.txt для окна номер number_Window ======================
        private String Parametr()
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\Параметр.txt"); }
        ////  ====== возвращает путь к клиенту ГЕ Америка, который записан в файле America_path.txt ====================
        //private String America_path()
        //{ return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_path.txt"); }
        ////  ====== возвращает путь к клиенту ГЕ Европа, который записан в файле Europa_path.txt ====================
        //private String Europa_path()
        //{ return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_path.txt"); }
        ////  ====== возвращает путь к клиенту ГЕ Синг, который записан в файле ====================
        //private String Sing_path()
        //{ return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_path.txt"); }
        //  ====== возвращает 1, если Америку надо грузить ====================
        //private int America_active()
        //{ return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\America_active.txt")); }
        ////  ====== возвращает 1, если Европу надо грузить ====================
        //private int Europa_active()
        //{ return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\Europa_active.txt")); }
        ////  ====== возвращает 1, если Сингапур надо грузить ====================
        //private int Sing_active()
        //{ return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\Singapoore_active.txt")); }
        // ========== функция возвращает номер канала, где стоит окно номер Number_Window ==============================
        private int Channal()
        { return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + numberWindow + "\\Каналы.txt")); }
        //========================== считываем из файла координаты Х расстановки треугольником =========================
        private int[] LoadTriangleX()
        {
            int SIZE_OF_ARRAY = 4;
            String[] Koord_X = new String[SIZE_OF_ARRAY];
            int[] intKoord_X = new int[SIZE_OF_ARRAY];        //координаты для расстановки треугольником
            Array_File_IO.Read_File_String(KATALOG_MY_PROGRAM + numberWindow + "\\РасстановкаX.txt", ref Koord_X); // Читаем файл с Координатами Х в папке с номером Number_Window
            for (int i = 1; i < SIZE_OF_ARRAY; i++) { intKoord_X[i] = int.Parse(Koord_X[i]); }
            return intKoord_X;
        }
        //========================== считываем из файла координаты Y расстановки треугольником =========================
        private int[] LoadTriangleY()
        {
            int SIZE_OF_ARRAY = 4;
            String[] Koord_Y = new String[SIZE_OF_ARRAY];
            int[] intKoord_Y = new int[SIZE_OF_ARRAY];        //координаты для расстановки треугольником
            Array_File_IO.Read_File_String(KATALOG_MY_PROGRAM + numberWindow + "\\РасстановкаY.txt", ref Koord_Y); // Читаем файл с Координатами Y в папке с номером Number_Window
            for (int i = 1; i < SIZE_OF_ARRAY; i++) { intKoord_Y[i] = int.Parse(Koord_Y[i]); }
            return intKoord_Y;
        }
        /// <summary>
        /// ========= функция возвращает количество аккаунтов, число берется из файла "Аккаунтов всего.txt" =====================
        /// </summary>
        /// <returns></returns>
        public static int KolvoAkk()
        {  return int.Parse(Array_File_IO.Read_File(KATALOG_MY_PROGRAM + "\\Аккаунтов всего.txt")); }
        /// <summary>
        /// функция возвращает имя семьи для функции создания новых ботов
        /// </summary>
        /// <returns></returns>
        public string NameOfFamily()                                                                                                     //07-09-2017
        { return Array_File_IO.Read_File(KATALOG_MY_PROGRAM + numberWindow + "\\Имя семьи.txt"); }                                          
        /// <summary>
        /// ========= метод возвращает значение 1, если нужно передавать песо торговцу. или 0, если не нужно ====================
        /// </summary>
        /// <returns></returns>
        private int NeedToChange()
        {
            int result;
            String LoadString = Array_File_IO.Read_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\НужноПередаватьПесо.txt");
            if (LoadString.Equals("1"))
            { result = 1; }   
            else
            {result = 0;  }
            return result;
        }
        /// <summary>
        /// запись значения NeedToChange в файл 
        /// </summary>
        public void NeedToChangeToFile()
        { Array_File_IO.Write_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\НужноПередаватьПесо.txt", (uint)this.needToChange); }

        #endregion

        #region методы Entity Framework, которые читают из БД значения для последующего присваивания переменным класса

        /// <summary>
        /// читаем из таблицы параметры ботов
        /// </summary>
        /// <returns></returns>
        private BotsNew GetBots(int i)
        {
            var context = new GEContext();

            IQueryable<BotsNew> query = context.BotsNew.Where (c => c.NumberOfWindow == i);

            BotsNew bots = query.Single<BotsNew>();

            return bots;
        }

        /// <summary>
        /// читаем из базы координаты расстановки ботов на карте
        /// </summary>
        /// <returns></returns>
        private List<CoordinatesNew> GetCoordinates(int i)
        {
            var context = new GEContext();

            //IQueryable<CoordinatesNew> query = context.CoordinatesNew.Where(c => c.Id_Bots == i);

            var query = from c in context.CoordinatesNew
                        where c.Id_Bots == i
                        orderby c.NumberOfHeroes
                        select c;

            var coordinates = query.ToList();

            return coordinates;
        }


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
        /// нажать мышью в конкретную точку
        /// дважды будет нажиматься правая кнопка и однажды левая, также к координатам будет прибавляться смещение окна от края монитора getX и getY
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouse(int x, int y)
        {
            PressMouseR(x, y);
            PressMouseR(x, y);
            PressMouseL(x, y);
        } //End PressMouse

        /// <summary>
        /// нажать мышью в конкретную точку только левой кнопкой
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseL(int x, int y)
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x + this.x, y + this.y, 1);    
            Pause(200);
        } 

        /// <summary>
        /// нажать мышью в конкретную точку только правой кнопкой
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseR(int x, int y)
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x + this.x, y + this.y, 8);
            Pause(200);
        }

        /// <summary>
        /// переместить мышь в координаты и покрутить колесо вверх
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseWheelUp(int x, int y)
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x + this.x, y + this.y, 9);
            Pause(200);
        }

        /// <summary>
        /// переместить мышь в координаты и покрутить колесо вниз
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseWheelDown(int x, int y)
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x + this.x, y + this.y, 3);
            Pause(200);
        } 

        /// <summary>
        /// перетаскивание мышью предмета из одних координат в другие (применяется при личной торговле)
        /// </summary>
        /// <param name="x1"> x1 - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y1"> y1 - вторая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="x2"> x2 - первая координата точки, где нужно отпустить кнопку мыши </param>
        /// <param name="y2"> y2 - вторая координата точки, где нужно отпустить кнопку мыши </param>
        public void MouseMoveAndDrop(int x1, int y1, int x2, int y2)
        {
            Click_Mouse_and_Keyboard.MMC(x1 + this.x, y1 + this.y, x2 + this.x, y2 + this.y);
            Pause(200);
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
        /// возвращает цвет пикселя экрана, т.е. не в конкретном окне, а на общем экране 1920х1080.            
        /// </summary>
        /// <param name="x"> x - первая координата проверяемой точки </param>
        /// <param name="y"> y - вторая координата проверяемой точки </param>
        /// <returns> цвет пикселя экрана </returns>
        public uint GetPixelColor(int x, int y)
        {
            IntPtr hwnd = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hwnd, x + this.x, y + this.y);
            ReleaseDC(IntPtr.Zero, hwnd);

            return pixel;
        }                                   //                    serverI

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

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Кликаем в закладку Sell  в магазине 
//        /// </summary>
//        public void Bookmark_Sell()
//        {
//            PressMouseL(225, 163);
//            PressMouseL(225, 163);
//            PressMouseL(225, 163);
//            Pause(1500);
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Продажа товаров в магазине вплоть до маленькой красной бутылки 
//        /// </summary>
//        public void SaleToTheRedBottle()
//        {
//            bool ff = true;
//            int ii = 0;
//            uint color1;
//            while (ff)
//            {
//                color1 = GetPixelColor(142, 219);                 // проверка цвета. бутылка или нет
//                if (color1 == 3360337) { ff = false; }            // Дошли до маленькой бутылки        
//                else
//                {
//                    ii++;
//                    if (ii >= 230) ff = false;     // Страховка против бесконечного цикла
//                    else
//                    {
//                        Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + getX(), 190 + 30 + getY(), 5);        //тыканье в стрелочку вверх + колесо вниз (количество продаваемого товара увеличивается на 1)
//                    }
//                }
//            }//Конец цикла
//            Pause(150);
//            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + getX(), 190 + 30 + getY(), 4); // колесо вверх
//            Pause(150);
////            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(305 + 30 + getX(), 190 + 30 + getY(), 1); // Делаем левый клик по стрелке количества товара(уменьшаем на один)
////            Pause(150);
//            PressMouseL(305 + 30, 190 + 30);
//            //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(305 + 30 + getX(), 190 + 30 + getY(), 1); // Делаем левый клик по стрелке количества товара(уменьшаем на один)
//            //Pause(150);
//            PressMouseL(305 + 30, 190 + 30);
//        }

//        /// <summary>
//        /// определяет, анализируется ли нужный товар либо данный товар можно продавать
//        /// </summary>
//        /// <param name="color"> цвет полностью определяет товар, который поступает на анализ </param>
//        /// <returns> true, если анализируемый товар нужный и его нельзя продавать </returns>
//        public bool NeedTovarOrNot(uint color)
//        {
//            bool result = false;

//            switch (color)                                             // Хорошая вещь или нет, сверяем по картотеке
//            {
//                case 394901:      //soul crystal                 **
//                case 3947742:     //красная бутылка 1200HP       **
//                case 2634708:     //красная бутылка 2500HP       **
//                case 7171437:     // devil whisper               **
//                case 5933520:     // маленькая красная бутылка   **
//                case 1714255:     // митридат                    **
//                case 7303023:     // чугун                       **
//                case 4487528:     // honey                       **
//                case 1522446:     // green leaf                  **
//                case 2112641:     // red leaf                    **
//                case 1533304:     // yelow leaf                  **
//                case 13408291:    // shiny                       **
//                case 3303827:     // карта перса                 **
//                case 6569293:     // warp                        **
//                case 662558:      // head of Mantis              **
//                case 4497887:     // Mana Stone                  **
//                case 7305078:     // Ящики для джеков            **
//                case 15420103:    // Бутылка хрина               **
//                case 9868940:     // композитная сталь           **
//                case 5334831:     // магическая сфера            **
//                    result = true;
//                    break;
//            }

//            return result;
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Посылаем нажатие числа 333 в окно с ботом с помощью команды PostMessage
//        /// </summary>
//        public void Press333()
//        {
//            const int WM_KEYDOWN = 0x0100;
//            const int WM_KEYUP = 0x0101;
//            UIntPtr lParam = new UIntPtr();
//            UIntPtr HWND = FindWindowEx(getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

//            for (int i = 1; i <= 3; i++)
//            {
//                uint dd = 0x00400001;
//                lParam = (UIntPtr)dd;
//                PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D3, lParam);
//                Pause(150);

//                dd = 0xC0400001;
//                lParam = (UIntPtr)dd;
//                PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D3, lParam);
//                Pause(150);
//            }
//        }

//        /// <summary>
//        /// Посылаем нажатие числа 44444 в окно с ботом с помощью команды PostMessage
//        /// </summary>
//        public void Press44444()
//        {
//            const int WM_KEYDOWN = 0x0100;
//            const int WM_KEYUP = 0x0101;
//            UIntPtr lParam = new UIntPtr();
//            UIntPtr HWND = FindWindowEx(getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

//            for (int i = 1; i <= 5; i++)
//            {
//                uint dd = 0x00500001;
//                lParam = (UIntPtr)dd;
//                PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D4, lParam);
//                Pause(150);

//                dd = 0xC0500001;
//                lParam = (UIntPtr)dd;
//                PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D4, lParam);
//                Pause(150);
//            }
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Продажа товара после маленькой красной бутылки, до момента пока прокручивается список продажи
//        /// </summary>
//        public void SaleOverTheRedBottle()
//        {
//            bool ff = true;
//            uint ss2 = 0;
//            uint ss3 = 0;
//            uint ss4 = 0;

//            uint sss2 = 0;
//            uint sss3 = 0;
//            uint sss4 = 0;

//            while (ff)
//            {
//                ss2 = GetPixelColor(149 - 5, 219 - 5);                 // проверка цвета первой точки текущего товара
//                ss3 = GetPixelColor(146 - 5, 219 - 5);                 // проверка цвета третьей точки текущего товара
//                ss4 = GetPixelColor(165 - 5, 214 - 5);                 // проверка цвета второй точки текущего товара
//                Pause(50);
//                if ((ss2 == sss2) & (ss3 == sss3) & (ss4 == sss4)) ff = false;           // если текущий цвет равен предыдущему текущему цвету, значит список не двигается и надо выходить из цикла
//                else
//                {
//                    if (NeedTovarOrNot(ss2))   //если нужный товар
//                    {
//                    }
//                    else // товар не нужен, значит продаем
//                    {
//                        PressMouseL(305 + 30, 190 + 30);                                      //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
//                        Pause(150);
//                        Press44444();
//                    }
//                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(345 + 30 + getX(), 190 + 30 + getY(), 3);        // колесо вниз
//                    Pause(200);  //пауза, чтобы ГЕ успела выполнить нажатие. Можно и увеличить     
//                    sss2 = ss2;
//                    sss3 = ss3;
//                    sss4 = ss4;
//                }
//            }//Конец цикла

//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Продажа товара, когда список уже не прокручивается 
//        /// </summary>
//        public void SaleToEnd()
//        {
//            uint color1;
//            int Y_tovar = 219 + 27;     //координата Y товара, у которого проверяем цвет
//            for (int j = 1; j <= 11; j++)
//            {
//                color1 = GetPixelColor(149 - 5, Y_tovar - 5);                 // проверка цвета текущего товара
//                Pause(50);
//                if (NeedTovarOrNot(color1))   //если нужный товар
//                {
//                }
//                else // товар не нужен, значит продаем
//                {
//                    PressMouseL(360 - 5, Y_tovar - 5);              //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
//                    Pause(150);
//                    Press44444();
//                }
//                Y_tovar = Y_tovar + 27;   //переходим к следующей строке
//            }//Конец цикла
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Кликаем в кнопку BUY  в магазине 
//        /// </summary>
//        public void Botton_BUY()
//        {
//            PressMouseL(725, 663);
//            PressMouseL(725, 663);
//            Pause(2000);
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Кликаем в кнопку Sell  в магазине 
//        /// </summary>
//        public void Botton_Sell()
//        {
//            PressMouseL(725, 663);
//            PressMouseL(725, 663);
//            Pause(2000);
//        }

//        //=============================================================================================== кандидат в абстрактный класс server
//        /// <summary>
//        /// Кликаем в кнопку Close в магазине
//        /// </summary>
//        public void Botton_Close()
//        {
//            PressMouse(847, 663);
//        }


        //        //=============================================================================================== кандидат в абстрактный класс server
        ///// <summary>
        ///// лечение персов нажатием на красную бутылку
        ///// </summary>
        //public void Cure()
        //{
        //    for (int j = 1; j <= 4; j++)
        //    {
        //        PressMouseL(210, 700);
        //        PressMouseL(210 + 255, 700);
        //        PressMouseL(210 + 255 * 2, 700);
        //    }
        //    for (int j = 1; j <= 3; j++)    //жрем патроны (или то, что будет лежать на этом месте под буквой I)
        //    {
        //        PressMouseL(210 + 30, 700);
        //    }

        //}

        ///// <summary>
        ///// вызываем телепорт через верхнее меню и телепортируемся по номеру телепорта 
        ///// </summary>
        ///// <param name="numberTeleport"> номер телепорта по порядку </param>
        //public void Teleport(int numberTeleport)
        //{
        //    Pause(400);
        //    server.TopMenu(12); //Click Teleport menu
        //    PressMouseL(400, 190 + (numberTeleport - 1) * 15);
        //    Pause(50);
        //    PressMouseL(400, 190 + (numberTeleport - 1) * 15);
        //    Pause(200);
        //    PressMouseL(355, 570); //Click on button Execute in Teleport menu
        //    Pause(200);
        //}




        
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
                Click_Mouse_and_Keyboard.Mouse_Move_and_Click(350, 700, 8);
                Pause(200);

            }
            return New_HWND_GE;
        }//Конец метода OpenWindow2


        /// <summary>
        /// восстановливает окно (т.е. переводит из состояния "нет окна" в состояние "логаут", плюс из состояния свернутого окна в состояние развернутого и на нужном месте)
        /// </summary>
        public bool ReOpenWindow()
        {
            setHwnd(Hwnd_in_file());                  //написал 14.11.2016   нуждается в проверке. сначала читаем hwnd из файла, а потом присваиваем его текущему hwnd (this.hwnd)
            bool result = isHwnd();                                //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
            // 26.04.2017  if (!result) setHwnd(OpenWindow());                             //Если вылетело окно, то открываем новое окно с помощью метода OpenWindow и присваиваем новое hwnd 
            //hwnd_to_file();
            if (result)  //26.04.2017 эта строка
            {
                ShowWindow(this.hwnd, 9);                                       // Разворачивает окно если свернуто
                SetForegroundWindow(this.hwnd);                                 // Перемещает окно в верхний список Z порядка     
                BringWindowToTop(this.hwnd);                                    // Делает окно активным                              
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
                SetWindowPos(New_HWND_GE, 0, this.x, this.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);
                Pause(1000);

                EnterLoginAndPasword();

                setHwnd(New_HWND_GE);
            }
            else
            {
                setHwnd((UIntPtr)2222222222);                     // если окно не нужно открывать, то возвращаем это
            }

            hwnd_to_file();     //записали новый hwnd в файл

            return this.hwnd;
        }//Конец метода OpenWindow

        /// <summary>
        /// вводим логин и пароль в соответствующие поля
        /// </summary>
        public void EnterLoginAndPasword()
        {
            // окно открылось, надо вставить логин и пароль
            PressMouseL(505, 350);       //Кликаю в строчку с паролем
            Pause(500);
            PressMouseL(505, 350);       //Кликаю в строчку с паролем
            Pause(500);
            SendKeys.SendWait("{TAB}");
            Pause(500);
            SendKeys.SendWait(this.login);
            Pause(500);
            SendKeys.SendWait("{TAB}");
            Pause(500);
            SendKeys.SendWait(this.password);
            Pause(500);
        }

        /// <summary>
        /// запись HWND в файл
        /// </summary>
        private void hwnd_to_file()
        {
            Array_File_IO.Write_File(KATALOG_MY_PROGRAM + this.numberWindow + "\\HWND.txt", (uint)this.hwnd);

            // обязательно прописать запись hwnd в базу данных Entity Framework
            //var context = new GEContext();
            //IQueryable<BotsNew> query = context.BotsNew.Where (c => c.NumberOfWindow == this.numberWindow);
            //BotsNew bots = query.Single<BotsNew>();
            //bots.HWND = this.hwnd.ToString();
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

            Test_Color = GetPixelColor(50, 50);       //запоминаем цвет в координатах 50, 50 для проверки того, сменился ли экран (т.е. принят ли логин-пароль)
            Tek_Color1 = Test_Color;

            ColorBOOL = (Test_Color == Tek_Color1);
            int counter = 0; //счетчик

            while ((aa | (ColorBOOL)) & (counter < MAX_NUMBER_ITERATION))
            {
                counter++;  //счетчик

                Tek_Color1 = GetPixelColor(50, 50);
                ColorBOOL = (Test_Color == Tek_Color1);
                pointButtonConnect.PressMouse();   // Кликаю в Connect
                //PressMouse(590, 480);            // Кликаю в Connect
                Pause(500);

                //если есть ошибки в логине-пароле, то возникает сообщение с кнопкой "OK". 

                currentColor = GetPixelColor(517, 413);

                if (currentColor == server.colorTest())                                // Обработка Ошибок.  ColorTest - это цвет для сравнения. 
                {
                    pointButtonOk.PressMouse();  //кликаю в кнопку  "ОК"
                    //PressMouse(520, 420);  //кликаю в кнопку  "ОК"
                    Pause(500);

                    currentColor = GetPixelColor(517, 413);
                    if (currentColor == Win32.ColorTest(this.param))
                    {
                        pointButtonOk.PressMouse();  //кликаю в кнопку  "ОК"
//                        PressMouse(520, 420);  //кликаю в кнопку  "ОК"    
                    }
                    pointButtonOk.PressMouseL();  //кликаю в кнопку  "ОК"
//                    PressMouseL(520, 420);     //кликаю в кнопку  "ОК"

                    pointButtonOk2.PressMouseL();  //кликаю в кнопку  "ОК" 3 min

                    EnterLoginAndPasword();
                }
                else
                {
                    aa = false;
                }

            } //while

            bool result = true;
            Pause(5000);
            currentColor = GetPixelColor(50, 50);
            if (currentColor == Test_Color)      //проверка входа в казарму. 
            {
                //тыкнуть в Quit 
                //PressMouseL(600, 530);          //если не вошли в казарму, то значит зависли и жмем кнопку Quit
                //PressMouseL(600, 530);
                result = false;
            }
            return result;
        }

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

            OpenMap2();             //открываем карту города
            Pause(500);

            PressMouseL(322, 350);

            PressEscThreeTimes();      // закрываем карту
            Pause(15000);               //ждем пока добежим
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
            PressMouseL(377, 257);
            Pause(200);

            PressMouseR(377, 257);
            Pause(500);

            //жмем левой  на пункт "Personal Trade"
            PressMouseL(431, 275);
        }

        /// <summary>
        /// обмен песо (часть 2) закрываем сделку со стороны бота
        /// </summary>
        public void ChangeVis2()
        {
            // открываем инвентарь
            server.TopMenu(8, 1);

            // открываем закладку кармана, там где фесо
            PressMouseL(1666 - 700, 329 - 180);
            Pause(500);

            // перетаскиваем песо
            MouseMoveAndDrop(1496 - 700, 361 - 180, 1090 - 700, 536 - 180);                         // песо берется из первой ячейки на этой закладке  
            Pause(500);

            // нажимаем Ок для подтверждения передаваемой суммы песо
            PressMouseL(1305 - 700, 573 - 180);

            // нажимаем ок и обмен
            PressMouseL(1135 - 700, 677 - 180);
            PressMouseL(1216 - 700, 677 - 180);
            Pause(500);
        }

        /// <summary>
        /// начать с выхода в город (нажать на кнопку "начать с нового места")
        /// </summary>
        public void NewPlace()
        {
            pointNewPlace.PressMouse();
        }                                                              //метод не привязан к botwindow






        //  // ======================================================================================== по идее должен быть в server
        /// <summary>
        /// Нажимаем Выбор канала и группы персов
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
            pointChoiceOfChannel = new Point(125 + x, 660 + (this.Kanal - 1) * 15 + server.sdvig() + y);    //переход на нужный канал в казарме
            pointChoiceOfChannel.PressMouseL();
        }

        /// <summary>
        /// выбрать первого (левого) бойца из тройки
        /// </summary>
        public void FirstHero()
        {
            PressMouseR(182, 664);
            PressMouseR(182, 664);
            PressMouseL(182, 635);
        }

        /// <summary>
        /// выбрать второго (среднего) бойца из тройки
        /// </summary>
        public void SecondHero()
        {
            PressMouseR(420, 664);
            PressMouseR(420, 664);
            PressMouseL(420, 635);
        }

        /// <summary>
        /// выбрать третьего (правого) бойца из тройки
        /// </summary>
        public void ThirdHero()
        {
            PressMouseR(670, 664);
            PressMouseR(670, 664);
            PressMouseL(670, 635);
        }

        /// <summary>
        /// проверяет включен ли командный режим или нет 
        /// </summary>
        /// <returns> true, если командный режим включен </returns>
        public bool isCommandMode()
        {
            bool result = true;

            uint color = GetPixelColor(118, 474);

            const uint STANDART_COLOR = 8000000;
            if (color > STANDART_COLOR)
            { result = true; }
            else
            { result = false; }
            return result;
        } 

        /// <summary>
        /// перевод бота в одиночный режим 
        /// </summary>
        public void OneMode()
        {

            if (isCommandMode())
            {
                // если включен командный режим, то надо нажать 1 раз
                PressMouse(118, 484);
            }
            else
            {   // если включен одиночный режим, то надо нажать два раза (в командный режим и обратно)
                PressMouse(118, 484);
                Pause(500);
                PressMouse(118, 484);
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
                PressMouse(118, 484);
                Pause(500);
            }
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
        /// нажимаем пробел (переход в боевой режим)
        /// </summary>
        public void ClickSpace()
        {
            PressMouse(185, 525);   // Кликаю на кнопку "боевой режим"
        }

        ///// <summary>
        ///// Расстановка треугольником на поле боя
        ///// </summary>
        //public void RasstanovkaOneWindow()
        //{
        //    if ((getTriangleX()[1] == 0) | (getTriangleY()[1] == 0))
        //    {
        //        SecondHero();
        //    }
        //    else
        //    {
        //        // ============= переводим в одиночный режим
        //        OneMode();
        //        Pause(500);

        //        // ============= открыть карту через верхнее меню с учетом сервера
        //        //OpenMap();    //локальный метод
        //        server.OpenMapForState();

        //        // ============= нажимаем на первого перса (обязательно на точку ниже открытой карты)
        //        FirstHero();

        //        PressMouseL(getTriangleX()[1], getTriangleY()[1]);

        //        // ============= нажимаем на третьего перса (обязательно на точку ниже открытой карты)
        //        ThirdHero();
        //        PressMouseL(getTriangleX()[3], getTriangleY()[3]);

        //        // ============= нажимаем на второго перса (обязательно на точку ниже открытой карты)
        //        SecondHero();
        //        PressMouseL(getTriangleX()[2], getTriangleY()[2]);

        //        // ============= закрыть карту через верхнее меню
        //        CloseMap();
        //        Pause(1500);

        //        //============== перевод в командный режим  ============
        //        CommandMode();

        //        // ============= нажать "пробел" через меню
        //        ClickSpace();
        //        Pause(500);
        //    }
        //}

        ///// <summary>
        ///// функция проверяет, убит ли хоть один герой из пати (проверка проходит на карте)
        ///// </summary>
        ///// <returns></returns>
        //public bool isKillHero()
        //{
        //    bool result = false;

        //    uint ss, tt, rr = 0;
        //    ss = Okruglenie(GetPixelColor(80 - 5, 636 - 5), 4);  //  проверяем точку в портрете первого героя 
        //    tt = Okruglenie(GetPixelColor(335 - 5, 636 - 5), 4);  //  проверяем точку в портрете второго героя 
        //    rr = Okruglenie(GetPixelColor(590 - 5, 636 - 5), 4);  //  проверяем точку в портрете третьего героя
        //    if (ss == 1900000) result = true;     //если черный цвет, т.е. убит первый перс, то возвращаем true.
        //    if (tt == 1900000) result = true;     //если черный цвет, т.е. убит второй перс, то возвращаем true.
        //    if (rr == 1900000) result = true;     //если черный цвет, т.е. убит третий перс, то возвращаем true.
        //    return result;
        //} //                                                                       кандидат в serverInterface

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
            PressMouseL(200, 200);  //отбегаю ботами в сторону, чтобы они вышли из боевого режима
            PressMouseL(200, 200);  //отбегаю ботами в сторону, чтобы они вышли из боевого режима
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
                PressMouseR(33, 481);                       // Кликаю правой кнопкой в панель с бытылками, чтобы сделать ее активной и поверх всех окон (группа может мешать)
                //Pause(500);
                PressMouseL(31 - 5, 140 - 5);                       // тыкаю в митридат (вторая ячейка)
                //Pause(500);
                PressMouseL(31 - 5, 170 - 5);                       // тыкаю в митридат (третья ячейка)
                //Pause(500);

                SecondHero();                               //выбираю главным второго героя (это нужно, чтобы не было тормозов) типа надо нажать в экран после митридата
                //Pause(1000);
                Pause(500);
                timeMitridat = DateTime.Now;              //обновляю время, когда был применен митридат
                counterMitridat++;
            }
        }

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

        ///// <summary>
        ///// перевод из состояния 16 (в казарме) в состояние 01 (на работе) 
        ///// </summary>
        //public void StateExitFromBarack()
        //{
        //        PressEscThreeTimes();
                
        //}

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

        ///// <summary>
        ///// открыть фесо шоп 
        ///// </summary>
        //public void OpenFesoShop()
        //{
        //    TopMenu(2, 2);
        //    Pause(1000);
        //}

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
        //            result = isColor2(333 - 5, 79 - 5, 13420000, 334 - 5, 79 - 5, 13420000, 4);
        //            break;
        //        case 6:
        //            result = isColor2(460 - 5, 92 - 5, 13420000, 461 - 5, 92 - 5, 13420000, 4);
        //            break;
        //        case 8:
        //            result = isColor2(558 - 5, 92 - 5, 13420000, 559 - 5, 92 - 5, 13420000, 4);
        //            break;
        //        case 9:
        //            result = isColor2(606 - 5, 79 - 5, 13420000, 607 - 5, 79 - 5, 13420000, 4);
        //            break;
        //        case 12:
        //            result = isColor2(411 - 5, 171- 5, 7590000, 412 - 5, 171 - 5, 7850000, 4);
        //            break;
        //        case 13:
        //            result = isColor2(371 - 5, 278 - 5, 16310000, 372 - 5, 278 - 5, 16510000, 4);
        //            break;
        //        default:
        //            result = true;
        //            break;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// метод проверяет, открылось ли меню с петом Alt + P
        ///// </summary>
        ///// <returns> true, если открыто </returns>
        //public bool isOpenMenuPet()
        //{
        //    return isColor2(471 - 5, 219 - 5, 12500000, 472 - 5, 219 - 5, 12500000, 5);
        //}

    
        /// <summary>
        /// проверяем, есть ли проблемы с ботом (убили, застряли, нужно продать)
        /// </summary>
        public void checkForProblems()  //используется
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
                                        if ( ( server.isTown() ) || ( server.isTown_2() ) )          //если стоят в городе (проверка по обоим стойкам - эксп.дробаш и ружье )         //**   было istown2()
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
        }                                                                  //зеленая кнопка


    }
}

//======================================================== ферма =================================================================
        ////======================================= синтезирует из листочков на ферме другую фигню =============================
        //public static void ferma(String KatalogMyProgram, int Number_Window)
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);

        //    // ============= Удаляем камеру  =============================================================================
        //    MaxHeight(KatalogMyProgram, Number_Window);
        //    Class_Timer.Pause(1000);

        //    OpenMap(KatalogMyProgram, Number_Window);

        //    GoToFermaMap(KatalogMyProgram, Number_Window);

        //    //ClickMoveMapFerma(KatalogMyProgram, Number_Window);

        //    CloseReklama2(KatalogMyProgram, Number_Window);

        //    zarjadka1(KatalogMyProgram, Number_Window);

        //    zarjadka2(KatalogMyProgram, Number_Window);

        //    zarjadka3(KatalogMyProgram, Number_Window);


        //}

        //static public void zarjadka1(String KatalogMyProgram, int Number_Window)
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);


            

        //}
        //static public void zarjadka2(String KatalogMyProgram, int Number_Window)
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);


            

        //}
        //static public void zarjadka3(String KatalogMyProgram, int Number_Window)
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);


            

        //}


        //static public bool GoToFermaMap(String KatalogMyProgram, int Number_Window)
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);
        //    int numberteleport = NomerTeleporta(KatalogMyProgram, Number_Window);

        //    //int numberteleportAm = NomerTeleportaAmerica(KatalogMyProgram);
        //    //int numberteleportEu = NomerTeleportaEuropa(KatalogMyProgram);


        //    switch (Par)    // проверяем, какой используется клиент игры (америка или европа) 
        //    {
        //        case "C:\\America\\":
        //        case "C:\\America2\\":
        //        case "C:\\America3\\":
        //                    //Class_Timer.Pause(500);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1040 - 305 + kanal, 467 - 305 + y, 8);
        //                    //Class_Timer.Pause(200);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1040 - 305 + kanal, 467 - 305 + y, 8);
        //                    //Class_Timer.Pause(200);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1040 - 305 + kanal, 467 - 305 + y, 1);
        //                    Class_Timer.Pause(500);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(742 - 305 + x, 816 - 305 + y, 8);
        //                    Class_Timer.Pause(200);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(742 - 305 + x, 816 - 305 + y, 8);
        //                    Class_Timer.Pause(200);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(742 - 305 + x, 816 - 305 + y, 1);
        //            break;
        //        case "C:\\Europa\\":

        //            break;
        //    }



        //    return true;
        //}

        //static public bool ClickMoveMapFerma(String KatalogMyProgram, int Number_Window)             //вторая закладка
        //{

        //    int x = Koord_X(KatalogMyProgram, Number_Window);
        //    int y = Koord_Y(KatalogMyProgram, Number_Window);
        //    String Par = Parametr(KatalogMyProgram, Number_Window);
        //    int numberteleport = NomerTeleporta(KatalogMyProgram, Number_Window);
        //    //int numberteleportAm = NomerTeleportaAmerica(KatalogMyProgram);
        //    //int numberteleportEu = NomerTeleportaEuropa(KatalogMyProgram);


        //    switch (Par)    // проверяем, какой используется клиент игры (америка или европа) 
        //    {
        //        case "C:\\America\\":
        //        case "C:\\America2\\":
        //        case "C:\\America3\\":
        //                    Class_Timer.Pause(500);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1130 - 305 + x, 936 - 305 + y, 8);
        //                    Class_Timer.Pause(200);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1130 - 305 + x, 936 - 305 + y, 8);
        //                    Class_Timer.Pause(200);
        //                    Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1130 - 305 + x, 936 - 305 + y, 1);
        //            break;
        //        case "C:\\Europa\\":

        //                    //Class_Timer.Pause(500);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1795 - 875 + kanal, 710 - 5 + y, 8);
        //                    //Class_Timer.Pause(200);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1795 - 875 + kanal, 710 - 5 + y, 8);
        //                    //Class_Timer.Pause(200);
        //                    //Click_Mouse_and_Keyboard.Mouse_Move_and_Click(1795 - 875 + kanal, 710 - 5 + y, 1);
        //            break;
        //    }

        //    Class_Timer.Pause(200);

        //    return true;
        //}
