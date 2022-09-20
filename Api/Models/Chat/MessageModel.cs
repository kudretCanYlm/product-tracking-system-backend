using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Chat
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class GetMessagePostModel
    {
        public string TargetUserId { get; set; }
    }

    public class SendMessagePostModel
    {
        public string TargetUserId { get; set; }
        public string message { get; set; }

    }
}