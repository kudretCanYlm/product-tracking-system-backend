using DAS.Model.Base;
using DAS.Model.Model.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Dto.Chat
{
    [NotMapped]
    public class ChatDtoView:ChatEntity
    {
        //will add others
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
