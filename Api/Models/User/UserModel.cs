using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.User
{
	public class UserModel
	{
		 public class UserRoleAndDescriptionViewModel
		{
			public Guid OwnerUserId { get; set; }
			public string FullName { get; set; }
			public string MiniRole { get; set; }
			public string MiniDescription { get; set; }
		}

		public class UserRoleAndDescriptionPostModel
		{
			public Guid? OwnerUserId { get; set; }
			public string MiniRole { get; set; }
			public string MiniDescription { get; set; }
		}
	}
}