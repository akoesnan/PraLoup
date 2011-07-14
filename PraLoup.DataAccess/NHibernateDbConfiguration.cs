using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Automapping;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Mapping;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace PraLoup.DataAccess
{
    public class NHibernateDbConfiguration
    {
        private ISessionFactory _sessionFactory;
        private const string SQLLITE_CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        private const string MYSQL_CONNECTION_STRING = "Server=localhost; Port=3306; Database=Praloup; Uid=root; Pwd='PraLoupDev!';";
        private static DbConnection m_Connection;
        private const string MYSQL = "MYSQL";
        private const string SQLLITE = "SQLLITE";

        private const string DBTYPE = MYSQL;


        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        private static AutoPersistenceModel CreateAutomappings()
        {
            // This is the actual automapping - use AutoMap to start automapping,
            // then pick one of the static methods to specify what to map (in this case
            // all the classes in the assembly that contains Employee), and then either
            // use the Setup and Where methods to restrict that behaviour, or (preferably)
            // supply a configuration instance of your definition to control the automapper.
            return AutoMap.AssemblyOf<Account>(new PraLoupAutoMappingConfiguration())
                .Conventions.Add<CascadeConvention>();
        }

        private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info)
            // and exports a database schema from it

            SchemaExport SE = new SchemaExport(config);
            SE.Execute(true, true, true, GetDbConnection(), Console.Out);
            SE.Create(true, true);
        }

        private static DbConnection GetDbConnection()
        {
            if (m_Connection == null)
            {
                if (DBTYPE == MYSQL)
                {
                    m_Connection = new MySqlConnection(MYSQL_CONNECTION_STRING);
                }
                else
                {
                    m_Connection = new SQLiteConnection(SQLLITE_CONNECTION_STRING);
                }
                m_Connection.Open();
            }
            return m_Connection;
        }

        /// <summary>
        /// Configure NHibernate. This method returns an ISessionFactory instance that is
        /// populated with mappings created by Fluent NHibernate.
        /// 
        /// Line 1:   Begin configuration
        ///      2+3: Configure the database being used (SQLite file db)
        ///      4+5: Specify what mappings are going to be used (Automappings from the CreateAutomappings method)
        ///      6:   Expose the underlying configuration instance to the BuildSchema method,
        ///           this creates the database.
        ///      7:   Finally, build the session factory.
        /// </summary>
        /// <returns></returns>
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(GetDBConfig())
                .Mappings(GetMappings)
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static FluentNHibernate.Cfg.Db.IPersistenceConfigurer GetDBConfig()
        {
            if (DBTYPE == MYSQL)
            {
                return MySQLConfiguration.Standard
                    .ConnectionString(cs => cs.Is(MYSQL_CONNECTION_STRING));
            }
            else
            {
                return SQLiteConfiguration.Standard
                    .ConnectionString(cs => cs.Is(SQLLITE_CONNECTION_STRING));
            }
        }

        private static void GetMappings(MappingConfiguration x)
        {
            x.AutoMappings.Add(AutoMap.AssemblyOf<Account>(new PraLoupAutoMappingConfiguration())
                .UseOverridesFromAssemblyOf<EventMappingOverride>()
                .Conventions.Add<CascadeConvention>());
            //.ExportTo("."); // This will export the hql file. we should comment this out otherwise it will try to use these files
        }
    }
}