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
using GEBot.Data;


namespace OpenGEWindows
{
    public class botWindow
    {
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

        private DataBot databot;              //начальные данные для бота (заданные пользователем)
        private IScriptDataBot scriptDataBot;

        private Server server;                 
        private Market market;
        private Pet pet;
        private Otit otit;
        private Dialog dialog;

        //private int counterMitridat;
        //private System.DateTime timeMitridat = System.DateTime.Now;

        private iPoint pointButtonClose;
        private iPoint pointOneMode;
        private bool isServerVersion;


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


            #region Вариант 1. переменные класса подчитываются из текстовых файлов
                this.databot = LoadUserDataBot(TypeLoadUserData.txt);
            #endregion

            #region Вариант 2. переменные класса подчитываются из БД
                //this.databot = LoadUserDataBot(TypeLoadUserData.db);

            #endregion

            // эти объекты создаются на основании предыдущих переменных класса, а именно param (на каком сервере бот) и nomerTeleport (город продажи)
            ServerFactory serverFactory = new ServerFactory(this);
            this.server = serverFactory.createServer();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            MarketFactory marketFactory = new MarketFactory(this);
            this.market = marketFactory.createMarket();
            PetFactory petFactory = new PetFactory(this);
            this.pet = petFactory.createPet();
            OtitFactory otitFactory = new OtitFactory(this);
            this.otit = otitFactory.createOtit();
            DialogFactory dialogFactory = new DialogFactory(this);
            this.dialog = dialogFactory.createDialog();


            // точки для тыканья. универсально для всех серверов
            this.pointButtonClose = new Point(850 - 5 + databot.x, 625 - 5 + databot.y);   //(848, 620);
            this.pointOneMode = new Point(123 - 5 + databot.x, 489 - 5 + databot.y);    // 118, 484
            this.isServerVersion = isServerVer();
        }

        // ============================== методы ============================================



        #region геттеры и сеттеры

        public void setHwnd(UIntPtr hwnd)
        { 
            databot.hwnd = hwnd; 
            hwnd_to_file(); 
        }
        public DataBot getDataBot()
        { return this.databot; }
        public Server getserver()
        {
            return this.server;
        }
        public Market getMarket()
        {
            return this.market;
        }
        public Pet getPet()
        {
            return this.pet;
        }
        public Otit getOtit()
        {
            return this.otit;
        }
        public Dialog getDialog()
        {
            return this.dialog;
        }


