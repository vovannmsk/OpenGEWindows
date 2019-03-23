using OpenGEWindows;


namespace States
{
    public class StateGT105 : IState
    {
        private botWindow botwindow;
        private Server server;
        private ServerFactory serverFactory;
        private BHDialog BHdialog;
        private BHDialogFactory dialogFactory;
        private int tekStateInt;

        public StateGT105()
        {

        }

        public StateGT105(botWindow botwindow)   
        {
            this.botwindow = botwindow;
            this.serverFactory = new ServerFactory(botwindow);
            this.server = serverFactory.create();   // создали конкретный экземпляр класса server по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.dialogFactory = new BHDialogFactory(botwindow);
            this.BHdialog = dialogFactory.create();   // создали конкретный экземпляр класса BHDialog по паттерну "простая Фабрика" (Америка, Европа или Синг)
            this.tekStateInt = 105;
        }

        /// <summary>
        /// задаем метод Equals для данного объекта для получения возможности сравнения объектов State
        /// </summary>
        /// <param name="other"> объект для сравнения </param>
        /// <returns> true, если номера состояний объектов равны </returns>
        public bool Equals(IState other)
        {
            bool result = false;
            if (!(other == null))            //если other не null, то проверяем на равенство
                if (other.getTekStateInt() == 1)         //27.04.17
                {
                    if (this.getTekStateInt() == other.getTekStateInt()) result = true;
                }
                else   //27.04.17
                {
                    if (this.getTekStateInt() >= other.getTekStateInt()) result = true;  //27.04.17
                }
            return result;
        }

        /// <summary>
        /// геттер, возвращает текущее состояние
        /// </summary>
        /// <returns></returns>
        public IState getTekState()
        {
            return this;
        }

        /// <summary>
        /// метод осуществляет действия для перехода в следующее состояние
        /// </summary>
        public void run()                // переход к следующему состоянию
        {
            //начинаем из третьего состояния, т.е. isGateBH3 = true

            BHdialog.PressStringDialog(1);  //нажимаем на нижнюю строку в меню
            BHdialog.PressOkButton(1);      //нажимаем на кнопку Ок один раз
            server.WriteToLogFileBH("105 состояние ворот 3. выбрали нижнюю строку и Ок");


            ////ожидание загрузки миссии или диалога 4
            //int counter = 0;
            //while ((!(server.isWork() || BHdialog.isGateBH4())) && (counter < 30))
            //{ botwindow.Pause(1000); counter++; }

            
            //оказываемся либо в миссии, либо прошло 10 раундов и надо вводить слово Initialize
        }

        /// <summary>
        /// метод осуществляет действия для перехода к запасному состоянию, если не удался переход 
        /// </summary>
        public void elseRun()
        {
            botwindow.PressEscThreeTimes();
            botwindow.Pause(500);
        }

        /// <summary>
        /// проверяет, получилось ли перейти к следующему состоянию 
        /// </summary>
        /// <returns> true, если получилось перейти к следующему состоянию </returns>
        public bool isAllCool()
        {
            //return (server.isWork() || BHdialog.isGateBH4());
            return true;
        }

        /// <summary>
        /// возвращает следующее состояние, если переход осуществился
        /// </summary>
        /// <returns> следующее состояние </returns>
        public IState StateNext()         // возвращает следующее состояние, если переход осуществился
        {
            //if (BHdialog.isGateBH4())
            //{
            //    server.WriteToLogFileBH("105 идем в 106 сост 4");
            //    return new StateGT106(botwindow);
            //}
            //else
            //{
            //    server.WriteToLogFileBH("105 идем в 108 на миссию");
            //    return new StateGT108(botwindow);
            //}
            return new StateGT108(botwindow);

        }

        /// <summary>
        /// возвращает запасное состояние, если переход не осуществился
        /// </summary>
        /// <returns> запасное состояние </returns>
        public IState StatePrev()         // возвращает запасное состояние, если переход не осуществился
        {
            server.WriteToLogFileBH("105 ELSE ");

            return this;
        }

        /// <summary>
        /// геттер. возвращает номер текущего состояния
        /// </summary>
        /// <returns> номер состояния </returns>
        public int getTekStateInt()
        {
            return this.tekStateInt;
        }
    }
}
