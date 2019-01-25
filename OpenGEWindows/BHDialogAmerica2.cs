using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            this.pointDialog1 = new PointColor(954 - 5 + xx, 365 - 5 + yy, 7700000, 5);            //isDialog
            this.pointDialog2 = new PointColor(954 - 5 + xx, 366 - 5 + yy, 7700000, 5);
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
