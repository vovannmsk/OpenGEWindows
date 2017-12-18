using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class MMFactory
    {
        private MM mm;
        private botWindow botwindow;

        public MMFactory()
        { }

        public MMFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        public MM create()
        {
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    mm = new MMAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    mm = new MMEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    mm = new MMSing(botwindow);
                    break;
                default:
                    mm = new MMSing(botwindow);
                    break;
            }
            return mm;
        }
    }
}
