using System;
using System.IO;
using System.Runtime.InteropServices;
namespace OpenGEWindows
{
    public class Array_File_IO
    {
        /// <summary>
        /// Читать из текстового файла в строковый массив
        /// </summary>
        /// <param name="Name_File"> имя файла </param>
        /// <param name="Arr"> массив для чтения данных </param>
        /// <returns> количество считанных строк </returns>
        public static void Read_File_String(string Name_File,ref string[] Arr) 
        
           {
             int counter = 1;
             string line = "1";
             StreamReader file = new StreamReader(Name_File);
             while (line != null)
                 {
                  line = file.ReadLine();
                  if (line != null) Arr[counter] = line;
                  counter++;
                 }
             file.Close();
           } // Окончание Метода

        /// <summary>
        /// чтение из текстового файла в строку
        /// </summary>
        /// <param name="Name_File"> имя файла </param>
        /// <returns> прочитанная из файла строка </returns>

        public static String Read_File(string Name_File)
        {
            string ff = "0";
            string line = "";
            StreamReader file = new StreamReader(Name_File);
            
            line = file.ReadLine();           //читаем первую строку из файла
            if (line != null) ff = line;      // если в файле чего-то было, то возвращаемое значение присваиваем тому, что было в файле
            
            file.Close();
            return ff;
        } // Окончание Метода

        /// <summary>
        /// запись числа в файл 
        /// </summary>
        /// <param name="Name_File"> имя файла </param>
        /// <param name="n"> число, которое надо записать в текстовый файл </param>
        public static void Write_File(string Name_File, uint n)
        {
            StreamWriter file = new StreamWriter(Name_File, false);
                file.WriteLine("" + n);
            file.Close();
        }


        /// <summary>
        /// запись массива в файл
        /// </summary>
        /// <param name="Name_File"> имя файла </param>
        /// <param name="Arr"> имя массива </param>
        /// <param name="n"></param>
        public static void Write_File_String(string Name_File,ref IntPtr[] Arr)
        {
            StreamWriter file = new StreamWriter(Name_File,false);
            for (int i = 1; i <= Arr.Length; i++)
            {
                file.WriteLine("" + Arr[i]);
            }
            file.Close();
        } 

    } // Окончание класса
}
