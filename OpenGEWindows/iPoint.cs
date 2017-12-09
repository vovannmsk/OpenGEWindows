using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGEWindows
{
    public interface iPoint
    {
        void PressMouseR();  
        void PressMouseL();
        void PressMouseLL();
        void PressMouse();
        void PressMouseWheelUp();
        void PressMouseWheelDown();
        void Pause(int ms);  //
        void DoubleClickL();
        void Drag(iPoint point);
        int getX();
        int getY();
    }
}
