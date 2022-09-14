using DAS.Core.Infrastructure;
using DAS.Core.Repository.Company;
using DAS.Model.Model.Company;

namespace DAS.Service.Services.Company
{
    public interface IWorkerRoleService
    {

    }

    public class WorkerRoleService:IWorkerRoleService
    {
        private readonly IWorkerRoleRepository workerRoleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWorkerRoleValidation validator;

        public WorkerRoleService(IWorkerRoleRepository workerRoleRepository, IUnitOfWork unitOfWork, IWorkerRoleValidation validator)
        {
            this.workerRoleRepository = workerRoleRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
