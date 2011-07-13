using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Data.SQLite;
using NHibernate.Tool.hbm2ddl;
using PraLoup.DataAccess.Mapping;
using FluentNHibernate.Automapping;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAcess.Tests
{
    public class SQLiteDatabaseScope<TClassFromMappingAssembly> : IDisposable
    {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        public SQLiteDatabaseScope()
        {
            BuildConfiguration();
        }

        private SQLiteConnection m_Connection;
        private ISessionFactory m_SessionFactory;

        private void BuildConfiguration()
        {
            m_SessionFactory = Fluently.Configure()
                    .Database(GetDBConfig())
                    .Mappings(GetMappings)
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private FluentNHibernate.Cfg.Db.IPersistenceConfigurer GetDBConfig()
        {
            return SQLiteConfiguration.Standard
                .ConnectionString(cs => cs.Is(CONNECTION_STRING));
        }     

        private void GetMappings(MappingConfiguration x)
        {
            x.AutoMappings.Add(AutoMap.AssemblyOf<Account>(new PraLoupAutoMappingConfiguration())
                .UseOverridesFromAssemblyOf<EventMappingOverride>()                    
                .Conventions.Add<CascadeConvention>())
                                    
             .ExportTo(".");
        }

        private void BuildSchema(NHibernate.Cfg.Configuration Cfg)
        {
            SchemaExport SE = new SchemaExport(Cfg);
            SE.Execute(false, true, false, GetConnection(), Console.Out);
        }

        private System.Data.SQLite.SQLiteConnection GetConnection()
        {
            if (m_Connection == null)
            {
                m_Connection = new SQLiteConnection(CONNECTION_STRING);
                m_Connection.Open();
            }
            return m_Connection;
        }

        public ISession OpenSession()
        {
            return m_SessionFactory.OpenSession(GetConnection());
        }

        private bool disposedValue = false;
        // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free other state (managed objects).
                    if (m_Connection != null) m_Connection.Close();
                    m_Connection = null;

                }
            }
            // TODO: free your own state (unmanaged objects).
            // TODO: set large fields to null.
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
