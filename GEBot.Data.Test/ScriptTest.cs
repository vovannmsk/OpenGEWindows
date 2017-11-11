using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GEBot.Data;

namespace GEBot.Data.Test
{
    [TestFixture]
    public class ScriptTest
    {
        [Test]
        public void DbLoginTest()
        {
            IScriptDataBot script = new ScriptDataBotDB(2);
            Assert.AreEqual(script.GetDataBot().Login, "dag002");

        }
    }
}
