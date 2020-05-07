using System;
using System.IO;
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
            string[] param = new string[10];

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Text = "1c starter";
            trayIcon.Icon = new Icon(SystemIcons.Information, 40, 40);
            trayIcon.Visible = true;



            param = ReadParams();
            string FlagFile = "";
            if (param[0].Length > 1)
            {
                file1 = new ExeFile(param[1], param[0]);
                FlagFile = param[7];
                int.TryParse(param[6], out TimeWait);
            }
            else
                file1 = new ExeFile();

            //###########################
            //      Основной цикл
            //###########################

            int PID;
            while (true)
            {
                try
                {
                    PID = file1.StartExe();                                 
                    LogLine("Process started with PID = " + PID.ToString());
                    //Thread.Sleep(TimeWait);
                    file1.WaitForExit();
                    while (CheckForBusy(FlagFile)) ;
                    Thread.Sleep(TimeWait);
                }
                catch { LogLine("You fucked it up"); Thread.Sleep(TimeWait); }
            }
        }

        static public void LogLine(string LogLine)      //лог в файл рядом с ехе. Usage: LogLine(sting);
        {
            StreamWriter LogFile = new StreamWriter("log.txt", true);
            LogFile.Write(DateTime.Now.ToString() + "   ");
            LogFile.WriteLine(LogLine);
            LogFile.Close();
        }

        static private bool CheckForBusy(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                LogLine("Exchange detected, halting");
                Thread.Sleep(TimeWait);
                return true;
            }
            else
                return false;
        }

        //Описание параметров и ихпозиция в файле options
        //собирается путь исполняемого файла с параметрами, 1 строка путь, + ост как параметры.
        //формат файла:
        //#0 Системная
        //#1 Путь к ехе 1С           C:\Program Files\1cv8\8.3.15.1830\bin\1cv8.exe
        //#2 Путь к базе данных      /F"C:\1CBases83"
        //#3 База данных             C:\1CBases83                  /F%DataBaseName%
        //#4 Пользователь            /N"Обмен"  /P"229120" /WA-
        //#5 Админ пользователь      /N"Продавец Зеленый"  /P"123" /WA-
        //#6 TimeOut между проверками на закрытие 1С в мс
        //#7 Путь к файлу-семафору для активного обмена

        static private string[] ReadParams()
        {
            string[] Param = new string[10];

            if (System.IO.File.Exists("options"))
            {
                try
                {
                    StreamReader OptFile = new StreamReader("options", Encoding.Default, true);

                    for (int i = 1; i <= 6; i++)
                        Param[i] = OptFile.ReadLine();


                    Param[0] = ("ENTERPRISE " + Param[2] + " " + Param[4] + @" /DisableStartupMessages");
                    LogLine("Params seted to: #" + Param[0] + "#");
                    LogLine("ExePath seted to #" + Param[1] + "#");
                }
                catch
                {
                    LogLine("Failed to read params from file");
                    LogLine("Using null params instead");
                    Param[0] = "";
                }
            }
            else
                Param[0] = "";

            return Param;
        }

    }

}
