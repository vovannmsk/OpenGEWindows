﻿using GlobalParametrs;

namespace OpenGEWindows
{
    public class MarketAmerica : Market
    {
        public MarketAmerica()
        { }

        public MarketAmerica(botWindow botwindow) 
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(902 + xx, 673 + yy, 7850000, 4);
            this.pointIsSale2 = new PointColor(903 + xx, 673 + yy, 7850000, 4);
            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7390000, 4);
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7390000, 4);
            this.pointIsClickSale1 = new PointColor(735 - 5 + xx, 665 - 5 + yy, 7390000, 4);
            this.pointIsClickSale2 = new PointColor(735 - 5 + xx, 664 - 5 + yy, 7390000, 4);
            this.pointBookmarkSell = new Point(225 + xx, 163 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);           //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonSell = new Point(725 + xx, 663 + yy);   //725, 663);
            this.pointButtonClose = new Point(847 + xx, 663 + yy);   //847, 663);
            this.pointAddProduct = new Point(380 - 5 + xx, 220 - 5 + yy);

            #endregion

            DialogFactory dialogFactory = new DialogFactory(this.botwindow);
            dialog = dialogFactory.createDialog();
            this.globalParam = new GlobalParam();
        }

        // ============  методы  ========================




    }
}
