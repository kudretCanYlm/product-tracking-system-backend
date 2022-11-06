using DAS.Core.Infrastructure;
using DAS.Model.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Project
{
    public class ProjectPersonRepository : RepositoryBase<ProjectPersonEntity>, IProjectPersonRepository
    {
        public ProjectPersonRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
    public interface IProjectPersonRepository : IRepository<ProjectPersonEntity>
    {

    }
}
