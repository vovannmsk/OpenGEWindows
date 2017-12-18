using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class MMAmerica : MM
    {
        public MMAmerica()
        { }

        public MMAmerica(botWindow botwindow) 
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            DialogFactory dialogFactory = new DialogFactory(this.botwindow);
            dialog = dialogFactory.createDialog();

        }

        // ============  методы  ========================




    }
}
