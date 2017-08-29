using System;
using System.Collections.Generic;

namespace FtpCrawler.Services.Interfaces
{
    public interface IFtpFileService
    {
        void Create(Data.Models.FtpFile model);

        void Delete(Data.Models.FtpFile model);

        void Delete(long id);

        IEnumerable<Data.Models.FtpFile> GetAll();

        Data.Models.FtpFile GetById(long id);

        void Update(Data.Models.FtpFile model);

        IEnumerable<Data.Models.FtpFile> GetAllNotModifiedSince(DateTime dateTime);

        void Delete(IEnumerable<Data.Models.FtpFile> files);
        Data.Models.FtpFile GetByPath(String fullName);
    }
}