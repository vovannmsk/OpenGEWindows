using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace OpenGEWindows
{
    public abstract class Market : Server2
    {

        // ============ переменные ======================

        #region Shop

        protected iPointColor pointIsSale1;
        protected iPointColor pointIsSale2;
        protected iPointColor pointIsSale21;
        protected iPointColor pointIsSale22;
        protected iPointColor pointIsClickSale1;
        protected iPointColor pointIsClickSale2;
        protected iPoint pointBookmarkSell;
        protected iPoint pointSaleToTheRedBottle;
        protected iPoint pointSaleOverTheRedBottle;
        protected iPoint pointWheelDown;
        protected iPoint pointButtonBUY;
        protected iPoint pointButtonSell;
        protected iPoint pointButtonClose;
        protected iPoint pointBuyingMitridat1;
        protected iPoint pointBuyingMitridat2;
        protected iPoint pointBuyingMitridat3;

        /// <summary>
        /// структура для сравнения товаров в магазине
        /// </summary>
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

        #endregion

        protected Dialog dialog;


        // ============  методы  ========================

        #region Shop

        /// <summary>
        /// Кликаем на строчку Sell и кнопку "Ok" в магазине   
        /// </summary>
        public void ClickSellAndOkInTrader()
        {
            dialog.PressStringDialog(1);  ////========= тыкаем в "Sell/Buy Items" ======================================
            dialog.PressOkButton(1);      ////========= тыкаем в OK =======================
        }

        /// <summary>
        /// проверяет, находится ли данное окно в магазине (а точнее на странице входа в магазин) 
        /// </summary>
        /// <returns> true, если находится в магазине </returns>
        public bool isSale()
        {
            return ((pointIsSale1.isColor()) && (pointIsSale2.isColor()));
        }

        /// <summary>
        /// проверяет, находится ли данное окно в самом магазине (на закладке BUY или SELL)                                       
        /// </summary>
        /// <returns> true, если находится в магазине </returns>
        public bool isSale2()
        {
            return ((pointIsSale21.isColor()) && (pointIsSale22.isColor()));
        }

        /// <summary>
        /// проверяет, открыта ли закладка Sell в магазине 
        /// </summary>
        /// <returns> true, если закладка Sell в магазине открыта </returns>
        public bool isClickSell()
        {
            return ((pointIsClickSale1.isColor()) && (pointIsClickSale2.isColor()));
        }

        /// <summary>
        /// Кликаем в закладку Sell  в магазине 
        /// </summary>
        public void Bookmark_Sell()
        {
            pointBookmarkSell.DoubleClickL();
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
            Pause(200);
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
            iPointColor pointMega = new PointColor();

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
                    result = false;
                    break;
                case 14210771:     // Mega Etr, Io Talt
                case 9803667:     // Mega A
                case 7645105:     // Mega Qu
                    result = true;
                    break;
            }

            return result;
        }

        /// <summary>
        /// определяет, анализируется ли нужный товар либо данный товар можно продавать
        /// </summary>
        /// <param name="color"> цвет полностью определяет товар, который поступает на анализ </param>
        /// <returns> true, если анализируемый товар нужный и его нельзя продавать </returns>
        public bool NeedToSellProduct(uint color, int numberOfString)
        {
            bool result = true;   //по умолчанию вещь надо продаывать, поэтому true
            iPointColor pointMega = new PointColor(174 - 5 + xx, 214 - 5 + yy + (numberOfString - 1) * 27, 10700000, 5);  //буква M в слове Mega

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
                case 5334831:     // магическая сфера            ** Magic sphere
                case 13164006:    // свекла  Beet                **
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
                case 5072004:     // Bone Stick                  **
                case 3559777:     // Dragon Lether               **
                case 1712711:     // Dragon Horn                 **
                case 6719975:     // Wild Boar Meat              **
                case 4448154:     // Green ore                   **
                case 13865807:    // blue ore                    **
                case 4670431:     // Red ore                     **
                case 13291199:    // Diamond Ore                 **
                case 1063140:     // Stone of Philos             **
                case 8486756:     // Ice Crystal                 **
                case 4143156:     // bulk of Coal                **
                case 9472397:     // Steel piece                 **
//                case 7187897:     // Mustang ore
                case 1381654:     // стрелы эксп
                case 11258069:    // пули эксп
                case 2569782:     // дробь эксп Metal Shell Ammo
                case 5137276:     // сундук деревянный как у сфер древней звезды
//                case 3031912:     // Reinforced Lether
                case 13667914:    // 600 SP
                case 2831927:     // Sign G, D
                case 2828377:     // Ancient Orb
                case 8363835:     // Icicle
                case 12642302:    // Bone pick
                case 15790834:    // Soft Cotton
                case 4543325:     // Pebbles
                case 1457773:     // Old Journal
                case 10401925:    // Sharp Horn
                case 6270101:     // Cabosse
                case 14344416:    // Tough Cotton
                case 13079681:    // Silk
                case 14278629:    // Chip 100
                case 14542297:    // Chip Veteran
                case 13417421:    // Octopus Arm
                case 573951:      // Golden Apple
                case 3033453:     // Clear Rum
                case 4474675:     // Fish Flesh
                case 4966811:     // Cabbage
                case 10931953:    // Psychic Sphere
                    result = false;
                    break;
                case 14210771:    // Mega Etr, Io Talt
                case 9803667:     // Mega A
                case 7645105:     // Mega Qu
                    if (pointMega.isColor())    result = false;     //если еще совпадает и вторая точка, то это мегакварц
                    break;
            }

            return result;
        }                         //товары здесь !!!!!!!!!!!!!!!!

        /// <summary>
        /// добавляем товар из указанной строки в корзину 
        /// </summary>
        /// <param name="numberOfString">номер строки</param>
        public void GoToNextproduct(int numberOfString)
        {
            Point pointAddProduct = new Point(380 - 5 + botwindow.getX(), 225 - 5 + (numberOfString - 1) * 27 + botwindow.getY());
            pointAddProduct.PressMouseWheelDown();   //прокручиваем список
            Pause(250);
        }

        /// <summary>
        /// добавляем товар из указанной строки в корзину 
        /// </summary>
        /// <param name="numberOfString">номер строки</param>
        public void AddToCartLotProduct(int numberOfString)
        {
            Point pointAddProduct = new Point(360 - 5 + botwindow.getX(), 220 - 5 + (numberOfString - 1) * 27 + botwindow.getY());  //305 + 30, 190 + 30)
            pointAddProduct.DoubleClickL();  //тыкаем в строчку с товаром
            Pause(150);
            SendKeys.SendWait("33000");
            Pause(250);
            //Press44444();                   // пишем 444444 , чтобы максимальное количество данного товара попало в корзину 
            pointAddProduct.PressMouseWheelDown();   //прокручиваем список
            Pause(250);
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
                if (NeedToSellProduct(currentProduct.color1, 1 ))    //проверяем товар в первой строке
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
                if (NeedToSellProduct(pointCurrentProduct.GetPixelColor(),j))       //если нужно продать товар в строке j
                    AddToCartLotProduct(j);                                         //добавляем в корзину весь товар в строке j
            }
        }

        /// <summary>
        /// Кликаем в кнопку BUY  в магазине 
        /// </summary>
        public void Botton_BUY()
        {
            pointButtonBUY.PressMouseL();
            pointButtonBUY.PressMouseL();
            Pause(2000);
        }

        /// <summary>
        /// Кликаем в кнопку Sell  в магазине 
        /// </summary>
        public void Botton_Sell()
        {
            pointButtonSell.PressMouseL();
            pointButtonSell.PressMouseL();
            Pause(2000);
        }

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
        /// Покупка митридата в количестве 333 штук
        /// </summary>
        public void BuyingMitridat()
        {
            //botwindow.PressMouseL(360, 537);          //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
            pointBuyingMitridat1.PressMouseL();             //кликаем левой кнопкой в ячейку, где надо написать количество продаваемого товара
            Pause(150);

            //Press333();
            SendKeys.SendWait("333");

            Botton_BUY();                             // Нажимаем на кнопку BUY 


            pointBuyingMitridat2.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если переполнение митридата
            //botwindow.PressMouseL(1392 - 875, 438 - 5);         
            Pause(500);

            pointBuyingMitridat3.PressMouseL();           //кликаем левой кнопкой мыши в кнопку Ок, если нет денег на покупку митридата
            //botwindow.PressMouseL(1392 - 875, 428 - 5);          
            Pause(500);
        }


        // закомментировано
        ///// <summary>
        ///// Посылаем нажатие числа 333 в окно с ботом с помощью команды PostMessage
        ///// </summary>
        //public void Press333()
        //{
        //    const int WM_KEYDOWN = 0x0100;
        //    const int WM_KEYUP = 0x0101;
        //    UIntPtr lParam = new UIntPtr();
        //    UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

        //    for (int i = 1; i <= 3; i++)
        //    {
        //        uint dd = 0x00400001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D3, lParam);
        //        Pause(150);

        //        dd = 0xC0400001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D3, lParam);
        //        Pause(150);
        //    }
        //}

        ///// <summary>
        ///// Посылаем нажатие числа 44444 в окно с ботом с помощью команды PostMessage
        ///// </summary>
        //public void Press44444()
        //{
        //    const int WM_KEYDOWN = 0x0100;
        //    const int WM_KEYUP = 0x0101;
        //    UIntPtr lParam = new UIntPtr();
        //    UIntPtr HWND = FindWindowEx(botwindow.getHwnd(), UIntPtr.Zero, "Edit", "");   // это handle дочернего окна ге, т.е. области, где можно писать циферки

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        uint dd = 0x00500001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYDOWN, (UIntPtr)Keys.D4, lParam);
        //        Pause(150);

        //        dd = 0xC0500001;
        //        lParam = (UIntPtr)dd;
        //        PostMessage(HWND, WM_KEYUP, (UIntPtr)Keys.D4, lParam);
        //        Pause(150);
        //    }
        //}


        #endregion


        ///// <summary>
        ///// выход из магазина путем нажатия кнопки Exit
        ///// </summary>
        //public void PressExitFromShop()                   
        //{
        //    dialog.PressOkButton(1);      ////========= тыкаем в OK =======================
        //}


    }
}
