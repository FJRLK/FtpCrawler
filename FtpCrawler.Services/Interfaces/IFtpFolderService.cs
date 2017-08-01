using System.Linq;
using FtpCrawler.Data.Models;

namespace FtpCrawler.Services.Interfaces
{
    public interface IFtpFolderService
    {
        void Create(Folder model);
        void Delete(Folder model);
        void Delete(long id);
        IQueryable<Folder> GetAll();
        Folder GetById(long id);
        void Update(Folder model);
    }
}