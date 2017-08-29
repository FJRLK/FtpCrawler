using System;
using System.Collections.Generic;
using System.Linq;

namespace FtpCrawler.Services
{
    internal class FtpFileService : Interfaces.IFtpFileService
    {
        #region Fields

        private Data.IDataRepository<Data.Models.FtpFile> _repo;

        #endregion Fields

        public FtpFileService(Data.IDataRepository<Data.Models.FtpFile> repo)
        {
            _repo = repo;
        }

        #region Public Methods

        public void Create(Data.Models.FtpFile model)
        {
            //Create item
            _repo.Insert(model);
        }

        public void Update(Data.Models.FtpFile model)
        {
            //Update item
            _repo.Update(model);
        }

        public void Delete(Data.Models.FtpFile model)
        {
            //Delete the item
            _repo.Delete(model);
        }

        public void Delete(IEnumerable<Data.Models.FtpFile> files)
        {
            foreach (var file in files)
                _repo.Delete(file, false);

            _repo.CommitChanges();
        }

        public void Delete(long id)
        {
            Delete(GetById(id));
        }

        public Data.Models.FtpFile GetById(long id)
        {
            return _repo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Data.Models.FtpFile> GetAll()
        {
            return _repo.Table;
        }

        public IEnumerable<Data.Models.FtpFile> GetAllNotModifiedSince(DateTime dateTime)
        {
            return this.GetAll().Where(f => f.Modified < dateTime);
        }

        public Data.Models.FtpFile GetByPath(String fullName)
        {
            return _repo.Table.FirstOrDefault(x => x.FullName == fullName);
        }

        #endregion Public Methods
    }
}