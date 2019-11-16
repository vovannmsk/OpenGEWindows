using System;
using System.IO;

namespace GEBot.Data
{
    /// <summary>
    /// Индивидуальные параметры конкретного бота
    /// </summary>
    public class BotParam
    {
        private string directoryOfMyProgram;
        private int numberOfWindow;
        private GlobalParam globalParam;

        private string login;
        private string password;
        private int x;
        private int y;
        private string param;              // синг, америка или европа
        private UIntPtr hwnd;
        private int kanal;
        private int nomerTeleport;
        private string nameOfFamily;
        private int[] triangleX;
        private int[] triangleY;
        private int bullet;
        private int statusOfAtk;

        public string Login { get => login; }
        public string Password { get => password; }
        public int X { get => x; }
        public int Y { get => y; }
        public string Param { get => param; }
        public UIntPtr Hwnd { get => hwnd; set { hwnd = value; SetHwnd(); } }
        public int Kanal { get => kanal; }
        public int NomerTeleport { get => nomerTeleport; }
        public string NameOfFamily { get => nameOfFamily; }
        public int[] TriangleX { get => triangleX; }
        public int[] TriangleY { get => triangleY; }
        public int Bullet { get => bullet; }
        public int StatusOfAtk { get => statusOfAtk; set { statusOfAtk = value; SetStatusAtkInFile(); } }

        /// <summary>
        /// конструктор
        /// </summary>
        public BotParam(int numberOfWindow)
        {
            this.globalParam = new GlobalParam();
            this.directoryOfMyProgram = globalParam.DirectoryOfMyProgram;
            this.numberOfWindow = numberOfWindow;

            this.x = Koord_X();
            this.y = Koord_Y();
            this.login = LoadLogin();
            this.password = Pass();
            this.hwnd = Hwnd_in_file();
            this.param = Parametr();
            this.kanal = Channal();
            this.nomerTeleport = NomerTeleporta();
            this.nameOfFamily = LoadNameOfFamily();
            this.triangleX = LoadTriangleX();
            this.triangleY = LoadTriangleY();
            this.statusOfAtk = GetStatusOfAtk();
        }

        //===================================== методы ==========================================


        /// <summary>
        /// изменяем Hwnd окна и записываем в файл
        /// </summary>
        /// <param name="hwnd"></param>
        public void SetHwnd()
        { File.WriteAllText(directoryOfMyProgram + this.numberOfWindow + "\\HWND.txt", this.hwnd.ToString()); }

        /// <summary>
        /// метод записывает значение статуса в файл, 1 - мы уже приступили у убиванию босса, 0 - нет 
        /// </summary>
        /// <returns> 1 - мы уже приступили у убиванию босса, 0 - нет </returns>
        private void SetStatusAtkInFile()
        {
            File.WriteAllText(directoryOfMyProgram + this.numberOfWindow + "\\StatusOfAtk.txt", this.statusOfAtk.ToString());
        }

        /// <summary>
        /// функция возвращает смещение по оси X окна бота на мониторе
        /// </summary>
        /// <returns></returns>
        private int Koord_X()
        {
            int[] koordX = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 875, 850, 825, 800, 775, 750, 875 };
            //int[] koordX = { 5, 5, 55, 80, 105, 130, 155, 180, 205, 5, 255, 280, 305, 875, 850, 825, 800, 775, 750, 875 };

            return koordX[this.numberOfWindow - 1];
        }

        /// <summary>
        /// функция возвращает смещение по оси Y окна бота на мониторе
        /// </summary>
        /// <returns></returns>
        private int Koord_Y()   // каталог и номер окна
        {
            int[] koordY = { 5, 30, 55, 80, 105, 130, 155, 180, 205, 230, 255, 280, 305, 5, 30, 55, 80, 105, 130, 5 };
            //int[] koordY = { 5, 5, 55, 80, 105, 130, 155, 180, 205, 5, 255, 280, 305, 5, 30, 55, 80, 105, 130, 55 };
            return koordY[this.numberOfWindow - 1];
        }

