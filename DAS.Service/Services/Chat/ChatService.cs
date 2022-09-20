using DAS.Core.Infrastructure;
using DAS.Core.Repository.Chat;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Chat;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Chat
{
    public interface IChatService
    {
        object AddChat(ref ChatEntity chatEntity);
        ChatEntity FindChat(Guid UserFirstId, Guid UserSecondId);
    }
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IChatValidation validator;

        public ChatService(IChatRepository chatRepository, IUnitOfWork unitOfWork, IChatValidation validator)
        {
            this.chatRepository = chatRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public object AddChat(ref ChatEntity chatEntity)
        {
            chatEntity.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(chatEntity))
            {
                if (chatRepository.IsUnique(chatEntity))
                {
                    chatRepository.Add(chatEntity);
                    
                    return SaveChat();
                }

                return "this chat already exists";
            }

            return validator.GetValidErrorMessages(chatEntity);

        }

        public ChatEntity FindChat(Guid UserFirstId, Guid UserSecondId)
        {
            return chatRepository
                .Get(
                    x =>
                        (x.UserFirstId == UserFirstId && x.UserSecondId == UserSecondId)
                        ||
                        (x.UserFirstId == UserSecondId && x.UserSecondId == UserFirstId))
                ?? null;
        }

        private object SaveChat()
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
    }
}
