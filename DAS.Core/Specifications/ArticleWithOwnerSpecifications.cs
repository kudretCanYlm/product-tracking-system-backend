using DAS.Core.Specifications.Base;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Specifications
{
	public class ArticleWithOwnerSpecifications : BaseSpecification<ArticleEntity>
	{
		public ArticleWithOwnerSpecifications(string articleTitle) : base(art => art.ArticleTitle.Contains(articleTitle))
		{
			AddInclude(art => art.ArticleOwner);
		}

		public ArticleWithOwnerSpecifications(Guid ArticleId) : base(art => art.Id == ArticleId)
		{
			AddInclude(art => art.ArticleOwner);
		}

		public ArticleWithOwnerSpecifications() : base(null)
		{
			AddInclude(art => art.ArticleOwner);
		}
	}
}
