using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OpenGEWindows
{
    public class PetEuropa : Pet
    {
        public PetEuropa()
        { }

        public PetEuropa(botWindow botwindow)
        {

            #region общие

            this.botwindow = botwindow;
            this.xx = botwindow.getX();
            this.yy = botwindow.getY();

            #endregion

            #region Pet

            this.pointisOpenMenuPet1 = new PointColor(829 + xx, 93 + yy, 12000000, 6);
            this.pointisOpenMenuPet2 = new PointColor(830 + xx, 93 + yy, 12000000, 6);
            this.pointisSummonPet1 = new PointColor(740 - 5 + xx, 237 - 5 + yy, 7400000, 5);
            this.pointisSummonPet2 = new PointColor(741 - 5 + xx, 237 - 5 + yy, 7400000, 5);
            this.pointisActivePet1 = new PointColor(828 - 5 + xx, 186 - 5 + yy, 13100000, 5);
            this.pointisActivePet2 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 13100000, 5);
            this.pointisActivePet3 = new PointColor(829 - 5 + xx, 186 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц                                      //проверено
            this.pointisActivePet4 = new PointColor(829 - 5 + xx, 185 - 5 + yy, 12000000, 6); // для проверки периодической еды на месяц
            this.pointCancelSummonPet = new Point(750 + xx, 265 + yy);   //750, 265                    //проверено
            this.pointSummonPet1 = new Point(868 + xx, 258 + yy);                   // 868, 258   //Click Pet
            this.pointSummonPet2 = new Point(748 + xx, 238 + yy);                   // 748, 238   //Click кнопку "Summon"
            this.pointActivePet = new Point(748 + xx, 288 + yy);                   // //Click Button Active Pet                            //проверено

            #endregion

        }

        // ===============================  Методы ==================================================



    }
}
