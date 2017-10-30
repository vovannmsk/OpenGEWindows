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
        DataBot GetDataBot();

        void SetHwnd(UIntPtr hwnd);

        void SetChangeToTrade(string needToChange);
    }
}
