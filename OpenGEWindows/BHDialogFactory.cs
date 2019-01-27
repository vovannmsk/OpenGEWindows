using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class BHDialogFactory
    {
        private BHDialog dialog;
        private botWindow botwindow;

        public BHDialogFactory()
        { }

        public BHDialogFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        /// <summary>
        /// возвращает ноывй экземпляр класса указанного сервера
        /// </summary>
        /// <returns></returns>
        public BHDialog create()
        {
            dialog = new BHDialogSing(botwindow);
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    dialog = new BHDialogAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    dialog = new BHDialogEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    dialog = new BHDialogSing(botwindow);
                    break;
                case "C:\\America2\\":
                    dialog = new BHDialogAmerica2(botwindow);
                    break;
                default:
                    dialog = new BHDialogSing(botwindow);
                    break;
            }
            return dialog;
        }
    }
}
