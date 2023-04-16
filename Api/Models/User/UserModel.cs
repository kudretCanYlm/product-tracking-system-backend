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
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Job { get; set; }
			public string Description { get; set; }
		}

		public class UserRoleAndDescriptionPostModel
		{
			public Guid? OwnerUserId { get; set; }
			public string MiniRole { get; set; }
			public string MiniDescription { get; set; }
		}
	}
}