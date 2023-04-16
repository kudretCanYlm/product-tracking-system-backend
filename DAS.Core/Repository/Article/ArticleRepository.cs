using DAS.Core.Infrastructure;
using DAS.Core.PagingAndFiltering;
using DAS.Core.Specifications;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Article
{
	public class ArticleRepository : RepositoryBase<ArticleEntity>, IArticleRepository
	{
		public ArticleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
		{
		}



		public IEnumerable<ArticleDtoView> GetArticlesWithDto(Expression<Func<ArticleEntity, bool>> where)
		{
			var articles = GetManyQuery(where == null ? x => x.IsPublic == true : where).Select(ArticleRepositorySelects.articleViewDto).ToList();
			return articles;
		}

		public ArticleDtoView GetArticleWithDto(Expression<Func<ArticleEntity, bool>> where)
		{
			var article = GetManyQuery(where == null ? x => x.IsPublic == true : where).Select(ArticleRepositorySelects.articleViewDto).FirstOrDefault();

			return article;
		}

		public IEnumerable<ArticleDtoView> GetArticlesPage<TOrder>(Page page, Expression<Func<ArticleEntity, bool>> where, Expression<Func<ArticleEntity, TOrder>> orderBy)
		{
			var articles = GetPage(page, where, orderBy).Select(ArticleRepositorySelects.articleViewDto);

			return articles.ToList();
		}

		public IPagedList<ArticleEntity> GetArticlesPagedList(PageSearchArgs args)
		{
			var query = Table.Include(x => x.ArticleOwner);

			var orderByList = new List<(SortingOption, Expression<Func<ArticleEntity, object>>)>();

			if (args.SortingOptions != null)
			{
				foreach (var sortingOption in args.SortingOptions)
				{
					switch (sortingOption.Field)
					{
						case "id":
							orderByList.Add((sortingOption, x => x.Id.ToString()));
							break;
						case "articleTitle":
							orderByList.Add((sortingOption, x => x.ArticleTitle));
							break;
						case "articleOwner.name":
							orderByList.Add((sortingOption, x => x.ArticleOwner.Name));
							break;
						default:
							throw new Exception("no match fieled");
					}
				}
			}

			if (orderByList.Count == 0)
			{
				orderByList.Add((new SortingOption { Direction = SortingDirection.ASC }, p => p.ArticleOwner.Name));
			}

			var filterList = new List<(FilteringOption, Expression<Func<ArticleEntity, bool>>)>();

			if (args.FilteringOptions != null)
			{
				foreach (var filteringOption in args.FilteringOptions)
				{
					switch (filteringOption.Field)
					{
						case "id":
							filterList.Add((filteringOption, p => p.Id == (Guid)filteringOption.Value));
							break;
						case "articleTitle":
							filterList.Add((filteringOption, p => p.ArticleTitle.Contains(((string)filteringOption.Value).ToLower())));
							break;
						
						case "articleOwner.name":
							filterList.Add((filteringOption, p => p.ArticleOwner.Name.Contains(((string)filteringOption.Value).ToLower())));
							break;
						default:
							throw new Exception("no match fieled");
					}
				}
			}

			var articlePagedList = new PagedList<ArticleEntity>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);
			return articlePagedList;
		}

		public IEnumerable<ArticleEntity> GetArticlesByName(string articleName)
		{
			var spec = new ArticleWithOwnerSpecifications(articleName);
			return GetSpec(spec);
		}

	}

	public interface IArticleRepository : IRepository<ArticleEntity>
	{
		IPagedList<ArticleEntity> GetArticlesPagedList(PageSearchArgs args);
		IEnumerable<ArticleEntity> GetArticlesByName(string articleName);
		ArticleDtoView GetArticleWithDto(Expression<Func<ArticleEntity, bool>> where);
		IEnumerable<ArticleDtoView> GetArticlesWithDto(Expression<Func<ArticleEntity, bool>> where);
		IEnumerable<ArticleDtoView> GetArticlesPage<TOrder>(Page page, Expression<Func<ArticleEntity, bool>> where, Expression<Func<ArticleEntity, TOrder>> order);
	}

	public class ArticleRepositorySelects
	{
		public static Expression<Func<ArticleEntity, ArticleDtoView>> articleViewDto = article => new ArticleDtoView
		{
			Id = article.Id,
			Summary = article.Summary,
			ArticleContent = article.ArticleContent,
			ArticleTitle = article.ArticleTitle,
			CreatedAt = article.CreatedAt,
			OwnerId = article.ArticleOwner.Id,
			OwnerName = article.ArticleOwner.Name,
			OwnerSurname = article.ArticleOwner.Surname
		};
	}
}
