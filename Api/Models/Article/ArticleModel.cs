using DAS.Model.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Article
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }
        public string ArticleTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid ArticleOwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
    }

    public class ArticlePostModel
    {
        public string ArticleTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
    }
}