using DAS.Core.Infrastructure;
using DAS.Core.Repository.Chat;
using DAS.Model.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Chat
{
    public interface IChatService
    {

    }
    public class ChatService:IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoginValidation validator;

        public ChatService(IChatRepository chatRepository, IUnitOfWork unitOfWork, ILoginValidation validator)
        {
            this.chatRepository = chatRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
