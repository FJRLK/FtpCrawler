using System;
using System.Collections.Generic;
using System.Linq;

namespace FtpCrawler.Utils
{
    public class ServerIndexer : IDisposable
    {
        private Data.Models.FtpServer _server;
        Data.Models.FtpServer Server => _server;
        private Object _locker = new Object();

        //private Services.Interfaces.IFtpServerService _FtpServerService;
        private Services.Interfaces.IFtpFolderService _FtpFolderService;

        private Services.Interfaces.IFtpFileService _FtpFileService;

        //private Services.Interfaces.IFtpServerService FtpServerService => _FtpServerService ?? (_FtpServerService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpServerService>());
        private Services.Interfaces.IFtpFolderService FtpFolderService => _FtpFolderService ?? (_FtpFolderService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpFolderService>());

        private Services.Interfaces.IFtpFileService FtpFileService => _FtpFileService ?? (_FtpFileService = Services.ServiceManager.ResolveService<Services.Interfaces.IFtpFileService>());

        private List<Data.Models.FtpFile> filesList = null;
        private List<Data.Models.FtpFile> fileChanges = null;

        private List<Data.Models.FtpFolder> folderList = null;

        public ServerIndexer(Data.Models.FtpServer server)
        {
            _server = server;

            fileChanges = new List<Data.Models.FtpFile>();

            filesList = FtpFileService.GetAll().Where(f => f.ServerId == Server.Id).ToList();
            folderList = FtpFolderService.GetAll().Where(f => f.ServerId == Server.Id).ToList();
        }

        public void IndexServer()
        {
            using (var client = new FluentFTP.FtpClient(Server.HostName, Server.Port, Server.Login, Server.PassWord))
            {
                ProcessFolderFiles(client, Server.StartingDir);
            }

            CleanUppFilesAndFolders();
        }

        /// <summary>
        /// removes files and folders that was not updated in the last crawling
        /// </summary>
        private void CleanUppFilesAndFolders()
        {
            Data.Models.FtpFile[] files = FtpFileService.GetAllNotModifiedSince(DateTime.Now.AddDays(-1)).ToArray();
            FtpFileService.Delete(files);

            Data.Models.FtpFolder[] folders = FtpFolderService.GetAllNotModifiedSince(DateTime.Now.AddDays(-1)).ToArray();
            FtpFileService.Delete(files);
        }

        private void ProcessFolderFiles(FluentFTP.FtpClient client, String directory, Data.Models.FtpFolder parentFolder = null)
        {
            Data.Models.FtpFolder folder = null;

            lock (_locker)
                folder = folderList.FirstOrDefault(f => f.FullName == directory.Trim());

            if (folder == null)
            {
                FtpFolderService.Create(folder = new Data.Models.FtpFolder
                {
                    FullName = directory,
                    ServerId = Server.Id,
                    ShortName = GetDirectoryName(directory) ?? "",
                    FolderId = parentFolder?.Id
                });
            }
            else
            {
                folder.Modified = DateTime.Now;
                folder.FolderId = parentFolder?.Id;
                FtpFolderService.Update(folder);
            }

            var list = client.GetListing(directory);

            //cannot be done in parallel some servers might be limited to 1 connection per IP
            list.ForEach((item) =>
            {
                if (item.Type == FluentFTP.FtpFileSystemObjectType.Directory)
                {
                    ProcessFolderFiles(client, item.FullName, folder);
                }
                else if (item.Type == FluentFTP.FtpFileSystemObjectType.File)
                {
                    ProcessFile(item, folder);
                }
            });
        }

        private void ProcessFile(FluentFTP.FtpListItem item, Data.Models.FtpFolder folder)
        {
            Data.Models.FtpFile file = null;
            lock (_locker)
                file = filesList.FirstOrDefault(f => f.FullName == item.FullName.Trim());

            if (null == file)
            {
                FtpFileService.Create(new Data.Models.FtpFile
                {
                    Extension = System.IO.Path.GetExtension(item.Name),
                    FileDate = item.Modified,
                    FileSize = item.Size,
                    FolderId = folder.Id,
                    FullName = item.FullName,
                    ServerId = Server.Id,
                    ShortName = item.Name
                });
            }
            else
            {
                file.Extension = System.IO.Path.GetExtension(item.Name);
                file.FileDate = item.Modified;
                file.FileSize = item.Size;
                file.FolderId = folder.Id;
                file.FullName = item.FullName;
                file.ServerId = Server.Id;
                file.ShortName = item.Name;

                FtpFileService.Update(file);
            }
        }

        private String GetDirectoryName(String directory)
        {
            var s = directory.Split("/".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            return s.Length > 0 ? s[s.Length - 1] : "";
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

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
        // ~ServerIndexer() {
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

    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
                action.Invoke(item);
        }
    }
}