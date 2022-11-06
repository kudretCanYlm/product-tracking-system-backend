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
    public interface IProjectUserService
    {

    }

    public class ProjectUserService:IProjectUserService
    {
        private readonly IProjectUserRepository projectUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectUserValidation validator;

        public ProjectUserService(IProjectUserRepository projectUserRepository, IUnitOfWork unitOfWork, IProjectUserValidation validator)
        {
            this.projectUserRepository = projectUserRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        //methods
        private object SaveProjectUserWithMessage()
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

        private void SaveProjectUser()
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
