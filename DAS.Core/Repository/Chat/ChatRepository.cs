using DAS.Core.Infrastructure;
using DAS.Model.Model.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Chat
{
    public class ChatRepository:RepositoryBase<ChatEntity>,IChatRepository
    {
        public ChatRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }

        public bool IsUnique(ChatEntity chatEntity)
        {
          return  GetMany(x =>
            (x.UserFirstId == chatEntity.UserFirstId && x.UserSecondId == chatEntity.UserSecondId)
            ||
            (x.UserFirstId == chatEntity.UserSecondId && x.UserSecondId == chatEntity.UserFirstId)
            ).Count() == 0;
        }
    }

    public interface IChatRepository:IRepository<ChatEntity>
    {
        bool IsUnique(ChatEntity chatEntity);
    }
}
