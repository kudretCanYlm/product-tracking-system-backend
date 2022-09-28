using DAS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private DASContext dasContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected DASContext DasContext
        {
            get { return dasContext ?? (dasContext = databaseFactory.Get()); }
        }

        public void BeginTransaction()
        {
            dasContext.BeginTransaction();
        }

        public void Commit()
        {
            DasContext.Commit();
        }

        public void RollBack()
        {
            DasContext.RollBack();
        }
    }
}
