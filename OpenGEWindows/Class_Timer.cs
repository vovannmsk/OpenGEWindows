using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Threading;

namespace OpenGEWindows
{
    class Class_Timer
    {
        public static bool Pause(int Ms)
    {
        //DateTime a,b;
        //a = DateTime.Now;
        //b = DateTime.Now.AddMilliseconds(Ms);
        //while (a < b)
        //{
        //    a = DateTime.Now;
        //}
        Thread.Sleep(Ms);
        return true;
    }
        public static bool PauseSecund(int Sec)
        {
            DateTime a, b;
            a = DateTime.Now;
            b = DateTime.Now.AddSeconds(Sec);
            while (a < b)
            {
                a = DateTime.Now;
            }
            return true;
        }
        public static bool PauseMinute(int Min)
        {
            
            DateTime a, b;
            a = DateTime.Now;
            b = DateTime.Now.AddMinutes(Min);

            while (a < b)
            {
                a = DateTime.Now;
                if (Keyboard.IsKeyDown(Key.D1))
                {
                    return false;
                }
            }
            return true;
        }

       
}
}
