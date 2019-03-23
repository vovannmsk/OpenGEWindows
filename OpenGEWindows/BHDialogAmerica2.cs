namespace OpenGEWindows
{
    public class BHDialogAmerica2 : BHDialog
    {
        public BHDialogAmerica2 ()
        {}

        public BHDialogAmerica2 (botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            this.ButtonOkDialog = new Point(953 - 5 + xx, 369 - 5 + yy);                           //нажимаем на Ок в диалоге
            //this.pointDialog1 = new PointColor(954 - 5 + xx, 365 - 5 + yy, 7700000, 5);            //isDialog
            //this.pointDialog2 = new PointColor(954 - 5 + xx, 366 - 5 + yy, 7700000, 5);

            //проверяем наличие кнопки Ок в открытом диалоге
            this.pointsBottonGateBH1 = new PointColor(979 - 30 + xx, 390 - 30 + yy, 7700000, 5);            //Ok
            this.pointsBottonGateBH2 = new PointColor(979 - 30 + xx, 391 - 30 + yy, 7700000, 5);            //Ok

            //проверяем то состояние ворот, где написано "Now. you can try N times for free" (N = 1..5)
            this.pointsGateBH1 = new PointColor(649 - 30 + xx, 310 - 30 + yy, 4600000, 5);            //Possible

            //проверяем то состояние ворот, где написано "Currently from N round can proceed" (N = 1..5)
            this.pointsGateBH2 = new PointColor(708 - 30 + xx, 290 - 30 + yy, 12000000, 6);           //Currently

            //проверяем то состояние ворот, где написано "You cannot ener for free today"
            this.pointsGateBH3 = new PointColor(716 - 30 + xx, 249 - 30 + yy, 13000000, 6);           //Next

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

        }

        // ===============================  Методы ==================================================

        /// <summary>
        /// нажать указанную строку в диалоге. Отсчет с низу вверх
        /// </summary>
        /// <param name="number"></param>
        public override void PressStringDialog(int number)
        {
            iPoint pointString = new Point(814 - 5 + xx, 338 - 5 + yy - (number - 1) * 19);
            pointString.PressMouseLL();
            Pause(1000);
        }



    }
}
