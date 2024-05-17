using MMS.Core;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data
{
    public class UnitOfWork2 : IDisposable
    {
        private readonly MMSContext2 context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork2(MMSContext2 context)
        {
            this.context = context;
        }

        public UnitOfWork2()
        {
            context = new MMSContext2();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public Repository2<T> Repository<T>() where T : BaseEntity2
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository2<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository2<T>)repositories[type];
        }
    }
}
