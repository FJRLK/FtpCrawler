using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Runner
{
    internal interface ITaskThread : IDisposable
    {
        String ActionName { get; }

        Boolean Run();

        void SetLogger(ILogger logger);

        ILogger Logger { get; }
    }
}
