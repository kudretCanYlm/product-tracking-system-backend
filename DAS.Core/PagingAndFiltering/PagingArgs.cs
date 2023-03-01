using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.PagingAndFiltering
{
	public class PagingArgs
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public PagingStrategy PagingStrategy { get; set; }
	}

	public enum PagingStrategy
	{
		WithCount = 0,
		NoCount = 1
	}
}
