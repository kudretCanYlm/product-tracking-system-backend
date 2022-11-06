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
    public interface IProjectPersonService
    {

    }

    public class ProjectPersonService:IProjectPersonService
    {
        private readonly IProjectPersonRepository projectPersonRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectPersonValidation validator;

        public ProjectPersonService(IProjectPersonRepository projectPersonRepository, IUnitOfWork unitOfWork, IProjectPersonValidation validator)
        {
            this.projectPersonRepository = projectPersonRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        //methods



        private object SaveProjectPersonWithMessage()
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

        private void SaveProjectPerson()
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
