using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Article
{
    public class ArticleCommentViewModel
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public Guid CommentOwnerId { get; set; }
        public string CommentOwnerName { get; set; }
        public string CommentOwnerSurname { get; set; }
        public DateTime? CreatedAt { get; set; }

    }

    public class ArticleCommentPostModel
    {
        public string Comment { get; set; }
        public Guid ArticleId { get; set; }
    }

}