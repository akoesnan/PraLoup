using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
