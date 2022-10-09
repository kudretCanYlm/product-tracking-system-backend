using DAS.Model.Model.Article;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Dto.Article
{
    [NotMapped]
    public class ArticleDtoView : ArticleEntity
    {
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }

    }
}
