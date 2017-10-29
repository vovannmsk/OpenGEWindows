using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


namespace OpenGEWindows
{
    public class PointColor : iPointColor
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        int x;
        int y;
        uint color;
        int accuracy;

        /// <summary>
        /// конструктор без арументов
        /// </summary>
        public PointColor()
        { 
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="x"> координата Х </param>
        /// <param name="y"> координата Y </param>
        /// <param name="color"> эталонный цвет пикселя </param>
        /// <param name="accuracy"> точность округления цвета </param>
        public PointColor(int x, int y,uint color, int accuracy)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.accuracy = accuracy;
        }

        /// <summary>
        /// округление вверх числа a на количество разрядов b
        /// если a = 1655, b = 2, то результат равен 1600
        /// </summary>
        /// <param name="a"> округляемое число </param>
        /// <param name="b"> количество разрядов для округления </param>
        /// <returns> если a = 1655, b = 2, то результат равен 1600 </returns>
        private uint Okruglenie(uint a, int b)
        {
            uint bb = 1;
            for (int j = 1; j <= b; j++) bb = bb * 10;
            uint result = a - a % bb;
            return result;
        }

        /// <summary>
        /// возвращает цвет пикселя экрана, т.е. не в конкретном окне, а на общем экране 1920х1080.            
        /// </summary>
        /// <param name="x"> x - первая координата проверяемой точки </param>
        /// <param name="y"> y - вторая координата проверяемой точки </param>
        /// <returns> цвет пикселя экрана </returns>
        public uint GetPixelColor()
        {
            IntPtr hwnd = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hwnd, x, y);
            ReleaseDC(IntPtr.Zero, hwnd);
            return pixel;
        }

        /// <summary>
        /// проверяет цвет в указанных координатах и сверяет с эталонным (с переменной класса color)
        /// </summary>
        /// <returns> true, если цвет совпадает с указанной точностью </returns>
        public bool isColor()
        {
            uint currentColor = Okruglenie(GetPixelColor(), accuracy);
            return (currentColor == color);
        }



    }
}
