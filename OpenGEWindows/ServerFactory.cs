using GEBot.Data;

namespace OpenGEWindows
{
    /// <summary>
    /// класс для реализация паттерна "Фабрика" (семейство классов server: serverAmerica,serverEuropa,serverSing)
    /// </summary>
    public class ServerFactory

    {
        private Server server;
        private botWindow botwindow;
        private string parametr;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public ServerFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.parametr = botwindow.getParam();
        }

        public ServerFactory(int numberOfWindow)
        {
            BotParam botParam = new BotParam(numberOfWindow);
            this.parametr = botParam.Param;
            this.botwindow = new botWindow(numberOfWindow);
        }



        public Server create()
        { 
            switch (parametr)    
            {
                case "C:\\America\\":
                    server = new ServerAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    server = new ServerEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    server = new ServerSing(botwindow);
                    break;
                case "C:\\America2\\":
                    server = new ServerAmerica2(botwindow);
                    break;
                default:
                    server = new ServerAmerica(botwindow);
                    break;
            }
            return server;
        }

    }
}
