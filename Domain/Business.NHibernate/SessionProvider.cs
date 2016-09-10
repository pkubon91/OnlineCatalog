using System;
using System.Configuration;
using Business.NHibernate.Administration;
using Business.NHibernate.Interceptors;
using Business.NHibernate.Products;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace Business.NHibernate
{
    public static class SessionProvider
    {
        private static ISessionFactory _session;
        private static readonly object SyncRoot = new Object();

        public static ISession OpenSession()
        {
            if (_session == null)
            {
                lock (SyncRoot)
                {
                    _session = ConfigureSessionFactory();
                }
            }
            return _session.OpenSession(new AuditInterceptor());
        }
        
        private static ISessionFactory ConfigureSessionFactory()
        {
            if (ShouldRecreateDb())
            {
                CreateDatabase();
            }
            FluentConfiguration configuration = CreateNHibernateConfiguration();
            return configuration.BuildConfiguration().BuildSessionFactory();
        }

        private static bool ShouldRecreateDb()
        {
            bool recreateDb;
            if (bool.TryParse(ConfigurationManager.AppSettings["RecreateDb"], out recreateDb))
            {
                return recreateDb;
            }
            return false;
        }

        private static FluentConfiguration CreateNHibernateConfiguration()
        {
            var nhibernateConfiguration = new Configuration().Cache(x => x.UseQueryCache = true);
            nhibernateConfiguration.SessionFactory().Caching.Through<SysCacheProvider>().WithDefaultExpiration(60);

            FluentConfiguration configuration = Fluently.Configure(nhibernateConfiguration)
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(
                        ConfigurationManager.ConnectionStrings["CatalogOnlineConnectionString"]
                            .ConnectionString)
                        .ShowSql())
                .CurrentSessionContext("thread_static")
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                    .AddFromAssemblyOf<UserAddressMap>()
                    .AddFromAssemblyOf<ProductCategoryMap>()
                    .AddFromAssemblyOf<ProductMap>());
            return configuration;
        }

        public static void CreateDatabase()
        {
            Configuration config = CreateNHibernateConfiguration().BuildConfiguration();
            var exporter = new SchemaExport(config);
            exporter.Execute(true, true, false);
        }
    }
}
