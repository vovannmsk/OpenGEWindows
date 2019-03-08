using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalParametrs
{
    public  class GlobalParam
    {
        private  int nintendo;
        private  int startingAccount;
        private  bool samara;
        private  string[] mmProduct;
        private  int totalNumberOfAccounts;
        private  int statusOfSale;
        private const string KATALOG_MY_PROGRAM = "C:\\!! Суперпрограмма V&K\\";

        /// <summary>
        /// конструктор
        /// </summary>
        public GlobalParam()
        {
            this.nintendo = TypeOfNintendo();
            this.startingAccount = BeginAcc();
            this.samara = IsSamara();
            this.mmProduct = LoadProduct();
            this.totalNumberOfAccounts = KolvoAkk();
            this.statusOfSale = GetStatusOfSale();
        }

        // ==================================================== Методы ============================================================

        //  ====== геттеры и сеттеры =============
        public int Nintendo { get => nintendo; set => nintendo = value; }
        public int StartingAccount { get => startingAccount; set => startingAccount = value; }
        public bool Samara { get => samara; }
        public string[] MMProduct { get => mmProduct; }
        public int TotalNumberOfAccounts { get => totalNumberOfAccounts; }
        public int StatusOfSale { get => statusOfSale; set { statusOfSale = value; SetStatusInFile(); } }
    
        // =================== прочие методы ===============================

        /// <summary>
        /// возвращаем тип чиповки
        /// 1 - без рассы
        /// 2 - wild
        /// 3 - LifeLess
        /// 4 - wild or Human
        /// 5 - Undeed
        /// 6 - Demon
        /// 7 - Human
        /// </summary>
        /// <returns></returns>
        private int TypeOfNintendo()
        { return int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\Чиповка.txt")); }

        /// <summary>
        /// метод считывает значение статуса из файла, 1 - мы направляемся на продажу товара в магазин, 0 - нет (обычный режим работы)
        /// </summary>
        /// <returns></returns>
        private int GetStatusOfSale()
        { return int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\StatusOfSale.txt")); }

        /// <summary>
        /// метод записывает значение статуса в файл, 1 - мы направляемся на продажу товара в магазин, 0 - нет (обычный режим работы)
        /// </summary>
        /// <returns></returns>
        private void SetStatusInFile()
        {
            File.WriteAllText(KATALOG_MY_PROGRAM + "\\StatusOfSale.txt", this.statusOfSale.ToString());
        }

        /// <summary>
        /// возвращаеи количество аккаунтов
        /// </summary>
        /// <returns>кол-во акков всего</returns>
        private static int KolvoAkk()
        {
            return int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\Аккаунтов всего.txt"));
        }

        /// <summary>
        /// читаем из файла значение
        /// </summary>
        /// <returns>с какого аккаунта начать работу методам</returns>
        private static int BeginAcc()
        {
            return int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\СтартовыйАккаунт.txt"));
        }

        /// <summary>
        /// читаем из текстового файла информацию о продаваемом продукте
        /// </summary>
        /// <returns></returns>
        private String[] LoadProduct()
        {
            String[] parametrs = File.ReadAllLines(KATALOG_MY_PROGRAM + "Продукт.txt"); 
            return parametrs;
        }

        /// <summary>
        /// метод возвращает параметр, который указывает, является ли данный компьютер удаленным сервером или локальным компом (различная обработка мыши)
        /// </summary>
        /// <returns> true, если комп является удаленным сервером (из Самары) </returns>
        private bool IsSamara()
        {
            int result = int.Parse(File.ReadAllText(KATALOG_MY_PROGRAM + "\\Сервер.txt"));

            bool isSamara = false;
            if (result == 1) isSamara = true;

            return isSamara;
        }

    }
}