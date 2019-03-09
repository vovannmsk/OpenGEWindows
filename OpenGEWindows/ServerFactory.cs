namespace OpenGEWindows
{
    /// <summary>
    /// класс для реализация паттерна "Фабрика" (семейство классов server: serverAmerica,serverEuropa,serverSing)
    /// </summary>
    public class ServerFactory

    {
        private Server server;
        private botWindow botwindow;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"></param>
        public ServerFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }
        public Server create()
        { 
            switch (botwindow.getParam())    
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
