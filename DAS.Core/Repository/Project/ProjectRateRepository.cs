using DAS.Core.Infrastructure;
using DAS.Model.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Project
{
    public class ProjectRateRepository : RepositoryBase<ProjectRateEntity>, IProjectRateRepository
    {
        public ProjectRateRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }

    public interface IProjectRateRepository : IRepository<ProjectRateEntity>
    {

    }
}