        public bool getIsServer()
        {
            return isServerVersion;
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
        public String getNameOfFamily()
        {
            return databot.nameOfFamily;
        }

        #endregion

        //#region методы для перекладывания песо в торговца
        
        ///// <summary>
        ///// открыть фесо шоп
        ///// </summary>
        //public void OpenFesoShop()
        //{
        //    server.TopMenu(2, 2);
        //    Pause(1000);
        //}

        ///// <summary>
        ///// для передачи песо торговцу. Идем на место и предложение персональной торговли                                          ////////////// перенести в Server
        ///// </summary>
        //public void ChangeVis1()
        //{
        //    iPoint pointTrader = new Point(472 - 5 + databot.x, 175 - 5 + databot.y);    
        //    iPoint pointPersonalTrade = new Point(536 - 5 + databot.x, 203 - 5 + databot.y);
        //    iPoint pointMap = new Point(405 - 5 + databot.x, 220 - 5 + databot.y);    

        //    //идем на место передачи песо
        //    PressEscThreeTimes();
        //    Pause(1000);

        //    town.MaxHeight();             //с учетом города и сервера
        //    Pause(500);

        //    server.OpenMapForState();                  //открываем карту города
        //    Pause(500);

        //    pointMap.DoubleClickL();   //тыкаем в карту, чтобы добежать до нужного места

        //    PressEscThreeTimes();       // закрываем карту
        //    Pause(25000);               // ждем пока добежим

        //    iPointColor pointMenuTrade = new PointColor(588 - 5 + databot.x, 230 - 5 + databot.y, 1710000 , 4);
        //    while (!pointMenuTrade.isColor())
        //    {
        //        //жмем правой на торговце
        //        pointTrader.PressMouseR();
        //        Pause(1000);
        //    }

        //    //жмем левой  на пункт "Personal Trade"
        //    pointPersonalTrade.PressMouseL();
        //    Pause(500);
        //}

        ///// <summary>
        ///// обмен песо (часть 2) закрываем сделку со стороны бота
        ///// </summary>
        //public void ChangeVis2()
        //{
        //    iPoint pointVis1 = new Point(903 - 5 + databot.x, 151 - 5 + databot.y);    
        //    iPoint pointVisMove1 = new Point(701 - 5 + databot.x, 186 - 5 + databot.y);
        //    iPoint pointVisMove2 = new Point(395 - 5 + databot.x, 361 - 5 + databot.y);
        //    iPoint pointVisOk = new Point(611 - 5 + databot.x, 397 - 5 + databot.y);   
        //    iPoint pointVisOk2 = new Point(442 - 5 + databot.x, 502 - 5 + databot.y);  
        //    iPoint pointVisTrade = new Point(523 - 5 + databot.x, 502 - 5 + databot.y);  

        //    // открываем инвентарь
        //    server.TopMenu(8, 1);

        //    // открываем закладку кармана, там где песо
        //    pointVis1.DoubleClickL();
        //    Pause(500);

        //    // перетаскиваем песо
        //    pointVisMove1.Drag(pointVisMove2);                                             // песо берется из первой ячейки на 4-й закладке  
        //    Pause(500);

        //    // нажимаем Ок для подтверждения передаваемой суммы песо
        //    pointVisOk.DoubleClickL();

        //    // нажимаем ок
        //    pointVisOk2.DoubleClickL();
        //    Pause(500);

        //    // нажимаем обмен
        //    pointVisTrade.DoubleClickL();
        //    Pause(500);
        //}

        ///// <summary>
        ///// купить 400 еды в фесо шопе                    вообще-то метод должен находится в ServerInterface
        ///// </summary>
        //public void Buy44PetFood()
        //{
        //    iPoint pointFood = new Point(361 - 5 + databot.x, 331 - 5 + databot.y);     //шаг = 27 пикселей на одну строчку магазина (на случай если добавят новые строчки)
        //    iPoint pointButtonBUY = new Point(730 - 5 + databot.x, 625 - 5 + databot.y);   //725, 620);

        //    // тыкаем два раза в стрелочку вверх
        //    pointFood.DoubleClickL();
        //    Pause(500);

        //    //нажимаем 125
        //    SendKeys.SendWait("125");

        //    // жмем кнопку купить
        //    pointButtonBUY.DoubleClickL();
        //    Pause(1500);

        //    //нажимаем кнопку Close
        //    pointButtonClose.DoubleClickL();
        //    Pause(1500);
        //}                                                                        

        ///// <summary>
        ///// продать 3 ВК (GS) в фесо шопе 
        ///// </summary>
        //public void SellGrowthStone3pcs()
        //{
        //    iPoint pointArrowUp2 = new Point(379 - 5 + databot.x, 250 - 5 + databot.y); 
        //    iPoint pointButtonSell = new Point(730 - 5 + databot.x, 625 - 5 + databot.y);   

        //    // 3 раза нажимаем на стрелку вверх, чтобы отсчитать 3 ВК
        //    for (int i = 1; i <= 3; i++)
        //    {
        //        pointArrowUp2.PressMouseL();
        //        Pause(700);
        //    }

        //    //нажимаем кнопку Sell
        //    pointButtonSell.PressMouseL();
        //    Pause(1000);

        //    //нажимаем кнопку Close
        //    pointButtonClose.PressMouseL();
        //    Pause(2500);
        //}                                 

        ///// <summary>
        ///// открыть вкладку Sell в фесо шопе
        ///// </summary>
        //public void OpenBookmarkSell()
        //{
        //    iPoint pointBookmarkSell = new Point(245 - 5 + databot.x, 201 - 5 + databot.y); 
        //    pointBookmarkSell.DoubleClickL();
        //    Pause(1500);
        //}                                 

        //#endregion

        #region Общие методы

        /// <summary>
        /// Перемещает окно с ботом в заданные координаты. Если окно есть, то result = true, а если вылетело окно, то result = false.
        /// </summary>
        /// <returns></returns>
        public bool isHwnd()
        {
            //не учитываются ширина и высота окна
            return SetWindowPos(databot.hwnd, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0001);  //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
            

            //return SetWindowPos(databot.hwnd, 0, databot.x, databot.y, WIDHT_WINDOW, HIGHT_WINDOW, 0x0040);  //Перемещает в заданные координаты. Если окно есть, то result=true, а если вылетело окно, то result=false.
        }

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
        /// запись HWND в файл
        /// </summary>
        private void hwnd_to_file()
        {
            scriptDataBot.SetHwnd(databot.hwnd);
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
        /// метод возвращает параметр, который указывает, является ли данный компьютер удаленным сервером или локальным компом (различная обработка мыши)
        /// </summary>
        /// <returns>true, если комп является удаленным сервером</returns>
        private bool isServerVer()
        { 
            int result = int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\Сервер.txt"));

            bool isServer = false;
            if (result == 1) isServer = true;

            return isServer;
        }


        #endregion

        #region No Window

        /// <summary>
        /// активируем окно
        /// </summary>
        private void ActiveWindow()
        {
            ShowWindow(databot.hwnd, 9);                                       // Разворачивает окно если свернуто  было 9
            SetForegroundWindow(databot.hwnd);                                 // Перемещает окно в верхний список Z порядка     
            //BringWindowToTop(databot.hwnd);                                    // Делает окно активным и Перемещает окно в верхний список Z порядка     

            //не учитываются ширина и высота окна
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

                while (!server.isLogout()) Pause(1000);    //ожидание логаута        бесконечный цикл

                ActiveWindow();
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
            server.runClient();    ///запускаем клиент игры и ждем 30 сек
            while (true)
            {
                Pause(5000);
                UIntPtr hwnd = server.FindWindowGE();          //ищем окно ГЭ с нужными параметрами
                if (hwnd != (UIntPtr)0) break;
            }
            Pause(20000);

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


        #endregion

        #region Logout

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
            iPoint pointButtonOk = new Point(525 - 5 + databot.x, 410 - 5 + databot.y);    // кнопка Ok в логауте
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

            server.serverSelection();          //17-05-2018

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


        #endregion

        #region Pet
        #endregion

        #region TopMenu
        #endregion

        #region Shop
        #endregion

        #region atWork

        /// <summary>
        /// расстановка героев треугольником
        /// </summary>
        public void Placement()
        {
            iPoint pointFirstHero = new Point(databot.triangleX[0] + databot.x, databot.triangleY[0] + databot.y);
            iPoint pointSecondHero = new Point(databot.triangleX[1] + databot.x, databot.triangleY[1] + databot.y);
            iPoint pointThirdHero = new Point(databot.triangleX[2] + databot.x, databot.triangleY[2] + databot.y);

            // ============= нажимаем на первого перса (обязательно на точку ниже открытой карты)
            FirstHero();
            pointFirstHero.PressMouseL();
            Pause(300);  //1000
            //PressMouseL(databot.triangleX[0], databot.triangleY[0]);

            // ============= нажимаем на третьего перса (обязательно на точку ниже открытой карты)
            ThirdHero();
            pointThirdHero.PressMouseL();
            Pause(300);
            //PressMouseL(databot.triangleX[2], databot.triangleY[2]);

            // ============= нажимаем на второго перса (обязательно на точку ниже открытой карты)
            SecondHero();
            pointSecondHero.PressMouseL();
            Pause(300);
            //PressMouseL(databot.triangleX[1], databot.triangleY[1]);

            // ============= закрыть карту через Esc =======================
            CloseMap();
            //Pause(1500);
        }

        /// <summary>
        /// выбрать первого (левого) бойца из тройки
        /// </summary>
        public void FirstHero()
        {
            iPoint pointFirstHeroL = new Point(187 - 5 + databot.x, 640 - 5 + databot.y);    // 182, 635
            iPoint pointFirstHeroR = new Point(177 - 5 + databot.x, 669 - 5 + databot.y);    // 182, 664
            //pointFirstHeroR.PressMouseR();
            //pointFirstHeroR.PressMouseR();
            pointFirstHeroR.DoubleClickL();  //вместо двух строк поставил одну

            pointFirstHeroL.PressMouseL();
        }

        /// <summary>
        /// выбрать второго (среднего) бойца из тройки
        /// </summary>
        public void SecondHero()
        {
            iPoint pointSecondHeroL = new Point(425 - 5 + databot.x, 640 - 5 + databot.y);    // 420, 635
            iPoint pointSecondHeroR = new Point(425 - 5 + databot.x, 669 - 5 + databot.y);    // 420, 664
            //pointSecondHeroR.PressMouseR();
            //pointSecondHeroR.PressMouseR();
            pointSecondHeroR.DoubleClickL();  //вместо двух строк поставил одну

            pointSecondHeroL.PressMouseL();
        }

        /// <summary>
        /// выбрать третьего (правого) бойца из тройки
        /// </summary>
        public void ThirdHero()
        {
            iPoint pointThirdHeroL = new Point(675 - 5 + databot.x, 640 - 5 + databot.y);    // 670, 635
            iPoint pointThirdHeroR = new Point(675 - 5 + databot.x, 669 - 5 + databot.y);    // 670, 664
            //pointThirdHeroR.PressMouseR();
            //pointThirdHeroR.PressMouseR();
            pointThirdHeroR.DoubleClickL();  //вместо двух строк поставил одну
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
        /// Лечение одного окна, если побили всех персов (лечение состоит в закрытии окна с ботом)
        /// </summary>
        public void CureOneWindow()
        {
            // ================================= убирает все лишние окна с экрана =========================================
            PressEscThreeTimes();
            Pause(1000);

            //server.GoToEnd();
            server.Logout();
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
            Pause(2000);

            //server.GoToEnd();
            server.Logout();
        }

        /// <summary>
        /// Нажать на бутылку митридата, которая лежит во второй ячейке                                перенести в server
        /// </summary>
        public void PressMitridat()
        {
            iPoint pointPanel = new Point(38 - 5 + databot.x, 486 - 5 + databot.y);    // 33, 481
            iPoint pointSecondBox = new Point(31 - 5 + databot.x, 140 - 5 + databot.y);
            iPoint pointThirdBox = new Point(31 - 5 + databot.x, 170 - 5 + databot.y);
            iPoint pointFourthBox = new Point(31 - 5 + databot.x, 200 - 5 + databot.y);
            iPoint pointFifthBox = new Point(31 - 5 + databot.x, 230 - 5 + databot.y);

            //System.DateTime timeNow = DateTime.Now;  //текущее время
            //System.TimeSpan PeriodMitridat = timeNow.Subtract(timeMitridat);   //сколько времени прошло с последнего применения митридата
            //uint PeriodMitridatSeconds = (uint)PeriodMitridat.TotalSeconds;          //сколько времени прошло с последнего применения митридата в секундах
            //if ((PeriodMitridatSeconds >= 600) | (counterMitridat == 0))
            //{

            pointPanel.PressMouseR();                   // Кликаю правой кнопкой в панель с бытылками, чтобы сделать ее активной и поверх всех окон (группа может мешать)
            pointSecondBox.PressMouseL();               // тыкаю в митридат (вторая ячейка)
            pointThirdBox.PressMouseL();                // тыкаю в  (третья ячейка)
            //pointFourthBox.PressMouseL();                // тыкаю в  (4-ю ячейку)
            //pointFifthBox.PressMouseL();                // тыкаю в  (5-ю ячейку)


            if (server.isSecondHero())                  //если есть второй перс ()
            {
                SecondHero();                           //выбираю главным второго героя (это нужно, чтобы не было тормозов) типа надо нажать в экран после митридата
                Pause(500);
            }
            else                                        //если бегаем одним персом
            {
                FirstHero();
                Pause(500);
            }

            //    timeMitridat = DateTime.Now;              //обновляю время, когда был применен митридат
            //    counterMitridat++;
            //}
        }


        #endregion

        #region inTown

        /// <summary>
        /// отодвинуть мышку в сторону, чтобы она не загораживала проверяемые точки
        /// </summary>
        public void ToMoveMouse()
        {
            iPoint pointToMoveMouse = new Point(205 - 5 + databot.x, 575 - 5 + databot.y);    //
            pointToMoveMouse.PressMouseR();
        }

        #endregion

        #region Barack

        ///// <summary>
        ///// начать с выхода в город (нажать на кнопку "начать с нового места")
        ///// </summary>
        //public void NewPlace()
        //{
        //    iPoint pointNewPlace = new Point(85 + databot.x, 670 + databot.y); //85, 670);
        //    pointNewPlace.DoubleClickL();
        //}

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

        #endregion

        #region новые боты
        #endregion

        #region кратер
        #endregion

        #region заточка
        #endregion

        #region чиповка
        #endregion

        #region Personal Trade 
        #endregion


    }
}
