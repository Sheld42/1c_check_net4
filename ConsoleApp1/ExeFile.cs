using System;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{

    class ExeFile
    {
        public string Params { get; set; }
        public string ExePath { get; set; }
        public string DataBaseName { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string AdminUser { get; set; }

        private System.Diagnostics.Process MyProc;

        public ExeFile()    //Для запуска 1С с параметрами по умолчанию
        {
            ExePath = @"C:\Program Files\1cv8\8.3.15.1830\bin\1cv8.exe";
            DataBaseName = @"C:\1CBases83";
            DataBase = @"/F%DataBaseName%";
            User = "/N\"Обмен\"  /P\"229120\" /WA-";
            AdminUser = "/N\"Продавец Зеленый\"  /P\"123\" /WA-";
            Params = "ENTERPRISE " + DataBaseName + " " + User + " " + @"/DisableStartupMessages";
        }

        public ExeFile(string _v8exe, string _DataBaseName, string _DataBase, string _User, string _AdminUser)
        {
            ExePath = _v8exe;
            DataBaseName = _DataBaseName;
            DataBase = _DataBase;
            User = _User;
            AdminUser = _AdminUser;
            Params = "ENTERPRISE " + DataBaseName + " " + User + " " + @"/DisableStartupMessages";
        }
        public ExeFile(string arg)
        {
            ExePath = "cmd";
            Params = arg;
        }


        public int StopExe()
        {
            try
            {
                //MyProc.Close();
                MyProc.Kill();
                System.Threading.Thread.Sleep(5000);
                return (1);
            }
            catch { return (-1); }
        }

        public void WaitForExit()
        {


            MyProc.WaitForExit();

                //!= null ? MyProc.Id : -1;
                //return MyProc.HasExited;
            }

        public int StartExe()
        {
            int PID = -1;
            MyProc = new System.Diagnostics.Process();

            MyProc.StartInfo.FileName = ExePath;
            MyProc.StartInfo.Arguments = Params;


            try
            {
                MyProc.Start();
                PID = MyProc.Id;
            }
            catch (System.Exception e)
            {

                Program.LogLine("Ошибка!");
                Program.LogLine(e.Message);
            }




            return PID;
        }
    }


}
