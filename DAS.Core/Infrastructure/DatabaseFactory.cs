using DAS.Data.Context;

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
