﻿using System;
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
        bool isAllCool();          // метод проверяет, получилось ли выполнить действия в методе run. true, если получилось и значит можно переходить к следующему состоянию
        IState StateNext();         // возвращает следующее состояние (куда дальше переходим в случае успешного выполнения run)
        IState StatePrev();         // возвращает запасное состояние, если run не выполнен (проверка прошла не успешно)
        int getTekStateInt();      //возвращает текущее состояние в цифрах
        IState getTekState();       //возвращает текущее состояние 
    }
}
