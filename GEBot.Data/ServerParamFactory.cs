namespace GEBot.Data
{
    /// <summary>
    /// класс для реализация паттерна "Фабрика" (семейство классов ServerParam)
    /// </summary>
    public class ServerParamFactory
    {
        private ServerParam serverParam;
        private BotParam botParam;
        private string param;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="param">синг, америка или европа</param>
        public ServerParamFactory(int numberOfWindow)
        {
            this.botParam = new BotParam(numberOfWindow);
            this.param = botParam.Param;
        }
        public ServerParam create()
        {
            switch (this.param)
            {
                case "C:\\America\\":
                    serverParam = new ServerParamAmerica();
                    break;
                case "C:\\Europa\\":
                    serverParam = new ServerParamEuropa();
                    break;
                case "C:\\SINGA\\":
                    serverParam = new ServerParamSing();
                    break;
                case "C:\\America2\\":
                    serverParam = new ServerParamAmerica2();
                    break;
                default:
                    serverParam = new ServerParamAmerica();
                    break;
            }
            return serverParam;
        }


    }
}
