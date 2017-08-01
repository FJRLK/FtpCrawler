using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Runner
{
    internal interface ILogger
    {
        void Log(String message);
        void Log(String message, Boolean consoleOnly);
        void LogException(Exception ex);
    }
}
