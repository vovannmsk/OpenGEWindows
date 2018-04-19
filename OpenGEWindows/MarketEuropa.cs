using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class MarketEuropa : Market
    {
        public MarketEuropa()
        { }

        public MarketEuropa(botWindow botwindow)
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(897 - 5 + xx, 677 - 5 + yy, 7600000, 5);      //работает
            this.pointIsSale2 = new PointColor(897 - 5 + xx, 678 - 5 + yy, 7700000, 5);      //работает
            this.pointIsSale21 = new PointColor(840 - 5 + xx, 665 - 5 + yy, 7390000, 4);      //работает
            this.pointIsSale22 = new PointColor(840 - 5 + xx, 668 - 5 + yy, 7390000, 4);      //работает
            this.pointIsClickSale1 = new PointColor(733 - 5 + xx, 665 - 5 + yy, 7390000, 4);      //работает
            this.pointIsClickSale2 = new PointColor(733 - 5 + xx, 664 - 5 + yy, 7390000, 4);      //работает
            this.pointBookmarkSell = new Point(230 - 5 + xx, 168 - 5 + yy);                      //работает                        
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(730 - 5 + xx, 668 - 5 + yy);   //725, 663);      //работает      
            this.pointButtonClose = new Point(852 - 5 + xx, 668 - 5 + yy);   //847, 663);      //работает      
            this.pointAddProduct = new Point(380 - 5 + xx, 220 - 5 + yy);

            #endregion

            DialogFactory dialogFactory = new DialogFactory(this.botwindow);
            dialog = dialogFactory.createDialog();

            //this.pointSellOnMenu = new Point(520 + xx, 654 + yy);          
            //this.pointOkOnMenu = new Point(902 + xx, 674 + yy);            

        }

        // ===============================  Методы ==================================================



    }
}
