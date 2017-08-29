using FtpCrawler.Data.Models;
using System;
using System.Collections.Generic;

namespace FtpCrawler.Services.Interfaces
{
    public interface IFtpFolderService
    {
        void Create(FtpFolder model);

        void Delete(FtpFolder model);

        void Delete(Int64 id);

        IEnumerable<FtpFolder> GetAll();

        FtpFolder GetById(Int64 id);

        void Update(FtpFolder model);

        FtpFolder GetByPath(String directory, FtpServer server);

        IEnumerable<FtpFolder> GetAllNotModifiedSince(DateTime dateTime);

        
    }
}