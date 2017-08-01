using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Runner
{
    internal class Logger : ILogger
    {
        private String logFile = "";

        public Logger(String instanceName)
        {
            logFile = String.Format("{0}_{1}.log", instanceName, DateTime.Now.ToString("yyyyMMdd"));
        }

        public void Log(String message)
        {
            Log(message, false);
        }

        public void Log(String message, Boolean consoleOnly)
        {
            Console.WriteLine(message);
            if (!consoleOnly)
                Write(message);
        }

        public void LogException(Exception ex)
        {
            Log(FormatException(ex));
        }

        private static Object _lock = new Object();

        private static String FormatException(Exception ex)
        {
            return String.Format("\r\n===================================================================================================\r\nError Message : {0}\r\n\r\n Stack Trace : {1}\r\n\r\n Target Site : {2}\r\n===================================================================================================\r\n", ex.Message, ex.StackTrace, ex.TargetSite, ex.InnerException);
        }

        private void Write(String message)
        {
            Write(message, System.Configuration.ConfigurationManager.AppSettings["LogPath"] + "/" + logFile);
        }

        private static void Write(String message, String filePath)
        {
            lock (_lock)
            {
                string path = filePath.Replace("\\", "/");
                path = path.Substring(0, path.LastIndexOf("/"));
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true))
                {
                    sw.WriteLine(String.Format("{0}\t:\t{1}", DateTime.Now, message));
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private static void LogException(Exception ex, String filePath)
        {
            Write(FormatException(ex), filePath);
        }
    }
}
