using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OpenGEWindows
{
    public class Point : iPoint
    {
        int x;
        int y;

        public Point()
        { 
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="x"> координата Х </param>
        /// <param name="y"> координата Y </param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }


        /// <summary>
        /// Останавливает поток на некоторый период
        /// </summary>
        /// <param name="ms"> ms - период в милисекундах </param>
        public void Pause(int ms)
        {
            Thread.Sleep(ms);
        }

        /// <summary>
        /// нажать мышью в конкретную точку только правой кнопкой
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseR()
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 2);
            Pause(200);
        }

        /// <summary>
        /// нажать мышью в конкретную точку только левой кнопкой
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseL()
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 1);
            Pause(200);
        }

        /// <summary>
        /// нажать мышью в конкретную точку
        /// дважды будет нажиматься правая кнопка и однажды левая, также к координатам будет прибавляться смещение окна от края монитора getX и getY
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouse()
        {
            PressMouseR();
            PressMouseR();
            PressMouseL();
        }

        /// <summary>
        /// переместить мышь в координаты и покрутить колесо вверх
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseWheelUp()
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 9);
            Pause(200);
        }

        /// <summary>
        /// переместить мышь в координаты и покрутить колесо вниз
        /// </summary>
        /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
        /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
        public void PressMouseWheelDown()
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 3);
            Pause(200);
        }

        /// <summary>
        /// двойной клик в указанных координатах
        /// </summary>
        public void DoubleClickL()
        {
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 1);
            Pause(50);
            Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 1);
            Pause(200);

        }

        /// <summary>
        /// перетаскивание из текущей точки в указанную
        /// </summary>
        /// <param name="point"></param>
        public void Drag(iPoint point)
        {
            Click_Mouse_and_Keyboard.MMC(x, y, point.getX(), point.getY());
            Pause(200);
        }
    }
}
