using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace States
{
    /// <summary>
    /// Интерфейс для создания всех состояний
    /// </summary>
    public interface IState : IEquatable<IState>

    {
        void run();                // переход к следующему состоянию
        void elseRun();            // дополнительные действия для перехода к запасному состоянию. Часто никаких действий не требуется
        bool isAllCool();          // получилось ли перейти к следующему состоянию. true, если получилось
        IState StateNext();         // возвращает следующее состояние, если переход осуществился
        IState StatePrev();         // возвращает запасное состояние, если переход не осуществился
        int getTekStateInt();      //возвращает текущее состояние в цифрах
        IState getTekState();       //возвращает текущее состояние 
    }
}
