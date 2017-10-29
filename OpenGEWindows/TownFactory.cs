using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{   
    /// <summary>
    /// абстрактный класс для реализация паттерна "Фабрика" (семейство классов AmericaTown)
    /// </summary>
    public abstract class TownFactory
    {

        abstract public Town createTown();

    }
}
