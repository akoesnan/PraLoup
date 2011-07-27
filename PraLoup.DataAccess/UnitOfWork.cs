using System;
using NHibernate;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISession Session { get; private set; }
        public UnitOfWork(ISession session)
        {
            this.Session = session;
        }

        ~UnitOfWork()
        {
            Dispose();
        }

        public void Dispose()
        {
            lock (Session)
            {
                if (Session.IsOpen)
                {
                    Session.Close();
                }
            }
            GC.SuppressFinalize(this);
        }

        public void Begin()
        {
            Session.BeginTransaction();
        }

        public void End()
        {
            if (Session.IsOpen)
            {
                Commit();
                Session.Close();
            }
            Session.Dispose();
        }

        public void Commit()
        {
            if (Session.Transaction != null && !Session.Transaction.IsActive)
            {
                throw new InvalidOperationException("No active transation");
            }

            try
            {
                Session.Transaction.Commit();
            }
            catch (Exception)
            {
                Session.Transaction.Rollback();
            }
        }

        public void Rollback()
        {
            if (Session.Transaction != null && !Session.Transaction.IsActive)
            {
                Session.Transaction.Rollback();
            }
        }
    }
}
