using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace OpenGEWindows
{
    public abstract class MM : Server2
    {

        // ============ переменные ======================

        //protected String ProductName;
        //protected int ProductQuantity;
        //protected int ProductMinPrice;
        //protected int ProductRow;
        //protected int ProductColumn;
        //protected botWindow botwindow;


        protected iPointColor pointIsMMSell1;
        protected iPointColor pointIsMMSell2;
        protected iPointColor pointIsMMBuy1;
        protected iPointColor pointIsMMBuy2;
        protected iPoint pointGotoBuySell;
        protected iPoint pointInitializeButton;
        protected iPoint pointSearchButton;
        protected iPoint pointSearchString;
        protected iPointColor pointIsFirstString1;
        protected iPointColor pointIsFirstString2;
        protected iPoint pointQuantity1;
        protected iPoint pointQuantity2;
        protected iPoint pointPrice;
        protected iPoint pointShadow;
        protected iPoint pointTime;
        protected iPoint pointTime48Hours;
        protected iPointColor pointIsHideFamily1;
        protected iPointColor pointIsHideFamily2;
        protected iPoint pointRegistration;
        protected iPoint pointYesRegistration;
        protected iPoint pointOkRegistration;

        protected Product product;

        /// <summary>
        /// структура для хранения информации о товаре, продаваемом на рынке
        /// </summary>
        protected struct Product
        {
            public String Name;
            public int Quantity;
            public int MinPrice;
            public int Row;
            public int Column;
        }




        // ============  методы  ========================

        /// <summary>
        /// читаем из текстового файла информацию о продаваемом продукте
        /// </summary>
        /// <returns></returns>
        protected String[] LoadProduct(String fileName)
        {
            String[] parametrs = File.ReadAllLines(fileName); // Читаем файл 
            return parametrs;
        }


        /// <summary>
        /// определяет, находимся ли мы на рынке на первой странице, где можно выставить товар на продажу  (MarketManager)
        /// </summary>
        /// <returns></returns>
        public bool isMMSell()
        {
            return ( pointIsMMSell1.isColor() && pointIsMMSell2.isColor() );
        }

        /// <summary>
        /// определяет, находимся ли мы на рынке на странице со списком товаров для покупки (MarketManager)
        /// </summary>
        /// <returns></returns>
        public bool isMMBuy()
        {
            return (pointIsMMBuy1.isColor() && pointIsMMBuy2.isColor());
        }

        /// <summary>
        /// определяет, наша ли строчка первая на рынке (наш ли товар самый дешевый). смотрим, по фамилии выставляющего
        /// </summary>
        /// <returns></returns>
        public bool isMyFirstString()
        {
            return (pointIsFirstString1.isColor() && pointIsFirstString2.isColor());
        }

        /// <summary>
        /// переходим на страницу рынка со списком товаров для покупки
        /// </summary>
        public void GotoPageBuy()
        {
            while (!isMMBuy())
            {
                pointGotoBuySell.PressMouseL();
                Pause(1000);
            }
        }

        /// <summary>
        /// переход на страницу, гже можно выставить товар на продажу
        /// </summary>
        private void GotoPageSell()
        {
            while (!isMMSell())
            {
                pointGotoBuySell.PressMouseL();
                Pause(1000);
            }
        }

        /// <summary>
        /// применяем фильтр (поиск отслеживаемого товара на рынке)
        /// </summary>
        public void ProductSearch()
        {
            if (isMMBuy())    //если мы на странице Buy
            {
                PressButtonInitialize();
                EnterSearchString(product.Name);
                PressButtonSearch();
            }
        }

        /// <summary>
        /// нажимаем кнопку Initialize, чтобы очистить параметры поиска
        /// </summary>
        private void PressButtonSearch()
        {
            pointSearchButton.DoubleClickL();
            Pause(4000);
        }

        /// <summary>
        /// нажимаем кнопку Search, чтобы запустить поиск продукта
        /// </summary>
        private void PressButtonInitialize()
        {
            pointInitializeButton.DoubleClickL();
            Pause(1500);
        }

        /// <summary>
        /// ввести в поисковую строку поисковый запрос
        /// </summary>
        /// <param name="searchString"></param>
        private void EnterSearchString(String searchString)
        {
            pointSearchString.DoubleClickL();
            SendKeys.SendWait(searchString);
            Pause(1000);
        }

        /// <summary>
        /// возвращает цифру от 0 до 9, которая соответствует i-той цифре с конца в цене товара
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Numeral(int i)
        {
            int[] koordX = {491, 483, 475,   463, 455, 447,   435, 427, 419,  407, 399, 391  };
            int koordY = 292;
            return Numeral(koordX[i],koordY);
        }

        /// <summary>
        /// возвращает цифру от 0 до 9, расположенную в указанных координатах (левый верхний угол)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int Numeral(int x, int y)
        {
            iPointColor pointdigit1 = new PointColor(x - 5 + xx + 3, y - 5 + yy + 8, 4030000, 4);  //1
            iPointColor pointdigit2 = new PointColor(x - 5 + xx + 5, y - 5 + yy + 9, 4090000, 4);  //2
            iPointColor pointdigit3 = new PointColor(x - 5 + xx + 2, y - 5 + yy + 4, 3630000, 4);  //3
            iPointColor pointdigit4 = new PointColor(x - 5 + xx + 4, y - 5 + yy + 7, 4030000, 4);  //4
            iPointColor pointdigit5 = new PointColor(x - 5 + xx + 5, y - 5 + yy + 0, 3560000, 4);  //5
            iPointColor pointdigit6 = new PointColor(x - 5 + xx + 3, y - 5 + yy + 3, 2640000, 4);  //6
            iPointColor pointdigit7 = new PointColor(x - 5 + xx + 0, y - 5 + yy + 0, 4030000, 4);  //7
            iPointColor pointdigit8 = new PointColor(x - 5 + xx + 1, y - 5 + yy + 1, 3630000, 4);  //8
            iPointColor pointdigit9 = new PointColor(x - 5 + xx + 2, y - 5 + yy + 5, 3170000, 4);  //9

            if (pointdigit1.isColor()) return 1;
            if (pointdigit2.isColor()) return 2;
            if (pointdigit3.isColor()) return 3;
            if (pointdigit4.isColor()) return 4;
            if (pointdigit5.isColor()) return 5;
            if (pointdigit6.isColor()) return 6;
            if (pointdigit7.isColor()) return 7;
            if (pointdigit8.isColor()) return 8;
            if (pointdigit9.isColor()) return 9;

            return 0;
        }

        /// <summary>
        /// возвращает цену товара в первой строке
        /// </summary>
        /// <returns></returns>
        private int FirstStringPrice()
        {
            int total = 0;                    //итоговое число - цена товара, получаем как 
            int digit = 1;                       //разряд получаемой цифры
            for (int i = 0; i < 10; i++ )
            {
                total = total + Numeral(i) * digit;    
                digit *= 10;
            }
            return total;
        }

        /// <summary>
        /// вводим указанное число в поле количество
        /// </summary>
        /// <param name="quantity"></param>
        private void EnterQuantity(int quantity)
        {
            pointQuantity1.DoubleClickL();
            Pause(500);
            pointQuantity1.Drag(pointQuantity2);
            SendKeys.SendWait(quantity.ToString());
            Pause(500);
        }

        /// <summary>
        /// вводим указанное число в поле цена
        /// </summary>
        /// <param name="quantity"></param>
        private void EnterPrice(int price)
        {
            pointPrice.DoubleClickL();
            Pause(500);
            SendKeys.SendWait(price.ToString());
            Pause(500);
        }

        /// <summary>
        /// положить товар на продажу
        /// </summary>
        private void MoveProductToSell()
        {
            int row = product.Row;
            int column = product.Column;
            iPoint pointInventorty = new Point(840 - 5 + xx, 150 - 5 + yy);     //третья закладка инвентаря
            iPoint pointMM = new Point(405 - 5 + xx, 605 - 5 + yy);
            iPoint pointProductBegin = new Point(700 - 5 + xx + (column - 1) * 39, 180 - 5 + yy + (row - 1) * 38);
            iPoint pointproductEnd = new Point(70 - 5 + xx, 225 - 5 + yy);

            pointInventorty.DoubleClickL();   //делаем инвентарь активным
            Pause(1500);
            pointProductBegin.Drag(pointproductEnd);
            Pause(1000);
            pointMM.PressMouseLL();
        }

        /// <summary>
        /// проверяем, есть ли галка в поле  Hide family name
        /// </summary>
        /// <returns></returns>
        private bool isHideFamily()
        {
            return (pointIsHideFamily1.isColor() && pointIsHideFamily2.isColor());
        }

        /// <summary>
        /// убираем галку "Hide famile name"
        /// </summary>
        private void RemoveShadow()
        {
            if (isHideFamily())
            {
                pointShadow.PressMouse();
                Pause(1000);
            }
        }

        /// <summary>
        /// вводим время, на которое ставим товар на продажу (48 часов)
        /// </summary>
        private void EnterTime()
        {
            pointTime.PressMouseL();
            Pause(1000);
            pointTime48Hours.PressMouseL();
            Pause(1000);
        }

        /// <summary>
        /// нажимаем кнопку регистрация продукта для продажи
        /// </summary>
        private void PressButtonRegistration()
        {
            pointRegistration.PressMouse();
            Pause(2000);
            pointYesRegistration.PressMouse();
            Pause(2000);
            pointOkRegistration.PressMouse();
            Pause(1000);
        }

        /// <summary>
        /// выставить продукт на продажу
        /// </summary>
        public void AddProduct ()
        {
            int myPrice = FirstStringPrice() - 1;

            if (myPrice > product.MinPrice)
            {
                GotoPageSell();
                MoveProductToSell();
                EnterQuantity(product.Quantity);
                EnterPrice(myPrice);
                EnterTime();
                RemoveShadow();
                PressButtonRegistration();
            }

            

        }
    }
}
