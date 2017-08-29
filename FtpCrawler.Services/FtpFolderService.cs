using FtpCrawler.Data.Models;
using System;
using System.Linq;
using FtpCrawler.Services.Interfaces;
using System.Collections.Generic;

namespace FtpCrawler.Services
{
    internal class FtpFolderService : Interfaces.IFtpFolderService
    {
        #region Fields

        private Data.IDataRepository<Data.Models.FtpFolder> _repo;

        #endregion Fields

        public FtpFolderService(Data.IDataRepository<Data.Models.FtpFolder> repo)
        {
            _repo = repo;
        }

        #region Public Methods

        public void Create(Data.Models.FtpFolder model)
        {
            //Create item
            _repo.Insert(model);
        }

        public void Update(Data.Models.FtpFolder model)
        {
            //Update item
            _repo.Update(model);
        }

        public void Delete(Data.Models.FtpFolder model)
        {
            //Delete the item
            _repo.Delete(model);
        }

        public void Delete(long id)
        {
            Delete(GetById(id));
        }

        public Data.Models.FtpFolder GetById(long id)
        {
            return _repo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Data.Models.FtpFolder> GetAll()
        {
            return _repo.Table;
        }

        public FtpFolder GetByPath(String directory, FtpServer server)
        {
            return _repo.Table.FirstOrDefault(x => x.FullName == directory && x.ServerId == server.Id);
        }

      

        public IEnumerable<FtpFolder> GetAllNotModifiedSince(DateTime dateTime)
        {
            return this.GetAll().Where(f => f.Modified < dateTime);
        }

        #endregion Public Methods
    }
}