using System;
using System.Linq;

namespace FtpCrawler.Runner.Threads
{
    [ThreadActionKey("FtpIndexer")]
    internal class Indexer : ITaskThread
    {
        public String ActionName => "FTP Server Indexer";

        public ILogger Logger { get; private set; }

        private Services.Interfaces.IFtpServerService _FtpServerService;
        private Services.Interfaces.IFtpFolderService _FtpFolderService;
        private Services.Interfaces.IFtpFileService _FtpFileService;

        private Services.Interfaces.IFtpServerService FtpServerService => _FtpServerService ?? (_FtpServerService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpServerService>());
        private Services.Interfaces.IFtpFolderService FtpFolderService => _FtpFolderService ?? (_FtpFolderService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpFolderService>());
        private Services.Interfaces.IFtpFileService FtpFileService => _FtpFileService ?? (_FtpFileService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpFileService>());

        public Boolean Run()
        {
            Data.Models.FtpServer[] servers = FtpServerService.GetAll().ToArray();

            if (servers.Any())
            {
                //index servers in parallel
                System.Threading.Tasks.Parallel.ForEach(servers, (server) =>
                //foreach (var server in servers)
                {
                    using (Utils.ServerIndexer serverIndexer = new Utils.ServerIndexer(server))
                    {
                        serverIndexer.IndexServer();
                    }
                });
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