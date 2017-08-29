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

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyContext, MigrateDBConfiguration>());
        //}

        /// <summary>
		/// Create the model based on the entity type configuration mappings
		/// </summary>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Look for and add the entity configurations to the model builder
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            //Don't pluaralize table names
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());

            base.OnModelCreating(modelBuilder);
        }

        /*
         public void InitializeDatabase(DataAccessManager context)
{
    if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
    {
        var configuration = new DbMigrationsConfiguration();
        var migrator = new DbMigrator(configuration);
        migrator.Configuration.TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient");
        var migrations = migrator.GetPendingMigrations();
        if (migrations.Any())
        {
            var scriptor = new MigratorScriptingDecorator(migrator);
            var script = scriptor.ScriptUpdate(null, migrations.Last());

            if (!string.IsNullOrEmpty(script))
            {
                context.Database.ExecuteSqlCommand(script);
            }
        }
    }
}
             */

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