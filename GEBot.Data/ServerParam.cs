using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBot.Data
{
    public abstract class ServerParam
    {
        protected GlobalParam globalParam;

        #region параметры, зависящие от сервера

        protected string pathClient;
        protected bool isActiveServer;

        public string PathClient { get => pathClient; }
        public bool IsActiveServer { get => isActiveServer;  }

        #endregion

        //===================== методы ===============================

        protected abstract string path_Client();
        protected abstract bool isActive();




    }
}
