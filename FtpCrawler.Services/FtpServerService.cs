using FtpCrawler.Data.Models;
using System;
using System.Linq;

namespace FtpCrawler.Services
{
    internal class FtpServerService : Interfaces.IFtpServerService
    {
        #region Fields

        private Data.IDataRepository<Data.Models.FtpServer> _repo;

        #endregion Fields

        #region Constructor

        public FtpServerService(Data.IDataRepository<Data.Models.FtpServer> repo)
        {
            _repo = repo;
        }

        #endregion Constructor

        #region Public Methods

        public void Create(Data.Models.FtpServer model)
        {
            //Create item
            _repo.Insert(model);
        }

        public void Update(Data.Models.FtpServer model)
        {
            //Update item
            _repo.Update(model);
        }

        public void Delete(Data.Models.FtpServer model)
        {
            //Delete the item
            _repo.Delete(model);
        }

        public void Delete(long id)
        {
            Delete(GetById(id));
        }

        public Data.Models.FtpServer GetById(long id)
        {
            return _repo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<FtpServer> GetAll()
        {
            return _repo.Table;
        }

        public FtpServer GetByHostName(String hostName)
        {
            return _repo.Table.FirstOrDefault(x => x.HostName == hostName);
        }

        #endregion Public Methods
    }
}