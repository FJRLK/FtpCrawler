using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Reflection;

namespace FtpCrawler.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base("name=ConnectionString")
        {
        }

        public DatabaseContext(System.Data.Entity.IDatabaseInitializer<DatabaseContext> initializer)
            : base("name=ConnectionString")
        {
            if (initializer != null) System.Data.Entity.Database.SetInitializer<DatabaseContext>(initializer);
        }

        /// <summary>
		/// Create the model based on the entity type configuration mappings
		/// </summary>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Look for and add the entity configurations to the model builder
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            //Don't pluaralize table names
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public Boolean DatabaseExists()
        {
            return this.Database.Exists();
        }

        public void ExecuteSql(String sql)
        {
            this.Database.ExecuteSqlCommand(sql);
        }

        public override Int32 SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}