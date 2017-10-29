using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEDataBot.BL
{
    public interface IScriptDataBot
    {
        /// <summary>
        /// выдача данных бота с номером окна numberOfWindow
        /// </summary>
        /// <param name="numberOfWindow"> номер окна бота</param>
        /// <returns>данные, необходимые для создания бота в формате DataBot </returns>
        public DataBot GetDataBot();

        public void SetHwnd(UIntPtr hwnd);

        public void SetChangeToTrade(string needToChange);
    }
}
