using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Dto.Article
{
    [NotMapped]
    public class ArticleLikeDtoViewSingle
    {
        public bool İsLiked { get; set; }
    }
}
