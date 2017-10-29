using System;
using System.Runtime.InteropServices;

namespace OpenGEWindows
{
    class Click_Mouse_and_Keyboard
    
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0100;             //было 80
        const uint MOUSEEVENTF_XUP = 0x0200;               //было 100
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint WHEEL_DELTA = 120;                      //добавил
        const int INPUT_MOUSE = 0;
        const int INPUT_KEYBOARD = 1;
        const int INPUT_HARDWARE = 2;

        const uint KEYEVENTF_KEYDOWN = 0x0000;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_UNICODE=0x0004;
        const uint KEYEVENTF_SCANCODE = 0x0008;


        public static void MMC(int dx1, int dy1, int dx2, int dy2)
        {
            MOUSEINPUT m;
            INPUT i;
            INPUT[] inputs;
            int isize;

            //============================== перетаскиваем предмет из одних координат в другие ===========================================================
         
                    // переместили мышь
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx1 * 34.1328);
                    m.dy = (int)(dy1 * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    // нажали левую

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx1 * 34.1328);
                    m.dy = (int)(dy1 * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    Class_Timer.Pause(100);

                    //  переместили в новые координаты **************************************
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx2 * 34.1328);
                    m.dy = (int)(dy2 * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    Class_Timer.Pause(500);

                    //  отпустили левую  **********************************************************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx2 * 34.1328);
                    m.dy = (int)(dy2 * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);


        }


        public static bool Mouse_Move_and_Click(int dx, int dy, uint Flag_)
        {
            MOUSEINPUT m;
            INPUT i;
            INPUT[] inputs;
            int isize;
            switch(Flag_)
            {
                case 1: // Перемещение мыши и левый клик
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(50);  //**********************************************************


                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    break;
                case 2:// Перемещение мыши и Двойной левый клик
                     m = new MOUSEINPUT();
                    
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(50);  //**********************************************************


                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(50);  //**********************************************************


                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);


                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(50);  //**********************************************************


                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    break;
                case 3:// Вращаем колесиком       =======================================================================  3   =====================================
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //********************** крутим колесом вниз

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = -120;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_WHEEL;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    break;
                
            case 4:
                   m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //********************** крутим колесом вверх

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 120;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_WHEEL;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    break;
  
                
            
          


                case 5:// Кликаем мышкой и прокручиваем
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(100);  
                //**********************************************************


                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    
                    //********************** крутим колесом вниз

                    Class_Timer.Pause(100);  
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = -120;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_WHEEL;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    break;



         //============================== перетаскиваем предмет из одних координат в другие ===========================================================
         
                case 6:// Кликаем мышкой перемещаем и отпускаем мышь
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    Class_Timer.Pause(100);

                //**********************************************************
                    int dx2 = 1090; 
                    int dy2 = 536;
                    m = new MOUSEINPUT();
                    dx = dx2;
                    dy = dy2;
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    Class_Timer.Pause(500);

                    //**********************************************************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    break;
                    //=========================================================================================================

                case 7:// Кликаем мышкой перемещаем и отпускаем мышь
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    //**********************************
                    Class_Timer.Pause(100);
                    //**********************************************************

                    m = new MOUSEINPUT();
                    dx = dx + 340;
                    dy = dy - 205;
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    Class_Timer.Pause(500);
                    
                    //**********************************************************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);

                    break;

                case 8:  // перемещаем мышь и нажимаем правую кнопку

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);            // перемещение мыши

