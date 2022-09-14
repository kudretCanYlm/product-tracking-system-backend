using DAS.Core.Infrastructure;
using DAS.Model.Model.Company;

namespace DAS.Core.Repository.Company
{
    public class WorkerRoleRepository:RepositoryBase<WorkerRoleEntity>,IWorkerRoleRepository
    {
        public WorkerRoleRepository(DatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface IWorkerRoleRepository:IRepository<WorkerRoleEntity>
    {

    }
}
