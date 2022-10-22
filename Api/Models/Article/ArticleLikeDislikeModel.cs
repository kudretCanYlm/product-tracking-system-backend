using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Article
{
    public class ArticleLikeDislikePostModel
    {
        public bool Isliked { get; set; }   
        public Guid ArticleId { get; set; }
    }

    public class ArticleLikeDislikeSingleViewModel
    {
        public bool Isliked { get; set; }
    }
}