namespace OpenGEWindows
{
    public class BHDialogSing : BHDialog
    {
        public BHDialogSing ()
        {}

        public BHDialogSing(botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            this.ButtonOkDialog = new Point(953 - 5 + xx, 369 - 5 + yy);                           //нажимаем на Ок в диалоге
           
            //проверяем наличие кнопки Ок в открытом диалоге
            this.pointsBottonGateBH1 = new PointColor(979 - 30 + xx, 390 - 30 + yy, 7700000, 5);            //Ok
            this.pointsBottonGateBH2 = new PointColor(979 - 30 + xx, 391 - 30 + yy, 7700000, 5);            //Ok

            //проверяем то состояние ворот, где написано "You currently have N tries remaining" (N = 1..5)
            this.pointsGateBH1 = new PointColor(891 - 5 + xx, 313 - 5 + yy, 4210914, 0);            //буква i в times

            //проверяем то состояние ворот, где написано ""
            this.pointsGateBH2 = new PointColor(683 - 5 + xx, 265 - 5 + yy, 12000000, 6);           

            //проверяем то состояние ворот, где написано "You have used up your daily entry count."
            this.pointsGateBH3 = new PointColor(860 - 5 + xx, 335 - 5 + yy, 4210914, 0);           //буква i в слове daily

            //проверяем то состояние ворот, где написано "Please input Initialize"
            this.pointsGateBH4_1 = new PointColor(932 - 30 + xx, 700 - 30 + yy, 7700000, 5);            //Ok
//            this.pointsGateBH4_2 = new PointColor(932 - 30 + xx, 701 - 30 + yy, 7700000, 5);            //Ok
            this.pointsGateBH4_2 = new PointColor(163 - 30 + xx, 641 - 30 + yy, 13752023, 0);            //If
            pointInputBox = new Point(310 - 30 + xx, 675 - 30 + yy);                                    //нажимаем на поле ввода
            pointInputBoxBottonOk = new Point(933 - 30 + xx, 704 - 30 + yy);                            //нажимаем на Ок в диалоге (Initialize)

            //проверяем то состояние ворот, где написано "The difficulty level has been reset normaly"
            this.pointsGateBH5 = new PointColor(670 - 30 + xx, 356 - 30 + yy, 13000000, 6);           //The

            //проверяем то состояние ворот, где написано "Reset difficulty by using Shiny Crystal 200 piece(s)"
            this.pointsGateBH6 = new PointColor(659 - 30 + xx, 359 - 30 + yy, 12900000, 5);           //Reset

            //уровень ворот меньше 10 ???
            this.pointsIsLess11_1 = new PointColor(687 - 5 + xx, 259 - 5 + yy, 12700000, 5);               // буква Y в слове Your
            //уровень ворот от 11 до 19 ???
            this.pointsIsLevelfrom11to19_1 = new PointColor(682 - 5 + xx, 221 - 5 + yy, 12700000, 5);      // буква Y в слове Your
            this.pointsIsLevelfrom11to19_2 = new PointColor(864 - 5 + xx, 226 - 5 + yy, 4270000, 4);       // красная цифра 1
            //уровень ворот от 20 и выше ???
            this.pointsIsLevelAbove20_1 = new PointColor(682 - 5 + xx, 221 - 5 + yy, 12700000, 5);         // буква Y в слове Your
            this.pointsIsLevelAbove20_2 = new PointColor(860 - 5 + xx, 233 - 5 + yy, 4670000, 4);          // красная цифра 2 

            //проверяем то состояние ворот, где написано "You cannot ener for free today. Next Challenge..."
            //this.pointsGateBH6 = new PointColor(716 - 30 + xx, 263 - 30 + yy, 13000000, 6);           //Next

        }

        // ===============================  Методы ==================================================

        /// <summary>
        /// нажать указанную строку в диалоге. Отсчет снизу вверх
        /// </summary>
        /// <param name="number"></param>
        public override void PressStringDialog(int number)
        {
            new Point(839 - 30 + xx, 363 - 30 + yy - (number - 1) * 19).PressMouseLL();
            Pause(1000);
        }



    }
}
