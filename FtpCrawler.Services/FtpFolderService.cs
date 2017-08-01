using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Services
{
    class FtpFolderService : Interfaces.IFtpFolderService
    {
        #region Fields

        private Data.IDataRepository<Data.Models.Folder> _repo;

        #endregion Fields

        public FtpFolderService(Data.IDataRepository<Data.Models.Folder> repo)
        {
            _repo = repo;
        }

        #region Public Methods

        public void Create(Data.Models.Folder model)
        {
            //Create item
            _repo.Insert(model);
        }

        public void Update(Data.Models.Folder model)
        {
            //Update item
            _repo.Update(model);
        }

        public void Delete(Data.Models.Folder model)
        {
            //Delete the item
            _repo.Delete(model);
        }

        public void Delete(long id)
        {
            Delete(GetById(id));
        }

        public Data.Models.Folder GetById(long id)
        {
            return _repo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Data.Models.Folder> GetAll()
        {
            return _repo.Table;
        }

        #endregion Public Methods
    }
}
