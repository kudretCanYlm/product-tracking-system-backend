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
    public interface IProjectRateService
    {

    }

    public class ProjectRateService:IProjectRateService
    {
        private readonly IProjectRateRepository projectRateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectRateValidation validator;

        public ProjectRateService(IProjectRateRepository projectRateRepository, IUnitOfWork unitOfWork, IProjectRateValidation validator)
        {
            this.projectRateRepository = projectRateRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        //methods

        private object SaveProjectRateWithMessage()
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

        private void SaveProjectRate()
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
