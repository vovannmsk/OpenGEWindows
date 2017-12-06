using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class PetFactory
    {
        private Pet pet;
        private botWindow botwindow;

        public PetFactory()
        { }

        public PetFactory(botWindow botwindow)
        {
            this.botwindow = botwindow;
        }

        public Pet createPet()
        {
            switch (botwindow.getParam())
            {
                case "C:\\America\\":
                    pet = new PetAmerica(botwindow);
                    break;
                case "C:\\Europa\\":
                    pet = new PetEuropa(botwindow);
                    break;
                case "C:\\SINGA\\":
                    pet = new PetSing(botwindow);
                    break;
                default:
                    pet = new PetSing(botwindow);
                    break;
            }
            return pet;
        }
    }
}
