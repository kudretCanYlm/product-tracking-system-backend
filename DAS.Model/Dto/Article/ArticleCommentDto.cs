using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Dto.Article
{
    [NotMapped]
    public class ArticleCommentDto:ArticleCommentEntity
    {

        public string CommentOwnerName { get; set; }
        public string CommentOwnerSurname { get; set; }
    }
}
