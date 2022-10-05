using DAS.Core.Infrastructure;
using DAS.Core.Repository.Post;
using DAS.Model.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Post
{
    public interface IPostService
    {

    }

    public class PostService:IPostService
    {
        private IPostRepository postRepository;
        private IUnitOfWork unitOfWork;
        private IPostValidation validator;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, IPostValidation validator)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }


        private object SavePostWithMessage()
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

        private void SavePost()
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
