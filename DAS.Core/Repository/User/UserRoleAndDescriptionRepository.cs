using DAS.Core.Infrastructure;
using DAS.Core.PagingAndFiltering;
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAS.Core.Repository.User
{
	public class UserRoleAndDescriptionRepository : RepositoryBase<UserRoleAndDescriptionEntity>, IUserRoleAndDescriptionRepository
	{
		public UserRoleAndDescriptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
		{
		}

		public IEnumerable<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionsByPage<TOrder>(Page page, Expression<Func<UserRoleAndDescriptionEntity, bool>> where, Expression<Func<UserRoleAndDescriptionEntity, TOrder>> order)
		{
			var query = Table.Include(x => x.OwnerUser);
			return GetPage(query, page, where, order).ToList();
		}

		public IPagedList<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionsByPagedList(PageSearchArgs args)
		{
			var query=Table.Include(x => x.OwnerUser);
			var orderByList=new List<(SortingOption, Expression<Func<UserRoleAndDescriptionEntity, object>>)>();

			if (args.SortingOptions != null)
			{
				foreach (var sortingOption in args.SortingOptions)
				{
					switch (sortingOption.Field)
					{
						case "name":
							orderByList.Add((sortingOption, x => x.OwnerUser.Name.ToString()));
							break;
						case "createdAt":
							orderByList.Add((sortingOption, x => x.CreatedAt.ToString()));
							break;
						default:
							throw new Exception("no match fieled");
					}
				}
			}

			var filterList = new List<(FilteringOption, Expression<Func<UserRoleAndDescriptionEntity, bool>>)>();

			if (args.FilteringOptions != null)
			{
				foreach (var filteringOption in args.FilteringOptions)
				{
					switch (filteringOption.Field)
					{
						case "search":
							filterList.Add((filteringOption, p => (p.OwnerUser.Name.ToLower() + " " + p.OwnerUser.Surname.ToLower()+" "+p.MiniRole.ToLower()).Contains(filteringOption.Value.ToString().ToLower())));
							break;
						default:
							continue;
					}
				}
			}

			var userRoleAndDescriptionPagedList = new PagedList<UserRoleAndDescriptionEntity>(query, new PagingArgs { PageIndex=args.PageIndex,PageSize=args.PageSize,PagingStrategy=args.PagingStrategy}, orderByList, filterList);
			return userRoleAndDescriptionPagedList;
		}
	}

	public interface IUserRoleAndDescriptionRepository:IRepository<UserRoleAndDescriptionEntity>
	{
		IEnumerable<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionsByPage<TOrder>(Page page, Expression<Func<UserRoleAndDescriptionEntity, bool>> where, Expression<Func<UserRoleAndDescriptionEntity, TOrder>> order);
		IPagedList<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionsByPagedList(PageSearchArgs args);
	}
}
