using System;
using System.Linq;

namespace FtpCrawler.Runner.Threads
{
    internal class Indexer : ITaskThread
    {
        public String ActionName => "FTP Server Indexer";

        public ILogger Logger { get; private set; }

        private Services.Interfaces.IFtpServerService _FtpServerService;

        private Services.Interfaces.IFtpServerService FtpServerService => _FtpServerService ?? (_FtpServerService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpServerService>());

        public Boolean Run()
        {
            var servers = FtpServerService.GetAll();

            if (servers.Any()) {

            }

            return true;
        }

        public void SetLogger(ILogger logger)
        {
            Logger = logger;
        }

        #region IDisposable Support

        private Boolean disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Indexer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}