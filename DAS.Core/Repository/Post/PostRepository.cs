using DAS.Core.Infrastructure;
using DAS.Model.Model.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Post
{
    public class PostRepository:RepositoryBase<PostEntity>,IPostRepository
    {
        public PostRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface IPostRepository:IRepository<PostEntity>
    {

    }
}
