using DAS.Core.Infrastructure;
using DAS.Model.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Project
{
    public class ProjectSupporterRepository : RepositoryBase<ProjectSupporterEntity>, IProjectSupporterRepository
    {
        public ProjectSupporterRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }

    public interface IProjectSupporterRepository : IRepository<ProjectSupporterEntity>
    {

    }
}
