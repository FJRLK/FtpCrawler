using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FtpCrawler.Services.Interfaces
{
    public interface IFtpServerService
    {
        void Create(Data.Models.FtpServer model);

        void Delete(Int64 id);

        void Delete(Data.Models.FtpServer model);

        IQueryable<Data.Models.FtpServer> GetAll();

        Data.Models.FtpServer GetById(Int64 id);

        void Update(Data.Models.FtpServer model);
        Data.Models.FtpServer GetByHostName(String hostName);
    }
}
