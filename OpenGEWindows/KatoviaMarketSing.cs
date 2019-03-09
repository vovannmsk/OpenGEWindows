﻿using GEBot.Data;

namespace OpenGEWindows
{
    public class KatoviaMarketSing : KatoviaMarket
    {
        public KatoviaMarketSing ()
        {}

        public KatoviaMarketSing(botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region Shop

            this.pointIsSale1 = new PointColor(843 - 5 + xx, 622 - 5 + yy, 7900000, 5);              // не используется
            this.pointIsSale2 = new PointColor(843 - 5 + xx, 623 - 5 + yy, 7900000, 5);

            this.pointIsSaleIn1 = new PointColor(843 - 5 + xx, 622 - 5 + yy, 7900000, 5);            // для isSale2
            this.pointIsSaleIn2 = new PointColor(843 - 5 + xx, 623 - 5 + yy, 7900000, 5);

            this.pointIsClickSell1 = new PointColor(739 - 5 + xx, 624 - 5 + yy, 7900000, 5);         // для проверки закладки Sell в магазине
            this.pointIsClickSell2 = new PointColor(739 - 5 + xx, 625 - 5 + yy, 7900000, 5);

            this.pointBookmarkSell = new Point(245 - 5 + xx, 202 - 5 + yy);                          // закладка Sell

            this.pointButtonBUY = new Point(731 - 5 + xx, 627 - 5 + yy);     // кнопка BUY (Purchase)
            this.pointButtonSell = new Point(731 - 5 + xx, 627 - 5 + yy);    // кнопка Sell
            this.pointButtonClose = new Point(851 - 5 + xx, 627 - 5 + yy);   // кнопка Close

            this.pointAddProduct = new Point(379 - 5 + xx, 250 - 5 + yy);    //кнопка на увеличение количество товара на один в верхней строке

            this.pointBuyingMitridat1 = new Point(360 + xx, 537 + yy);      // пока не используется, не проверено
            this.pointBuyingMitridat2 = new Point(517 + xx, 433 + yy);      //
            this.pointBuyingMitridat3 = new Point(517 + xx, 423 + yy);      //


            #endregion


            DialogFactory dialogFactory = new DialogFactory(this.botwindow);
            dialog = dialogFactory.createDialog();
            this.globalParam = new GlobalParam();
        }

    }
}
