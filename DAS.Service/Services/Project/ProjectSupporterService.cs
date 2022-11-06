using DAS.Core.Infrastructure;
using DAS.Core.Repository.Project;
using DAS.Model.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Project
{
    public interface IProjectSupporterService
    {

    }

    public class ProjectSupporterService:IProjectSupporterService
    {
        private readonly IProjectSupporterRepository projectSupporterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectSupporterValidation validator;

        public ProjectSupporterService(IProjectSupporterRepository projectSupporterRepository, IUnitOfWork unitOfWork, IProjectSupporterValidation validator)
        {
            this.projectSupporterRepository = projectSupporterRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        //methods

        private object SaveProjectSupporterWithMessage()
        {
            try
            {
                unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        private void SaveProjectSupporter()
        {
            unitOfWork.Commit();
        }

        private void BeginTransaction()
        {
            unitOfWork.BeginTransaction();
        }
        private void RollBack()
        {
            unitOfWork.RollBack();
        }
    }
}
