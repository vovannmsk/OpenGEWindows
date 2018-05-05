using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class PetSing : Pet
    {
        public PetSing ()
        {}

        public PetSing(botWindow botwindow)
        {
            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region Pet

            this.pointisSummonPet1 = new PointColor(494 - 5 + xx, 304 - 5 + yy, 13000000, 6);
            this.pointisSummonPet2 = new PointColor(494 - 5 + xx, 305 - 5 + yy, 13000000, 6);
            this.pointisActivePet1 = new PointColor(493 - 5 + xx, 310 - 5 + yy, 10000000, 7);
            this.pointisActivePet2 = new PointColor(494 - 5 + xx, 309 - 5 + yy, 10000000, 7);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //не проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц
            //this.pointisActivePet5 = new PointColor(517 - 5 + xx, 302 - 5 + yy, 12900000, 5);                //проверено
            //this.pointisActivePet6 = new PointColor(495 - 5 + xx, 310 - 5 + yy, 12400000, 5);                //проверено
            this.pointisOpenMenuPet1 = new PointColor(474 - 5 + xx, 219 - 5 + yy, 12000000, 6);      //834 - 5, 98 - 5, 12400000, 835 - 5, 98 - 5, 12400000, 5);             //проверено
            this.pointisOpenMenuPet2 = new PointColor(474 - 5 + xx, 220 - 5 + yy, 12000000, 6);
            this.pointCancelSummonPet = new Point(410 - 5 + xx, 390 - 5 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(540 - 5 + xx, 380 - 5 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(410 - 5 + xx, 360 - 5 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet = new Point(410 - 5 + xx, 410 - 5 + yy);                   // //Click Button Active Pet                            //проверено

            #endregion
        }

        // ===============================  Методы ==================================================
    }
}
