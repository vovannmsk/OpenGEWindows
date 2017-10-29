using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public class StateDriverTrader : IEquatable<State>
    {
        //private botWindow botwindow;
        //private botMerchant dealer;
        private State currentState;    //текущее состояние

        /// <summary>
        /// конструктор
        /// </summary>
        public StateDriverTrader()
        { }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="botwindow"> это все данные про окно бота </param>
        /// <param name="dealer"> это все данные про окно торговца </param>
        /// <param name="currentState"> текущее состояние </param>
        public StateDriverTrader(State currentState)
        {
            //this.botwindow = botwindow;
            //this.dealer = dealer;
            this.currentState = currentState;
        }

        /// <summary>
        /// задаем метод Equals для данного объекта для получения возможности сравнения объектов State
        /// </summary>
        /// <param name="other"> объект для сравнения </param>
        /// <returns> true, если номера состояний объектов равны </returns>
        public bool Equals(State other)
        {
            bool result = false;
            if (!(other == null))            //если other не null, то проверяем на равенство
                if (this.getTekStateInt() == other.getTekStateInt()) result = true;
            return result;
        }

        /// <summary>
        /// метод осуществляет действия для перехода в следующее состояние
        /// </summary>
        public void run()                
        {
            currentState.run();
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
            currentState.elseRun();
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()          
        {
            return currentState.isAllCool();
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public State StateNext()         
        {
            return currentState.StateNext();
        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public State StatePrev()         
        {
            return currentState.StatePrev();
        }

        /// <summary>
        /// изменение текущего состояния. Если переход прошел, то  StateNext(). Иначе StatePrev(),      elseRun() - доп. действия для перехода в состояние StatePrev()
        /// </summary>
        public void setState()
        {
            if (isAllCool())
            { currentState = StateNext(); }
            else
            {
                elseRun();
                currentState = StatePrev();
            }
        }

        /// <summary>
        /// геттер. возвращает номер текущего состояния
        /// </summary>
        /// <returns> номер состояния </returns>
        public int getTekStateInt()
        {
            return currentState.getTekStateInt();
        }

        /// <summary>
        /// геттер, возвращает текущее состояние
        /// </summary>
        /// <returns> текущее состояние в виде объекта </returns>
        public State getTekState()
        {
            return currentState.getTekState();
        }













    }
}
