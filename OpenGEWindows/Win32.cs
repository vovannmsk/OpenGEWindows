using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace OpenGEWindows
{
   sealed class Win32
  {
      [DllImport("user32.dll")]
      static extern IntPtr GetDC(IntPtr hwnd);

      [DllImport("user32.dll")]
      static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

      [DllImport("gdi32.dll")]
      static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);


      /// <summary>
      /// нажать мышью в конкретную точку
      /// дважды будет нажиматься правая кнопка и однажды левая, также к координатам будет прибавляться смещение окна от края монитора getX и getY
      /// </summary>
      /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
      /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
      public static void PressMouse(int x, int y)
      {
          PressMouseR(x, y);
          PressMouseR(x, y);
          PressMouseL(x, y);
      } 


      /// <summary>
      /// нажать мышью в конкретную точку только левой кнопкой
      /// </summary>
      /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
      /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
      public static void PressMouseL(int x, int y)
      {
          Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 1);
          Win32.Pause(200);
      }

      /// <summary>
      /// нажать мышью в конкретную точку только правой кнопкой
      /// </summary>
      /// <param name="x"> x - первая координата точки, куда нужно ткнуть мышью </param>
      /// <param name="y"> y - вторая координата точки, куда нужно ткнуть мышью </param>
      public static void PressMouseR(int x, int y)
      {
          Click_Mouse_and_Keyboard.Mouse_Move_and_Click(x, y, 8);
          Win32.Pause(200);
      }


      ////=========================================== нажимать на выбранный пункт верхнего меню ========================================
      // /// <summary>
      ///// нажать на выбранный пункт верхнего меню
      // /// </summary>
      // /// <param name="numberOfThePartitionMenu"> номер пункта меню, который необходимро нажать </param>
      //protected void TopMenu(int numberOfThePartitionMenu)
      //{
      //    int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
      //    int x = MenukoordX[numberOfThePartitionMenu - 1];
      //    int y = 55;
      //    PressMouse(x, y);
      //}

      ////============================== нажимать на выбранный пункт верхнего меню а далее на пункт раскрывшегося списка =======================
      //public void TopMenu(int numberOfThePartitionMenu, int punkt)
      //{
      //    int[] numberOfPunkt = { 0, 8, 4, 5, 0, 3, 2, 6, 9, 0, 0, 0, 0 };
      //    int[] MenukoordX = { 300, 333, 365, 398, 431, 470, 518, 565, 606, 637, 669, 700, 733 };
      //    int[] FirstPunktOfMenuKoordY = { 0, 80, 80, 80, 0, 92, 92, 92, 80, 0, 0, 0, 0 };

      //    if (punkt <= numberOfPunkt[numberOfThePartitionMenu - 1])
      //    {
      //        int x = MenukoordX[numberOfThePartitionMenu - 1];
      //        int y = FirstPunktOfMenuKoordY[numberOfThePartitionMenu - 1] + 25 * (punkt - 1);

      //        TopMenu(numberOfThePartitionMenu);
      //        Win32.Pause(500);
      //        PressMouse(x, y);
      //    }
      //} //End topmenu





      /// <summary>
      /// Останавливает поток на некоторый период
      /// </summary>
      /// <param name="ms"> ms - период в милисекундах </param>
      public static void Pause(int ms)
      {
          Thread.Sleep(ms);
      }

      /// <summary>
      /// эмулирует тройное нажатие кнопки "Esc", тем самым в окне бота убираются все лишние окна (в том числе реклама)
      /// </summary>
      public static void PressEscThreeTimes()
      {
          TextSend.SendText2(1);           // нажимаем Esc
          Thread.Sleep(200);
          TextSend.SendText2(1);           // нажимаем Esc
          Thread.Sleep(200);
          TextSend.SendText2(1);           // нажимаем Esc
          Thread.Sleep(200);
      }

       //============== возвращает цвет пикселя экрана, т.е. не в конкретном окне, а на общем экране 1920х1080.             РАБОТАЕТ
       static public uint GetPixelColor(int x, int y)
      {
       IntPtr hwnd = GetDC(IntPtr.Zero);
       uint pixel = GetPixel(hwnd, x, y);
       ReleaseDC(IntPtr.Zero, hwnd);
       
       return pixel;
      }

       static public uint ColorTest(String tt)
       {
           uint bbb = 0;
           switch (tt)    // проверяем каталог
           {
               case "C:\\America\\":
               case "C:\\America2\\":
               case "C:\\America3\\":
               case "C:\\SINGA\\":
                   bbb = 7859187;
                   break;
               case "C:\\Europa\\":
                   bbb = 6670287;
                   break;
           }
           return bbb;




        
       }





       //============== возвращает цвет пикселя окна hwnd в виде структуры Color . ПОКА НЕ РАБОТАЕТ
      static public Color GetColorHWND(IntPtr Hwnd, int x, int y)
      {
          Graphics newGraphics = Graphics.FromHwnd(Hwnd);

          //newGraphics.Clear(Color.White);
          Bitmap myBitmap = new Bitmap(1024, 700, newGraphics);
          myBitmap.Save("c:\\ssss.png");
          Color pixelColor = myBitmap.GetPixel(x, y);


          newGraphics.Dispose();
          myBitmap.Dispose();
          return pixelColor;

          //Image img = new Image();


         // return pixelColor;
      }
      static public int GetColorHWND2(IntPtr Hwnd, int x, int y)
      {
          
          
          
          
          
          return 5;
      }
       //==================== округление числа a на количество разрядов b   =============================
      static public uint Okruglenie(uint a, int b)
      {
          uint bb = 1;
          for (int j = 1; j <= b; j++)
          {
              bb = bb * 10;
          }
          double ddd = a /bb;
          double sss = Math.Round(ddd) * bb;

          return (uint)sss;
      }


   }
   
 


}
   
