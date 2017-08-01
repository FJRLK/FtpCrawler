using System.Linq;

namespace FtpCrawler.Services
{
     class FtpFileService : Interfaces.IFtpFileService
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

        public void Delete(long id)
        {
            Delete(GetById(id));
        }

        public Data.Models.FtpFile GetById(long id)
        {
            return _repo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Data.Models.FtpFile> GetAll()
        {
            return _repo.Table;
        }

        #endregion Public Methods
    }
}