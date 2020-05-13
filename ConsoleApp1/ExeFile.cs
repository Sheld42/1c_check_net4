using System;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{

    class ExeFile
    {
        public string Params { get; set; }
        public string ExePath { get; set; }

        private System.Diagnostics.Process MyProc;

        public ExeFile(){}

        public ExeFile(string path, string arg)
        {
            ExePath = path;
            Params = arg;
        }

        public void WaitForExit()
        {
            MyProc.WaitForExit();
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

                //Program.LogLine("Ошибка!");
                Program.LogLine(e.Message);
            }

            return PID;
        }
    }


}