                    //**********************

                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTDOWN;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);    //**************   нажимаем правую кнопку  *************************************************************************************
                    
                    Class_Timer.Pause(50);  

                    
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx*34.1328);
                    m.dy = (int)(dy*60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTUP;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);    //Отжимаем правую кнопку


                    break;
                case 9:// перемещаем мышку в координаты и прокручиваем вверх
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 0;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    Class_Timer.Pause(200); 
                    //**********************

                    //m = new MOUSEINPUT();
                    //m.dx = (int)(dx * 34.1328);
                    //m.dy = (int)(dy * 60.6805);
                    //m.mouseData = 0;
                    //m.time = 0;
                    //m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN;
                    //i = new INPUT();
                    //i.type = INPUT_MOUSE;
                    //i.mi = m;

                    //inputs = new INPUT[] { i };
                    //isize = Marshal.SizeOf(i);

                    //SendInput(1, inputs, isize);
                    ////**********************************
                    //Class_Timer.Pause(200);
                    ////**********************************************************


                    //m = new MOUSEINPUT();
                    //m.dx = (int)(dx * 34.1328);
                    //m.dy = (int)(dy * 60.6805);
                    //m.mouseData = 0;
                    //m.time = 0;
                    //m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
                    //i = new INPUT();
                    //i.type = INPUT_MOUSE;
                    //i.mi = m;

                    //inputs = new INPUT[] { i };
                    //isize = Marshal.SizeOf(i);

                    //SendInput(1, inputs, isize);

                    //Class_Timer.Pause(200);    
                    //********************** крутим колесом вниз

                    Class_Timer.Pause(100);
                    m = new MOUSEINPUT();
                    m.dx = (int)(dx * 34.1328);
                    m.dy = (int)(dy * 60.6805);
                    m.mouseData = 120;
                    m.time = 0;
                    m.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_WHEEL;
                    i = new INPUT();
                    i.type = INPUT_MOUSE;
                    i.mi = m;

                    inputs = new INPUT[] { i };
                    isize = Marshal.SizeOf(i);

                    SendInput(1, inputs, isize);
                    Class_Timer.Pause(200);  
                    break;
            }//END Switch
            return true;
        }//End Mouse_Move_and_Click

        public static bool ClickKey (ushort Key_)
        {
            KEYBDINPUT k;
            INPUT i;
            INPUT[] inputs;
            int isize;

            k = new KEYBDINPUT();
            k.wVk = 0;
            k.wScan = Key_;
            k.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYDOWN;
            k.time = 0;
            k.dwExtraInfo = (IntPtr)0;
            i = new INPUT();
            i.type = INPUT_KEYBOARD;
            i.ki = k;
            inputs = new INPUT[] { i };
            isize = Marshal.SizeOf(i);
            SendInput(1, inputs, isize);

            return true;
        }//END ClickKey
        public static bool UnClickKey(ushort Key_)
        {
            KEYBDINPUT k;
            INPUT i;
            INPUT[] inputs;
            int isize;

            k = new KEYBDINPUT();
            k.wVk = 0;
            k.wScan = Key_;
            k.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
            k.time = 0;
            k.dwExtraInfo = (IntPtr)0;
            i = new INPUT();
            i.type = INPUT_KEYBOARD;
            i.ki = k;
            inputs = new INPUT[] { i };
            isize = Marshal.SizeOf(i);
            SendInput(1, inputs, isize);

            return true;
        }//END ClickKey

        public static bool ClickOneKey(ushort Key_)
        {
            KEYBDINPUT k;
            INPUT i;
            INPUT[] inputs;
            int isize;

            k = new KEYBDINPUT();
            k.wVk = 0;
            k.wScan = Key_;
            k.dwFlags = KEYEVENTF_UNICODE;
            k.time = 0;
            k.dwExtraInfo = (IntPtr)0;
            i = new INPUT();
            i.type = INPUT_KEYBOARD;
            i.ki = k;
            inputs = new INPUT[] { i };
            isize = Marshal.SizeOf(i);
            SendInput(1, inputs, isize);

            //k = new KEYBDINPUT();
            //k.wVk = 0;
            //k.wScan = Key_;
            //k.dwFlags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP;
            //k.time = 0;
            //k.dwExtraInfo = (IntPtr)0;
            //i = new INPUT();
            //i.type = INPUT_KEYBOARD;
            //i.ki = k;
            //inputs = new INPUT[] { i };
            //isize = Marshal.SizeOf(i);
            //SendInput(1, inputs, isize);



            return true;
        }//END ClickKey


        public static void GenerateKey(int vk, bool bExtended)
        {
            INPUT[] inputs = new INPUT[1]; inputs[0].type = INPUT_KEYBOARD; KEYBDINPUT kb = new KEYBDINPUT(); //{0};
            // generate down 
            if (bExtended) kb.dwFlags = KEYEVENTF_EXTENDEDKEY; kb.wVk = (ushort)vk; inputs[0].ki = kb;
            SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));

            // generate up 
            //ZeroMemory(&kb, sizeof(KEYBDINPUT));
            //ZeroMemory(&inputs,sizeof(inputs)); kb.dwFlags = KEYEVENTF_KEYUP;
            if (bExtended) kb.dwFlags |= KEYEVENTF_EXTENDEDKEY; kb.wVk = (ushort)vk; inputs[0].type = INPUT_KEYBOARD; inputs[0].ki = kb;
            SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        }


        //public static void Test_KeyDown()
        //{
        //    INPUT[] InputData = new INPUT[2];
        //    Key ScanCode = Microsoft.DirectX.DirectInput.Key.W;

        //    InputData[0].type = 1; //INPUT_KEYBOARD
        //    InputData[0].wScan = (ushort)ScanCode;
        //    InputData[0].dwFlags = (uint)SendInputFlags.KEYEVENTF_SCANCODE;

        //    InputData[1].type = 1; //INPUT_KEYBOARD
        //    InputData[1].wScan = (ushort)ScanCode;
        //    InputData[1].dwFlags = (uint)(SendInputFlags.KEYEVENTF_KEYUP | SendInputFlags.KEYEVENTF_UNICODE);

        //    // send keydown
        //    if (SendInput(2, InputData, Marshal.SizeOf(InputData[1])) == 0)
        //    {
        //        System.Diagnostics.Debug.WriteLine("SendInput failed with code: " +
        //        Marshal.GetLastWin32Error().ToString());
        //    }
        //}

    }
}


