using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class DialogSing : Dialog
    {
        public DialogSing ()
        {}

        public DialogSing(botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            this.ButtonOkDialog = new Point(910 - 5 + xx, 680 - 5 + yy);                           //нажимаем на Ок в диалоге

        }

        // ===============================  Методы ==================================================

        /// <summary>
        /// нажать указанную строку в диалоге. Отсчет с низу вверх
        /// </summary>
        /// <param name="number"></param>
        public override void PressStringDialog(int number)
        {
            iPoint pointString = new Point(520 - 5 + xx, 660 - 5 + yy - (number - 1) * 20);
            pointString.PressMouse();
            Pause(1000);
        }



    }
}
