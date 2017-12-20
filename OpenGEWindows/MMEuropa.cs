using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class MMEuropa : MM
    {
        public MMEuropa()
        { }

        public MMEuropa(botWindow botwindow)
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion


        }

        // ===============================  Методы ==================================================



    }
}