        /// <summary>
        /// функция возвращает логин бота
        /// </summary>
        /// <returns></returns>
        private String LoadLogin()   // каталог и номер окна
        { return File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Логины.txt"); }

        /// <summary>
        /// функция возвращает пароль от бота
        /// </summary>
        /// <returns></returns>
        private String Pass()   // каталог и номер окна
        { return File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Пароли.txt"); }

        /// <summary>
        /// функция возвращает hwnd бота
        /// </summary>
        /// <returns></returns>
        private UIntPtr Hwnd_in_file()
        {
            UIntPtr ff;
            String ss = File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\HWND.txt");
            if (ss.Equals(""))
            { ff = (UIntPtr)2222222; }   //если пусто в файле, то hwnd = 2222222;
            else
            {
                uint dd = uint.Parse(ss);
                ff = (UIntPtr)dd;
            }
            return ff;
        }

        /// <summary>
        /// функция возвращает имя сервера (Америка, европа или Синг)
        /// </summary>
        /// <returns></returns>
        private String Parametr()
        { return File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Параметр.txt"); }

        /// <summary>
        /// функция возвращает номер канала, где стоит бот
        /// </summary>
        /// <returns></returns>
        private int Channal()
        { return int.Parse(File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Каналы.txt")); }

        /// <summary>
        /// функция возвращает номер телепорта, по которому летим продаваться
        /// </summary>
        /// <returns></returns>
        private int NomerTeleporta()
        { return int.Parse(File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\ТелепортПродажа.txt")); }

        /// <summary>
        /// функция возвращает имя семьи для функции создания новых ботов
        /// </summary>
        /// <returns></returns>
        public string LoadNameOfFamily()
        { return File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Имя семьи.txt"); }

        /// <summary>
        /// функция возвращает тип патронов, которые нужно покупать в автомате патронов
        /// 0 - не нужно покупать
        /// 1 - стальные пули
        /// 2 - картечь
        /// 3 - Болты
        /// 4 - болшой калибр 
        /// 5 - элементальные сферы
        /// 6 - психические сферы
        /// </summary>
        /// <returns></returns>
        private int NumberOfBullets()
        { return int.Parse(File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\Патроны.txt")); }

        /// <summary>
        /// считываем из файла координаты Х расстановки треугольником
        /// </summary>
        /// <returns></returns>
        private int[] LoadTriangleX()
        {
            int SIZE_OF_ARRAY = 3;
            String[] Koord_X = new String[SIZE_OF_ARRAY];
            int[] intKoord_X = new int[SIZE_OF_ARRAY];        //координаты для расстановки треугольником
            Koord_X = File.ReadAllLines(directoryOfMyProgram + this.numberOfWindow + "\\РасстановкаX.txt"); // Читаем файл с Координатами Х в папке с номером Number_Window
            for (int i = 0; i < SIZE_OF_ARRAY; i++) { intKoord_X[i] = int.Parse(Koord_X[i]); }
            return intKoord_X;
        }

        /// <summary>
        /// считываем из файла координаты Y расстановки треугольником
        /// </summary>
        /// <returns></returns>
        private int[] LoadTriangleY()
        {
            int SIZE_OF_ARRAY = 3;
            String[] Koord_Y = new String[SIZE_OF_ARRAY];
            int[] intKoord_Y = new int[SIZE_OF_ARRAY];        //координаты для расстановки треугольником
            Koord_Y = File.ReadAllLines(directoryOfMyProgram + this.numberOfWindow + "\\РасстановкаY.txt"); // Читаем файл с Координатами Y в папке с номером Number_Window
            for (int i = 0; i < SIZE_OF_ARRAY; i++) { intKoord_Y[i] = int.Parse(Koord_Y[i]); }
            return intKoord_Y;
        }

        /// <summary>
        /// метод считывает значение статуса из файла, 1 -  мы уже приступили у убиванию босса, 0 - нет 
        /// </summary>
        /// <returns> 1 -  мы уже приступили у убиванию босса, 0 - нет </returns>
        private int GetStatusOfAtk()
        { return int.Parse(File.ReadAllText(directoryOfMyProgram + this.numberOfWindow + "\\StatusOfAtk.txt")); }



        //public String Login { get; set; }
        //public String Password { get; set; }
        //public int x { get; set; }
        //public int y { get; set; }
        //public String param { get; set; }    
        //public UIntPtr hwnd { get; set; }
        //public int Kanal { get; set; }
        //public int nomerTeleport { get; set; }
        //public String nameOfFamily { get; set; }
        //public int[] triangleX { get; set; }
        //public int[] triangleY { get; set; }
        //public int Bullet { get; set; }                              //используемы тип патронов
        //public int statusOfAtk { get; set; }


        //public int NumberOfAccaunts { get; set; }

    }
}
