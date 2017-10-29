using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public interface State : IEquatable<State>

    {
        void run();                // переход к следующему состоянию
        void elseRun();            // дополнительные действия для перехода к запасному состоянию. Часто никаких действий не требуется
        bool isAllCool();          // получилось ли перейти к следующему состоянию. true, если получилось
        State StateNext();         // возвращает следующее состояние, если переход осуществился
        State StatePrev();         // возвращает запасное состояние, если переход не осуществился
        int getTekStateInt();      //возвращает текущее состояние в цифрах
        State getTekState();       //возвращает текущее состояние 
    }
}
