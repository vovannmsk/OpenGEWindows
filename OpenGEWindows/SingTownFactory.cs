using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    /// <summary>
    /// реализация паттерна "Фабрика" (семейство классов SingTown)
    /// </summary>
    public class SingTownFactory : TownFactory
    {
        private botWindow botwindow;

        /// <summary>
        /// создаёт экземпляр класса для SingTown
        /// </summary>
        /// <param name="nomerOfTown"> номер города </param>
        /// <param name="nomerOfWindow"> номер окна по порядку </param>
        /// <returns></returns>

        public SingTownFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        public override Town createTown()
        {
            Town town = null;
            switch (botwindow.getNomerTeleport())
            {
                case 1:
                    //=================== ребольдо =======================================
                    town = new SingTownReboldo(botwindow);
                    break;
                //case 2:
                //    //=================== Коимбра =======================================
                //    town = new SingTownCoimbra(nomerOfWindow);
                //    break;
                case 3:
                    //=================== Ош ============================================
                    town = new SingTownAuch(botwindow);
                    break;
                case 4:
                    //=================== Юстиар =======================================
                    town = new SingTownUstiar(botwindow);
                    break;
                case 5:
                    //=================== багама =======================================
                    town = new SingTownBagama(botwindow);
                    break;
                case 6:
                    //=================== Эррак =======================================
                    town = new SingTownErrac(botwindow);
                    break;
                case 7:
                    //=================== байрон =======================================
                    town = new SingTownViron(botwindow);
                    break;
                case 10:
                    //=================== байрон =======================================
                    town = new SingTownArmonia(botwindow);
                    break;
                case 13:
                    //=================== Кастилия=======================================
                    town = new SingTownCastilia(botwindow);
                    break;
                case 14:
                    //=================== лос толдос ======================================
                    town = new SingTownLosToldos(botwindow);
                    break;
                default:
                    //=================== такого быть не должно, но пусть будет Ребольдо =======================================
                    town = new SingTownReboldo(botwindow);
                    break;
            }
            return town;
        }
    }
}
