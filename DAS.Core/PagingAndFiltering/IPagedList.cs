﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.PagingAndFiltering
{
	public interface IPagedList<out T>
	{
		/// <summary>
		/// Page index
		/// </summary>
		int PageIndex { get; }

		/// <summary>
		/// Page size
		/// </summary>
		int PageSize { get; }

		/// <summary>
		/// Total count
		/// </summary>
		int TotalCount { get; }

		/// <summary>
		/// Total pages
		/// </summary>
		int TotalPages { get; }

		/// <summary>
		/// Has previous page
		/// </summary>
		bool HasPreviousPage { get; }

		/// <summary>
		/// Has next age
		/// </summary>
		bool HasNextPage { get; }

		/// <summary>
		/// Items
		/// </summary>
		IEnumerable<T> Items { get; }
	}
}
