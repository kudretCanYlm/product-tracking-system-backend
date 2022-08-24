using DAS.Core.Infrastructure;
using DAS.Model.Model.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Media
{
    public class ImageRepository:RepositoryBase<ImageEntity>,IImageRepository
    {
        public ImageRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface IImageRepository:IRepository<ImageEntity>
    {

    }
}
