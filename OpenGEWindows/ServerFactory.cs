using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    /// <summary>
    /// класс для реализация паттерна "Фабрика" (семейство классов server: serverAmerica,serverEuropa,serverSing)
    /// </summary>
    public class ServerFactory

    {
        private ServerInterface server;
        private botWindow botwindow;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public ServerFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }
        public ServerInterface createServer()
        { 
            switch (botwindow.getParam())    
            {
                case "C:\\America\\":
                case "C:\\America2\\":
                case "C:\\America3\\":
                    server = new ServerAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    server = new ServerEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    server = new ServerSing(botwindow);
                    break;
                default:
                    server = new ServerAmerica(botwindow);
                    break;
            }
            return server;
        }

    }
}
