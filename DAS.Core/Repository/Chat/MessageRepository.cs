using DAS.Core.Infrastructure;
using DAS.Model.Model.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Chat
{
    public class MessageRepository : RepositoryBase<MessageEntity>, IMessageRepository
    {
        public MessageRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public void SetMessagesIsViewedTrue(IEnumerable<MessageEntity> messages)
        {
            foreach (var message in messages)
                message.IsViewed = true;

            UpdateMany(messages);
            
        }
    }

    public interface IMessageRepository : IRepository<MessageEntity>
    {
        void SetMessagesIsViewedTrue(IEnumerable<MessageEntity> messages);
    }
}
