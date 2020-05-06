using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static ExeFile file1;
        private static object exit;

        static void Main(string[] args)
        {
            string param = "";
            //StreamReader settings;
            if (!System.IO.File.Exists("Options"))
                param = StartUpMenu();

            file1 = param.Length > 1 ? new ExeFile(param) : new ExeFile();

            int PID;
            while (true)
            {
                try
                {
                    PID = file1.StartExe();
                    LogLine("Process started with PID = " + PID.ToString());
                    Thread.Sleep(3000);
                    file1.WaitForExit();
                    Thread.Sleep(3000);
                }
                catch { LogLine("You fucked it up"); }
            }
          

            LogLine("Конец кода программы.");
            
        }

        static public void LogLine(string LogLine)      //лог в файл рядом с ехе. Usage: LogLine(sting);
        {
            StreamWriter LogFile = new StreamWriter("log.txt",true);
            LogFile.Write(DateTime.Now.ToString() + "   ");
            LogFile.WriteLine(LogLine);
            LogFile.Close();
        }

        static private string StartUpMenu()
        {
            string param = "";
            string menu;
            while (true)
            {

                Console.WriteLine("1 - Использовать параметры по умолчанию");
                Console.WriteLine("2 - Указать параметры запуска");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("0 - Выход");
                menu = Console.ReadLine();
                if (menu == "0")        break;
                else if (menu == "1")   break;
                else if (menu == "2")
                {
                    param = Console.ReadLine();
                    //file1 = new ExeFile(param);
                    return param;
        
                }

            }
            return param;
        }


    }

}
