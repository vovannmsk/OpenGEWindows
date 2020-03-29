﻿using GEBot.Data;

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

            this.pointIsSale1 = new PointColor(907 - 5 + xx, 675 - 5 + yy, 7600000, 5);
            this.pointIsSale2 = new PointColor(907 - 5 + xx, 676 - 5 + yy, 7600000, 5);

            this.pointIsSale21 = new PointColor(841 - 5 + xx, 665 - 5 + yy, 7000000, 5);              //для isSale2  по кнопке Close
            this.pointIsSale22 = new PointColor(841 - 5 + xx, 668 - 5 + yy, 7000000, 5);

            this.pointIsClickSale1 = new PointColor(731 - 5 + xx, 665 - 5 + yy, 7000000, 5);          //нажата ли закладка "Sell"
            this.pointIsClickSale2 = new PointColor(731 - 5 + xx, 666 - 5 + yy, 7000000, 5);

            this.pointIsClickPurchase1 = new PointColor(696 - 5 + xx, 664 - 5 + yy, 7500000, 5);      //нажата ли закладка "Purchase"
            this.pointIsClickPurchase2 = new PointColor(696 - 5 + xx, 665 - 5 + yy, 7500000, 5);

            this.pointBookmarkSell = new Point(229-5 + xx, 168-5 + yy);
            this.pointSaleToTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointSaleOverTheRedBottle = new Point(335 + xx, 220 + yy);
            this.pointWheelDown = new Point(375 + xx, 220 + yy);            //345 + 30 + botwindow.getX(), 190 + 30 + botwindow.getY(), 3);        // колесо вниз
            this.pointButtonBUY = new Point(725 - 5 + xx, 667 - 5 + yy);            //725, 663);
            this.pointButtonSell = new Point(725 - 5 + xx, 667 - 5 + yy);           //725, 663);
            this.pointButtonClose = new Point(852 - 5 + xx, 667 - 5 + yy);          //847, 663);
            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      //360, 537
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //1392 - 875, 438 - 5
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //1392 - 875, 428 - 5
            this.pointAddProduct = new Point(380 - 5 + xx, 220 - 5 + yy);


            #endregion

            DialogFactory dialogFactory = new DialogFactory(this.botwindow);
            dialog = dialogFactory.createDialog();
            this.globalParam = new GlobalParam();
            this.botParam = new BotParam(botwindow.getNumberWindow());

        }

        // ============  методы  ========================




    }
}
