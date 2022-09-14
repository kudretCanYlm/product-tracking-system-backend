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
    }

    public interface IChatRepository:IRepository<ChatEntity>
    {

    }
}
