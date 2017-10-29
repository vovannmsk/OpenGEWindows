using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace OpenGEWindows
{
    public class GotoTradeTest
    {
        private botWindow botwindow;
        private ServerInterface server;

        public GotoTradeTest(botWindow botwindow)
        {
            this.botwindow = botwindow;
            this.server = botwindow.getserver();
        }
        public void gotoTradeTestDrive()
        {

            if (server.isWork())
            {
                botwindow.StateGotoTrade();                                          // по паттерну "Состояние".  01-14       (работа - продажа - нет окна)
                botwindow.Pause(2000);
                botwindow.StateGotoWork();                                           // по паттерну "Состояние".  14-01       (нет окна - логаут - казарма - город - работа)
            }
        }

        public void gotoWorkTestDrive()
        {
            //botWindow botwindow = new botWindow(14);
            //botwindow.StateGotoWork();
            //botWindow botwindow = new botWindow(1);     // окно бота

            //StateDriverDealerRun(new StateCV01(botwindow, botwindow2), new StateCV02(botwindow, botwindow2));
            
        }


        /// <summary>
        /// запускает движок состояний StateDriver от пункта stateBegin до stateEnd
        /// </summary>
        /// <param name="stateBegin"> начальное состояние </param>
        /// <param name="stateEnd"> конечное состояние </param>
        public void StateDriverDealerRun(State stateBegin, State stateEnd)
        {
            StateDriverTrader stateDriverTrader = new StateDriverTrader(stateBegin);
            while (!stateDriverTrader.Equals(stateEnd))
            {
                stateDriverTrader.run();
                stateDriverTrader.setState();
            }
        }
    


    }
}
