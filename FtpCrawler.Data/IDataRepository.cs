using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Data
{
    public interface IDataRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Insert(T entity, bool allowImmediateInsert);
        void InsertAll(ICollection<T> entities);
        void Update(T entity);
        void Update(T entity, bool allowImmediateUpdate);
        void UpdateAll(ICollection<T> entities);
        void Delete(T entity);
        void Delete(T entity, bool allowImmediateDelete);
        void DeleteAll(ICollection<T> entities);
        void Hide(T entity);
        void Hide(T entity, bool allowImmediateHide);
        void HideAll(ICollection<T> entities);
        IQueryable<T> Table { get; }
        IDatabaseContext DataContext { get; }
    }
}
