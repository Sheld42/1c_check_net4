using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp1
{


    class Program
    {

        private static ExeFile file1;
        private static int TimeWait = 5000;

        static void Main(string[] args)
        {
            string[] param = new string[7];

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Text = "1c starter";
            trayIcon.Icon = new Icon(SystemIcons.Information, 40, 40);
            trayIcon.Visible = true;



            //собирается путь исполняемого файла с параметрами, 1 строка путь, + ост как параметры.
            //формат файла:
            //# Путь к ехе 1С
            //# Путь к базе данных
            //# База данных
            //# Пользователь
            //# Админ пользователь
            //#TimeOut между проверками на закрытие 1С.
            if (System.IO.File.Exists("options"))
            {
                try
                {
                    StreamReader OptFile = new StreamReader("options", Encoding.Default, true);

                    for (int i = 1; i <= 6; i++)
                        param[i] = OptFile.ReadLine();

                    TimeWait = int.Parse(param[6]);
                    param[0] = ("ENTERPRISE " + param[2] + " " + param[4] + @" /DisableStartupMessages");
                    LogLine("Params seted to: #" + param[0] + "#");
                    LogLine("ExePath seted to #" + param[1] + "#");
                }
                catch
                {
                    LogLine("Failed to read params from file");
                    LogLine("Using default params instead");
                    param[0] = "";
                }
            }
            else
                param[0] = "";

            file1 = param[0].Length > 1 ? new ExeFile(param[1], param[0]) : new ExeFile();



            int PID;
            while (true)
            {
                try
                {
                    PID = file1.StartExe();
                    //Console.WriteLine("Process started with PID = " + PID.ToString());
                    LogLine("Process started with PID = " + PID.ToString());
                    Thread.Sleep(TimeWait);
                    file1.WaitForExit();
                    Thread.Sleep(TimeWait);
                }
                catch { LogLine("You fucked it up"); }
            }


            //LogLine("Конец кода программы.");

        }

        static public void LogLine(string LogLine)      //лог в файл рядом с ехе. Usage: LogLine(sting);
        {
            StreamWriter LogFile = new StreamWriter("log.txt", true);
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
                if (menu == "0") break;
                else if (menu == "1") break;
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
