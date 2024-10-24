﻿using MMS.Core;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data
{
  public  class UnitOfWork: IDisposable
    {
      private readonly MMSContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;
        private Dictionary<string, object> repositories_;

        public UnitOfWork(MMSContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new MMSContext();
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

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }
        public RepositoryJob<T> Repository_<T>() where T : BaseEntity
        {
            if (repositories_ == null)
            {
                repositories_ = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories_.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryJob<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories_.Add(type, repositoryInstance);
            }
            return (RepositoryJob<T>)repositories_[type];
        }
    }
}
