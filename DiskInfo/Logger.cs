using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskInfo
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    public class Logger
    {
        public void Log(LogType logType, string message, string method, string exception)
        {
            // implement NLog at a later...
            DateTime dt = DateTime.Now;
            using (StreamWriter file = File.AppendText(dt.ToString("dd-MM-yyyy") + "_log.txt"))
            {
                file.WriteLine();

                switch (logType)
                {
                    case LogType.Info:
                        file.WriteLine(message);
                        break;
                    case LogType.Warning:
                        file.WriteLine("Warning on - " + method);
                        file.WriteLine(message);
                        break;
                    case LogType.Error:
                        file.WriteLine("ERROR --> " + method);
                        file.WriteLine(exception);
                        break;
                    default:
                        break;
                }
                file.Write(dt);
                file.WriteLine("----------------");
                file.Close();
            }
        }
    }
}
