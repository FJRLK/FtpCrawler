using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace FtpCrawler.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : BaseEntity
    {
        #region Fields

        protected readonly IDatabaseContext _db;
        protected IDbSet<T> _set;

        #endregion Fields

        public IDatabaseContext DataContext
        {
            get { return _db; }
        }

        #region Constructor

        /// <summary>
        /// Constrcuts a new instance of a data repository
        /// </summary>
        public DataRepository(IDatabaseContext context)
        {
            this._db = context;
        }

        #endregion Constructor

        /// <summary>
        /// Fetch an entity from the repository by id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>Returns an entity or null"/></returns>
        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// Insert a new entity into the repository
        /// </summary>
        public virtual void Insert(T entity, bool allowImmediateInsert)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            //Look for modified date and set it
            System.Reflection.PropertyInfo createdProperty = entity.GetType().GetProperty("Created");
            if (createdProperty != null)
                createdProperty.SetValue(entity, DateTime.Now);
            //Look for modified date and set it
            System.Reflection.PropertyInfo modifiedProperty = entity.GetType().GetProperty("Modified");
            if (modifiedProperty != null)
                modifiedProperty.SetValue(entity, DateTime.Now);

            this.Entities.Add(entity);
            if (allowImmediateInsert)
                this._db.SaveChanges();
        }

        /// <summary>
        /// Insert a new entity into the repository
        /// </summary>
        public virtual void Insert(T entity)
        {
            this.Insert(entity, true);
        }

        /// <summary>
        /// Inserts many of this entity into the repository, saving of the entities will only occur when the transaction completes
        /// </summary>
        /// <param name="entities"></param>
        public virtual void InsertAll(ICollection<T> entities)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                foreach (T entity in entities)
                {
                    this.Insert(entity, false);
                }

                this._db.SaveChanges();
                ts.Complete();
            }
        }

        /// <summary>
        /// Update an existing entity in the repository
        /// </summary>
        public virtual void Update(T entity, bool allowImmediateUpdate)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            //Look for modified date and set it
            System.Reflection.PropertyInfo modifiedProperty = entity.GetType().GetProperty("Modified");
            if (modifiedProperty != null)
                modifiedProperty.SetValue(entity, DateTime.Now);

            if (this.Entities.Local.FirstOrDefault(e => e == entity) == null)
                Entities.Attach(entity);

            Throw(_db).Entry(entity).State = EntityState.Modified;
            if (allowImmediateUpdate)
                this._db.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            this.Update(entity, true);
        }

        /// <summary>
        /// Updates many of this entity in the repository
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateAll(ICollection<T> entities)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                foreach (T entity in entities)
                {
                    this.Update(entity, false);
                }

                this._db.SaveChanges();
                ts.Complete();
            }
        }

        /// <summary>
        /// Deletes an existing entity from the repository
        /// </summary>
        public virtual void Delete(T entity, bool allowImmediateDelete)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (this.Entities.Local.FirstOrDefault(e => e == entity) == null)
                Entities.Attach(entity);

            this.Entities.Remove(entity);
            if (allowImmediateDelete)
                this._db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            this.Delete(entity, true);
        }

        /// <summary>
        /// Deletes a list of existing entities from the repository
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteAll(ICollection<T> entities)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                foreach (T entity in entities)
                {
                    this.Delete(entity, false);
                }

                this._db.SaveChanges();
                ts.Complete();
            }
        }

        /// <summary>
        /// Hides an existing entity from the repository. Hidden will be set to true, and Enabled will be set to false.
        /// </summary>
        public virtual void Hide(T entity, bool allowImmediateHide)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            //Look for Hidden date and set it
            System.Reflection.PropertyInfo hiddenProperty = entity.GetType().GetProperty("Hidden");
            if (hiddenProperty != null)
                hiddenProperty.SetValue(entity, true);
            //Look for Enabled date and set it
            System.Reflection.PropertyInfo enabledProperty = entity.GetType().GetProperty("Enabled");
            if (enabledProperty != null)
                enabledProperty.SetValue(entity, false);
            //Look for modified date and set it
            System.Reflection.PropertyInfo modifiedProperty = entity.GetType().GetProperty("Modified");
            if (modifiedProperty != null)
                modifiedProperty.SetValue(entity, DateTime.Now);

            if (this.Entities.Local.FirstOrDefault(e => e == entity) == null)
                Entities.Attach(entity);

            Throw(_db).Entry(entity).State = EntityState.Modified;
            if (allowImmediateHide)
                this._db.SaveChanges();
        }

        public virtual void Hide(T entity)
        {
            this.Hide(entity, true);
        }

        /// <summary>
        /// Hides an existing list of entities from the repository. Hidden will be set to true, and Enabled will be set to false.
        /// </summary>
        /// <param name="entities"></param>
        public virtual void HideAll(ICollection<T> entities)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                foreach (T entity in entities)
                {
                    this.Hide(entity, false);
                }

                this._db.SaveChanges();
                ts.Complete();
            }
        }

        /// <summary>
        /// Acces the entire entity table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Access the entire set of entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_set == null) _set = _db.Set<T>();
                return _set;
            }
        }

        /// <summary>
        /// Throw the interface to a database context
        /// </summary>
        private DbContext Throw(IDatabaseContext context)
        {
            return (DbContext)(context as DbContext);
        }
    }
}