// Create a Bitmap object from an image file.
    //Bitmap myBitmap = new Bitmap("Grapes.jpg");

    //// Get the color of a pixel within myBitmap.
    //Color pixelColor = myBitmap.GetPixel(50, 50);                    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    //// Fill a rectangle with pixelColor.
    //SolidBrush pixelBrush = new SolidBrush(pixelColor);
    //e.Graphics.FillRectangle(pixelBrush, 0, 0, 100, 100);
 //Bitmap(Int32, Int32, Graphics)   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


//// Create image.
   // Image imageFile = Image.FromFile("SampImag.jpg");                                       !!!!!!!!!!!!!!!!!!!!!!!!!

   // // Create graphics object for alteration.
   // Graphics newGraphics = Graphics.FromImage(imageFile);                                !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

   // // Alter image.
   // newGraphics.FillRectangle(new SolidBrush(Color.Black), 100, 50, 100, 100);

   // // Draw image to screen.
   // e.Graphics.DrawImage(imageFile, new PointF(0.0F, 0.0F));

   // // Release graphics object.
   // newGraphics.Dispose();


//// Create solid brush with arbitrary color.
//    Color arbColor = Color.FromArgb(255, 165, 63, 136);                                   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//    SolidBrush arbBrush = new SolidBrush(arbColor);

//    // Fill ellipse on screen.
//    e.Graphics.FillEllipse(arbBrush, 0, 0, 200, 100);

//    // Get nearest color.
//    Color realColor = e.Graphics.GetNearestColor(arbColor);
//    SolidBrush realBrush = new SolidBrush(realColor);

//    // Fill ellipse on screen.
//    e.Graphics.FillEllipse(realBrush, 0, 100, 200, 100);

    //// Create the starting point.
    //Point startPoint = new Point(subtractButton.Size);                                !!!!!!!!!!!!!!!!

    //// Use the addition operator to get the end point.
    //Point endPoint = startPoint + new Size(140, 150);                                 !!!!!!!!!!!!!!!!!!!!!!!

    //// Draw a line between the points.
    //e.Graphics.DrawLine(SystemPens.Highlight, startPoint, endPoint);

    //// Convert the starting point to a size and compare it to the
    //// subtractButton size.  
    //Size buttonSize = (Size)startPoint;
    //if (buttonSize == subtractButton.Size)

    //    // If the sizes are equal, tell the user.
    //{
    //    e.Graphics.DrawString("The sizes are equal.", 
    //        new Font(this.Font, FontStyle.Italic), 
    //        Brushes.Indigo, 10.0F, 65.0F);
    //}
