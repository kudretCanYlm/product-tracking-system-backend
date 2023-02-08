using DAS.Core.Infrastructure;
using DAS.Core.Repository.Project;
using DAS.Model.Model.Project;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Project
{
    public interface IProjectService
    {
        object DeleteProject(Guid ProjectId);
        object AddNewProject(ref ProjectEntity project);
        IEnumerable<ProjectEntity> GetProjectsByRange<TOrder>(int pageNumber, int pageSize, Expression<Func<ProjectEntity, TOrder>> orderBy);
    }

    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IProjectPersonRepository projectPersonRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectValidation validator;

        public ProjectService(IProjectRepository projectRepository, IProjectPersonRepository projectPersonRepository, IUnitOfWork unitOfWork, IProjectValidation validator)
        {
            this.projectRepository = projectRepository;
            this.projectPersonRepository = projectPersonRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        //methods
        public object AddNewProject(ref ProjectEntity project)
        {
            project.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(project))
            {
                //will add project owner to projectPersonRepository
                projectRepository.Add(project);
                return SaveProjectWithMessage();
            }

            return validator.GetValidErrorMessages(project);
        }

        public IEnumerable<ProjectEntity> GetProjectsByRange<TOrder>(int pageNumber, int pageSize, Expression<Func<ProjectEntity, TOrder>> orderBy)
        {
            var page = new Page(pageNumber,pageSize);
            if (page.Skip < 0)
                return null;
            var projects = projectRepository.GetPage(page, x => true, orderBy).ToList();

            return projects;
        }

		public object DeleteProject(Guid ProjectId)
		{
			var projectEntity = projectRepository.GetById(ProjectId);

			if (projectEntity == null)
				return false;

				projectRepository.Delete(projectEntity);
                return SaveProjectWithMessage();
		}

		private object SaveProjectWithMessage()
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

        private void SaveProject()
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
