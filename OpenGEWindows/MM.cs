using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace OpenGEWindows
{
    public abstract class MM : Server2
    {

        // ============ переменные ======================

        protected Dialog dialog;

        protected iPointColor pointIsMMSell1;
        protected iPointColor pointIsMMSell2;
        protected iPointColor pointIsMMBuy1;
        protected iPointColor pointIsMMBuy2;

        // ============  методы  ========================

        /// <summary>
        /// определяет, находимся ли мы на рынке на первой странице, где можно выставить товар на продажу  (MarketManager)
        /// </summary>
        /// <returns></returns>
        public bool isMMSell()
        {
            return ( pointIsMMSell1.isColor() && pointIsMMSell2.isColor() );
        }

        /// <summary>
        /// определяет, находимся ли мы на рынке на странице со списком товаров для покупки (MarketManager)
        /// </summary>
        /// <returns></returns>
        public bool isMMBuy()
        {
            return (pointIsMMBuy1.isColor() && pointIsMMBuy2.isColor());
        }

 
        /// <summary>
        /// определяет, наша ли строчка первая на рынке (наш ли товар самый дешевый)
        /// </summary>
        /// <returns></returns>
        public bool isMyFirstString()
        {
            bool result = false;



            return result;
        }

        /// <summary>
        /// переходим на вторую страницу рынка с первой
        /// </summary>
        public void GotoSecondPage()
        {

        }

        /// <summary>
        /// переход со второй на первую страницу
        /// </summary>
        public void GotoFirstPage()
        { 

        }

    }
}
