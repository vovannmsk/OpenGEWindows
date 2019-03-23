namespace OpenGEWindows
{
    public class MarketFactory
    {
        private Market market;
        private botWindow botwindow;

        public MarketFactory()
        { }

        public MarketFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        public Market createMarket()
        {
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    market = new MarketAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    market = new MarketEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    market = new MarketSing(botwindow);
                    break;
                case "C:\\America2\\":
                    market = new MarketAmerica2(botwindow);
                    break;
                default:
                    market = new MarketSing(botwindow);
                    break;
            }
            return market;
        }
    }
}
