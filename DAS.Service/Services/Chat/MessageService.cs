using DAS.Core.Infrastructure;
using DAS.Core.Repository.Chat;
using DAS.Model.Model.Chat;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Chat
{
    public interface IMessageService
    {
        object AddMessage(MessageEntity messageEntity);
        IEnumerable<MessageEntity> GetMessages(Expression<Func<MessageEntity, bool>> where);
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMessageValidation validator;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IMessageValidation validator)
        {
            this.messageRepository = messageRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public object AddMessage(MessageEntity messageEntity)
        {
            messageEntity.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(messageEntity))
            {
                messageRepository.Add(messageEntity);
                return SaveMessageWithMessage();
            }

            return validator.GetValidErrorMessages(messageEntity);
        }

        public IEnumerable<MessageEntity> GetMessages(Expression<Func<MessageEntity, bool>> where)
        {
            var messages=messageRepository.GetMany(where).OrderBy(x => x.CreatedAt).ToList() ?? null;
            var isNotViewMessages = messageRepository.GetMany(where).Where(x=>x.IsViewed==false) ?? null;

            if (isNotViewMessages != null)
            {
                messageRepository.SetMessagesIsViewedTrue(isNotViewMessages);
                SaveMessage();
            }
                
            return messages;

        }

        private object SaveMessageWithMessage()
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

        private void SaveMessage()
        {
            unitOfWork.Commit();
        }
    }
}
