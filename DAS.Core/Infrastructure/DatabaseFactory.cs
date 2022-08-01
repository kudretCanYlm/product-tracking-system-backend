using DAS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DASContext dasContext;

        public DASContext Get()
        {
            return dasContext ?? (dasContext = new DASContext());
        }
        protected override void DisposeCore()
        {
            if (dasContext != null)
                dasContext.Dispose();
        }
    }
}
