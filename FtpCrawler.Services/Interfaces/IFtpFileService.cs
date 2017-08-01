using FtpCrawler.Data.Models;
using System.Linq;

namespace FtpCrawler.Services.Interfaces
{
    public interface IFtpFileService
    {
        void Create(FtpFile model);
        void Delete(FtpFile model);
        void Delete(long id);
        IQueryable<FtpFile> GetAll();
        FtpFile GetById(long id);
        void Update(FtpFile model);
    }
}