using DAS.Core.Infrastructure;
using DAS.Core.Repository.Article;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Article
{
    public interface IArticleService
    {

    }
    public class ArticleService:IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleValidation validator;

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IArticleValidation validator)
        {
            this.articleRepository = articleRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
