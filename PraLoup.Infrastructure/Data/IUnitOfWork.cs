using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void End();
        void Commit();
        void Rollback();
    }
}
