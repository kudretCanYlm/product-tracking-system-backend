using DAS.Core.Infrastructure;
using DAS.Model.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Project
{
    public class ProjectUserRepository : RepositoryBase<ProjectUserEntity>, IProjectUserRepository
    {
        public ProjectUserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }

    public interface IProjectUserRepository : IRepository<ProjectUserEntity>
    {

    }
}
