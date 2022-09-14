using DAS.Core.Infrastructure;
using DAS.Core.Repository.Chat;
using DAS.Model.Model.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Chat
{
    public interface IMessageService
    {

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
    }
}
